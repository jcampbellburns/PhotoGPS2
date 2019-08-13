Imports Microsoft.WindowsAPICodePack.Dialogs

#If DEBUG Then

Partial Class null
    'This just makes sure this file doesn't open in a designer
End Class

#End If

Partial Public Class fMain

    ''' <summary>
    ''' The currently selected destination folder.
    ''' </summary>
    Private _s6DestFolder As String

    Private Sub _s6bBrowse_Click(sender As Object, e As EventArgs) Handles _s6bBrowse.Click
        Dim brws As New CommonOpenFileDialog

        brws.IsFolderPicker = True

        If Not brws.ShowDialog = CommonFileDialogResult.Cancel Then
            _s6tbDestFolder.Text = brws.FileName
            _s6CommitNewFolderValue()

        End If

    End Sub

    Private Sub _s6bStart_Click(sender As Object, e As EventArgs) Handles _s6bStart.Click
        _s6StartMove()
    End Sub

    ''' <summary>
    ''' Validates and selects the destination folder entered or browsed by the user. If the folder is not valid, <see cref="_s6tbDestFolder"/> will have red text.
    ''' </summary>
    Private Sub _s6CommitNewFolderValue()

        Dim proposedFolder = _s6tbDestFolder.Text

        If IO.Directory.Exists(proposedFolder) Then
            Dim di As New IO.DirectoryInfo(proposedFolder)

            'set the working folder
            _s6DestFolder = di.FullName
            _s6tbDestFolder.Text = di.FullName
            _s6bStart.Enabled = True

            'clear the 'bad folder' indication since folder is good.
            _s6tbDestFolder.ForeColor = SystemColors.ControlText
        Else
            'indicate the folder is no good
            _s6tbDestFolder.ForeColor = System.Drawing.Color.Red
            _s6bStart.Enabled = False
        End If

    End Sub


    ''' <summary>
    ''' Displays a prompt asking the user whether to overwrite the current CSV file selected in step 3 with the correlation data, save a new CSV file, or skip saving the CSV file.
    ''' </summary>
    Private Sub _s6PromptSaveCSV()
        Dim a As New TaskDialog

        a.InstructionText = "Where would you like to save the correlation data?"
        a.StartupLocation = TaskDialogStartupLocation.CenterOwner
        a.OwnerWindowHandle = Me.Handle

        Dim bSource As New TaskDialogCommandLink("bOverwrite", "")
        Dim bBrowse As New TaskDialogCommandLink("bBrowse", "Browse...")
        Dim bDontSave As New TaskDialogCommandLink("bDontSave", "Don't Save")

        'a.StandardButtons = TaskDialogStandardButtons.Cancel

        AddHandler bSource.Click,
            Sub(sender1, e1)
                If _s6SaveCSV(False) Then a.Close(TaskDialogResult.Ok)

            End Sub

        AddHandler bBrowse.Click,
            Sub(sender1, e1)
                If _s6SaveCSV(True) Then a.Close(TaskDialogResult.Ok)
            End Sub

        AddHandler bDontSave.Click,
            Sub(sender1, e1)
                a.Close(TaskDialogResult.Ok)
            End Sub

        If _s3CSVFile IsNot Nothing Then

            bSource.Text = String.Format("Overwrite '{0}'", _s3CSVFile.Name)
            a.Controls.Add(bSource)

        End If

        a.Controls.Add(bBrowse)
        a.Controls.Add(bDontSave)

        a.Show()

    End Sub

    ''' <summary>
    ''' Called when the user selects either save option in <see cref="_s6PromptSaveCSV()"/>. Not called if the user decides to skip saving the CSV. Saves the CSV, optionally allow the user to browse to select a new CSV file first.
    ''' </summary>
    ''' <param name="Browse">If <c>True</c>, the user is prompted where to save the CSV. <see cref="_s3CSVFile"/> is updated to the file selected by the user before saving. If <c>False</c>, the existing file referenced by <see cref="_s3CSVFile"/> is overwritten.</param>
    ''' <returns><c>True</c> if the file was saved. <c>False</c> if the user clicked the cancel button on the file save window and the file was not saved.</returns>
    Private Function _s6SaveCSV(Browse As Boolean) As Boolean
        'return's true if saved

        If Browse Then
            Dim a = New Microsoft.WindowsAPICodePack.Dialogs.CommonSaveFileDialog

            a.Filters.Add(New CommonFileDialogFilter("Comma Separated Values", ".csv"))
            a.Filters.Add(New CommonFileDialogFilter("All Files", "*"))

            If a.ShowDialog = CommonFileDialogResult.Cancel Then
                Return False
            Else
                _s3CSVFile = New IO.FileInfo(a.FileName)
                _s3tstbCSVFile.Text = _s3CSVFile.Name
            End If

        End If

        Try
            IO.File.WriteAllText(_s3CSVFile.FullName.EnsureFilePrepend, CSVSerializer(Of Job).Serialize(From i In Project.Jobs Where Not i.ExcludeFromSerialization))
        Catch ex As Exception
            MsgBox(String.Format("An exception occured when trying to write to '{0}'. Please try again or select a different file. Exception type: '{1}'. Exception message: '{2}'", _s3CSVFile.Name, ex.GetType.ToString, ex.Message), MsgBoxStyle.Critical)
            Return False

        End Try

        Return True
    End Function

    ''' <summary>
    ''' Starts moving the files based on the currently select options.
    ''' </summary>
    Private Sub _s6StartMove()

        _s6bStart.Enabled = False

        Dim basefolder = _s6tbDestFolder.Text
        Dim foldername As Func(Of PhotoFile, String) = Nothing

        Dim SourceFolders = (From i In Project.Photos Where Not i.IsDeleted Select i.File.DirectoryName Distinct).ToArray

        Select Case True
            Case _s6rSameFolder.Checked = True
                foldername =
                    Function(p)
                        Return basefolder
                    End Function

            Case _s6rTakenDate.Checked = True
                foldername =
                    Function(p)
                        If p.TakenDate.HasValue Then
                            Return IO.Path.Combine(basefolder, p.TakenDate.Value.ToString("yyyy-MM-dd"))
                        Else
                            Return p.File.DirectoryName
                        End If
                    End Function
            Case _s6rJobID.Checked = True
                foldername =
                    Function(p)
                        Dim j = From i In Project.Jobs Where i.Photos.Contains(p) And Not p.IsDeleted Select a = i.DispatchNumber Order By a Ascending Select a.Replace("\", "-").Replace("/", "-") Distinct

                        If j.Count > 0 Then
                            Return IO.Path.Combine(basefolder, String.Join(", ", j))
                        Else
                            Return p.File.DirectoryName
                        End If
                    End Function
        End Select

        _s0ProgressStatusbar.StartTask()

        Dim totalphotos = (From p In Project.Photos Where Not p.IsDeleted)
        Dim completedphotos = 0

        Dim threads = (From i In totalphotos Select (
            Sub(p As PhotoFile)

                Threading.Thread.CurrentThread.Priority = Threading.ThreadPriority.BelowNormal

                If Not _s0ProgressStatusbar.StopRequested Then

                    p.RenameAndMove(foldername(p))

                End If
                completedphotos += 1
                _s0ProgressStatusbar.ReportTaskProgress("Moving photos", totalphotos.Count, completedphotos, False)

            End Sub).BeginInvoke(i, Nothing, Nothing)).ToArray

        Dim monitoringthread =
            Sub()
                Threading.Thread.CurrentThread.Priority = Threading.ThreadPriority.BelowNormal

                'wait here until all files are moved
                While (From i In threads Where i.IsCompleted).Count < threads.Count
                    Threading.Thread.Sleep(100)
                End While

                If _s6cDeleteEmpty.Checked Then
                    ''edge case: delete thumbs.db and desktop.ini wherever they exist
                    'Dim q = From i In SourceFolders, file In IO.Directory.GetFiles(i) Where IO.Directory.Exists("\\?\" & i) And (IO.Path.GetFileName(file).ToLower = "thumbs.db" Or IO.Path.GetFileName(file).ToLower = "desktop.ini") Select file

                    'For Each f In q
                    '    IO.File.Delete(f)
                    'Next

                    'delete empty source folders
                    Dim emptysourcefolders = From i In SourceFolders Where IO.Directory.GetFiles(i).Count = 0

                    For Each f In emptysourcefolders
                        Try
                            IO.Directory.Delete(f)
                        Catch ex As IO.IOException
                            Select Case ex.HResult
                                Case &H80131620 'access denied (file is open?)
                                    'skip
                                Case &H80070091 'directory not empty
                                    'skip
                                Case Else
                                    Throw
                            End Select
                        End Try
                    Next
                End If

                'remove deleted photos from project and from all jobs
                Project.Photos = (From i In Project.Photos Where Not i.IsDeleted).ToList

                For Each j In Project.Jobs
                    j.Photos = (From i In j.Photos Where Not i.IsDeleted).ToList
                Next

                _s0ProgressStatusbar.EndTask()

                _s0ProgressStatusbar.EndTask()

                Me.Invoke(Sub()
                              _s6bStart.Enabled = True
                              _s6PromptSaveCSV()
                          End Sub)
            End Sub

        monitoringthread.BeginInvoke(Nothing, Nothing)
    End Sub

    Private Sub _s6tbDestFolder_KeyDown(sender As Object, e As KeyEventArgs) Handles _s6tbDestFolder.KeyDown

        'clear the 'bad folder' indication since user is typing something else.
        _s6tbDestFolder.ForeColor = SystemColors.ControlText

        Select Case e.KeyCode
            Case Keys.Enter
                e.Handled = True
                _s6CommitNewFolderValue()
            Case Keys.Escape
                e.Handled = True
                _s6tbDestFolder.Text = _s6DestFolder

        End Select
    End Sub

    Private Sub _s6tbDestFolder_Leave(sender As Object, e As EventArgs) Handles _s6tbDestFolder.Leave
        _s6CommitNewFolderValue()

    End Sub

End Class