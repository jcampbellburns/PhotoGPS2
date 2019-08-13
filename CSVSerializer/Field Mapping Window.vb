Friend Class FFieldMapping
    Public CSVData As List(Of List(Of String))
    Public ObjectFields As List(Of String)

    Private Sub Bok_Click(sender As Object, e As EventArgs) Handles Bok.Click
        'validate that at least one Object field is mapped
        Me.DialogResult = DialogResult.None

        For i = 0 To Me.DGcsv.Columns.GetColumnCount(DataGridViewElementStates.None) - 1
            If DGcsv.Rows(0).Cells(i).Value <> String.Empty Then
                Me.DialogResult = DialogResult.OK
                Exit For
            End If
        Next

        If DialogResult = DialogResult.None Then
            MsgBox("You must select at least one field to map.")
        End If

        PrepareData()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CBXFirstRowColumnNames.CheckedChanged
        UpdateDatagrid()
    End Sub

    Private Sub DGcsv_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DGcsv.EditingControlShowing
        'This method ensures that each object field can be selected only once.
        'This is done by removing items from the drop down control which are selected in other columns.
        With DirectCast(e.Control, ComboBox).Items
            Dim ColumnCount = DGcsv.Columns.GetColumnCount(DataGridViewElementStates.None)

            For i = 0 To ColumnCount - 1
                If i <> DGcsv.SelectedCells(0).ColumnIndex Then
                    Dim v = DGcsv.Rows(0).Cells(i).Value

                    If Not v = String.Empty Then

                        Dim c = DirectCast(e.Control, ComboBox)
                        If c.Items.Contains(v) Then
                            c.Items.Remove(v)
                        End If
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub FFieldMapping_Load(sender As Object, e As EventArgs) Handles Me.Load
        UpdateDatagrid()
    End Sub

    Private Sub PrepareData()
        'set first row of data to Object field drop downs
        Dim HeaderRow As New List(Of String)
        For i = 0 To Me.DGcsv.Columns.GetColumnCount(DataGridViewElementStates.None) - 1
            HeaderRow.Add(DGcsv.Rows(0).Cells(i).Value)
        Next

        If CBXFirstRowColumnNames.Checked Then
            'replace first row of data with values from Object Field drop downs
            CSVData(0) = HeaderRow
        Else
            'insert new row of data with values from Object Field drop downs
            CSVData.Insert(0, HeaderRow)
        End If

        'find all columns where the first row (0) is blank
        Dim DeletionColumns As New List(Of Integer)
        For i = 0 To CSVData(0).Count - 1
            If CSVData(0)(i) = String.Empty Then
                DeletionColumns.Add(i)
            End If
        Next

        'delete the columns. Because we're using a list of lists to store the data, we have to iterate through each row to do this. The problem is, since the indexes change when we delete stuff, we'll have to copy the whole table, sans the deleted columns
        Dim resData As New List(Of List(Of String))
        For r = 0 To CSVData.Count() - 1
            Dim row As New List(Of String)
            For c = 0 To CSVData(r).Count - 1
                If Not DeletionColumns.Contains(c) Then
                    row.Add(CSVData(r)(c))
                End If
            Next
            resData.Add(row)
        Next

        CSVData = resData 'return the re-mapped data

    End Sub

    Private Sub UpdateDatagrid()
        If CSVData IsNot Nothing Then

            Dim NumberOfColumns = CSVData.First.Count
            Dim NumberOfRows = CSVData.LongCount

            With DGcsv
                'reset dataGridView
                .Columns.Clear()
                .Rows.Clear()

                'add column headers
                If Me.CBXFirstRowColumnNames.Checked Then
                    'first row is headers

                    For i = 0 To NumberOfColumns - 1
                        Call .Columns.Add(i, CSVData(0)(i))
                    Next
                Else
                    'first row is data (column headers are "1", "2", "3", ...)

                    For i = 0 To NumberOfColumns - 1
                        Call .Columns.Add(i, i.ToString)
                    Next
                End If

                'add field selection drop down
                Dim currentheaderrow As New DataGridViewRow
                For i = 0 To NumberOfColumns - 1

                    Dim cell As New DataGridViewComboBoxCell With {
                        .ValueType = GetType(String)
                    }

                    cell.Items.Add(String.Empty)

                    For Each h In ObjectFields
                        cell.Items.Add(h)
                    Next

                    currentheaderrow.Cells.Add(cell)
                Next
                currentheaderrow.Frozen = True
                .Rows.Add(currentheaderrow)

                'check if any of the CSV headers match any of the Object Headers and set as appropriate
                If Me.CBXFirstRowColumnNames.Checked Then
                    Dim ObjectHeadersInUse As New List(Of String)

                    For i = 0 To NumberOfColumns - 1

                        For Each h In ObjectFields
                            If h.Trim.Equals(.Columns(i).HeaderText.Trim, StringComparison.CurrentCultureIgnoreCase) Then
                                '(case-insensitive match)
                                If Not ObjectHeadersInUse.Contains(h) Then
                                    .Rows(0).Cells(i).Value = h
                                    ObjectHeadersInUse.Add(h)
                                End If

                                Exit For 'we're setting on the first matching CSV column, not all of them

                            End If
                        Next
                    Next
                End If

                'add sample CSV data
                For r As Long = If(Me.CBXFirstRowColumnNames.Checked, 1, 0) To If((NumberOfRows - 1) > 100, 100, (NumberOfRows - 1))

                    Dim currentdatarow As New DataGridViewRow

                    'make it clear to the user the data is read-only
                    currentdatarow.DefaultCellStyle.BackColor = Drawing.SystemColors.ButtonFace
                    currentdatarow.DefaultCellStyle.ForeColor = Drawing.SystemColors.GrayText

                    For c As Integer = 0 To NumberOfColumns - 1

                        If Not c > CSVData(r).Count Then
                            Dim cell As New DataGridViewTextBoxCell With {
                                .ValueType = GetType(String),
                                .Value = CSVData(r)(c)
                            }

                            currentdatarow.Cells.Add(cell)

                            cell.ReadOnly = True

                        End If
                    Next
                    .Rows.Add(currentdatarow)
                Next

                'turn off sorting, set autosize for columns
                For Each i As DataGridViewColumn In .Columns
                    i.SortMode = DataGridViewColumnSortMode.NotSortable
                    i.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                Next
            End With

        End If
    End Sub

End Class