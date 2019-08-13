#If DEBUG Then

Partial Class null
    'This just makes sure this file doesn't open in a designer
End Class

#End If

Partial Public Class fMain

    ''' <summary>
    ''' Private field representing the currently loaded CSV file.
    ''' </summary>
    Private _s3CSVFile As IO.FileInfo

    ''' <summary>
    ''' Private field containing an array of <see cref="ListViewItem"/> displayed in the jobs list. The jobs list for step 3 uses virtual items. This is the backing store.
    ''' </summary>
    Private _s3JobListViewItems As ListViewItem()

    ''' <summary>
    ''' Initializes step 3 by populating the earliest and latest value of <see cref="PhotoFile.TakenDate"/> of the loaded photos into <see cref="_s3tstbStart"/> and <see cref="_s3tstbEnd"/>, respectively. Also populates a "(No jobs loaded)" item into the jobs list if no jobs are loaded.
    ''' </summary>
    Private Sub _s3BeginStep3()
        'prepare the readonly text box values

        'photo date range
        If Project.Photos.Count > 0 Then
            Dim q = From i In Project.Photos Where i.TakenDate.HasValue Select i.TakenDate

            _s3tstbStart.Text = q.Min.Value.ToShortDateString
            _s3tstbEnd.Text = q.Max.Value.ToShortDateString
        End If

        If Project.Jobs.Count = 0 Then
            _s3lvJobs.BeginUpdate()
            _s3JobListViewItems = {New ListViewItem({"(No jobs loaded)", String.Empty, String.Empty, String.Empty, String.Empty})}
            _s3lvJobs.VirtualListSize = 1
            _s3lvJobs.EndUpdate()
        End If

    End Sub

    ''' <summary>
    ''' Prompst the user for a CSV file and loads the Jobs from it. Sets <see cref="_s3CSVFile"/> to an instance of <see cref="IO.FileInfo"/> which represents the loaded CSV file.
    ''' </summary>
    Private Sub _s3LoadJobs()
        Dim pb As CSVDeserializer(Of Job).PostBack = Sub(Message As String, progress As Double, force As Boolean) _s0ProgressStatusbar.ReportTaskProgress(Message, 100, progress * 100, force)

        Dim ow As New Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog

        ow.Filters.Add(New Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogFilter("Comma Separated Values Spreadsheet", "*.csv"))

        ow.EnsureFileExists = True

        If ow.ShowDialog() <> Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Cancel Then
            Me._s0ProgressStatusbar.StartTask(False)

            _s3CSVFile = New IO.FileInfo(ow.FileName)

            Dim j = CSVDeserializer(Of Job).Deserialize(_s3CSVFile, pb, Me)

            If j Is Nothing Then
                Me._s0ProgressStatusbar.EndTask()
                [Step].UpdateStepAvailability()
                Return
            End If

            Project.Jobs = j

            _s3lvJobs.BeginUpdate()
            Me._s3JobListViewItems = (From i In Project.Jobs Select New ListViewItem({i.DispatchNumber, i.Start.Value.ToShortDateString, i.End.Value.ToShortDateString, String.Format("Lat: {0}, Lon: {1}", i.Lat, i.Long), i.ID})).ToArray
            _s3lvJobs.VirtualListSize = _s3JobListViewItems.Count
            _s3lvJobs.EndUpdate()

            Me._s0ProgressStatusbar.EndTask()

            _s3tstbCSVFile.Text = _s3CSVFile.Name

            _s4CorrelationCompleted = False

            [Step].UpdateStepAvailability()

            [Step].GoToNextStep()
        End If
    End Sub

    Private Sub _s3lvJobs_RetrieveVirtualItem(sender As Object, e As RetrieveVirtualItemEventArgs) Handles _s3lvJobs.RetrieveVirtualItem

        e.Item = _s3JobListViewItems(e.ItemIndex)
    End Sub

    Private Sub _s3tsbLoadjobs_Click(sender As Object, e As EventArgs) Handles _s3tsbLoadjobs.Click
        _s3LoadJobs()
    End Sub

End Class