#If DEBUG Then

Partial Class null
    'This just makes sure this file doesn't open in a designer
End Class

#End If

Public Class ProgressStatusBar
    Inherits StatusStrip

    Public StopRequested As Boolean = False

    Sub New()
        _initControls()
    End Sub

    Public Event TaskEnded()

    Public Event TaskStarted()

    Public Sub EndTask()
        _IsInTask = False

        Dim a = Sub()
                    Me._sbMessage.Text = "Idle."
                    Me._sbProgress.Visible = False
                    Me._sbStopButton.Visible = False

                    RaiseEvent TaskEnded()
                End Sub

        If Me.InvokeRequired Then
            Me.Invoke(a)
        Else
            a()
        End If

    End Sub

    Public Sub ReportTaskProgress(Message As String, SubItemCount As Integer, CurrentItem As Integer, Force As Boolean)
        Static n As Date

        Dim a = Sub()

                    If (Date.Now > n) Or Force Then
                        Dim total = SubItemCount
                        Dim current = CurrentItem

                        If current > total Then current = total

                        n = Date.Now.AddSeconds(0.1)

                        If total = 0 Then 'no progress indication
                            Dim statustext As String = Message
                            Me._sbMessage.Text = statustext
                            Me._sbProgress.Visible = False
                        ElseIf total = 100 Then 'percentage progress indication
                            Me._sbMessage.Text = String.Format("{0}: {1}%", Message, CurrentItem.ToString("N0"))
                            Me._sbProgress.Visible = True
                            Me._sbProgress.Value = current
                        Else 'total items indication
                            Me._sbMessage.Text = String.Format("{0}: {1} of {2}", Message, CurrentItem.ToString("N0"), total.ToString("N0"))
                            Me._sbProgress.Visible = True
                            Me._sbProgress.Value = current / total * 100
                            'Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressValue(Percentage * 100, 100)
                        End If

                        Application.DoEvents()
                    End If

                End Sub

        If Me.InvokeRequired Then
            Me.Invoke(a)
        Else
            a()
        End If
    End Sub

    Public Sub StartTask(Optional Stopable As Boolean = True)
        StopRequested = False

        _IsInTask = True

        Dim a = Sub()
                    Me._sbProgress.Visible = True
                    Me._sbStopButton.Visible = Stopable
                    RaiseEvent TaskStarted()

                End Sub

        If Me.InvokeRequired Then
            Me.Invoke(a)
        Else
            a()
        End If
    End Sub

#Region "Controls"

    Private WithEvents _sbStopButton As ToolStripButton
    Private _sbMessage As ToolStripLabel
    Private _sbProgress As ToolStripProgressBar

    Public ReadOnly Property IsInTask As Boolean

    Private Sub _initControls()
        Dim status As New ToolStripLabel With {.Text = "Status:", .AutoSize = True}
        _sbMessage = New ToolStripLabel With {.Text = "Idle.", .AutoSize = True}
        _sbProgress = New ToolStripProgressBar With {.Maximum = 100, .Minimum = 0, .Visible = False}
        _sbStopButton = New ToolStripButton With {.DisplayStyle = ToolStripItemDisplayStyle.Text, .Text = "<", .Font = New System.Drawing.Font("Webdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte)), .ForeColor = System.Drawing.Color.Red, .Visible = False}

        Me.Items.AddRange({status, _sbMessage, _sbProgress, _sbStopButton})
    End Sub

    Private Sub _sbStopButton_Click(sender As Object, e As EventArgs) Handles _sbStopButton.Click
        Me.StopRequested = True
        EndTask()
    End Sub

#End Region

End Class