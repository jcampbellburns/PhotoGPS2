#If DEBUG Then

Partial Class null
    'This just makes sure this file doesn't open in a designer
End Class

#End If

Partial Public Class fMain

    ''' <summary>
    ''' If this is <c>True</c>, we used the SendMessage Winodws API call to select all items in <see cref="_s5lbFolderFiles"/>. Doing this causes the <see cref="ListBox.SelectedIndices"/> to not represent the selected items so other method which rely on this must know that this method was used to select all of the items.
    ''' </summary>
    Private _s5FolderFilesSelectedAll As Boolean

    ''' <summary>
    ''' An array of instances of <see cref="ListViewItem"/> to be displayed in <see cref="_s5lvJobs"/>. These items are the ones retrieved by <see cref="ListView.RetrieveVirtualItem"/>.
    ''' </summary>
    Private _s5JobsLVItems As ListViewItem() = {}

    ''' <summary>
    ''' A <see cref="Dictionary(Of TKey, TValue)"/> which contains the same instances of <see cref="ListViewItem"/> as in <see cref="_s5JobsLVItems"/> but correlated with the <see cref="Job"/> instance used to generate the <see cref="ListViewItem"/>. This is used to correlate selected items in <see cref="_s5lvJobs"/> with the related instances of <see cref="Job"/>.
    ''' </summary>
    Private _s5JobsLVItemsLookup As Dictionary(Of Job, ListViewItem)

    ''' <summary>
    ''' If this is <c>True</c>, we used the SendMessage Winodws API call to select all items in <see cref="_s5lbJobFiles"/>. Doing this causes the <see cref="ListBox.SelectedIndices"/> to not represent the selected items so other method which rely on this must know that this method was used to select all of the items.
    ''' </summary>
    Private _s5lbJobFilesSelectedAll As Boolean

    ''' <summary>
    ''' The current selection of files from either <see cref="_s5lbFolderFiles"/> or <see cref="_s5lbJobFiles"/> which is selected to be displayed in <see cref="_s5Preview"/>.
    ''' </summary>
    Private _s5PreviewFiles As _s5FileListEntry(Of IO.FileInfo)() = {}

    ''' <summary>
    ''' The index of the file from <see cref="_s5PreviewFiles"/> currently displayed in <see cref="_s5Preview"/>.
    ''' </summary>
    Private _s5PreviewFilesCurrentIndex As Integer

    ''' <summary>
    ''' The currently selected files in <see cref="_s5lbJobFiles"/>.
    ''' </summary>
    Private _s5SelectedCorrelatedFiles As _s5FileListEntry(Of IO.FileInfo)() = {}

    ''' <summary>
    ''' An array of <see cref="Job"/> instances represented by the currently selected items in <see cref="_s5lvJobs"/>.
    ''' </summary>
    Private _s5SelectedJobs As Job() = {}

    ''' <summary>
    ''' The currently selected files in <see cref="_s5lbFolderFiles"/>.
    ''' </summary>
    Private _s5SelectedUncorrelatedFiles As _s5FileListEntry(Of IO.FileInfo)() = {}

    ''' <summary>
    ''' An array of <see cref="PhotoFile"/> instances which are not correlated to an instance of <see cref="Job"/> in <see cref="Project"/>.
    ''' </summary>
    Private _s5UncorrelatedPhotos As PhotoFile() = {}

    ''' <summary>
    ''' Assign the <see cref="PhotoFile"/> instances represented by the currently selected items in <see cref="_s5lbFolderFiles"/> to the instances of <see cref="Job"/> represented by the selected items in <see cref="_s5lvJobs"/>.
    ''' </summary>
    Sub _s5AssignPhotos()
        'need a list of selected uncorrelated photofiles
        Dim files = _s5SelectedUncorrelatedFiles.ToArray
        Dim p = From i In Project.Photos, j In files Where i.File.FullName = j.Value.FullName Select i

        'associate with the selected jobs
        For Each i In _s5SelectedJobs
            i.Photos.AddRange(p)
        Next

        ''update the uncorrelated files listbox
        '_s5lbFolderFiles.BeginUpdate()
        '_s5lbFolderFiles.Items.Clear()
        '_s5lbFolderFiles.Items.AddRange((From i In _s5lbFolderFiles.Items Where Not files.Contains(i)).ToArray)
        '_s5lbFolderFiles.EndUpdate()

        ''update the correlated files listbox
        '_s5lbJobFiles.BeginUpdate()
        '_s5lbJobFiles.Items.AddRange(files)
        '_s5lbJobFiles.EndUpdate()

        _s5UpdateJobFiles()
        _s5UpdateFolderFiles()

        _s5RefreshFoldersAndJobs()

    End Sub

    ''' <summary>
    ''' Unassign the <see cref="PhotoFile"/> instances represented by the currently selected items in <see cref="_s5lbJobFiles"/> from the instances of <see cref="Job"/> represented by the selected items in <see cref="_s5lvJobs"/>.
    ''' </summary>
    Sub _s5UnassignPhotos()
        'need a list of selected correlated photofiles
        Dim files = _s5SelectedCorrelatedFiles.ToArray
        Dim p = (From i In Project.Photos, j In files Where i.File.FullName = j.Value.FullName Select i).ToList

        'unassociate with the selected jobs
        For Each i In _s5SelectedJobs
            i.Photos = (From j In i.Photos Where Not p.Contains(j)).ToList
        Next

        ''update the correlated files listbox
        '_s5lbJobFiles.BeginUpdate()
        '_s5lbJobFiles.Items.Clear()
        '_s5lbJobFiles.Items.AddRange((From i In _s5lbJobFiles.Items Where Not files.Contains(i)).ToArray)
        '_s5lbJobFiles.EndUpdate()

        ''update the uncorrelated files listbox
        '_s5lbFolderFiles.BeginUpdate()
        '_s5lbFolderFiles.Items.AddRange(files)
        '_s5lbFolderFiles.EndUpdate()

        _s5UpdateJobFiles()
        _s5UpdateFolderFiles()

        _s5RefreshFoldersAndJobs()
    End Sub

    ''' <summary>
    ''' Called when the selection changes in <see cref="_s5lvJobs"/>. Updates the contents of <see cref="_s5lbJobFiles"/> to represent the <see cref="PhotoFile"/> instances associated with the instances of <see cref="Job"/> represented by the selected items in <see cref="_s5lvJobs"/>.
    ''' </summary>
    Sub _s5UpdateJobFiles()
        _s5SelectedJobs = (From i In _s5lvJobs.SelectedIndices, j In _s5JobsLVItemsLookup Where j.Value Is _s5JobsLVItems(i) Select j.Key).ToArray

        Dim files = From i In _s5SelectedJobs, j In i.Photos Select New _s5FileListEntry(Of IO.FileInfo)(j.File)

        'disable the remove button if there are any jobs selected which were not manually added. Enable otherwise.
        _s5tsbRemoveJob.Enabled = Not (From i In _s5SelectedJobs Where Not i.ExcludeFromSerialization).Count > 0

        _s5lbJobFiles.BeginUpdate()
        _s5lbJobFiles.Items.Clear()

        If files.Count = 0 Then
            _s5lbJobFiles.Items.Add("(No photos for this job)")
        Else
            _s5lbJobFiles.Items.AddRange(files.ToArray)

            If Not _s5SuppressNextSelectAllJobFiles Then
                _s5lbJobFiles.SelectAll 'bug fix: manually selecting all items raises ListBox.SelectedIndexChanged for each new item being selected. The SelectAll extension calls SendMessage to select all items.
                '_s5UpdatePreview() 'bug fix: when using Sendmessage to select all items, ListBox.SelectedItems is not updated. Added code to _s5UpdatePreview to use ListBox.Items instead of ListBox.SelectedItems when True is passed
                _s5lbJobFilesSelectedAll = True
            Else
                _s5lbJobFiles.SelectedIndex = 0
                _s5SuppressNextSelectAllJobFiles = False
            End If
            _s5lbJobFiles.Focus()
                _s5UpdatePreview()

            End If

            _s5lbJobFiles.EndUpdate()
    End Sub

    ''' <summary>
    ''' Initializes step 4 by populating both <see cref="_s5lbFolders"/> and <see cref="_s5lvJobs"/>. Also configures the autocomplete list for <see cref="_s5tbJobSearch"/>.
    ''' </summary>
    Private Sub _s5BeginStep5()
        _s5RefreshFoldersAndJobs()

        _s5lbFolders.SelectedIndex = 0
        _s5lvJobs.SelectedIndices.Clear()
        _s5lvJobs.SelectedIndices.Add(0)

    End Sub

    Private Sub _s5RefreshFoldersAndJobs()

        _s5SuppressNextSelectAllFolderFiles = True
        _s5SuppressNextSelectAllJobFiles = True
        _s5UpdateUncorrelatedFoldersList()
        _s5UpdateJobsList()

    End Sub

    ''' <summary>
    ''' Updates the <see cref="ToolStripButton.Enabled"/> property for both <see cref="_s5tsbNext"/> and <see cref="_s5tsbPrevious"/> based on the number of items in <see cref="_s5PreviewFiles"/> and the current value of <see cref="_s5PreviewFilesCurrentIndex"/>.
    ''' </summary>
    Private Sub _s5CheckPrevNextEnabledState()

        _s5tsbPrevious.Enabled = (_s5PreviewFilesCurrentIndex > 0)
        _s5tsbNext.Enabled = (_s5PreviewFilesCurrentIndex < _s5PreviewFiles.Count - 1)

    End Sub

    ''' <summary>
    ''' Called when the user presses <c>Enter</c> in <see cref="_s5tstbAddJob"/> or when <see cref="ToolStripDropDownButton.DropDownClosed"/> event is raised for <see cref="_s5tsbAddJob"/>. Creates a new instance of <see cref="Job"/> and adds it to the <see cref="Project"/>. Refreshes <see cref="_s5lbFolders"/> and <see cref="_s5lvJobs"/>.
    ''' </summary>
    Private Sub _s5CommitNewJob()
        If Not String.IsNullOrWhiteSpace(_s5tstbAddJob.Text) Then
            Project.Jobs = Project.Jobs.Append(New Job With {.DispatchNumber = _s5tstbAddJob.Text.Trim, .ExcludeFromSerialization = True})

            _s5tstbAddJob.Text = ""

            _s5tsbAddJob.HideDropDown()

            _s5RefreshFoldersAndJobs()
        End If
    End Sub

    Private Sub _s5lbFolderFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles _s5lbFolderFiles.SelectedIndexChanged
        _s5FolderFilesSelectedAll = False
        _s5UpdatePreview()
    End Sub

    Private Sub _s5lbFolders_SelectedIndexChanged(sender As Object, e As EventArgs) Handles _s5lbFolders.SelectedIndexChanged
        _s5UpdateFolderFiles()
    End Sub

    Private Sub _s5lbJobFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles _s5lbJobFiles.SelectedIndexChanged
        _s5lbJobFilesSelectedAll = False
        _s5UpdatePreview()
    End Sub

    Private Sub _s5lSearchSF_Click(sender As Object, e As EventArgs) Handles _s5lSearchSF.Click
        Dim a = TryCast(_s5lbFolders.SelectedItem, _s5FileListEntry(Of IO.DirectoryInfo))

        If a IsNot Nothing Then

            Start("https://burns-enviro.my.salesforce.com/_ui/search/ui/UnifiedSearchResults?searchType=2&sen=001&sen=00Q&sen=003&sen=00T&sen=a0D&sen=00U&sen=005&sen=a06&sen=006&sen=a0I&sen=00l&sen=00O&str=" & System.Web.HttpUtility.UrlEncode(a.ToString))
        End If
    End Sub

    Private Sub _s5lvJobs_RetrieveVirtualItem(sender As Object, e As RetrieveVirtualItemEventArgs) Handles _s5lvJobs.RetrieveVirtualItem

        e.Item = _s5JobsLVItems(e.ItemIndex)
    End Sub

    Private Sub _s5lvJobs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles _s5lvJobs.SelectedIndexChanged
        _s5UpdateJobFiles()
    End Sub

    Private Sub _s5tbJobSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles _s5tbJobSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            e.SuppressKeyPress = False

            Dim a = (From i In _s5JobsLVItemsLookup Where i.Key.DispatchNumber.ToUpper = _s5tbJobSearch.Text.Trim.ToUpper Select i.Value).FirstOrDefault

            If a IsNot Nothing Then
                Dim index As Integer = Array.IndexOf(_s5JobsLVItems, a)
                _s5lvJobs.SelectedIndices.Clear()
                _s5lvJobs.SelectedIndices.Add(index)
                _s5lvJobs.EnsureVisible(index)
                _s5lvJobs.Select()
            End If
        End If
    End Sub

    Private Sub _s5tsbAddJob_DropDownClosed(sender As Object, e As EventArgs) Handles _s5tsbAddJob.DropDownClosed
        _s5CommitNewJob()
    End Sub

    Private Sub _s5tsbAddJob_DropDownOpened(sender As Object, e As EventArgs) Handles _s5tsbAddJob.DropDownOpened
        _s5tstbAddJob.Focus()
    End Sub

    Private Sub _s5tsbAssign_Click(sender As Object, e As EventArgs) Handles _s5tsbAssign.Click
        _s5AssignPhotos()
    End Sub

    Private Sub _s5tsbNext_Click(sender As Object, e As EventArgs) Handles _s5tsbNext.Click

        _s5PreviewFilesCurrentIndex += 1
        _s5Preview.File = _s5PreviewFiles(_s5PreviewFilesCurrentIndex).Value
        _s5CheckPrevNextEnabledState()
    End Sub

    Private Sub _s5tsbPrevious_Click(sender As Object, e As EventArgs) Handles _s5tsbPrevious.Click

        _s5PreviewFilesCurrentIndex -= 1
        _s5Preview.File = _s5PreviewFiles(_s5PreviewFilesCurrentIndex).Value
        _s5CheckPrevNextEnabledState()
    End Sub

    Private Sub _s5tsbUnassign_Click(sender As Object, e As EventArgs) Handles _s5tsbUnassign.Click
        _s5UnassignPhotos()
    End Sub

    Private Sub _s5tstbAddJob_KeyDown(sender As Object, e As KeyEventArgs) Handles _s5tstbAddJob.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            e.SuppressKeyPress = True
            _s5CommitNewJob()
        End If
    End Sub

    ''' <summary>
    ''' Called when the selection changes in <see cref="_s5lbFolders"/>. Updates the contents of <see cref="_s5lbFolderFiles"/> to represent the <see cref="PhotoFile"/> instances associated with no instances of <see cref="Job"/> in <see cref="Project"/> but are contained within the folder represented by the selected item in <see cref="_s5lbFolders"/>.
    ''' </summary>
    Private Sub _s5UpdateFolderFiles()
        Dim a = TryCast(_s5lbFolders.SelectedItem, _s5FileListEntry(Of IO.DirectoryInfo))

        If a IsNot Nothing Then
            'get uncorrelated files in this folder

            Dim files = From i In _s5UncorrelatedPhotos Where i.File.DirectoryName = a.Value.FullName Select New _s5FileListEntry(Of IO.FileInfo)(i.File)

            _s5lbFolderFiles.BeginUpdate()
            _s5lbFolderFiles.Items.Clear()

            If files.Count = 0 Then
                _s5lbFolderFiles.Items.Add("***BUG: Empty uncorrelated folder***")
            Else
                _s5lbFolderFiles.Items.AddRange(files.ToArray)

                If Not _s5SuppressNextSelectAllFolderFiles Then
                    _s5lbFolderFiles.SelectAll() 'bug fix: manually selecting all items raises ListBox.SelectedIndexChanged for each new item being selected. The SelectAll extension calls SendMessage to select all items.
                    _s5FolderFilesSelectedAll = True
                Else
                    _s5lbFolderFiles.SelectedIndex = 0
                    _s5SuppressNextSelectAllFolderFiles = False
                End If

                '_s5UpdatePreview() 'bug fix: when using Sendmessage to select all items, ListBox.SelectedItems is not updated. Added code to _s5UpdatePreview to use ListBox.Items instead of ListBox.SelectedItems when True is passed

                _s5lbFolderFiles.Focus()
                _s5UpdatePreview()
            End If

            _s5lbFolderFiles.EndUpdate()

        End If
    End Sub

    Private _s5SuppressNextSelectAllFolderFiles As Boolean
    Private _s5SuppressNextSelectAllJobFiles As Boolean

    ''' <summary>
    ''' Clears and refreshes the contents of <see cref="_s5lvJobs"/>.
    ''' </summary>
    Private Sub _s5UpdateJobsList()
        'get a list of all jobs
        _s5JobsLVItemsLookup = New Dictionary(Of Job, ListViewItem)

        For Each i In From j In Project.Jobs Order By j.DispatchNumber Ascending
            Dim start As String = ""
            Dim [end] As String = ""

            If i.HasDates Then
                start = i.Start.Value.ToShortDateString
                [end] = i.End.Value.ToShortDateString
            End If

            _s5JobsLVItemsLookup.Add(i, New ListViewItem({i.DispatchNumber, start, [end], i.Photos.Count.ToString("N0")}))
        Next

        _s5lvJobs.BeginUpdate()
        _s5JobsLVItems = (From i In _s5JobsLVItemsLookup Select i.Value).ToArray
        _s5lvJobs.VirtualListSize = _s5JobsLVItems.Count
        _s5lvJobs.EndUpdate()

        _s5tbJobSearch.AutoCompleteMode = AutoCompleteMode.Suggest
        _s5tbJobSearch.AutoCompleteSource = AutoCompleteSource.CustomSource
        _s5tbJobSearch.AutoCompleteCustomSource.Clear()
        _s5tbJobSearch.AutoCompleteCustomSource.AddRange((From i In _s5JobsLVItemsLookup Select i.Key.DispatchNumber).ToArray)

    End Sub

    ''' <summary>
    ''' Update the <see cref="FilePreviewControl.File"/> property of <see cref="_s5Preview"/> and updates the control to show the currently selected file. Called by the <see cref="ToolStripButton.Click"/> event of both <see cref="_s5tsbNext"/> and <see cref="_s5tsbPrevious"/>. Also called when the selection changes in <see cref="_s5lbFolders"/>, <see cref="_s5lbFolderFiles"/>, <see cref="_s5lbJobFiles"/>, and <see cref="_s5lvJobs"/>.
    ''' </summary>
    Private Sub _s5UpdatePreview()

        If _s5FolderFilesSelectedAll Then 'bug fix: manual select all (via send message) doesn't update "selected items" so use _s5lbFolderFiles.Items instead of _s5lbFolderFiles.SelectedItems
            _s5SelectedUncorrelatedFiles = (From i In _s5lbFolderFiles.Items Select j = TryCast(i, _s5FileListEntry(Of IO.FileInfo)) Where j IsNot Nothing).ToArray
        Else
            _s5SelectedUncorrelatedFiles = (From i In _s5lbFolderFiles.SelectedItems Select j = TryCast(i, _s5FileListEntry(Of IO.FileInfo)) Where j IsNot Nothing).ToArray
        End If

        If _s5lbJobFilesSelectedAll Then 'bug fix: manual select all (via send message) doesn't update "selected items" so use _s5lbJobFiles.Items instead of _s5lbJobFiles.SelectedItems
            _s5SelectedCorrelatedFiles = (From i In _s5lbJobFiles.Items Select j = TryCast(i, _s5FileListEntry(Of IO.FileInfo)) Where j IsNot Nothing).ToArray
        Else
            _s5SelectedCorrelatedFiles = (From i In _s5lbJobFiles.SelectedItems Select j = TryCast(i, _s5FileListEntry(Of IO.FileInfo)) Where j IsNot Nothing).ToArray
        End If

        If _s5lbFolderFiles.Focused Then
            _s5PreviewFiles = _s5SelectedUncorrelatedFiles
        ElseIf _s5lbJobFiles.Focused Then
            _s5PreviewFiles = _s5SelectedCorrelatedFiles
        End If

        If _s5PreviewFiles.Count > 0 Then
            _s5PreviewFilesCurrentIndex = 0
            _s5Preview.File = _s5PreviewFiles(0).Value
        Else
            _s5PreviewFilesCurrentIndex = 0
            _s5Preview.File = Nothing
        End If

        _s5CheckPrevNextEnabledState()
    End Sub

    ''' <summary>
    ''' Clears and refreshes the contents of <see cref="_s5lbFolders"/>.
    ''' </summary>
    Private Sub _s5UpdateUncorrelatedFoldersList()
        'get a list of folders where at least one photo is uncorrelated

        _s5UncorrelatedPhotos = (From p In Project.Photos Where p.Jobs.Count = 0).ToArray

        Dim uncFolders = From j In (From i In _s5UncorrelatedPhotos Select i.File.Directory.FullName) Distinct Select New IO.DirectoryInfo(j)

        Dim folders = From j In uncFolders Order By j.Name Ascending Select New _s5FileListEntry(Of IO.DirectoryInfo)(j)

        _s5lbFolders.BeginUpdate()
        _s5lbFolders.Items.Clear()

        If folders.Count = 0 Then
            _s5lbFolderFiles.Items.Clear()
            _s5lbFolders.Items.Add("(No uncorrelated files)")
        Else
            _s5lbFolders.Items.AddRange(folders.ToArray)
        End If
        _s5lbFolders.EndUpdate()
    End Sub

    ''' <summary>
    ''' Removes the selected jobs from <see cref="Project"/> and refreshes both <see cref="_s5lbFolders"/> and <see cref="_s5lvJobs"/>.
    ''' </summary>
    ''' <remarks>Only removes those jobs where <see cref="Job.ExcludeFromSerialization"/> is set to <c>True</c>.</remarks>
    Private Sub _s5RemoveManuallyAddedJob()
        Dim q = From i In _s5SelectedJobs Where i.ExcludeFromSerialization

        Project.Jobs = (From i In Project.Jobs Where Not q.Contains(i)).ToList

        _s5RefreshFoldersAndJobs()
    End Sub

    Private Sub _s5tsbRemoveJob_Click(sender As Object, e As EventArgs) Handles _s5tsbRemoveJob.Click
        _s5RemoveManuallyAddedJob()
    End Sub

    ''' <summary>
    ''' This class represents in instance of <see cref="IO.FileSystemInfo"/> to be listed in a <see cref="ListBox"/>.
    ''' </summary>
    ''' <typeparam name="T">A type derrived from <see cref="IO.FileSystemInfo"/> which represents the type of filesystem item this class instance represents.</typeparam>
    Private Class _s5FileListEntry(Of T As IO.FileSystemInfo)

        ''' <summary>
        ''' Creates a new instance and sets the initial value of <see cref="_s5FileListEntry.Value"/>.
        ''' </summary>
        ''' <param name="Value"></param>
        Sub New(Value As T)
            _Value = Value
        End Sub

        ''' <summary>
        ''' The specific <see cref="IO.FileSystemInfo"/> instance represented by this item.
        ''' </summary>
        ''' <returns></returns>
        Public Property Value As T

        ''' <summary>
        ''' Overridden. Convert this instance to a <see cref="String"/>.
        ''' </summary>
        ''' <returns>Returnes the value of <see cref="IO.FileSystemInfo.Name"/> for <see cref="Value"/>.</returns>
        Public Overrides Function ToString() As String
            'Return MyBase.ToString()

            Return Value.Name
        End Function

    End Class

End Class