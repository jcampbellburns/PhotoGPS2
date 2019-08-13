Imports Microsoft.WindowsAPICodePack.Dialogs

#If DEBUG Then

Partial Class null
    'This just makes sure this file doesn't open in a designer
End Class

#End If

Partial Public Class fMain

    ''' <summary>
    ''' All of the folders which the user has explicitly added, along with a reference to a <see cref="_s1SelectedFolder"/> instance containing the folder, a <see cref="ListViewItem"/> and an indication of if the folder is to be searched recursively or not.
    ''' </summary>
    Private _s1Selectedfolders As New Dictionary(Of IO.DirectoryInfo, _s1SelectedFolder)

    ''' <summary>
    ''' Supports the functionality of the user hitting the Escape key while typing a folder name to cancel the folder name entry. Returns the current folder name to the textbox.
    ''' </summary>
    Private _s1TopWorkingFolder As String

    ''' <summary>
    ''' Add a folder from a specified FolderTreeView to <see cref="_s1Selectedfolders"/>.
    ''' </summary>
    ''' <param name="fn">The node representing the folder to add.</param>
    Private Sub _s1AddFolderFromNode(fn As FolderTreeview.FolderNode)
        'adds a foldernode to the list of selected folders

        If Not _s1Selectedfolders.ContainsKey(fn.Folder) Then
            _s1lvSelectedFolders.BeginUpdate()

            Dim sf As New _s1SelectedFolder(fn.Folder)
            _s1Selectedfolders.Add(fn.Folder, sf)

            Dim cs = _s1tsbRecursive.CheckState
            sf.Recursive = (cs = CheckState.Checked Or cs = CheckState.Indeterminate)

            _s1lvSelectedFolders.Items.Add(sf.ListViewItem)

            _s1lvSelectedFolders.EndUpdate()
        End If
    End Sub

    ''' <summary>
    ''' Add the selected node in the FolderTreeview to <see cref="_s1Selectedfolders"/>.
    ''' </summary>
    ''' <remarks>this method determines the selected node in <see cref="_s1ftvBrowser"/> and calls <see cref="_s1AddFolderFromNode"/> passing this node as the parameter.</remarks>
    Private Sub _s1AddSelectedFolder()
        Dim fn = TryCast(Me._s1ftvBrowser.SelectedNode, FolderTreeview.FolderNode)

        If fn IsNot Nothing Then
            _s1AddFolderFromNode(fn)
        End If
    End Sub

    ''' <summary>
    ''' Add the subfolders of the selected node in the FolderTreeview to <see cref="_s1Selectedfolders"/>.
    ''' </summary>
    ''' <remarks>this method determines the selected node in <see cref="_s1ftvBrowser"/> and calls <see cref="_s1AddFolderFromNode"/> one for each sub-node, passing the sub-nodes as the parameter.</remarks>
    Private Sub _s1AddSelectedFoldersSubfolders()
        Dim fn = TryCast(Me._s1ftvBrowser.SelectedNode, FolderTreeview.FolderNode)

        If fn IsNot Nothing Then
            If Not fn.IsExpanded Then fn.Expand()

            For Each node In fn.Nodes

                Dim n = TryCast(node, FolderTreeview.FolderNode)

                If n IsNot Nothing Then _s1AddFolderFromNode(n)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Validates and selects the top-level folder entered or browsed by the user. If the folder is not valid, <see cref="_s1ftvBrowser"/> will be shown empty and <see cref="_s1tstbWorkingFolderTB"/> will have red text
    ''' </summary>
    Private Sub _s1CommitNewFolderValue()

        Dim proposedFolder = _s1tstbWorkingFolderTB.Text

        If IO.Directory.Exists(proposedFolder) Then
            Dim di As New IO.DirectoryInfo(proposedFolder)

            'set the working folder
            _s1TopWorkingFolder = di.FullName
            _s1tstbWorkingFolderTB.Text = di.FullName

            'refresh the folder treeview
            _s1ftvBrowser.Focus()
            _s1ftvBrowser.Folder = di

            'clear the 'bad folder' indication since folder is good.
            _s1tstbWorkingFolderTB.ForeColor = SystemColors.ControlText
        Else
            'indicate the folder is no good
            _s1tstbWorkingFolderTB.ForeColor = System.Drawing.Color.Red
            _s1ftvBrowser.Folder = Nothing

            _s1tsbAddFolder.Enabled = False
            _s1tsbAddSubFolders.Enabled = False
        End If

    End Sub

    Private Sub _s1ftvBrowser_AfterEnumSubfolder(sender As FolderTreeview) Handles _s1ftvBrowser.AfterEnumSubfolder
        _s0ProgressStatusbar.EndTask()
    End Sub

    Private Sub _s1ftvBrowser_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles _s1ftvBrowser.AfterSelect
        Dim fn = TryCast(e.Node, FolderTreeview.FolderNode)

        If fn Is Nothing Then
            _s1tsbAddFolder.Enabled = False
            _s1tsbAddSubFolders.Enabled = False
        Else
            _s1tsbAddFolder.Enabled = True
            _s1tsbAddSubFolders.Enabled = True
        End If
    End Sub

    Private Sub _s1ftvBrowser_BeforeEnumSubfolder(sender As FolderTreeview) Handles _s1ftvBrowser.BeforeEnumSubfolder
        _s0ProgressStatusbar.StartTask(False)
    End Sub

    Private Sub _s1ftvBrowser_EnumSubfolderProgress(sender As FolderTreeview, ItemCount As Integer, CurrentItem As Integer) Handles _s1ftvBrowser.EnumSubfolderProgress

        _s0ProgressStatusbar.ReportTaskProgress("Enumerating subfolders", ItemCount, CurrentItem, False)
    End Sub

    ''' <summary>
    ''' Removes the selected folder or folders from the list of selected folders.
    ''' </summary>
    Private Sub _s1RemoveSelectedFolders()
        'get the selected folder based on the current selection
        Dim q = From i In _s1Selectedfolders Where i.Value.ListViewItem.Selected Select New With {.k = i.Key, .v = i.Value}

        _s1lvSelectedFolders.BeginUpdate()

        For Each sf In q.ToList

            _s1lvSelectedFolders.Items.Remove(sf.v.ListViewItem)
            _s1Selectedfolders.Remove(sf.k)
        Next
        _s1lvSelectedFolders.EndUpdate()

    End Sub

    Private Sub _s1tsbAddFolder_Click(sender As Object, e As EventArgs) Handles _s1tsbAddFolder.Click
        _s1AddSelectedFolder()
        [Step].UpdateStepAvailability()
    End Sub

    Private Sub _s1tsbAddSubFolders_Click(sender As Object, e As EventArgs) Handles _s1tsbAddSubFolders.Click
        _s1AddSelectedFoldersSubfolders()
        [Step].UpdateStepAvailability()
    End Sub

    Private Sub _s1tsbBrowse_Click(sender As Object, e As EventArgs) Handles _s1tsbBrowse.Click
        Dim brws As New CommonOpenFileDialog

        brws.IsFolderPicker = True

        If Not brws.ShowDialog = CommonFileDialogResult.Cancel Then
            _s1tstbWorkingFolderTB.Text = brws.FileName
            _s1CommitNewFolderValue()

            _s1AddSelectedFolder()
            [Step].UpdateStepAvailability()
        End If

    End Sub

    Private Sub _s1tsbRecursive_CheckedChanged(sender As Object, e As EventArgs) Handles _s1tsbRecursive.CheckedChanged
        'User clicked Recursive? button

        _s1UpdateRecursive()

    End Sub

    Private Sub _s1tsbRemoveFolders_Click(sender As Object, e As EventArgs) Handles _s1tsbRemoveFolders.Click
        'User clicked the "Remove folder(s)" button
        _s1RemoveSelectedFolders()
        [Step].UpdateStepAvailability()
    End Sub

    Private Sub _s1tstbWorkingFolderTB_KeyDown(sender As Object, e As KeyEventArgs) Handles _s1tstbWorkingFolderTB.KeyDown

        'clear the 'bad folder' indication since user is typing something else.
        _s1tstbWorkingFolderTB.ForeColor = SystemColors.ControlText

        Select Case e.KeyCode
            Case Keys.Enter
                e.Handled = True
                _s1CommitNewFolderValue()
            Case Keys.Escape
                e.Handled = True
                _s1tstbWorkingFolderTB.Text = _s1TopWorkingFolder

        End Select
    End Sub

    Private Sub _s1tstbWorkingFolderTB_Leave(sender As Object, e As EventArgs) Handles _s1tstbWorkingFolderTB.Leave
        _s1CommitNewFolderValue()

    End Sub

    ''' <summary>
    ''' Sets or removes the <see cref="_s1SelectedFolder.Recursive"/> flag for each selected folder.
    ''' </summary>
    ''' <remarks>This method does not take into account the current status of the <see cref="_s1SelectedFolder.Recursive"/> flag and instead bases the new value on if <see cref="_s1tsbRecursive"/> is checked.</remarks>
    Private Sub _s1UpdateRecursive()
        'get the selected folder based on the current selection
        Dim q = From i In _s1Selectedfolders Where i.Value.ListViewItem.Selected Select i.Value

        _s1lvSelectedFolders.BeginUpdate()

        For Each fn In q
            fn.Recursive = _s1tsbRecursive.Checked
        Next
        _s1lvSelectedFolders.EndUpdate()
    End Sub

    ''' <summary>
    ''' A private class which associates an <see cref="io.DirectoryInfo"/> instance, a <see cref="ListViewItem"/> instance, and a flag indicating that a folder is to be searched recursively or not.
    ''' </summary>
    Private Class _s1SelectedFolder
        Private _folder As IO.DirectoryInfo

        Private _lvi As ListViewItem

        Private _recursive As Boolean

        ''' <summary>
        ''' Creates a new instance of <see cref="_s1SelectedFolder"/> with the <see cref="_s1SelectedFolder.Folder"/> property set.
        ''' </summary>
        ''' <param name="Folder">An instance of <see cref="IO.DirectoryInfo"/> upon which to base this <see cref="_s1SelectedFolder"/>.</param>
        Sub New(Folder As IO.DirectoryInfo)
            Me._folder = Folder
        End Sub

        ''' <summary>
        ''' The <see cref="IO.DirectoryInfo"/> instance associates with this <see cref="_s1SelectedFolder"/> instance.
        ''' </summary>
        Public Property Folder As IO.DirectoryInfo
            Get
                Return _folder
            End Get
            Set(value As IO.DirectoryInfo)
                _folder = value
                _updateLVI()
            End Set
        End Property

        ''' <summary>
        ''' A <see cref="system.windows.forms.ListViewItem"/> representing the value of <see cref="_s1SelectedFolder.Folder"/>.
        ''' </summary>
        ''' <remarks>The <see cref="system.windows.forms.ListViewItem"/> is generated the first time this property is read and then cached. The same instance of <see cref="system.windows.forms.ListViewItem"/> is returned on each subsequent call.</remarks>
        Public ReadOnly Property ListViewItem As ListViewItem
            Get
                _updateLVI()

                Return _lvi
            End Get
        End Property

        ''' <summary>
        ''' Indicates whether the folder referenced by <see cref="_s1SelectedFolder.Folder"/> is to be searched recursively.
        ''' </summary>
        Public Property Recursive As Boolean
            Get
                Return _recursive
            End Get
            Set(value As Boolean)
                _recursive = value
                _updateLVI()
            End Set
        End Property

        ''' <summary>
        ''' Generates the <see cref="system.windows.forms.ListViewItem"/> returned by <see cref="ListViewItem"/>.
        ''' </summary>
        Private Sub _updateLVI()
            If _lvi Is Nothing Then

                _lvi = New ListViewItem
                _lvi.SubItems.Add("")
                _lvi.SubItems.Add("")
            End If

            With _lvi
                .Text = _folder.Name
                .SubItems(1).Text = (_folder.FullName)
                .SubItems(2).Text = (IIf(_recursive, "Yes", "No"))
            End With
        End Sub

    End Class

End Class