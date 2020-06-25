Public Class GuiContext

    Public Shared ReadOnly Property GuiLifelines As String()
        Get
            Dim lfl(5) As String
            lfl = (Quiz_Operator.ViewModel.Gpx.Lifelines.ThisGameLifelines + ";;;;").Split(";")
            Return lfl
        End Get
    End Property

    Friend Shared Sub SomethingToDoWithLifeline(lifelinePosition As Short, action As Short)
        Select Case action
            Case 0
                XmarkLifeline(lifelinePosition)
            Case 1
                UnusedLifeline(lifelinePosition)
            Case 2
                InUsemarkLifeline(lifelinePosition)
        End Select
    End Sub

    Friend Shared Sub ResetAll()
        UnusedLifeline(1)
        UnusedLifeline(2)
        UnusedLifeline(3)
        UnusedLifeline(4)
    End Sub

    Friend Shared Sub UnusedLifeline(lifelinePosition As Integer)
        If lifelinePosition = 1 Then
            Quiz_Operator.Lifeline1_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(0).ToUpper}_0")
        ElseIf lifelinePosition = 2 Then
            Quiz_Operator.Lifeline2_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(1).ToUpper}_0")
        ElseIf lifelinePosition = 3 Then
            Quiz_Operator.Lifeline3_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(2).ToUpper}_0")
        ElseIf lifelinePosition = 4 Then
            Quiz_Operator.Lifeline4_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(3).ToUpper}_0")
        End If
    End Sub


    Friend Shared Sub InUsemarkLifeline(lifelinePosition As Integer)
        If lifelinePosition = 1 Then
            Quiz_Operator.Lifeline1_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(0).ToUpper}_1")
        ElseIf lifelinePosition = 2 Then
            Quiz_Operator.Lifeline2_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(1).ToUpper}_1")
        ElseIf lifelinePosition = 3 Then
            Quiz_Operator.Lifeline3_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(2).ToUpper}_1")
        ElseIf lifelinePosition = 4 Then
            Quiz_Operator.Lifeline4_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(3).ToUpper}_1")
        End If
    End Sub

    Friend Shared Sub XmarkLifeline(lifelinePosition As Integer)
        If lifelinePosition = 1 Then
            Quiz_Operator.Lifeline1_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(0).ToUpper}_X")
        ElseIf lifelinePosition = 2 Then
            Quiz_Operator.Lifeline2_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(1).ToUpper}_X")
        ElseIf lifelinePosition = 3 Then
            Quiz_Operator.Lifeline3_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(2).ToUpper}_X")
        ElseIf lifelinePosition = 4 Then
            Quiz_Operator.Lifeline4_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(3).ToUpper}_X")
        End If
    End Sub

    Friend Shared Sub PositionLifelineTab(position As Short)
        If position < 1 Then Return
        If position > 5 Then Return

        Dim Lifeline As String = GuiLifelines.ElementAt(position - 1)

        Select Case Lifeline.ToUpper
            Case "5050"
                Quiz_Operator.TabControl2.SelectedTab = Quiz_Operator.TabPage7
                Quiz_Operator.TabControl1.SelectedTab = Quiz_Operator.TabPage8
            Case "PAF"
                Quiz_Operator.TabControl2.SelectedTab = Quiz_Operator.TabPage7
                Quiz_Operator.TabControl1.SelectedTab = Quiz_Operator.TabPage9
            Case "ATA"
                Quiz_Operator.TabControl2.SelectedTab = Quiz_Operator.TabPage7
                Quiz_Operator.TabControl1.SelectedTab = Quiz_Operator.TabPage10
            Case "STQ"
                Quiz_Operator.TabControl2.SelectedTab = Quiz_Operator.TabPage7
                Quiz_Operator.TabControl1.SelectedTab = Quiz_Operator.TabPage11
            Case "DDIP"
                Quiz_Operator.TabControl2.SelectedTab = Quiz_Operator.TabPage7
                Quiz_Operator.TabControl1.SelectedTab = Quiz_Operator.TabPage13
            Case "ATH"

            Case "ATE"

        End Select


    End Sub

    Friend Shared Sub FiftyFiftyResetOptionOperator(CorrectAnswer As String)
        Quiz_Operator.AremoveFF_Label.Visible = True
        Quiz_Operator.BremoveFF_Label.Visible = True
        Quiz_Operator.CremoveFF_Label.Visible = True
        Quiz_Operator.DremoveFF_Label.Visible = True

        Select Case CorrectAnswer
            Case 1
                Quiz_Operator.AremoveFF_Label.Visible = False
            Case 2
                Quiz_Operator.BremoveFF_Label.Visible = False
            Case 3
                Quiz_Operator.CremoveFF_Label.Visible = False
            Case 4
                Quiz_Operator.DremoveFF_Label.Visible = False
        End Select
    End Sub

    Friend Shared Sub ParseGamePlayContextIntoOperatorForm(OperatorForm As Quiz_Operator, Context As GamePlayContext)
        OperatorForm.MomentStatusCUEorder_TextBox.Text = Context.MomentStatus
        OperatorForm.Question_TextBox.Text = Context.Question.QuestionText
        OperatorForm.AnswerA_TextBox.Text = Context.Question.Answer1Text
        OperatorForm.AnswerB_TextBox.Text = Context.Question.Answer2Text
        OperatorForm.AnswerC_TextBox.Text = Context.Question.Answer3Text
        OperatorForm.AnswerD_TextBox.Text = Context.Question.Answer4Text
        OperatorForm.CorrectAnswer_TextBox.Text = Context.Question.CorrectAnswer
        OperatorForm.LevelQ_TextBox.Text = Context.Question.LevelQ
    End Sub

    Public Shared Sub MomentStatus(MomentStatus As String)
        '"EmptyQuestion_Fired"
        '"5050_Fire"
        '"AskAudience_EndVote"
        '"AskAudience_Voting"
        '"SwitchTheQuestion_Progess"
        '"PhoneFriend_Interrupted"
        '"NewGame_Fired"
        '"Question_AnswerD_Fired"
        '"QuestionAnswers_Load"
        '"Question_Fired"
        '"ABCDHex_Show"
        '"Question_AnswerA_Fired"
        '"Question_AnswerB_Fired"
        '"Question_AnswerC_Fired"
        '"AnswerA_Final_Fired"
        '"AnswerB_Final_Fired"
        '"AnswerC_Final_Fired"
        '"AnswerD_Final_Fired"
        '"DoubleDip_Final_Fired"
        '"CorrectAnswer_Fired"
        '"WonPrize_Fired"
        '"PhoneFriend_Dialing"
        '"PhoneFriend_Progress"
        '"AskAudience_Questioning"
        '"VariableMilestone_Set"
        '"DoubleDipIsFirstInstance.Quesion.FinalAnswer_Correct_Fired"
        '"DoubleDipAnswer_Final_Fired"

        'FinalA_Button.Visible = True
        'FinalB_Button.Visible = True
        'FinalC_Button.Visible = True
        'FinalD_Button.Visible = True
        'QuestionAppear_Button.Visible = True
        'WonPrizeReveal_Button.Visible = True


        Select Case Quiz_Operator.ViewModel.Gpx.MomentStatus
            Case "EmptyQuestion_Fired", "QuestionAnswers_Load"
                Quiz_Operator.FinalA_Button.Visible = False
                Quiz_Operator.FinalB_Button.Visible = False
                Quiz_Operator.FinalC_Button.Visible = False
                Quiz_Operator.FinalD_Button.Visible = False
                Quiz_Operator.CorrectAnswerReveal_Button.Visible = False
                Quiz_Operator.WonPrizeReveal_Button.Visible = False
                Quiz_Operator.SoundLX_Button.Visible = True
                Quiz_Operator.LifelineRemind_Button.Visible = False
                Quiz_Operator.QFor_Button.Visible = False
                Quiz_Operator.VariableMilestoneSet_Button.Visible = True
                Quiz_Operator.VariableMilestone_TextBox.Visible = True
                Quiz_Operator.WalkAwayStart_Button.Visible = False
                Quiz_Operator.WalkAwayQoppinion_Label.Visible = False
                Quiz_Operator.Lifeline1_PictureBox.Visible = False
                Quiz_Operator.Lifeline2_PictureBox.Visible = False
                Quiz_Operator.Lifeline3_PictureBox.Visible = False
                Quiz_Operator.Lifeline4_PictureBox.Visible = False

            Case "Question_Fired"
                Quiz_Operator.FinalA_Button.Visible = False
                Quiz_Operator.FinalB_Button.Visible = False
                Quiz_Operator.FinalC_Button.Visible = False
                Quiz_Operator.FinalD_Button.Visible = False
                Quiz_Operator.CorrectAnswerReveal_Button.Visible = False
                Quiz_Operator.WonPrizeReveal_Button.Visible = False
                Quiz_Operator.SoundLX_Button.Visible = False
                Quiz_Operator.LifelineRemind_Button.Visible = False
                Quiz_Operator.QFor_Button.Visible = True
                Quiz_Operator.VariableMilestoneSet_Button.Visible = False
                Quiz_Operator.VariableMilestone_TextBox.Visible = False
                Quiz_Operator.WalkAwayStart_Button.Visible = False
                Quiz_Operator.WalkAwayQoppinion_Label.Visible = False
                Quiz_Operator.Lifeline1_PictureBox.Visible = False
                Quiz_Operator.Lifeline2_PictureBox.Visible = False
                Quiz_Operator.Lifeline3_PictureBox.Visible = False
                Quiz_Operator.Lifeline4_PictureBox.Visible = False

            Case "AnswerA_Final_Fired",
                 "AnswerB_Final_Fired",
                 "AnswerC_Final_Fired",
                 "AnswerD_Final_Fired"

                Quiz_Operator.FinalA_Button.Visible = False
                Quiz_Operator.FinalB_Button.Visible = False
                Quiz_Operator.FinalC_Button.Visible = False
                Quiz_Operator.FinalD_Button.Visible = False
                Quiz_Operator.WonPrizeReveal_Button.Visible = False
                Quiz_Operator.CorrectAnswerReveal_Button.Visible = True
                Quiz_Operator.SoundLX_Button.Visible = False
                Quiz_Operator.LifelineRemind_Button.Visible = False
                Quiz_Operator.QFor_Button.Visible = True
                Quiz_Operator.VariableMilestoneSet_Button.Visible = False
                Quiz_Operator.VariableMilestone_TextBox.Visible = False
                Quiz_Operator.WalkAwayStart_Button.Visible = False
                Quiz_Operator.Lifeline1_PictureBox.Visible = False
                Quiz_Operator.Lifeline2_PictureBox.Visible = False
                Quiz_Operator.Lifeline3_PictureBox.Visible = False
                Quiz_Operator.Lifeline4_PictureBox.Visible = False

            Case "Question_AnswerD_Fired", "DoubleDipAnswer_Final_Fired" 'prikazi konecen
                Quiz_Operator.FinalA_Button.Visible = True
                Quiz_Operator.FinalB_Button.Visible = True
                Quiz_Operator.FinalC_Button.Visible = True
                Quiz_Operator.FinalD_Button.Visible = True
                Quiz_Operator.WonPrizeReveal_Button.Visible = False
                Quiz_Operator.SoundLX_Button.Visible = False
                Quiz_Operator.LifelineRemind_Button.Visible = True
                Quiz_Operator.QFor_Button.Visible = True
                Quiz_Operator.VariableMilestoneSet_Button.Visible = False
                Quiz_Operator.VariableMilestone_TextBox.Visible = False
                Quiz_Operator.WalkAwayStart_Button.Visible = True
                Quiz_Operator.WalkAwayQoppinion_Label.Visible = False
                Quiz_Operator.Lifeline1_PictureBox.Visible = True
                Quiz_Operator.Lifeline2_PictureBox.Visible = True
                Quiz_Operator.Lifeline3_PictureBox.Visible = True
                Quiz_Operator.Lifeline4_PictureBox.Visible = True

            Case "WonPrize_Fired"
                Quiz_Operator.WonPrizeReveal_Button.Visible = False

            Case "CorrectAnswer_Fired"
                Quiz_Operator.FinalA_Button.Visible = False
                Quiz_Operator.FinalB_Button.Visible = False
                Quiz_Operator.FinalC_Button.Visible = False
                Quiz_Operator.FinalD_Button.Visible = False
                Quiz_Operator.CorrectAnswerReveal_Button.Visible = False
                Quiz_Operator.WonPrizeReveal_Button.Visible = True
                Quiz_Operator.SoundLX_Button.Visible = True
                Quiz_Operator.LifelineRemind_Button.Visible = False
                Quiz_Operator.QFor_Button.Visible = False
                Quiz_Operator.VariableMilestoneSet_Button.Visible = True
                Quiz_Operator.VariableMilestone_TextBox.Visible = True
                Quiz_Operator.WalkAwayStart_Button.Visible = False
                Quiz_Operator.WalkAwayQoppinion_Label.Visible = False
                Quiz_Operator.Lifeline1_PictureBox.Visible = False
                Quiz_Operator.Lifeline2_PictureBox.Visible = False
                Quiz_Operator.Lifeline3_PictureBox.Visible = False
                Quiz_Operator.Lifeline4_PictureBox.Visible = False

            Case "DoubleDipIsFirstInstance.Quesion.FinalAnswer_Correct_Fired"
                Quiz_Operator.FinalA_Button.Visible = True
                Quiz_Operator.FinalB_Button.Visible = True
                Quiz_Operator.FinalC_Button.Visible = True
                Quiz_Operator.FinalD_Button.Visible = True
                Quiz_Operator.WonPrizeReveal_Button.Visible = False
                Quiz_Operator.CorrectAnswerReveal_Button.Visible = False
                Quiz_Operator.SoundLX_Button.Visible = False
                Quiz_Operator.LifelineRemind_Button.Visible = False
                Quiz_Operator.QFor_Button.Visible = True
                Quiz_Operator.VariableMilestoneSet_Button.Visible = False
                Quiz_Operator.VariableMilestone_TextBox.Visible = False
                Quiz_Operator.WalkAwayStart_Button.Visible = False
                Quiz_Operator.WalkAwayQoppinion_Label.Visible = False

            Case "Walkaway_Fired", "JustOpinion_Fired"
                Quiz_Operator.WonPrizeReveal_Button.Visible = True
                Quiz_Operator.CorrectAnswerReveal_Button.Visible = False
                Quiz_Operator.LifelineRemind_Button.Visible = False
                Quiz_Operator.QFor_Button.Visible = True
                Quiz_Operator.WalkAwayStart_Button.Visible = False
                Quiz_Operator.WalkAwayQoppinion_Label.Visible = True

        End Select

        'ParseGamePlayContextIntoOperatorForm(Quiz_Operator, Instance)
        Quiz_Operator.NextThing_Button.Text = "CUE NEXT..." + Quiz_Operator.NextActivity
    End Sub


End Class
