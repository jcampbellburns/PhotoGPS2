Imports System.IO

#If DEBUG Then

Partial Class null
    'This just makes sure this file doesn't open in a designer
End Class

#End If

''' <summary>
''' A customized treeview which shows a folderstructure based on the filesystem. Nodes with subfolders enumerate the subfolders when first expanded to save time versus enumerating the whole folder structure when a new top-level folder is selected.
''' </summary>
Public Class FolderTreeview
    Inherits TreeView

    Private _folder As IO.DirectoryInfo

    Public Event AfterEnumSubfolder(sender As FolderTreeview)

    Public Event BeforeEnumSubfolder(sender As FolderTreeview)

    Public Event EnumSubfolderProgress(sender As FolderTreeview, ItemCount As Integer, CurrentItem As Integer)

    Public Property Folder As DirectoryInfo
        Get
            Return _folder
        End Get
        Set(value As DirectoryInfo)
            _folder = value
            SetTopWorkingFolder(_folder)
        End Set
    End Property

    Private Async Sub _folderToNodes(fi As IO.DirectoryInfo, n As TreeNodeCollection)
        'enumerate folders
        RaiseEvent BeforeEnumSubfolder(Me)
        Me.Update() 'fix: shows the "Please wait..." placeholder node
        Me.BeginUpdate() 'fix: don't show sub-folder nodes being added one-at-a-time

        Dim a = Sub()

                    n.Clear()

                    Dim f1 = (From f In fi.GetDirectories Where FileNotSystemOrHidden(f) Order By f.Name Ascending).ToList

                    Dim folders = (From f In f1 Select New FolderNode(f)).WithProgressReporting(f1.Count, Sub(t, c) RaiseEvent EnumSubfolderProgress(Me, t, c)).ToList

                    n.AddRange(folders.ToArray)
                End Sub

        Await Task.Run(Sub() Me.Invoke(a))

        Me.EndUpdate()

        RaiseEvent AfterEnumSubfolder(Me)
    End Sub

    Private Sub FolderTreeview_AfterCollapse(sender As Object, e As TreeViewEventArgs) Handles Me.AfterCollapse
        Dim fn = TryCast(e.Node, FolderNode)

        If fn IsNot Nothing Then
            fn.Nodes.Clear()

            If fn.HasSubfolders Then fn.Nodes.Add(fn.PlaceHolderSubNode)
        End If
    End Sub

    Private Sub FolderTreeview_AfterExpand(sender As Object, e As TreeViewEventArgs) Handles Me.AfterExpand
        Dim fn = TryCast(e.Node, FolderNode)

        If fn IsNot Nothing Then
            Me._folderToNodes(fn.Folder, fn.Nodes)
        End If
    End Sub

    ''' <summary>
    ''' Displays the sub-folders of a folder. Sub-folders which have sub-folders will be shown with a [+] and will allow the user to expand the folder to see even more sub-folders.
    ''' </summary>
    ''' <param name="Folder">Which folder to start at.</param>
    ''' <remarks>Child subnodes are re-generated every time the parent node is expanded. Collapsing and expanding a sub-node is an effective way to refresh the child nodes in the even that the folder structure changes at run time.</remarks>
    Private Sub SetTopWorkingFolder(Folder As IO.DirectoryInfo)

        Me.Nodes.Clear()

        'design streamline note:
        'originally, when selecting a folder, we'd show just the subfolders of the top folder.
        'this prevents the user from selecting the top-level folder.
        'instead, we add a root node for this folder so the user can select it.
        'also, to reduce clicks, we pre-expand this root node as well as highlight it.

        Me._folder = Folder

        If Folder IsNot Nothing Then
            Dim root = New FolderNode(_folder)
            Me.Nodes.Add(root)
            Me.SelectedNode = root
            root.Expand()

            'FolderToNodes(Folder, Me.Nodes)
        End If
    End Sub

    Public Class FolderNode
        Inherits TreeNode

        Public Folder As IO.DirectoryInfo

        Friend PlaceHolderSubNode As TreeNode = New TreeNode("Please wait...")

        Public Sub New(Folder As IO.DirectoryInfo)
            Me.Folder = Folder
            Me.Text = Folder.Name

            If HasSubfolders Then Nodes.Add(PlaceHolderSubNode)

        End Sub

        Private Sub New()

        End Sub

        Friend ReadOnly Property HasSubfolders As Boolean
            Get
                Try
                    Return Folder.GetDirectories().Count > 0
                Catch ex As Exception
                    Return False
                End Try

            End Get
        End Property

    End Class

End Class