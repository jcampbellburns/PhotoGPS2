#If DEBUG Then

Partial Class null
    'This just makes sure this file doesn't open in a designer
End Class

#End If

Partial Public Class fMain

    ''' <summary>
    ''' A private filed indicating that a Photos/Jobs correlation has been completed with the current dataset
    ''' </summary>
    Private _s4CorrelationCompleted As Boolean

    ''' <summary>
    ''' A subset of the loaded photos with only photos which have GPS data.
    ''' </summary>
    Private _s4PhotosWithGPS As IEnumerable(Of PhotoFile)

    ''' <summary>
    ''' Initializes step 4 by populating <see cref="_s4PhotosWithGPS"/> with a subset of the loaded phots containing only those photos with GPS data. Also populate <see cref="_s4tlTotals"/> with the number of jobs and the number of photos with GPS data.
    ''' </summary>
    Private Sub _s4BeginStep4()
        _s4PhotosWithGPS = From i In Project.Photos Where i.GPS.HasValue

        _s4tlTotals.Text = String.Format("{0} photos (with GPS) and {1} jobs loaded.", _s4PhotosWithGPS.Count, Project.Jobs.Count)
    End Sub

    ''' <summary>
    ''' Run a correlation against the loaded jobs and the loaded photos. Photos where <see cref="PhotoFile.GPS"/> is less than 0.5 miles from <see cref="Job.GPS"/> and where <see cref="PhotoFile.TakenDate"/> is on or between <see cref="job.Start"/> and <see cref="Job.End"/> will be added to the collection <see cref="Job.Photos"/>. Sets <see cref="_s4CorrelationCompleted"/> to <c>True</c>.
    ''' </summary>
    ''' <remarks>This method attempts to correlate each job's worth of photos in its own thread and reports progress to <see cref="_s0ProgressStatusbar"/>. This method can be interrupted by the user clicking on the stop button provided in that control.</remarks>
    Private Sub _s4RunCorrelation()

        _s0ProgressStatusbar.StartTask()

        Dim processedJobsCount As Integer

        Dim threads = (From i In Project.Jobs Select (
            Sub()
                Threading.Thread.CurrentThread.Priority = Threading.ThreadPriority.BelowNormal
                If _s0ProgressStatusbar.StopRequested Then Exit Sub

                i.Photos = (From p In _s4PhotosWithGPS Where i.ComparePhoto(p)).ToList

                _s0ProgressStatusbar.ReportTaskProgress("Correlating photos with jobs", Project.Jobs.Count, processedJobsCount, False)

                processedJobsCount += 1
            End Sub).BeginInvoke(Nothing, Nothing)).ToList

        Dim a =
            Sub()
                Threading.Thread.CurrentThread.Priority = Threading.ThreadPriority.BelowNormal

                'poll the entire list of threads. wait for all to be completed. also, we're only polling once every 1000 miliseconds
                While (From i In threads Where Not i.IsCompleted).Count > 0
                    Threading.Thread.Sleep(100)
                End While

                _s0ProgressStatusbar.EndTask()
                _s4CorrelationCompleted = True

                Me.Invoke(
                    Sub()
                        [Step].UpdateStepAvailability()
                        [Step].GoToNextStep()
                    End Sub)

            End Sub

        a.BeginInvoke(Nothing, Nothing)
    End Sub

    Private Sub _s4tsbRun_Click(sender As Object, e As EventArgs) Handles _s4tsbRun.Click
        _s4RunCorrelation()
    End Sub

End Class