#If DEBUG Then

Partial Class null
    'This just makes sure this file doesn't open in a designer
End Class

#End If

Partial Public Class fMain

    ''' <summary>
    ''' The total list of all enumerated files. Includes non-photos as well.
    ''' </summary>
    Private _s2EnumeratedFiles As IEnumerable(Of IO.FileInfo)

    ''' <summary>
    ''' The total list of all enumerated folders.
    ''' </summary>
    ''' <remarks>
    ''' Includes the list of selected folders from step 1. Also includes all sub-folders (recursively) of all selected folders from step 1 which were marked to be serched recursively.
    ''' </remarks>
    Private _s2EnumeratedFolders As IEnumerable(Of IO.DirectoryInfo)

    ''' <summary>
    ''' A collection of <see cref="ListViewItem"/> instances representing all loaded photos.
    ''' </summary>
    Private _s2PhotoListItems As ListViewItem()

    ''' <summary>
    ''' Initializes step 2 by enumerating all folders selected in step 1 in <see cref="_s2EnumeratedFolders"/>. After that, all files in the enumerated folders are enumerated into <see cref="_s2EnumeratedFiles"/>. Finally, <see cref="_s2tsbLoadPhotos"/> is enabled and the text updated to indicate how many files are to be scanned for photos.
    ''' </summary>
    Private Sub _s2BeginStep2()

        Dim a =
            Sub()

                'enumerate folders
                Dim placeholder As New ListViewItem("Please wait...")

                Me.Invoke(
                    Sub()
                        'Add a "Please wait..." message
                        _s2lvEnumeratedFolders.BeginUpdate()
                        _s2lvEnumeratedFolders.Items.Clear()
                        _s2lvEnumeratedFolders.Items.Add(placeholder)
                        _s2lvEnumeratedFolders.EndUpdate()

                        'Reset the Load Photos button
                        _s2tsbLoadPhotos.Enabled = False
                        _s2tsbLoadPhotos.Text = "Load photos"

                        'Add a (No Photos Loaded) message
                        _s2lvPhotos.BeginUpdate()
                        _s2PhotoListItems = {New ListViewItem({"(No photos loaded)", String.Empty, String.Empty, String.Empty, String.Empty})}
                        _s2lvPhotos.VirtualListSize = 1
                        _s2lvPhotos.EndUpdate()

                    End Sub)

                Me._s0ProgressStatusbar.StartTask(False)
                Me._s0ProgressStatusbar.ReportTaskProgress("Enumerating folders", 0, 0, True)

                Dim flist = (From f In Me._s1Selectedfolders Select f.Key).ToList

                Dim q = (From f In Me._s1Selectedfolders Where f.Value.Recursive From d In f.Key.GetDirectories("*", IO.SearchOption.AllDirectories) Select d)

                flist.AddRange(q.ToArray)

                _s2EnumeratedFolders = flist

                Me.Invoke(
                    Sub()
                        _s2lvEnumeratedFolders.BeginUpdate()
                        _s2lvEnumeratedFolders.Items.Clear()
                        _s2lvEnumeratedFolders.Items.AddRange((From f In flist Order By f.Name Select New ListViewItem({f.Name, f.FullName})).ToArray)
                        _s2lvEnumeratedFolders.EndUpdate()
                    End Sub)

                'enumerate files
                Me._s0ProgressStatusbar.StartTask()
                Dim foldercount = 1
                Dim files As New Collections.Concurrent.ConcurrentBag(Of IO.FileInfo)

                For Each folder In _s2EnumeratedFolders
                    If Me._s0ProgressStatusbar.StopRequested Then Exit For
                    Me._s0ProgressStatusbar.ReportTaskProgress("Enumerating files from folders", _s2EnumeratedFolders.Count, foldercount, False)

                    files.AddRange((From i In folder.GetFiles Where FileNotSystemOrHidden(i)).ToList)
                    foldercount += 1
                Next

                _s2EnumeratedFiles = files

                Me._s0ProgressStatusbar.EndTask()

                Me.Invoke(
                    Sub()
                        _s2tsbLoadPhotos.Enabled = True
                        _s2tsbLoadPhotos.Text = String.Format("Load photos from {0} files", _s2EnumeratedFiles.Count.ToString("N0"))

                    End Sub)

            End Sub

        a.BeginInvoke(Nothing, Nothing)

    End Sub

    ''' <summary>
    ''' Attempt to generate instances of <see cref="PhotoFile"/> for each file in <see cref="_s2EnumeratedFiles"/>. Successfully generated instances are populated to the <see cref="Project"/>.
    ''' </summary>
    ''' <remarks>This method attempts to load each photo in its own thread and reports progress to <see cref="_s0ProgressStatusbar"/>. This method can be interrupted by the user clicking on the stop button provided in that control.</remarks>
    Private Sub _s2LoadPhotos()
        'start progress bar
        _s0ProgressStatusbar.StartTask()
        Dim photos As New Collections.Concurrent.ConcurrentBag(Of PhotoFile)

        _s4CorrelationCompleted = False

        Dim lvi As New Collections.Concurrent.ConcurrentBag(Of ListViewItem)

        'create a thread for each file

        Dim threads = (From i In _s2EnumeratedFiles Select (
            Sub()
                Threading.Thread.CurrentThread.Priority = Threading.ThreadPriority.BelowNormal

                If _s0ProgressStatusbar.StopRequested Then Exit Sub

                Dim p = New PhotoFile(i, Project)

                photos.Add(p)

                Dim TakenDateText As String
                Dim GPSText As String

                If p.TakenDate.HasValue Then
                    TakenDateText = p.TakenDate.Value.ToShortDateString
                Else
                    TakenDateText = "(No taken date)"
                End If

                If p.GPS.HasValue Then
                    GPSText = String.Format("Lat: {0}, Lon: {1}", p.GPS.Value.Latitude, p.GPS.Value.Longitude)
                Else
                    GPSText = "(No GPS data)"
                End If

                lvi.Add(New ListViewItem({p.File.Directory.Name, p.File.Name, TakenDateText, GPSText}))

                _s0ProgressStatusbar.ReportTaskProgress("Loading photo metadata", _s2EnumeratedFiles.Count, photos.Count, False)

            End Sub).BeginInvoke(Nothing, Nothing)).ToList

        'monitoring thread so we know when all photos are processed
        'body of the monitoring thread
        Dim monitoringThread =
            Sub()
                Threading.Thread.CurrentThread.Priority = Threading.ThreadPriority.BelowNormal

                'poll the entire list of threads. wait for all to be completed. also, we're only polling once every 100 miliseconds
                While (From i In threads Where Not i.IsCompleted).Count > 0
                    Threading.Thread.Sleep(100)
                End While

                'all threads are now complete.

                Project.Photos = photos
                _s2PhotoListItems = lvi.ToArray

                'turn off progress bar ("Status: Idle")
                _s0ProgressStatusbar.EndTask()

                Me.Invoke(
                    Sub()
                        _s2lvPhotos.Items.Clear()
                        _s2lvPhotos.VirtualListSize = Project.Photos.Count

                        [Step].UpdateStepAvailability()

                        If Project.Photos.Count = 0 Then
                            MsgBox("No photos with usable metatdata were found. Please select a different set of folders on Step 1.")
                        Else
                            [Step].GoToNextStep()
                        End If

                    End Sub)
            End Sub

        'execute the monitoring thread and return immediately
        Dim tmp = monitoringThread.BeginInvoke(Nothing, Nothing)

    End Sub

    Private Sub _s2lvPhotos_RetrieveVirtualItem(sender As Object, e As RetrieveVirtualItemEventArgs) Handles _s2lvPhotos.RetrieveVirtualItem

        e.Item = Me._s2PhotoListItems(e.ItemIndex)

    End Sub

    Private Sub _s2tsbLoadPhotos_Click(sender As Object, e As EventArgs) Handles _s2tsbLoadPhotos.Click
        _s2LoadPhotos()

    End Sub

End Class