Partial Public Class fMain
    ''' <summary>
    ''' A reference to an instance of <see cref="Project"/> which contains all of the instances of both <see cref="PhotoFile"/> and <see cref="Job"/> currently loaded. Similar to a DBContext model.
    ''' </summary>
    Private Project As New Project

    Private Sub _s0ProgressStatusbar_TaskEnded() Handles _s0ProgressStatusbar.TaskEnded
        [Step].UpdateStepAvailability()
    End Sub

    Private Sub _s0ProgressStatusbar_TaskStarted() Handles _s0ProgressStatusbar.TaskStarted
        [Step].UpdateStepAvailability()
    End Sub

    Private Sub fMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim s1 As New [Step] With {.Button = _s0tsbStep1, .Panel = _s1Page, .Index = 1}
        Dim s2 As New [Step] With {.Button = _s0tsbStep2, .Panel = _s2Page, .Index = 2}
        Dim s3 As New [Step] With {.Button = _s0tsbStep3, .Panel = _s3Page, .Index = 3}
        Dim s4 As New [Step] With {.Button = _s0tsbStep4, .Panel = _s4Page, .Index = 4}
        Dim s5 As New [Step] With {.Button = _s0tsbStep5, .Panel = _s5Page, .Index = 5}
        Dim s6 As New [Step] With {.Button = _s0tsbStep6, .Panel = _s6Page, .Index = 6}

        AddHandler [Step].AfterStepChange, AddressOf Me.Step_AfterStepChange
        AddHandler [Step].BeforeStepChange, AddressOf Me.Step_BeforeStepChange
        AddHandler [Step].IsStepEnabled, AddressOf Me.Step_IsStepEnabled

        [Step].Steps.AddRange({s1, s2, s3, s4, s5, s6})

        [Step].CurrentStep = s1

        [Step].UpdateStepAvailability()
    End Sub

    Private Sub Step_AfterStepChange(OldStep As [Step], NewStep As [Step])
        Select Case NewStep.Index
            Case 1

                _s2EnumeratedFiles = Enumerable.Empty(Of IO.FileInfo)
                _s2EnumeratedFolders = Enumerable.Empty(Of IO.DirectoryInfo)
                Project.Photos = Enumerable.Empty(Of PhotoFile)
                _s4CorrelationCompleted = False

                For Each i In Project.Jobs
                    i.Photos = New List(Of PhotoFile)
                Next

                _s4CorrelationCompleted = False

                [Step].UpdateStepAvailability()
            Case 2
                If OldStep.Index = 1 Then
                    _s2BeginStep2()
                End If
            Case 3
                _s3BeginStep3()
            Case 4
                _s4BeginStep4()
            Case 5
                _s4BeginStep5()
            Case 6
                '_s6BeginStep6()
        End Select
    End Sub

    Private Sub Step_BeforeStepChange(OldStep As [Step], NewStep As [Step], ByRef Cancel As Boolean)

        Select Case NewStep.Index
            Case 1
                If OldStep.Index > 1 Then
                    Cancel = (MsgBox("This will delete the following datasets: Enumerated Folders, Enumerated Files, Loaded Photo Metadata, Photo/Job Corellation Data. Continue?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo) = MsgBoxResult.No)

                End If
            Case 2

            Case 3
            Case 4
            Case 5
            Case 6

        End Select
    End Sub

    Private Sub Step_IsStepEnabled([Step] As [Step], ByRef Enabled As Boolean)
        If _s0ProgressStatusbar.IsInTask Then
            Enabled = ([Step].CurrentStep Is [Step])
        Else
            Select Case [Step].Index
                Case 1
                    'Requires:
                    'step 1 is always available
                    Enabled = True
                Case 2
                    'Requires:
                    'non-zero number of selected folders

                    Enabled = (_s1Selectedfolders.Count > 0)
                Case 3
                    'Requires:
                    'non-zero number of loaded photos
                    Enabled = (Project.Photos.Count > 0)
                Case 4
                    'Requires:
                    'non-zero number of loaded photos
                    'non-zero number of loaded jobs

                    Enabled = (Project.Photos.Count > 0) And (Project.Jobs.Count > 0)
                Case 5, 6
                    'Requires:
                    'correlation has been run

                    Enabled = _s4CorrelationCompleted
            End Select
        End If

    End Sub

    ''' <summary>
    ''' Private class with shared and non-shared members for keeping track of which step the user is on as well as centrally managing step-related UI elements.
    ''' </summary>
    Private Class [Step]

        ''' <summary>
        ''' An instance field containing the <see cref="ToolStripButton" /> controlled by this step.
        ''' </summary>
        Public WithEvents Button As ToolStripButton



        ''' <summary>
        ''' A shared field of type <see cref="List(Of T)"/> instance containing a reference to all instances of <see cref="[step]"/>.
        ''' </summary>
        Public Shared Steps As New List(Of [Step])

        ''' <summary>
        ''' The 1-based index of this step instance.
        ''' </summary>
        ''' <remarks>No checking is performed by either instance or shared members of <see cref="[Step]"/> to ensure no duplicates. The behavior of these members in the event of a duplicate value for index is undefined.
        '''
        ''' This index does NOT represent the index of this instance within <see cref="[step].Steps"/>.</remarks>
        Public Index As Integer

        ''' <summary>
        ''' An instance field containing the <see cref="Panel" /> controlled by this step.
        ''' </summary>
        Public Panel As Panel


        Private Shared _CurrentStep As [Step]

        ''' <summary>
        ''' Raised after the current step has been changed.
        ''' </summary>
        ''' <param name="OldStep">The previous step which was active.</param>
        ''' <param name="NewStep">The current step which is now active.</param>
        ''' <remarks>It is possible for either <c>OldStep</c> or <c>NewStep</c> to pass a null reference.</remarks>
        Public Shared Event AfterStepChange(OldStep As [Step], NewStep As [Step])

        ''' <summary>
        ''' Raised before the current step has been changed and allows logic to cancel the step change.
        ''' </summary>
        ''' <param name="OldStep">The current step which is now active.</param>
        ''' <param name="NewStep">The step which is proposed to change to.</param>
        ''' <param name="Cancel">Set <c>Cancel</c> to <c>True</c> to prevent the step from changing.</param>
        ''' <remarks>It is possible for either <c>OldStep</c> or <c>NewStep</c> to pass a null reference.
        '''
        ''' If <c>Cancel</c> is <c>True</c>, event <see cref="[Step].AfterStepChange"/> is not raised as the step did not change.</remarks>
        Public Shared Event BeforeStepChange(OldStep As [Step], NewStep As [Step], ByRef Cancel As Boolean)

        ''' <summary>
        ''' Allows for criteria checking to determine if a given step should be enabled or not.
        ''' </summary>
        ''' <param name="[Step]">An instance reference to a particular step being queried.</param>
        ''' <param name="Enabled">Set to <c>True</c> to indicate that the step should be enabled. Set to <c>False</c> to indicate the step should be disabled.</param>
        ''' <remarks>Disabling a step disables both <see cref="Button"/> and <see cref="Panel"/>.</remarks>
        Public Shared Event IsStepEnabled([Step] As [Step], ByRef Enabled As Boolean)

        ''' <summary>
        ''' A shared property containing the currently active instance of <see cref="[Step]"/>.
        ''' </summary>
        Public Shared Property CurrentStep As [Step]
            Get
                If _CurrentStep Is Nothing Then
                    If Steps.Count > 0 Then
                        _CurrentStep = (From i In Steps Where i.Index = 1).First
                    End If
                End If

                Return _CurrentStep
            End Get
            Set(value As [Step])
                If Steps.Contains(value) Then

                    'to support a step being explicitly set even if the step's requirements are not met
                    Dim a = value.Button.Enabled
                    value.Button.Enabled = True

                    value.Button.PerformClick()

                    'to support a step being explicitly set even if the step's requirements are not met
                    value.Button.Enabled = a
                    UpdateStepAvailability()
                Else
                    Throw New InvalidOperationException("The specified Step reference isn't contained in the Steps collection.")
                End If
            End Set
        End Property

        Public Shared Sub GoToNextStep()
            CurrentStep = (From i In Steps Where i.Index = CurrentStep.Index + 1).FirstOrDefault
        End Sub

        ''' <summary>
        ''' Raises the <see cref="IsStepEnabled"/> event for each step in <see cref="[step].Steps"/>.
        ''' </summary>
        Public Shared Sub UpdateStepAvailability()
            For Each s In Steps
                If s IsNot _CurrentStep Then
                    Dim e As Boolean = True
                    RaiseEvent IsStepEnabled(s, e)

                    s.Button.Enabled = e
                    s.Panel.Enabled = e
                Else
                    'to support a step being explicitly set even if the step's requirements are not met
                    s.Button.Enabled = True
                    s.Panel.Enabled = True

                End If

            Next
        End Sub

        ''' <summary>
        ''' Functionality to make the instance of <see cref="Button"/> for each step in <see cref="[Step].Steps"/> act as an option group where only one may be checked at a time.
        ''' </summary>
        Private Sub _UpdateButtonChecks()
            Dim CancelRequested As Boolean = False

            RaiseEvent BeforeStepChange(CurrentStep, Me, CancelRequested)

            If Not CancelRequested Then
                For Each i In Steps
                    Dim state = i Is Me
                    i.Button.Checked = state
                    i.Panel.Visible = state
                Next

                Dim oldstep = _CurrentStep
                _CurrentStep = Me

                RaiseEvent AfterStepChange(oldstep, Me)

            End If

        End Sub

        Private Sub Button_Click(sender As Object, e As EventArgs) Handles Button.Click
            _UpdateButtonChecks()
        End Sub

    End Class

End Class