Imports PhotoGPS

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fMain
    Inherits System.Windows.Forms.Form

    Friend WithEvents _s0ProgressStatusbar As PhotoGPS.ProgressStatusBar
    Friend WithEvents _s0tsbStep1 As ToolStripButton
    Friend WithEvents _s0tsbStep2 As ToolStripButton
    Friend WithEvents _s0tsbStep3 As ToolStripButton
    Friend WithEvents _s0tsbStep4 As ToolStripButton
    Friend WithEvents _s0tsbStep6 As ToolStripButton
    Friend WithEvents _s0tsbStep5 As ToolStripButton
    Friend WithEvents _s0tsSteps As ToolStrip
    Friend WithEvents _s1chFolder As ColumnHeader
    Friend WithEvents _s1chFullPath As ColumnHeader
    Friend WithEvents _s1chRecursive As ColumnHeader
    Friend WithEvents _s1ftvBrowser As PhotoGPS.FolderTreeview
    Friend WithEvents _s1lvSelectedFolders As ListView
    Friend WithEvents _s1Page As Panel
    Friend WithEvents _s1tsbAddFolder As ToolStripButton
    Friend WithEvents _s1tsbAddSubFolders As ToolStripButton
    Friend WithEvents _s1tsbBrowse As ToolStripButton
    Friend WithEvents _s1tsbRecursive As ToolStripButton
    Friend WithEvents _s1tsbRemoveFolders As ToolStripButton
    Friend WithEvents _s1tstbWorkingFolderTB As ToolStripTextBox
    Friend WithEvents _s2chFolderName As ColumnHeader
    Friend WithEvents _s2chFolderPath As ColumnHeader
    Friend WithEvents _s2chGPS As ColumnHeader
    Friend WithEvents _s2chHash As ColumnHeader
    Friend WithEvents _s2chTaken As ColumnHeader
    Friend WithEvents _s2lvEnumeratedFolders As ListView
    Friend WithEvents _s2lvPhotos As ListView
    Friend WithEvents _s2Page As Panel
    Friend WithEvents _s2tsbLoadPhotos As ToolStripButton
    Friend WithEvents _s3chDispatchNumber As ColumnHeader
    Friend WithEvents _s3chEndDate As ColumnHeader
    Friend WithEvents _s3chGPS As ColumnHeader
    Friend WithEvents _s3chSFID As ColumnHeader
    Friend WithEvents _s3chStartDate As ColumnHeader
    Friend WithEvents _s3lvJobs As ListView
    Friend WithEvents _s3Page As Panel
    Friend WithEvents _s3tsbLoadjobs As ToolStripButton
    Friend WithEvents _s3tstbCSVFile As ToolStripTextBox
    Friend WithEvents _s3tstbEnd As ToolStripTextBox
    Friend WithEvents _s3tstbStart As ToolStripTextBox
    Friend WithEvents _s3tsTop As ToolStrip

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim _s1pMainWorkingArea As System.Windows.Forms.Panel
        Dim _s1splitter As System.Windows.Forms.Splitter
        Dim _s1tsTop As System.Windows.Forms.ToolStrip
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fMain))
        Dim _s1tsBottom As System.Windows.Forms.ToolStrip
        Dim _s2Splitter As System.Windows.Forms.Splitter
        Dim _s2pMainWorkingArea As System.Windows.Forms.Panel
        Dim _s2pRightWorkingArea As System.Windows.Forms.Panel
        Dim _s2chFolder As System.Windows.Forms.ColumnHeader
        Dim _s2tsTopRight As System.Windows.Forms.ToolStrip
        Dim _s3tss1 As System.Windows.Forms.ToolStripSeparator
        Dim _s3tsl1 As System.Windows.Forms.ToolStripLabel
        Dim _s3tss2 As System.Windows.Forms.ToolStripSeparator
        Dim _s3tsl2 As System.Windows.Forms.ToolStripLabel
        Dim _s3tss3 As System.Windows.Forms.ToolStripSeparator
        Dim _s0tss1 As System.Windows.Forms.ToolStripSeparator
        Dim _s0tss2 As System.Windows.Forms.ToolStripSeparator
        Dim _s0tss3 As System.Windows.Forms.ToolStripSeparator
        Dim _s0tss4 As System.Windows.Forms.ToolStripSeparator
        Dim _s0tss5 As System.Windows.Forms.ToolStripSeparator
        Dim _s4tsTop As System.Windows.Forms.ToolStrip
        Dim _s6g1 As System.Windows.Forms.GroupBox
        Dim _s6g2 As System.Windows.Forms.GroupBox
        Dim _s6cDeleteDuplicate As System.Windows.Forms.CheckBox
        Dim _s6l1 As System.Windows.Forms.Label
        Dim _s5pCenter As System.Windows.Forms.Panel
        Dim _s5tsPreviewControls As System.Windows.Forms.ToolStrip
        Dim _s5l5 As System.Windows.Forms.Label
        Dim _s5tsPreview As System.Windows.Forms.ToolStrip
        Dim _s5s4 As System.Windows.Forms.Splitter
        Dim _s5Right As System.Windows.Forms.Panel
        Dim _s5l3 As System.Windows.Forms.Label
        Dim _s5s3 As System.Windows.Forms.Splitter
        Dim _s5RightRight As System.Windows.Forms.Panel
        Dim _s5ch2 As System.Windows.Forms.ColumnHeader
        Dim _s5l4 As System.Windows.Forms.Label
        Dim s2 As System.Windows.Forms.Splitter
        Dim _s5Left As System.Windows.Forms.Panel
        Dim _s5l2 As System.Windows.Forms.Label
        Dim _s5s1 As System.Windows.Forms.Splitter
        Dim _s5l1 As System.Windows.Forms.Label
        Dim _s5tsLeftLeft As System.Windows.Forms.ToolStrip
        Me._s1lvSelectedFolders = New System.Windows.Forms.ListView()
        Me._s1chFolder = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me._s1chFullPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me._s1chRecursive = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me._s1ftvBrowser = New PhotoGPS.FolderTreeview()
        Me._s1tstbWorkingFolderTB = New System.Windows.Forms.ToolStripTextBox()
        Me._s1tsbBrowse = New System.Windows.Forms.ToolStripButton()
        Me._s1tsbAddFolder = New System.Windows.Forms.ToolStripButton()
        Me._s1tsbAddSubFolders = New System.Windows.Forms.ToolStripButton()
        Me._s1tsbRecursive = New System.Windows.Forms.ToolStripButton()
        Me._s1tsbRemoveFolders = New System.Windows.Forms.ToolStripButton()
        Me._s2lvPhotos = New System.Windows.Forms.ListView()
        Me._s2chFile = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me._s2chTaken = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me._s2chGPS = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me._s2chHash = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me._s2tsbLoadPhotos = New System.Windows.Forms.ToolStripButton()
        Me._s2lvEnumeratedFolders = New System.Windows.Forms.ListView()
        Me._s2chFolderName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me._s2chFolderPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me._s4tsbRun = New System.Windows.Forms.ToolStripButton()
        Me._s4tlTotals = New System.Windows.Forms.ToolStripLabel()
        Me._s6rSameFolder = New System.Windows.Forms.RadioButton()
        Me._s6rTakenDate = New System.Windows.Forms.RadioButton()
        Me._s6rJobID = New System.Windows.Forms.RadioButton()
        Me._s6cDeleteEmpty = New System.Windows.Forms.CheckBox()
        Me._s6bBrowse = New System.Windows.Forms.Button()
        Me._s6tbDestFolder = New System.Windows.Forms.TextBox()
        Me._s5Preview = New PhotoGPS.FilePreviewControl()
        Me._s5tsbPrevious = New System.Windows.Forms.ToolStripButton()
        Me._s5tsbNext = New System.Windows.Forms.ToolStripButton()
        Me._s5tsbAssign = New System.Windows.Forms.ToolStripButton()
        Me._s5tsbUnassign = New System.Windows.Forms.ToolStripButton()
        Me._s5lbJobFiles = New System.Windows.Forms.ListBox()
        Me._s5lvJobs = New System.Windows.Forms.ListView()
        Me._s5ch1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me._s5tbJobSearch = New System.Windows.Forms.TextBox()
        Me._s5lbFolderFiles = New System.Windows.Forms.ListBox()
        Me._s5lSearchSF = New System.Windows.Forms.ToolStripLabel()
        Me._s1Page = New System.Windows.Forms.Panel()
        Me._s2Page = New System.Windows.Forms.Panel()
        Me._s3Page = New System.Windows.Forms.Panel()
        Me._s3lvJobs = New System.Windows.Forms.ListView()
        Me._s3chDispatchNumber = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me._s3chStartDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me._s3chEndDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me._s3chGPS = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me._s3chSFID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me._s3tsTop = New System.Windows.Forms.ToolStrip()
        Me._s3tsbLoadjobs = New System.Windows.Forms.ToolStripButton()
        Me._s3tstbCSVFile = New System.Windows.Forms.ToolStripTextBox()
        Me._s3tstbStart = New System.Windows.Forms.ToolStripTextBox()
        Me._s3tstbEnd = New System.Windows.Forms.ToolStripTextBox()
        Me._s0tsSteps = New System.Windows.Forms.ToolStrip()
        Me._s0tsbStep1 = New System.Windows.Forms.ToolStripButton()
        Me._s0tsbStep2 = New System.Windows.Forms.ToolStripButton()
        Me._s0tsbStep3 = New System.Windows.Forms.ToolStripButton()
        Me._s0tsbStep4 = New System.Windows.Forms.ToolStripButton()
        Me._s0tsbStep5 = New System.Windows.Forms.ToolStripButton()
        Me._s0tsbStep6 = New System.Windows.Forms.ToolStripButton()
        Me._s4Page = New System.Windows.Forms.Panel()
        Me._s6Page = New System.Windows.Forms.Panel()
        Me._s6bStart = New System.Windows.Forms.Button()
        Me._s5Page = New System.Windows.Forms.Panel()
        Me._s5LeftLeft = New System.Windows.Forms.Panel()
        Me._s5lbFolders = New System.Windows.Forms.ListBox()
        Me._s0ProgressStatusbar = New PhotoGPS.ProgressStatusBar()
        _s1pMainWorkingArea = New System.Windows.Forms.Panel()
        _s1splitter = New System.Windows.Forms.Splitter()
        _s1tsTop = New System.Windows.Forms.ToolStrip()
        _s1tsBottom = New System.Windows.Forms.ToolStrip()
        _s2Splitter = New System.Windows.Forms.Splitter()
        _s2pMainWorkingArea = New System.Windows.Forms.Panel()
        _s2pRightWorkingArea = New System.Windows.Forms.Panel()
        _s2chFolder = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        _s2tsTopRight = New System.Windows.Forms.ToolStrip()
        _s3tss1 = New System.Windows.Forms.ToolStripSeparator()
        _s3tsl1 = New System.Windows.Forms.ToolStripLabel()
        _s3tss2 = New System.Windows.Forms.ToolStripSeparator()
        _s3tsl2 = New System.Windows.Forms.ToolStripLabel()
        _s3tss3 = New System.Windows.Forms.ToolStripSeparator()
        _s0tss1 = New System.Windows.Forms.ToolStripSeparator()
        _s0tss2 = New System.Windows.Forms.ToolStripSeparator()
        _s0tss3 = New System.Windows.Forms.ToolStripSeparator()
        _s0tss4 = New System.Windows.Forms.ToolStripSeparator()
        _s0tss5 = New System.Windows.Forms.ToolStripSeparator()
        _s4tsTop = New System.Windows.Forms.ToolStrip()
        _s6g1 = New System.Windows.Forms.GroupBox()
        _s6g2 = New System.Windows.Forms.GroupBox()
        _s6cDeleteDuplicate = New System.Windows.Forms.CheckBox()
        _s6l1 = New System.Windows.Forms.Label()
        _s5pCenter = New System.Windows.Forms.Panel()
        _s5tsPreviewControls = New System.Windows.Forms.ToolStrip()
        _s5l5 = New System.Windows.Forms.Label()
        _s5tsPreview = New System.Windows.Forms.ToolStrip()
        _s5s4 = New System.Windows.Forms.Splitter()
        _s5Right = New System.Windows.Forms.Panel()
        _s5l3 = New System.Windows.Forms.Label()
        _s5s3 = New System.Windows.Forms.Splitter()
        _s5RightRight = New System.Windows.Forms.Panel()
        _s5ch2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        _s5l4 = New System.Windows.Forms.Label()
        s2 = New System.Windows.Forms.Splitter()
        _s5Left = New System.Windows.Forms.Panel()
        _s5l2 = New System.Windows.Forms.Label()
        _s5s1 = New System.Windows.Forms.Splitter()
        _s5l1 = New System.Windows.Forms.Label()
        _s5tsLeftLeft = New System.Windows.Forms.ToolStrip()
        _s1pMainWorkingArea.SuspendLayout()
        _s1tsTop.SuspendLayout()
        _s1tsBottom.SuspendLayout()
        _s2pMainWorkingArea.SuspendLayout()
        _s2pRightWorkingArea.SuspendLayout()
        _s2tsTopRight.SuspendLayout()
        _s4tsTop.SuspendLayout()
        _s6g1.SuspendLayout()
        _s6g2.SuspendLayout()
        _s5pCenter.SuspendLayout()
        _s5tsPreviewControls.SuspendLayout()
        _s5tsPreview.SuspendLayout()
        _s5Right.SuspendLayout()
        _s5RightRight.SuspendLayout()
        _s5Left.SuspendLayout()
        _s5tsLeftLeft.SuspendLayout()
        Me._s1Page.SuspendLayout()
        Me._s2Page.SuspendLayout()
        Me._s3Page.SuspendLayout()
        Me._s3tsTop.SuspendLayout()
        Me._s0tsSteps.SuspendLayout()
        Me._s4Page.SuspendLayout()
        Me._s6Page.SuspendLayout()
        Me._s5Page.SuspendLayout()
        Me._s5LeftLeft.SuspendLayout()
        Me.SuspendLayout()
        '
        '_s1pMainWorkingArea
        '
        _s1pMainWorkingArea.Controls.Add(Me._s1lvSelectedFolders)
        _s1pMainWorkingArea.Controls.Add(_s1splitter)
        _s1pMainWorkingArea.Controls.Add(Me._s1ftvBrowser)
        _s1pMainWorkingArea.Dock = System.Windows.Forms.DockStyle.Fill
        _s1pMainWorkingArea.Location = New System.Drawing.Point(3, 28)
        _s1pMainWorkingArea.Name = "_s1pMainWorkingArea"
        _s1pMainWorkingArea.Size = New System.Drawing.Size(925, 305)
        _s1pMainWorkingArea.TabIndex = 4
        '
        '_s1lvSelectedFolders
        '
        Me._s1lvSelectedFolders.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me._s1chFolder, Me._s1chFullPath, Me._s1chRecursive})
        Me._s1lvSelectedFolders.Dock = System.Windows.Forms.DockStyle.Fill
        Me._s1lvSelectedFolders.FullRowSelect = True
        Me._s1lvSelectedFolders.HideSelection = False
        Me._s1lvSelectedFolders.Location = New System.Drawing.Point(262, 0)
        Me._s1lvSelectedFolders.Name = "_s1lvSelectedFolders"
        Me._s1lvSelectedFolders.Size = New System.Drawing.Size(663, 305)
        Me._s1lvSelectedFolders.TabIndex = 3
        Me._s1lvSelectedFolders.UseCompatibleStateImageBehavior = False
        Me._s1lvSelectedFolders.View = System.Windows.Forms.View.Details
        '
        '_s1chFolder
        '
        Me._s1chFolder.Text = "Folder"
        Me._s1chFolder.Width = 173
        '
        '_s1chFullPath
        '
        Me._s1chFullPath.Text = "Full Path"
        '
        '_s1chRecursive
        '
        Me._s1chRecursive.Text = "Recursive?"
        Me._s1chRecursive.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me._s1chRecursive.Width = 82
        '
        '_s1splitter
        '
        _s1splitter.Location = New System.Drawing.Point(259, 0)
        _s1splitter.Name = "_s1splitter"
        _s1splitter.Size = New System.Drawing.Size(3, 305)
        _s1splitter.TabIndex = 2
        _s1splitter.TabStop = False
        '
        '_s1ftvBrowser
        '
        Me._s1ftvBrowser.BackColor = System.Drawing.SystemColors.Window
        Me._s1ftvBrowser.Dock = System.Windows.Forms.DockStyle.Left
        Me._s1ftvBrowser.Folder = Nothing
        Me._s1ftvBrowser.FullRowSelect = True
        Me._s1ftvBrowser.HideSelection = False
        Me._s1ftvBrowser.Location = New System.Drawing.Point(0, 0)
        Me._s1ftvBrowser.Name = "_s1ftvBrowser"
        Me._s1ftvBrowser.Size = New System.Drawing.Size(259, 305)
        Me._s1ftvBrowser.TabIndex = 0
        '
        '_s1tsTop
        '
        _s1tsTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        _s1tsTop.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._s1tstbWorkingFolderTB, Me._s1tsbBrowse})
        _s1tsTop.Location = New System.Drawing.Point(3, 3)
        _s1tsTop.Name = "_s1tsTop"
        _s1tsTop.Size = New System.Drawing.Size(925, 25)
        _s1tsTop.TabIndex = 1
        _s1tsTop.Text = "ToolStrip1"
        '
        '_s1tstbWorkingFolderTB
        '
        Me._s1tstbWorkingFolderTB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me._s1tstbWorkingFolderTB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
        Me._s1tstbWorkingFolderTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._s1tstbWorkingFolderTB.Name = "_s1tstbWorkingFolderTB"
        Me._s1tstbWorkingFolderTB.Size = New System.Drawing.Size(300, 25)
        '
        '_s1tsbBrowse
        '
        Me._s1tsbBrowse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s1tsbBrowse.Image = CType(resources.GetObject("_s1tsbBrowse.Image"), System.Drawing.Image)
        Me._s1tsbBrowse.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s1tsbBrowse.Name = "_s1tsbBrowse"
        Me._s1tsbBrowse.Size = New System.Drawing.Size(58, 22)
        Me._s1tsbBrowse.Text = "Browse..."
        Me._s1tsbBrowse.ToolTipText = "Browse..."
        '
        '_s1tsBottom
        '
        _s1tsBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        _s1tsBottom.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        _s1tsBottom.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._s1tsbAddFolder, Me._s1tsbAddSubFolders, Me._s1tsbRecursive, Me._s1tsbRemoveFolders})
        _s1tsBottom.Location = New System.Drawing.Point(3, 333)
        _s1tsBottom.Name = "_s1tsBottom"
        _s1tsBottom.Size = New System.Drawing.Size(925, 25)
        _s1tsBottom.TabIndex = 5
        _s1tsBottom.Text = "ToolStrip2"
        '
        '_s1tsbAddFolder
        '
        Me._s1tsbAddFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s1tsbAddFolder.Enabled = False
        Me._s1tsbAddFolder.Image = CType(resources.GetObject("_s1tsbAddFolder.Image"), System.Drawing.Image)
        Me._s1tsbAddFolder.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s1tsbAddFolder.Name = "_s1tsbAddFolder"
        Me._s1tsbAddFolder.Size = New System.Drawing.Size(69, 22)
        Me._s1tsbAddFolder.Text = "Add Folder"
        '
        '_s1tsbAddSubFolders
        '
        Me._s1tsbAddSubFolders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s1tsbAddSubFolders.Enabled = False
        Me._s1tsbAddSubFolders.Image = CType(resources.GetObject("_s1tsbAddSubFolders.Image"), System.Drawing.Image)
        Me._s1tsbAddSubFolders.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s1tsbAddSubFolders.Name = "_s1tsbAddSubFolders"
        Me._s1tsbAddSubFolders.Size = New System.Drawing.Size(127, 22)
        Me._s1tsbAddSubFolders.Text = "Add Sub-Folders Only"
        '
        '_s1tsbRecursive
        '
        Me._s1tsbRecursive.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me._s1tsbRecursive.Checked = True
        Me._s1tsbRecursive.CheckOnClick = True
        Me._s1tsbRecursive.CheckState = System.Windows.Forms.CheckState.Checked
        Me._s1tsbRecursive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s1tsbRecursive.Image = CType(resources.GetObject("_s1tsbRecursive.Image"), System.Drawing.Image)
        Me._s1tsbRecursive.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s1tsbRecursive.Name = "_s1tsbRecursive"
        Me._s1tsbRecursive.Size = New System.Drawing.Size(66, 22)
        Me._s1tsbRecursive.Text = "Recursive?"
        '
        '_s1tsbRemoveFolders
        '
        Me._s1tsbRemoveFolders.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me._s1tsbRemoveFolders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s1tsbRemoveFolders.Image = CType(resources.GetObject("_s1tsbRemoveFolders.Image"), System.Drawing.Image)
        Me._s1tsbRemoveFolders.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s1tsbRemoveFolders.Name = "_s1tsbRemoveFolders"
        Me._s1tsbRemoveFolders.Size = New System.Drawing.Size(103, 22)
        Me._s1tsbRemoveFolders.Text = "Remove Folder(s)"
        '
        '_s2Splitter
        '
        _s2Splitter.Location = New System.Drawing.Point(267, 0)
        _s2Splitter.Name = "_s2Splitter"
        _s2Splitter.Size = New System.Drawing.Size(3, 355)
        _s2Splitter.TabIndex = 5
        _s2Splitter.TabStop = False
        '
        '_s2pMainWorkingArea
        '
        _s2pMainWorkingArea.Controls.Add(_s2pRightWorkingArea)
        _s2pMainWorkingArea.Controls.Add(_s2Splitter)
        _s2pMainWorkingArea.Controls.Add(Me._s2lvEnumeratedFolders)
        _s2pMainWorkingArea.Dock = System.Windows.Forms.DockStyle.Fill
        _s2pMainWorkingArea.Location = New System.Drawing.Point(3, 3)
        _s2pMainWorkingArea.Name = "_s2pMainWorkingArea"
        _s2pMainWorkingArea.Size = New System.Drawing.Size(925, 355)
        _s2pMainWorkingArea.TabIndex = 2
        '
        '_s2pRightWorkingArea
        '
        _s2pRightWorkingArea.Controls.Add(Me._s2lvPhotos)
        _s2pRightWorkingArea.Controls.Add(_s2tsTopRight)
        _s2pRightWorkingArea.Dock = System.Windows.Forms.DockStyle.Fill
        _s2pRightWorkingArea.Location = New System.Drawing.Point(270, 0)
        _s2pRightWorkingArea.Name = "_s2pRightWorkingArea"
        _s2pRightWorkingArea.Size = New System.Drawing.Size(655, 355)
        _s2pRightWorkingArea.TabIndex = 10
        '
        '_s2lvPhotos
        '
        Me._s2lvPhotos.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {_s2chFolder, Me._s2chFile, Me._s2chTaken, Me._s2chGPS, Me._s2chHash})
        Me._s2lvPhotos.Dock = System.Windows.Forms.DockStyle.Fill
        Me._s2lvPhotos.FullRowSelect = True
        Me._s2lvPhotos.Location = New System.Drawing.Point(0, 25)
        Me._s2lvPhotos.Name = "_s2lvPhotos"
        Me._s2lvPhotos.Size = New System.Drawing.Size(655, 330)
        Me._s2lvPhotos.TabIndex = 9
        Me._s2lvPhotos.UseCompatibleStateImageBehavior = False
        Me._s2lvPhotos.View = System.Windows.Forms.View.Details
        Me._s2lvPhotos.VirtualMode = True
        '
        '_s2chFolder
        '
        _s2chFolder.Text = "Folder"
        _s2chFolder.Width = 145
        '
        '_s2chFile
        '
        Me._s2chFile.Text = "Filename"
        Me._s2chFile.Width = 73
        '
        '_s2chTaken
        '
        Me._s2chTaken.Text = "Taken"
        Me._s2chTaken.Width = 133
        '
        '_s2chGPS
        '
        Me._s2chGPS.Text = "GPS"
        Me._s2chGPS.Width = 104
        '
        '_s2chHash
        '
        Me._s2chHash.Text = "Hash"
        Me._s2chHash.Width = 99
        '
        '_s2tsTopRight
        '
        _s2tsTopRight.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        _s2tsTopRight.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._s2tsbLoadPhotos})
        _s2tsTopRight.Location = New System.Drawing.Point(0, 0)
        _s2tsTopRight.Name = "_s2tsTopRight"
        _s2tsTopRight.Size = New System.Drawing.Size(655, 25)
        _s2tsTopRight.TabIndex = 8
        _s2tsTopRight.Text = "ToolStrip3"
        '
        '_s2tsbLoadPhotos
        '
        Me._s2tsbLoadPhotos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s2tsbLoadPhotos.Image = CType(resources.GetObject("_s2tsbLoadPhotos.Image"), System.Drawing.Image)
        Me._s2tsbLoadPhotos.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s2tsbLoadPhotos.Name = "_s2tsbLoadPhotos"
        Me._s2tsbLoadPhotos.Size = New System.Drawing.Size(77, 22)
        Me._s2tsbLoadPhotos.Text = "Load Photos"
        '
        '_s2lvEnumeratedFolders
        '
        Me._s2lvEnumeratedFolders.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me._s2chFolderName, Me._s2chFolderPath})
        Me._s2lvEnumeratedFolders.Dock = System.Windows.Forms.DockStyle.Left
        Me._s2lvEnumeratedFolders.FullRowSelect = True
        Me._s2lvEnumeratedFolders.HideSelection = False
        Me._s2lvEnumeratedFolders.Location = New System.Drawing.Point(0, 0)
        Me._s2lvEnumeratedFolders.Name = "_s2lvEnumeratedFolders"
        Me._s2lvEnumeratedFolders.Size = New System.Drawing.Size(267, 355)
        Me._s2lvEnumeratedFolders.TabIndex = 4
        Me._s2lvEnumeratedFolders.UseCompatibleStateImageBehavior = False
        Me._s2lvEnumeratedFolders.View = System.Windows.Forms.View.Details
        '
        '_s2chFolderName
        '
        Me._s2chFolderName.Text = "Folder"
        Me._s2chFolderName.Width = 173
        '
        '_s2chFolderPath
        '
        Me._s2chFolderPath.Text = "Full Path"
        '
        '_s3tss1
        '
        _s3tss1.Name = "_s3tss1"
        _s3tss1.Size = New System.Drawing.Size(6, 25)
        '
        '_s3tsl1
        '
        _s3tsl1.Name = "_s3tsl1"
        _s3tsl1.Size = New System.Drawing.Size(52, 22)
        _s3tsl1.Text = "CSV File:"
        '
        '_s3tss2
        '
        _s3tss2.Name = "_s3tss2"
        _s3tss2.Size = New System.Drawing.Size(6, 25)
        '
        '_s3tsl2
        '
        _s3tsl2.Name = "_s3tsl2"
        _s3tsl2.Size = New System.Drawing.Size(101, 22)
        _s3tsl2.Text = "Photo date range:"
        '
        '_s3tss3
        '
        _s3tss3.Name = "_s3tss3"
        _s3tss3.Size = New System.Drawing.Size(6, 25)
        '
        '_s0tss1
        '
        _s0tss1.Name = "_s0tss1"
        _s0tss1.Size = New System.Drawing.Size(6, 23)
        '
        '_s0tss2
        '
        _s0tss2.Name = "_s0tss2"
        _s0tss2.Size = New System.Drawing.Size(6, 23)
        '
        '_s0tss3
        '
        _s0tss3.Name = "_s0tss3"
        _s0tss3.Size = New System.Drawing.Size(6, 23)
        '
        '_s0tss4
        '
        _s0tss4.Name = "_s0tss4"
        _s0tss4.Size = New System.Drawing.Size(6, 23)
        '
        '_s0tss5
        '
        _s0tss5.Name = "_s0tss5"
        _s0tss5.Size = New System.Drawing.Size(6, 23)
        '
        '_s4tsTop
        '
        _s4tsTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        _s4tsTop.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._s4tsbRun, Me._s4tlTotals})
        _s4tsTop.Location = New System.Drawing.Point(0, 0)
        _s4tsTop.Name = "_s4tsTop"
        _s4tsTop.Size = New System.Drawing.Size(931, 25)
        _s4tsTop.TabIndex = 0
        _s4tsTop.Text = "ToolStrip1"
        '
        '_s4tsbRun
        '
        Me._s4tsbRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s4tsbRun.Image = CType(resources.GetObject("_s4tsbRun.Image"), System.Drawing.Image)
        Me._s4tsbRun.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s4tsbRun.Name = "_s4tsbRun"
        Me._s4tsbRun.Size = New System.Drawing.Size(152, 22)
        Me._s4tsbRun.Text = "Run Photo/Job Correlation"
        '
        '_s4tlTotals
        '
        Me._s4tlTotals.Name = "_s4tlTotals"
        Me._s4tlTotals.Size = New System.Drawing.Size(207, 22)
        Me._s4tlTotals.Text = "0 photos (with GPS) and 0 jobs loaded"
        '
        '_s6g1
        '
        _s6g1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        _s6g1.Controls.Add(_s6g2)
        _s6g1.Controls.Add(Me._s6cDeleteEmpty)
        _s6g1.Controls.Add(_s6cDeleteDuplicate)
        _s6g1.Controls.Add(Me._s6bBrowse)
        _s6g1.Controls.Add(Me._s6tbDestFolder)
        _s6g1.Controls.Add(_s6l1)
        _s6g1.Location = New System.Drawing.Point(6, 6)
        _s6g1.Name = "_s6g1"
        _s6g1.Size = New System.Drawing.Size(922, 320)
        _s6g1.TabIndex = 0
        _s6g1.TabStop = False
        _s6g1.Text = "Options"
        '
        '_s6g2
        '
        _s6g2.Controls.Add(Me._s6rSameFolder)
        _s6g2.Controls.Add(Me._s6rTakenDate)
        _s6g2.Controls.Add(Me._s6rJobID)
        _s6g2.Location = New System.Drawing.Point(6, 107)
        _s6g2.Name = "_s6g2"
        _s6g2.Size = New System.Drawing.Size(200, 95)
        _s6g2.TabIndex = 4
        _s6g2.TabStop = False
        _s6g2.Text = "Move to Folders Based on:"
        '
        '_s6rSameFolder
        '
        Me._s6rSameFolder.AutoSize = True
        Me._s6rSameFolder.Location = New System.Drawing.Point(6, 65)
        Me._s6rSameFolder.Name = "_s6rSameFolder"
        Me._s6rSameFolder.Size = New System.Drawing.Size(81, 17)
        Me._s6rSameFolder.TabIndex = 2
        Me._s6rSameFolder.Text = "Same folder"
        Me._s6rSameFolder.UseVisualStyleBackColor = True
        '
        '_s6rTakenDate
        '
        Me._s6rTakenDate.AutoSize = True
        Me._s6rTakenDate.Location = New System.Drawing.Point(6, 42)
        Me._s6rTakenDate.Name = "_s6rTakenDate"
        Me._s6rTakenDate.Size = New System.Drawing.Size(176, 17)
        Me._s6rTakenDate.TabIndex = 1
        Me._s6rTakenDate.Text = "Photo Taken Date (yyyy-mm-dd)"
        Me._s6rTakenDate.UseVisualStyleBackColor = True
        '
        '_s6rJobID
        '
        Me._s6rJobID.AutoSize = True
        Me._s6rJobID.Checked = True
        Me._s6rJobID.Location = New System.Drawing.Point(6, 19)
        Me._s6rJobID.Name = "_s6rJobID"
        Me._s6rJobID.Size = New System.Drawing.Size(147, 17)
        Me._s6rJobID.TabIndex = 0
        Me._s6rJobID.TabStop = True
        Me._s6rJobID.Text = "Job ID (Dispatch Number)"
        Me._s6rJobID.UseVisualStyleBackColor = True
        '
        '_s6cDeleteEmpty
        '
        Me._s6cDeleteEmpty.AutoSize = True
        Me._s6cDeleteEmpty.Checked = True
        Me._s6cDeleteEmpty.CheckState = System.Windows.Forms.CheckState.Checked
        Me._s6cDeleteEmpty.Location = New System.Drawing.Point(6, 84)
        Me._s6cDeleteEmpty.Name = "_s6cDeleteEmpty"
        Me._s6cDeleteEmpty.Size = New System.Drawing.Size(135, 17)
        Me._s6cDeleteEmpty.TabIndex = 1
        Me._s6cDeleteEmpty.Text = "Remove Empty Folders"
        Me._s6cDeleteEmpty.UseVisualStyleBackColor = True
        '
        '_s6cDeleteDuplicate
        '
        _s6cDeleteDuplicate.AutoSize = True
        _s6cDeleteDuplicate.Checked = True
        _s6cDeleteDuplicate.CheckState = System.Windows.Forms.CheckState.Checked
        _s6cDeleteDuplicate.Enabled = False
        _s6cDeleteDuplicate.Location = New System.Drawing.Point(6, 61)
        _s6cDeleteDuplicate.Name = "_s6cDeleteDuplicate"
        _s6cDeleteDuplicate.Size = New System.Drawing.Size(150, 17)
        _s6cDeleteDuplicate.TabIndex = 0
        _s6cDeleteDuplicate.Text = "Remove Duplicate Photos"
        _s6cDeleteDuplicate.UseVisualStyleBackColor = True
        '
        '_s6bBrowse
        '
        Me._s6bBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._s6bBrowse.Location = New System.Drawing.Point(887, 35)
        Me._s6bBrowse.Name = "_s6bBrowse"
        Me._s6bBrowse.Size = New System.Drawing.Size(31, 20)
        Me._s6bBrowse.TabIndex = 3
        Me._s6bBrowse.Text = "..."
        Me._s6bBrowse.UseVisualStyleBackColor = True
        '
        '_s6tbDestFolder
        '
        Me._s6tbDestFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._s6tbDestFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me._s6tbDestFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
        Me._s6tbDestFolder.Location = New System.Drawing.Point(6, 35)
        Me._s6tbDestFolder.Name = "_s6tbDestFolder"
        Me._s6tbDestFolder.Size = New System.Drawing.Size(875, 20)
        Me._s6tbDestFolder.TabIndex = 2
        '
        '_s6l1
        '
        _s6l1.AutoSize = True
        _s6l1.Location = New System.Drawing.Point(6, 19)
        _s6l1.Name = "_s6l1"
        _s6l1.Size = New System.Drawing.Size(95, 13)
        _s6l1.TabIndex = 5
        _s6l1.Text = "Destination Folder:"
        '
        '_s5pCenter
        '
        _s5pCenter.Controls.Add(Me._s5Preview)
        _s5pCenter.Controls.Add(_s5tsPreviewControls)
        _s5pCenter.Controls.Add(_s5l5)
        _s5pCenter.Controls.Add(_s5tsPreview)
        _s5pCenter.Dock = System.Windows.Forms.DockStyle.Fill
        _s5pCenter.Location = New System.Drawing.Point(276, 0)
        _s5pCenter.Name = "_s5pCenter"
        _s5pCenter.Size = New System.Drawing.Size(324, 361)
        _s5pCenter.TabIndex = 8
        '
        '_s5Preview
        '
        Me._s5Preview.Dock = System.Windows.Forms.DockStyle.Fill
        Me._s5Preview.File = Nothing
        Me._s5Preview.Location = New System.Drawing.Point(0, 13)
        Me._s5Preview.Name = "_s5Preview"
        Me._s5Preview.Size = New System.Drawing.Size(324, 298)
        Me._s5Preview.TabIndex = 3
        Me._s5Preview.Text = "_s5FilePreviewControl1"
        '
        '_s5tsPreviewControls
        '
        _s5tsPreviewControls.Dock = System.Windows.Forms.DockStyle.Bottom
        _s5tsPreviewControls.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        _s5tsPreviewControls.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._s5tsbPrevious, Me._s5tsbNext})
        _s5tsPreviewControls.Location = New System.Drawing.Point(0, 311)
        _s5tsPreviewControls.Name = "_s5tsPreviewControls"
        _s5tsPreviewControls.Size = New System.Drawing.Size(324, 25)
        _s5tsPreviewControls.TabIndex = 4
        _s5tsPreviewControls.Text = "ToolStrip1"
        '
        '_s5tsbPrevious
        '
        Me._s5tsbPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s5tsbPrevious.Enabled = False
        Me._s5tsbPrevious.Image = CType(resources.GetObject("_s5tsbPrevious.Image"), System.Drawing.Image)
        Me._s5tsbPrevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s5tsbPrevious.Name = "_s5tsbPrevious"
        Me._s5tsbPrevious.Size = New System.Drawing.Size(67, 22)
        Me._s5tsbPrevious.Text = "< Previous"
        '
        '_s5tsbNext
        '
        Me._s5tsbNext.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me._s5tsbNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s5tsbNext.Enabled = False
        Me._s5tsbNext.Image = CType(resources.GetObject("_s5tsbNext.Image"), System.Drawing.Image)
        Me._s5tsbNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s5tsbNext.Name = "_s5tsbNext"
        Me._s5tsbNext.Size = New System.Drawing.Size(46, 22)
        Me._s5tsbNext.Text = "Next >"
        '
        '_s5l5
        '
        _s5l5.AutoSize = True
        _s5l5.Dock = System.Windows.Forms.DockStyle.Top
        _s5l5.Location = New System.Drawing.Point(0, 0)
        _s5l5.Name = "_s5l5"
        _s5l5.Size = New System.Drawing.Size(48, 13)
        _s5l5.TabIndex = 2
        _s5l5.Text = "Preview:"
        '
        '_s5tsPreview
        '
        _s5tsPreview.Dock = System.Windows.Forms.DockStyle.Bottom
        _s5tsPreview.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        _s5tsPreview.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._s5tsbAssign, Me._s5tsbUnassign})
        _s5tsPreview.Location = New System.Drawing.Point(0, 336)
        _s5tsPreview.Name = "_s5tsPreview"
        _s5tsPreview.Size = New System.Drawing.Size(324, 25)
        _s5tsPreview.TabIndex = 0
        _s5tsPreview.Text = "ToolStrip1"
        '
        '_s5tsbAssign
        '
        Me._s5tsbAssign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s5tsbAssign.Image = CType(resources.GetObject("_s5tsbAssign.Image"), System.Drawing.Image)
        Me._s5tsbAssign.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s5tsbAssign.Name = "_s5tsbAssign"
        Me._s5tsbAssign.Size = New System.Drawing.Size(103, 22)
        Me._s5tsbAssign.Text = "Assign to # job(s)"
        '
        '_s5tsbUnassign
        '
        Me._s5tsbUnassign.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me._s5tsbUnassign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s5tsbUnassign.Image = CType(resources.GetObject("_s5tsbUnassign.Image"), System.Drawing.Image)
        Me._s5tsbUnassign.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s5tsbUnassign.Name = "_s5tsbUnassign"
        Me._s5tsbUnassign.Size = New System.Drawing.Size(117, 22)
        Me._s5tsbUnassign.Text = "Unassign # Photo(s)"
        '
        '_s5s4
        '
        _s5s4.Dock = System.Windows.Forms.DockStyle.Right
        _s5s4.Location = New System.Drawing.Point(600, 0)
        _s5s4.Name = "_s5s4"
        _s5s4.Size = New System.Drawing.Size(3, 361)
        _s5s4.TabIndex = 7
        _s5s4.TabStop = False
        '
        '_s5Right
        '
        _s5Right.Controls.Add(Me._s5lbJobFiles)
        _s5Right.Controls.Add(_s5l3)
        _s5Right.Dock = System.Windows.Forms.DockStyle.Right
        _s5Right.Location = New System.Drawing.Point(603, 0)
        _s5Right.Name = "_s5Right"
        _s5Right.Size = New System.Drawing.Size(145, 361)
        _s5Right.TabIndex = 6
        '
        '_s5lbJobFiles
        '
        Me._s5lbJobFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me._s5lbJobFiles.FormattingEnabled = True
        Me._s5lbJobFiles.IntegralHeight = False
        Me._s5lbJobFiles.Location = New System.Drawing.Point(0, 13)
        Me._s5lbJobFiles.Name = "_s5lbJobFiles"
        Me._s5lbJobFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me._s5lbJobFiles.Size = New System.Drawing.Size(145, 348)
        Me._s5lbJobFiles.TabIndex = 2
        '
        '_s5l3
        '
        _s5l3.AutoSize = True
        _s5l3.Dock = System.Windows.Forms.DockStyle.Top
        _s5l3.Location = New System.Drawing.Point(0, 0)
        _s5l3.Name = "_s5l3"
        _s5l3.Size = New System.Drawing.Size(82, 13)
        _s5l3.TabIndex = 1
        _s5l3.Text = "Correlated Files:"
        '
        '_s5s3
        '
        _s5s3.Dock = System.Windows.Forms.DockStyle.Right
        _s5s3.Location = New System.Drawing.Point(748, 0)
        _s5s3.Name = "_s5s3"
        _s5s3.Size = New System.Drawing.Size(3, 361)
        _s5s3.TabIndex = 5
        _s5s3.TabStop = False
        '
        '_s5RightRight
        '
        _s5RightRight.Controls.Add(Me._s5lvJobs)
        _s5RightRight.Controls.Add(Me._s5tbJobSearch)
        _s5RightRight.Controls.Add(_s5l4)
        _s5RightRight.Dock = System.Windows.Forms.DockStyle.Right
        _s5RightRight.Location = New System.Drawing.Point(751, 0)
        _s5RightRight.Name = "_s5RightRight"
        _s5RightRight.Size = New System.Drawing.Size(180, 361)
        _s5RightRight.TabIndex = 4
        '
        '_s5lvJobs
        '
        Me._s5lvJobs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me._s5ch1, _s5ch2})
        Me._s5lvJobs.Dock = System.Windows.Forms.DockStyle.Fill
        Me._s5lvJobs.FullRowSelect = True
        Me._s5lvJobs.HideSelection = False
        Me._s5lvJobs.Location = New System.Drawing.Point(0, 33)
        Me._s5lvJobs.Name = "_s5lvJobs"
        Me._s5lvJobs.Size = New System.Drawing.Size(180, 328)
        Me._s5lvJobs.TabIndex = 3
        Me._s5lvJobs.UseCompatibleStateImageBehavior = False
        Me._s5lvJobs.View = System.Windows.Forms.View.Details
        Me._s5lvJobs.VirtualMode = True
        '
        '_s5ch1
        '
        Me._s5ch1.Text = "Dispatch #"
        Me._s5ch1.Width = 82
        '
        '_s5ch2
        '
        _s5ch2.Text = "Photo Count"
        _s5ch2.Width = 77
        '
        '_s5tbJobSearch
        '
        Me._s5tbJobSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me._s5tbJobSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me._s5tbJobSearch.Location = New System.Drawing.Point(0, 13)
        Me._s5tbJobSearch.Name = "_s5tbJobSearch"
        Me._s5tbJobSearch.Size = New System.Drawing.Size(180, 20)
        Me._s5tbJobSearch.TabIndex = 4
        '
        '_s5l4
        '
        _s5l4.AutoSize = True
        _s5l4.Dock = System.Windows.Forms.DockStyle.Top
        _s5l4.Location = New System.Drawing.Point(0, 0)
        _s5l4.Name = "_s5l4"
        _s5l4.Size = New System.Drawing.Size(32, 13)
        _s5l4.TabIndex = 2
        _s5l4.Text = "Jobs:"
        '
        's2
        '
        s2.Location = New System.Drawing.Point(273, 0)
        s2.Name = "s2"
        s2.Size = New System.Drawing.Size(3, 361)
        s2.TabIndex = 3
        s2.TabStop = False
        '
        '_s5Left
        '
        _s5Left.Controls.Add(Me._s5lbFolderFiles)
        _s5Left.Controls.Add(_s5l2)
        _s5Left.Dock = System.Windows.Forms.DockStyle.Left
        _s5Left.Location = New System.Drawing.Point(150, 0)
        _s5Left.Name = "_s5Left"
        _s5Left.Size = New System.Drawing.Size(123, 361)
        _s5Left.TabIndex = 2
        '
        '_s5lbFolderFiles
        '
        Me._s5lbFolderFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me._s5lbFolderFiles.FormattingEnabled = True
        Me._s5lbFolderFiles.IntegralHeight = False
        Me._s5lbFolderFiles.Location = New System.Drawing.Point(0, 13)
        Me._s5lbFolderFiles.Name = "_s5lbFolderFiles"
        Me._s5lbFolderFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me._s5lbFolderFiles.Size = New System.Drawing.Size(123, 348)
        Me._s5lbFolderFiles.TabIndex = 2
        '
        '_s5l2
        '
        _s5l2.AutoSize = True
        _s5l2.Dock = System.Windows.Forms.DockStyle.Top
        _s5l2.Location = New System.Drawing.Point(0, 0)
        _s5l2.Name = "_s5l2"
        _s5l2.Size = New System.Drawing.Size(95, 13)
        _s5l2.TabIndex = 1
        _s5l2.Text = "Uncorrelated Files:"
        '
        '_s5s1
        '
        _s5s1.Location = New System.Drawing.Point(147, 0)
        _s5s1.Name = "_s5s1"
        _s5s1.Size = New System.Drawing.Size(3, 361)
        _s5s1.TabIndex = 1
        _s5s1.TabStop = False
        '
        '_s5l1
        '
        _s5l1.AutoSize = True
        _s5l1.Dock = System.Windows.Forms.DockStyle.Top
        _s5l1.Location = New System.Drawing.Point(0, 0)
        _s5l1.Name = "_s5l1"
        _s5l1.Size = New System.Drawing.Size(108, 13)
        _s5l1.TabIndex = 0
        _s5l1.Text = "Uncorrelated Folders:"
        '
        '_s5tsLeftLeft
        '
        _s5tsLeftLeft.Dock = System.Windows.Forms.DockStyle.Bottom
        _s5tsLeftLeft.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        _s5tsLeftLeft.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._s5lSearchSF})
        _s5tsLeftLeft.Location = New System.Drawing.Point(0, 336)
        _s5tsLeftLeft.Name = "_s5tsLeftLeft"
        _s5tsLeftLeft.Size = New System.Drawing.Size(147, 25)
        _s5tsLeftLeft.TabIndex = 2
        _s5tsLeftLeft.Text = "ToolStrip2"
        '
        '_s5lSearchSF
        '
        Me._s5lSearchSF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s5lSearchSF.Image = CType(resources.GetObject("_s5lSearchSF.Image"), System.Drawing.Image)
        Me._s5lSearchSF.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s5lSearchSF.IsLink = True
        Me._s5lSearchSF.Name = "_s5lSearchSF"
        Me._s5lSearchSF.Size = New System.Drawing.Size(98, 22)
        Me._s5lSearchSF.Text = "Search Salesforce"
        '
        '_s1Page
        '
        Me._s1Page.Controls.Add(_s1pMainWorkingArea)
        Me._s1Page.Controls.Add(_s1tsTop)
        Me._s1Page.Controls.Add(_s1tsBottom)
        Me._s1Page.Dock = System.Windows.Forms.DockStyle.Fill
        Me._s1Page.Location = New System.Drawing.Point(0, 23)
        Me._s1Page.Name = "_s1Page"
        Me._s1Page.Padding = New System.Windows.Forms.Padding(3)
        Me._s1Page.Size = New System.Drawing.Size(931, 361)
        Me._s1Page.TabIndex = 0
        Me._s1Page.Text = "Step 1: Select Working Folder(s)"
        '
        '_s2Page
        '
        Me._s2Page.Controls.Add(_s2pMainWorkingArea)
        Me._s2Page.Dock = System.Windows.Forms.DockStyle.Fill
        Me._s2Page.Location = New System.Drawing.Point(0, 23)
        Me._s2Page.Name = "_s2Page"
        Me._s2Page.Padding = New System.Windows.Forms.Padding(3)
        Me._s2Page.Size = New System.Drawing.Size(931, 361)
        Me._s2Page.TabIndex = 1
        Me._s2Page.Text = "Step 2: Add Photos"
        Me._s2Page.Visible = False
        '
        '_s3Page
        '
        Me._s3Page.Controls.Add(Me._s3lvJobs)
        Me._s3Page.Controls.Add(Me._s3tsTop)
        Me._s3Page.Dock = System.Windows.Forms.DockStyle.Fill
        Me._s3Page.Location = New System.Drawing.Point(0, 23)
        Me._s3Page.Name = "_s3Page"
        Me._s3Page.Size = New System.Drawing.Size(931, 361)
        Me._s3Page.TabIndex = 2
        Me._s3Page.Text = "Step 3: Add Jobs"
        Me._s3Page.Visible = False
        '
        '_s3lvJobs
        '
        Me._s3lvJobs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me._s3chDispatchNumber, Me._s3chStartDate, Me._s3chEndDate, Me._s3chGPS, Me._s3chSFID})
        Me._s3lvJobs.Dock = System.Windows.Forms.DockStyle.Fill
        Me._s3lvJobs.FullRowSelect = True
        Me._s3lvJobs.Location = New System.Drawing.Point(0, 25)
        Me._s3lvJobs.Name = "_s3lvJobs"
        Me._s3lvJobs.Size = New System.Drawing.Size(931, 336)
        Me._s3lvJobs.TabIndex = 1
        Me._s3lvJobs.UseCompatibleStateImageBehavior = False
        Me._s3lvJobs.View = System.Windows.Forms.View.Details
        Me._s3lvJobs.VirtualMode = True
        '
        '_s3chDispatchNumber
        '
        Me._s3chDispatchNumber.Text = "Dispatch Number"
        Me._s3chDispatchNumber.Width = 112
        '
        '_s3chStartDate
        '
        Me._s3chStartDate.Text = "Start Date"
        Me._s3chStartDate.Width = 97
        '
        '_s3chEndDate
        '
        Me._s3chEndDate.Text = "End Date"
        Me._s3chEndDate.Width = 83
        '
        '_s3chGPS
        '
        Me._s3chGPS.Text = "GPS"
        Me._s3chGPS.Width = 111
        '
        '_s3chSFID
        '
        Me._s3chSFID.Text = "Salesforce ID"
        Me._s3chSFID.Width = 82
        '
        '_s3tsTop
        '
        Me._s3tsTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me._s3tsTop.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._s3tsbLoadjobs, _s3tss1, _s3tsl1, Me._s3tstbCSVFile, _s3tss2, _s3tsl2, Me._s3tstbStart, _s3tss3, Me._s3tstbEnd})
        Me._s3tsTop.Location = New System.Drawing.Point(0, 0)
        Me._s3tsTop.Name = "_s3tsTop"
        Me._s3tsTop.Size = New System.Drawing.Size(931, 25)
        Me._s3tsTop.TabIndex = 0
        Me._s3tsTop.Text = "ToolStrip1"
        '
        '_s3tsbLoadjobs
        '
        Me._s3tsbLoadjobs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s3tsbLoadjobs.Image = CType(resources.GetObject("_s3tsbLoadjobs.Image"), System.Drawing.Image)
        Me._s3tsbLoadjobs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s3tsbLoadjobs.Name = "_s3tsbLoadjobs"
        Me._s3tsbLoadjobs.Size = New System.Drawing.Size(116, 22)
        Me._s3tsbLoadjobs.Text = "Load Jobs from CSV"
        '
        '_s3tstbCSVFile
        '
        Me._s3tstbCSVFile.Name = "_s3tstbCSVFile"
        Me._s3tstbCSVFile.ReadOnly = True
        Me._s3tstbCSVFile.Size = New System.Drawing.Size(100, 25)
        '
        '_s3tstbStart
        '
        Me._s3tstbStart.Name = "_s3tstbStart"
        Me._s3tstbStart.ReadOnly = True
        Me._s3tstbStart.Size = New System.Drawing.Size(100, 25)
        '
        '_s3tstbEnd
        '
        Me._s3tstbEnd.Name = "_s3tstbEnd"
        Me._s3tstbEnd.ReadOnly = True
        Me._s3tstbEnd.Size = New System.Drawing.Size(100, 25)
        '
        '_s0tsSteps
        '
        Me._s0tsSteps.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me._s0tsSteps.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._s0tsbStep1, _s0tss1, Me._s0tsbStep2, _s0tss2, Me._s0tsbStep3, _s0tss3, Me._s0tsbStep4, _s0tss4, Me._s0tsbStep5, _s0tss5, Me._s0tsbStep6})
        Me._s0tsSteps.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me._s0tsSteps.Location = New System.Drawing.Point(0, 0)
        Me._s0tsSteps.Name = "_s0tsSteps"
        Me._s0tsSteps.Size = New System.Drawing.Size(931, 23)
        Me._s0tsSteps.TabIndex = 5
        Me._s0tsSteps.Text = "ToolStrip2"
        '
        '_s0tsbStep1
        '
        Me._s0tsbStep1.Checked = True
        Me._s0tsbStep1.CheckState = System.Windows.Forms.CheckState.Checked
        Me._s0tsbStep1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s0tsbStep1.Image = CType(resources.GetObject("_s0tsbStep1.Image"), System.Drawing.Image)
        Me._s0tsbStep1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s0tsbStep1.Name = "_s0tsbStep1"
        Me._s0tsbStep1.Size = New System.Drawing.Size(122, 19)
        Me._s0tsbStep1.Text = "Step 1: Photo Folders"
        '
        '_s0tsbStep2
        '
        Me._s0tsbStep2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s0tsbStep2.Image = CType(resources.GetObject("_s0tsbStep2.Image"), System.Drawing.Image)
        Me._s0tsbStep2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s0tsbStep2.Name = "_s0tsbStep2"
        Me._s0tsbStep2.Size = New System.Drawing.Size(115, 19)
        Me._s0tsbStep2.Text = "Step 2: Load Photos"
        '
        '_s0tsbStep3
        '
        Me._s0tsbStep3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s0tsbStep3.Image = CType(resources.GetObject("_s0tsbStep3.Image"), System.Drawing.Image)
        Me._s0tsbStep3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s0tsbStep3.Name = "_s0tsbStep3"
        Me._s0tsbStep3.Size = New System.Drawing.Size(101, 19)
        Me._s0tsbStep3.Text = "Step 3: Load Jobs"
        '
        '_s0tsbStep4
        '
        Me._s0tsbStep4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s0tsbStep4.Image = CType(resources.GetObject("_s0tsbStep4.Image"), System.Drawing.Image)
        Me._s0tsbStep4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s0tsbStep4.Name = "_s0tsbStep4"
        Me._s0tsbStep4.Size = New System.Drawing.Size(177, 19)
        Me._s0tsbStep4.Text = "Step 4: Correlate Photos to Jobs"
        '
        '_s0tsbStep5
        '
        Me._s0tsbStep5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s0tsbStep5.Image = CType(resources.GetObject("_s0tsbStep5.Image"), System.Drawing.Image)
        Me._s0tsbStep5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s0tsbStep5.Name = "_s0tsbStep5"
        Me._s0tsbStep5.Size = New System.Drawing.Size(157, 19)
        Me._s0tsbStep5.Text = "Step 5: Uncorrelated Photos"
        '
        '_s0tsbStep6
        '
        Me._s0tsbStep6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me._s0tsbStep6.Image = CType(resources.GetObject("_s0tsbStep6.Image"), System.Drawing.Image)
        Me._s0tsbStep6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._s0tsbStep6.Name = "_s0tsbStep6"
        Me._s0tsbStep6.Size = New System.Drawing.Size(119, 19)
        Me._s0tsbStep6.Text = "Step 6: Move Photos"
        '
        '_s4Page
        '
        Me._s4Page.Controls.Add(_s4tsTop)
        Me._s4Page.Dock = System.Windows.Forms.DockStyle.Fill
        Me._s4Page.Location = New System.Drawing.Point(0, 23)
        Me._s4Page.Name = "_s4Page"
        Me._s4Page.Size = New System.Drawing.Size(931, 361)
        Me._s4Page.TabIndex = 6
        Me._s4Page.Visible = False
        '
        '_s6Page
        '
        Me._s6Page.Controls.Add(Me._s6bStart)
        Me._s6Page.Controls.Add(_s6g1)
        Me._s6Page.Dock = System.Windows.Forms.DockStyle.Fill
        Me._s6Page.Location = New System.Drawing.Point(0, 23)
        Me._s6Page.Name = "_s6Page"
        Me._s6Page.Size = New System.Drawing.Size(931, 361)
        Me._s6Page.TabIndex = 0
        '
        '_s6bStart
        '
        Me._s6bStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._s6bStart.Enabled = False
        Me._s6bStart.Location = New System.Drawing.Point(844, 333)
        Me._s6bStart.Name = "_s6bStart"
        Me._s6bStart.Size = New System.Drawing.Size(80, 23)
        Me._s6bStart.TabIndex = 1
        Me._s6bStart.Text = "Start"
        Me._s6bStart.UseVisualStyleBackColor = True
        '
        '_s5Page
        '
        Me._s5Page.Controls.Add(_s5pCenter)
        Me._s5Page.Controls.Add(_s5s4)
        Me._s5Page.Controls.Add(_s5Right)
        Me._s5Page.Controls.Add(_s5s3)
        Me._s5Page.Controls.Add(_s5RightRight)
        Me._s5Page.Controls.Add(s2)
        Me._s5Page.Controls.Add(_s5Left)
        Me._s5Page.Controls.Add(_s5s1)
        Me._s5Page.Controls.Add(Me._s5LeftLeft)
        Me._s5Page.Dock = System.Windows.Forms.DockStyle.Fill
        Me._s5Page.Location = New System.Drawing.Point(0, 23)
        Me._s5Page.Name = "_s5Page"
        Me._s5Page.Size = New System.Drawing.Size(931, 361)
        Me._s5Page.TabIndex = 0
        Me._s5Page.Visible = False
        '
        '_s5LeftLeft
        '
        Me._s5LeftLeft.Controls.Add(Me._s5lbFolders)
        Me._s5LeftLeft.Controls.Add(_s5l1)
        Me._s5LeftLeft.Controls.Add(_s5tsLeftLeft)
        Me._s5LeftLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me._s5LeftLeft.Location = New System.Drawing.Point(0, 0)
        Me._s5LeftLeft.Name = "_s5LeftLeft"
        Me._s5LeftLeft.Size = New System.Drawing.Size(147, 361)
        Me._s5LeftLeft.TabIndex = 0
        '
        '_s5lbFolders
        '
        Me._s5lbFolders.Dock = System.Windows.Forms.DockStyle.Fill
        Me._s5lbFolders.FormattingEnabled = True
        Me._s5lbFolders.IntegralHeight = False
        Me._s5lbFolders.Location = New System.Drawing.Point(0, 13)
        Me._s5lbFolders.Name = "_s5lbFolders"
        Me._s5lbFolders.Size = New System.Drawing.Size(147, 323)
        Me._s5lbFolders.TabIndex = 1
        '
        '_s0ProgressStatusbar
        '
        Me._s0ProgressStatusbar.Location = New System.Drawing.Point(0, 384)
        Me._s0ProgressStatusbar.Name = "_s0ProgressStatusbar"
        Me._s0ProgressStatusbar.Size = New System.Drawing.Size(931, 22)
        Me._s0ProgressStatusbar.TabIndex = 4
        Me._s0ProgressStatusbar.Text = "ProgressStatusBar1"
        '
        'fMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(931, 406)
        Me.Controls.Add(Me._s5Page)
        Me.Controls.Add(Me._s6Page)
        Me.Controls.Add(Me._s2Page)
        Me.Controls.Add(Me._s1Page)
        Me.Controls.Add(Me._s4Page)
        Me.Controls.Add(Me._s3Page)
        Me.Controls.Add(Me._s0tsSteps)
        Me.Controls.Add(Me._s0ProgressStatusbar)
        Me.Name = "fMain"
        Me.Text = "Form1"
        _s1pMainWorkingArea.ResumeLayout(False)
        _s1tsTop.ResumeLayout(False)
        _s1tsTop.PerformLayout()
        _s1tsBottom.ResumeLayout(False)
        _s1tsBottom.PerformLayout()
        _s2pMainWorkingArea.ResumeLayout(False)
        _s2pRightWorkingArea.ResumeLayout(False)
        _s2pRightWorkingArea.PerformLayout()
        _s2tsTopRight.ResumeLayout(False)
        _s2tsTopRight.PerformLayout()
        _s4tsTop.ResumeLayout(False)
        _s4tsTop.PerformLayout()
        _s6g1.ResumeLayout(False)
        _s6g1.PerformLayout()
        _s6g2.ResumeLayout(False)
        _s6g2.PerformLayout()
        _s5pCenter.ResumeLayout(False)
        _s5pCenter.PerformLayout()
        _s5tsPreviewControls.ResumeLayout(False)
        _s5tsPreviewControls.PerformLayout()
        _s5tsPreview.ResumeLayout(False)
        _s5tsPreview.PerformLayout()
        _s5Right.ResumeLayout(False)
        _s5Right.PerformLayout()
        _s5RightRight.ResumeLayout(False)
        _s5RightRight.PerformLayout()
        _s5Left.ResumeLayout(False)
        _s5Left.PerformLayout()
        _s5tsLeftLeft.ResumeLayout(False)
        _s5tsLeftLeft.PerformLayout()
        Me._s1Page.ResumeLayout(False)
        Me._s1Page.PerformLayout()
        Me._s2Page.ResumeLayout(False)
        Me._s3Page.ResumeLayout(False)
        Me._s3Page.PerformLayout()
        Me._s3tsTop.ResumeLayout(False)
        Me._s3tsTop.PerformLayout()
        Me._s0tsSteps.ResumeLayout(False)
        Me._s0tsSteps.PerformLayout()
        Me._s4Page.ResumeLayout(False)
        Me._s4Page.PerformLayout()
        Me._s6Page.ResumeLayout(False)
        Me._s5Page.ResumeLayout(False)
        Me._s5LeftLeft.ResumeLayout(False)
        Me._s5LeftLeft.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents _s4Page As Panel
    Friend WithEvents _s5Page As Panel
    Friend WithEvents _s6Page As Panel
    Friend WithEvents _s4tsbRun As ToolStripButton
    Friend WithEvents _s4tlTotals As ToolStripLabel
    Friend WithEvents _s6bStart As Button
    Friend WithEvents _s6rSameFolder As RadioButton
    Friend WithEvents _s6rTakenDate As RadioButton
    Friend WithEvents _s6rJobID As RadioButton
    Friend WithEvents _s6bBrowse As Button
    Friend WithEvents _s6tbDestFolder As TextBox
    Friend WithEvents _s6cDeleteEmpty As CheckBox
    Friend WithEvents _s2chFile As ColumnHeader
    Friend WithEvents _s5LeftLeft As Panel
    Friend WithEvents _s5lbJobFiles As ListBox
    Friend WithEvents _s5lvJobs As ListView
    Friend WithEvents _s5lbFolderFiles As ListBox
    Friend WithEvents _s5lbFolders As ListBox
    Friend WithEvents _s5tsbAssign As ToolStripButton
    Friend WithEvents _s5tsbUnassign As ToolStripButton
    Friend WithEvents _s5tbJobSearch As TextBox
    Friend WithEvents _s5Preview As FilePreviewControl
    Friend WithEvents _s5tsbPrevious As ToolStripButton
    Friend WithEvents _s5tsbNext As ToolStripButton
    Friend WithEvents _s5lSearchSF As ToolStripLabel
    Friend WithEvents _s5ch1 As ColumnHeader
End Class
