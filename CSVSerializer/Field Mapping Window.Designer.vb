<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FFieldMapping
    Inherits System.Windows.Forms.Form

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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Bcancel = New System.Windows.Forms.Button()
        Me.Bok = New System.Windows.Forms.Button()
        Me.DGcsv = New System.Windows.Forms.DataGridView()
        Me.CBXFirstRowColumnNames = New System.Windows.Forms.CheckBox()
        CType(Me.DGcsv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Bcancel
        '
        Me.Bcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Bcancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Bcancel.Location = New System.Drawing.Point(633, 385)
        Me.Bcancel.Name = "Bcancel"
        Me.Bcancel.Size = New System.Drawing.Size(75, 23)
        Me.Bcancel.TabIndex = 0
        Me.Bcancel.Text = "Cancel"
        Me.Bcancel.UseVisualStyleBackColor = True
        '
        'Bok
        '
        Me.Bok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Bok.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Bok.Location = New System.Drawing.Point(552, 385)
        Me.Bok.Name = "Bok"
        Me.Bok.Size = New System.Drawing.Size(75, 23)
        Me.Bok.TabIndex = 1
        Me.Bok.Text = "OK"
        Me.Bok.UseVisualStyleBackColor = True
        '
        'DGcsv
        '
        Me.DGcsv.AllowUserToAddRows = False
        Me.DGcsv.AllowUserToDeleteRows = False
        Me.DGcsv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGcsv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGcsv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.DGcsv.Location = New System.Drawing.Point(12, 36)
        Me.DGcsv.MultiSelect = False
        Me.DGcsv.Name = "DGcsv"
        Me.DGcsv.ShowCellErrors = False
        Me.DGcsv.ShowCellToolTips = False
        Me.DGcsv.ShowEditingIcon = False
        Me.DGcsv.ShowRowErrors = False
        Me.DGcsv.Size = New System.Drawing.Size(696, 343)
        Me.DGcsv.TabIndex = 2
        '
        'CBXFirstRowColumnNames
        '
        Me.CBXFirstRowColumnNames.AutoSize = True
        Me.CBXFirstRowColumnNames.Checked = True
        Me.CBXFirstRowColumnNames.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBXFirstRowColumnNames.Location = New System.Drawing.Point(13, 13)
        Me.CBXFirstRowColumnNames.Name = "CBXFirstRowColumnNames"
        Me.CBXFirstRowColumnNames.Size = New System.Drawing.Size(192, 17)
        Me.CBXFirstRowColumnNames.TabIndex = 3
        Me.CBXFirstRowColumnNames.Text = "First row of data has column names"
        Me.CBXFirstRowColumnNames.UseVisualStyleBackColor = True
        '
        'FFieldMapping
        '
        Me.AcceptButton = Me.Bok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Bcancel
        Me.ClientSize = New System.Drawing.Size(720, 420)
        Me.Controls.Add(Me.CBXFirstRowColumnNames)
        Me.Controls.Add(Me.DGcsv)
        Me.Controls.Add(Me.Bok)
        Me.Controls.Add(Me.Bcancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MinimumSize = New System.Drawing.Size(276, 132)
        Me.Name = "FFieldMapping"
        CType(Me.DGcsv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Bcancel As Global.System.Windows.Forms.Button
    Friend WithEvents Bok As Global.System.Windows.Forms.Button
    Friend WithEvents DGcsv As Global.System.Windows.Forms.DataGridView
    Friend WithEvents CBXFirstRowColumnNames As System.Windows.Forms.CheckBox
End Class
