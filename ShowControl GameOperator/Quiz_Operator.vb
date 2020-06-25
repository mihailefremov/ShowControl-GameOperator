Imports System.Threading.Tasks

Public Class Quiz_Operator

    Private RulesQcount As Integer
    Private defaultATAvoteTime As String = 6

    Private SwitchedQuestion As Integer

    Private MainGameMusicLayerObj As MainGameMusicLayer
    Private Localizer As WwtbamLocalizer

    Private DataLayer As IDataLayer = New DataLayerMsSql

    Public ViewModel As New GamePlayContext

    Private Sub LoadConfiguration()

        Try
            Dim ConfigurationPath As String = GameConfiguration.Default.DefaultGameConfigurationPath
            Dim BasicConfigurationText As String = System.IO.File.ReadAllText(String.Format("{0}\{1}", ConfigurationPath, "BasicGameConfiguration.xml"))

            Dim BasicConfigurationReader As System.IO.TextReader = New System.IO.StringReader(BasicConfigurationText)

            Dim serializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(Xml2CSharp.BASICGAMECONFIGURATIONS))
            Dim WwtbamConfiguraiton As Xml2CSharp.BASICGAMECONFIGURATIONS

            WwtbamConfiguraiton = serializer.Deserialize(BasicConfigurationReader)

#Region "MONEYTREE REAL VALUE"

            ViewModel.Gpx.CurrentGamePlayLevels = New LevelTree()
            For Each level As Xml2CSharp.LEVEL In WwtbamConfiguraiton.MONEYTREE.LEVEL
                ViewModel.Gpx.CurrentGamePlayLevels.Add(New MainGameLevel(level.POSITION, level.PREVIEWVALUE, level.REALVALUE, level.SAFEHEAVEN))
            Next
            ViewModel.Gpx.CurrentGamePlayLevels.OrderBy(Function(t) t.PositionLevel)

#End Region

#Region "MONEYTREE PREVIEW VALUE SET"

            For Each level As Xml2CSharp.LEVEL In WwtbamConfiguraiton.MONEYTREE.LEVEL
                Dim TextBoxSume As String = "QSum" + (level.POSITION).ToString + "_TextBox"
                For Each tb As TextBox In Me.Controls.OfType(Of TextBox)()
                    If String.Compare(TextBoxSume, tb.Name, True) = 0 Then
                        tb.Text = level.PREVIEWVALUE
                        tb.BackColor = Color.White
                        If level.SAFEHEAVEN = "1" Then tb.BackColor = Color.Bisque
                    End If
                Next
            Next

#End Region

            Localizer = New WwtbamLocalizer
            Localizer.LocalizeControl(Me)

            HostContPresentationLayer.ConfigurationMoneyTreeSet()

            If WwtbamConfiguraiton.LIFELINES.DEFAULTLIFELINECOUNT.Equals(3) Then
                ThreeLifelinesStatus_Label_Click(threeLifelinesStatus_Label, Nothing)
            ElseIf WwtbamConfiguraiton.LIFELINES.DEFAULTLIFELINECOUNT.Equals(4) Then
                FourLifelinesStatus_Label_Click(fourLifelinesStatus_Label, Nothing)
            End If

            ViewModel.Gpx.Lifelines.ThisGameLifelines = $"{WwtbamConfiguraiton.LIFELINES.LIFELINE1};{WwtbamConfiguraiton.LIFELINES.LIFELINE2};{WwtbamConfiguraiton.LIFELINES.LIFELINE3};{WwtbamConfiguraiton.LIFELINES.LIFELINE4};{WwtbamConfiguraiton.LIFELINES.LIFELINE5}"
            HostContPresentationLayer.ConfigurationLifelines($"{WwtbamConfiguraiton.LIFELINES.LIFELINE1}", $"{WwtbamConfiguraiton.LIFELINES.LIFELINE2}", $"{WwtbamConfiguraiton.LIFELINES.LIFELINE3}", $"{WwtbamConfiguraiton.LIFELINES.LIFELINE4}", $"{WwtbamConfiguraiton.LIFELINES.LIFELINE5}")

            ViewModel.Gpx.AnswerMarks = IIf(Localizer.GetValueByKey("ANSWERMARKS").Length >= 4, Localizer.GetValueByKey("ANSWERMARKS"), ViewModel.Gpx.AnswerMarks)
            HostContPresentationLayer.ConfigurationLocalization(ViewModel.Gpx.AnswerMarks, Localizer.GetValueByKey("TOTALPRIZEWONTAG"))

            GuiContext.ResetAll()

            MainGameMusicLayerObj = New MainGameMusicLayer(WwtbamConfiguraiton.MONEYTREE.LEVEL.Count)
            MusicList_ComboBox.DataSource = MainGameMusicLayerObj.WwtbamMusicPlaylistConfig.SOUND
            MusicList_ComboBox.DisplayMember = "TITLE"

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try

    End Sub


    Private Sub Quiz_Operator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LoadConfiguration()
        LiveRehearselViewButton(m_IsGameGoingLive)

        ViewModel.Run()

        TimerPAFsecondDrop.Interval = 1000
        Timer_PLAY.Interval = 300

        If (Not Debugger.IsAttached) Or (Not Debugger.IsAttached And My.Settings.DebugMode = True) Then
            Timer_STOP.Start()
        End If


    End Sub

    Private Sub QuestionLoad_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuestionLoad_Label.Click

        If ViewModel.Gpx.Question.LevelQ <> "111" Or ViewModel.Gpx.Question.LevelQ <> "666" Then
            Using selectedQuestionTable As DataTable = DataLayer.SelectSuitableQuestion(ViewModel.Gpx.Question.LevelQ)
                With selectedQuestionTable
                    If selectedQuestionTable.Rows.Count > 0 Then
                        ViewModel.Gpx.Question.ID = .Rows(0)("QuestionID").ToString()
                        ViewModel.Gpx.Question = New Question(selectedQuestionTable)
                    End If
                End With
            End Using
        End If

        If RandomizeAnswers_CheckBox.Checked = True Then
            ViewModel.Gpx.Question.RandomizeAnswers()
        End If

        GuiContext.FiftyFiftyResetOptionOperator(ViewModel.Gpx.Question.CorrectAnswer)

        ViewModel.Gpx.MomentStatus = "QuestionAnswers_Load"
        '' ******* CASPARCG *******
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.CGQuestionSet(ViewModel.Gpx.Question.QuestionText, ViewModel.Gpx.Question.Answer1Text, ViewModel.Gpx.Question.Answer2Text, ViewModel.Gpx.Question.Answer3Text, ViewModel.Gpx.Question.Answer4Text, ViewModel.Gpx.QuestionForSume)
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Add(1, My.Settings.questionFlashTempl, True, GraphicsProcessingUnit.cgDataQA)

        End If

        'ViewModel.UpdateView()
        '' ******* CASPARCG ******* 

    End Sub

    Private Sub QuestionAppear()
        ViewModel.Gpx.MomentStatus = "Question_Fired"

        ViewModel.Gpx.QuestionForSume = 0
        If ViewModel.Gpx.CurrentGamePlayLevels.Where(Function(t) t.PositionLevel = ViewModel.Gpx.Question.LevelQ).FirstOrDefault IsNot Nothing Then
            ViewModel.Gpx.QuestionForSume = DirectCast(ViewModel.Gpx.CurrentGamePlayLevels.Where(Function(t) t.PositionLevel = ViewModel.Gpx.Question.LevelQ).FirstOrDefault, MainGameLevel).PreviewValueSume
        End If
        MainGameMusicLayerObj.PlayHeartbeatMusic(ViewModel.Gpx.Question.LevelQ)

        ViewModel.Gpx.Question.FinalAnswer = -1
        AnswerDappear_Label.BackColor = Color.Yellow

        TimerQuestionRemove.Interval = Val(SecondsToDissolveAfterCorrectAnswer_TextBox.Text) * 1000

        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.CGQuestionSet(ViewModel.Gpx.Question.QuestionText, ViewModel.Gpx.Question.Answer1Text, ViewModel.Gpx.Question.Answer2Text, ViewModel.Gpx.Question.Answer3Text, ViewModel.Gpx.Question.Answer4Text, ViewModel.Gpx.QuestionForSume)
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Update(1, GraphicsProcessingUnit.cgDataQA)
            'casparQA_.Channels(0).CG.Play(1)
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "QuestionFlyIN")
        End If

        DataLayer.DisposeATAvoteData()

        GraphicsProcessingUnit.InteractiveWallScreenObj.MotionBackgroundDuringQuestion(ViewModel.Gpx.Question.LevelQ)

    End Sub

    Private Sub AnswerAappear_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnswerAappear_Label.Click

        AnswerAappear_Label.BackColor = Color.Blue
        AnswerBappear_Label.BackColor = Color.Yellow
        AnswerCappear_Label.BackColor = Color.Yellow
        AnswerDappear_Label.BackColor = Color.Yellow

        '' ******* CASPARCG ******* CASPARCG *******
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "QuestionAnswersFlyIN")
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "showAnswerA")
        End If
        '' ******* CASPARCG ******* CASPARCG *******

        ViewModel.Gpx.MomentStatus = "Question_AnswerA_Fired" ''IZMENA!!

        MainGameMusicLayerObj.StopLetsPlay()

        ''SQL
        DataLayer.MarkQuestionFiredDB(ViewModel.Gpx.Question.ID, Me.IsGameGoingLive)
        DataLayer.MarkQuestionAnsweredDB(ViewModel.Gpx.Question.ID, Me.IsGameGoingLive)
        ''SQL

    End Sub

    Private Sub AnswerBappear_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnswerBappear_Label.Click

        AnswerBappear_Label.BackColor = Color.Blue
        AnswerAappear_Label.BackColor = Color.Yellow
        AnswerCappear_Label.BackColor = Color.Yellow
        AnswerDappear_Label.BackColor = Color.Yellow

        '' ******* CASPARCG ******* CASPARCG *******
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "showAnswerB")
        End If
        '' ******* CASPARCG ******* CASPARCG *******

        ViewModel.Gpx.MomentStatus = "Question_AnswerB_Fired" ''IZMENA!!

    End Sub

    Private Sub AnswerCappear_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnswerCappear_Label.Click

        AnswerCappear_Label.BackColor = Color.Blue
        AnswerBappear_Label.BackColor = Color.Yellow
        AnswerAappear_Label.BackColor = Color.Yellow
        AnswerDappear_Label.BackColor = Color.Yellow

        '' ******* CASPARCG ******* CASPARCG *******
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "showAnswerC")
        End If
        '' ******* CASPARCG ******* CASPARCG *******

        ViewModel.Gpx.MomentStatus = "Question_AnswerC_Fired" ''IZMENA!!

    End Sub

    Private Sub AnswerDappear_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnswerDappear_Label.Click

        TimerABCDyellowLabels.Start()

        AnswerDappear_Label.BackColor = Color.Blue
        AnswerBappear_Label.BackColor = Color.Yellow
        AnswerCappear_Label.BackColor = Color.Yellow
        AnswerAappear_Label.BackColor = Color.Yellow

        '' ******* CASPARCG ******* CASPARCG *******
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "showAnswerD")
        End If
        '' ******* CASPARCG ******* CASPARCG *******

        ViewModel.Gpx.MomentStatus = "Question_AnswerD_Fired" ''IZMENA!!

    End Sub


#Region "FINALANSWERS"

    Public Sub HostFinalAnswerData()

        LimitedGameClockStop_Label_Click(LimitedGameClockStop_Label, Nothing)

        If Not ViewModel.Gpx.Question.FinalAnswer = ViewModel.Gpx.Question.CorrectAnswer Then
            SumeShow_CheckBox.Checked = False
            Empty_CheckBox.Checked = False
        End If

        MainGameMusicLayerObj.PlayFinalAnswerSound(ViewModel.Gpx.Question.LevelQ, ViewModel.Gpx.DoubleDipState)

        If Val(ViewModel.Gpx.Question.LevelQ) >= 6 And Val(ViewModel.Gpx.Question.LevelQ) <= 15 Then
            Timer_STOP.Interval = 500
            Timer_STOP.Start()

        End If

        If GraphicsProcessingUnit.flagATAgraph = True Then
            GraphicsProcessingUnit.ShowHideATAGraph()
        End If

        GraphicsProcessingUnit.CGgleenQAstop()

    End Sub

    Private Sub FinalA_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinalA_Button.Click, AnswerA_TextBox.DoubleClick

        AnswerA_TextBox.BackColor = Color.Yellow

        ViewModel.Gpx.Question.FinalAnswer = 1

        '' ******* CASPARCG ******* CASPARCG *******
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "finalAnswerA")
        End If
        '' ******* CASPARCG ******* CASPARCG *******

        HostFinalAnswerData()

        ViewModel.Gpx.MomentStatus = "AnswerA_Final_Fired" ''IZMENA!!

    End Sub

    Private Sub FinalB_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinalB_Button.Click, AnswerB_TextBox.DoubleClick

        AnswerB_TextBox.BackColor = Color.Yellow

        ViewModel.Gpx.Question.FinalAnswer = 2

        '' ******* CASPARCG ******* CASPARCG *******
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "finalAnswerB")
        End If
        '' ******* CASPARCG ******* CASPARCG *******

        HostFinalAnswerData()

        ViewModel.Gpx.MomentStatus = "AnswerB_Final_Fired" ''IZMENA!!

    End Sub

    Private Sub FinalC_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinalC_Button.Click, AnswerC_TextBox.DoubleClick

        AnswerC_TextBox.BackColor = Color.Yellow

        ViewModel.Gpx.Question.FinalAnswer = 3

        If ViewModel.Gpx.Question.LevelQ = "6" Or ViewModel.Gpx.Question.LevelQ = "7" Or ViewModel.Gpx.Question.LevelQ = "8" Or ViewModel.Gpx.Question.LevelQ = "9" Or ViewModel.Gpx.Question.LevelQ = "10" Or ViewModel.Gpx.Question.LevelQ = "11" Or ViewModel.Gpx.Question.LevelQ = "12" Or ViewModel.Gpx.Question.LevelQ = "13" Or ViewModel.Gpx.Question.LevelQ = "14" Or ViewModel.Gpx.Question.LevelQ = "15" Then
            Timer_STOP.Interval = 500
            Timer_STOP.Start()
        End If

        '' ******* CASPARCG ******* CASPARCG *******
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "finalAnswerC")
        End If
        '' ******* CASPARCG ******* CASPARCG *******

        HostFinalAnswerData()

        ViewModel.Gpx.MomentStatus = "AnswerC_Final_Fired" ''IZMENA!!

    End Sub

    Private Sub FinalD_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinalD_Button.Click, AnswerD_TextBox.DoubleClick

        AnswerD_TextBox.BackColor = Color.Yellow

        ViewModel.Gpx.Question.FinalAnswer = 4

        '' ******* CASPARCG ******* CASPARCG *******
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "finalAnswerD")
        End If
        '' ******* CASPARCG ******* CASPARCG *******

        HostFinalAnswerData()

        ViewModel.Gpx.MomentStatus = "AnswerD_Final_Fired" ''IZMENA!!

    End Sub

#End Region
    Private Sub CorrectAnswerProcedure()

        MainGameMusicLayerObj.StopIncorrectAnswer()
        MainGameMusicLayerObj.PlayCorrectAnswer(ViewModel.Gpx.Question.LevelQ, VariableMilestone_TextBox.Text)

        If ViewModel.Gpx.Question.LevelQ <> "888" And ViewModel.Gpx.Question.LevelQ <> "666" Then
            ViewModel.Gpx.Question.LevelQ = Val(ViewModel.Gpx.Question.LevelQ) + 1
        End If

        Leveling()

        GraphicsProcessingUnit.InteractiveWallScreenObj.CorrectAnswer(ViewModel.Gpx.Question.LevelQ)

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerQuestionRemove.Tick
        RemoveQuestion_Button_Click(RemoveQuestion_Button, Nothing)

    End Sub

    Public Function SetSecondMilestonePrize(variableMilestone As String) As String
        If String.IsNullOrEmpty(variableMilestone) OrElse (Val(variableMilestone) < 5 Or Val(variableMilestone) > 15) Then
            Return Nothing
        End If
        For index As Integer = 5 To 15
            Dim TextBox As String = "QSum" + variableMilestone + "_TextBox"
            For Each tb As TextBox In Me.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    Return tb.Text
                End If
            Next
        Next
        Return Nothing
    End Function

    Public Function CalculateIncorrectAnswer(levelq As String, Optional realvalue As Boolean = False) As String

        If Not IsNumeric(levelq) Then Return ""

        Dim reval As String = "0"
        Dim Milestones As New List(Of MainGameLevel)

        For Each level As MainGameLevel In ViewModel.Gpx.CurrentGamePlayLevels
            If level.SafeHeaven = True Then
                Milestones.Add(level)
            End If
        Next

        'zemi go prviot sto kje go najdes naopacki od nizata za koj vazi uslovot
        If Milestones.Where(Function(t) t.PositionLevel < levelq).Count > 0 Then
            Dim lastSafeSume As Long
            lastSafeSume = Milestones.Where(Function(t) t.PositionLevel < levelq).First.RealValueSume
            reval = lastSafeSume
        End If

        Return reval

    End Function

    Private Sub IncorrectAnswerProcedure()

        If (ViewModel.Gpx.Question.LevelQ <> "666") And Not (String.Equals(ViewModel.Gpx.DoubleDipState, "DoubleDipFirstFinal", StringComparison.OrdinalIgnoreCase)) Then 'Ne stavaj suma za utnato ako e vo walkaway mod, togas e samo radi reda
            ViewModel.Gpx.QuestionForSume = CalculateIncorrectAnswer(ViewModel.Gpx.Question.LevelQ)
        End If

        If (ViewModel.Gpx.Question.LevelQ <> "666") And Not (String.Equals(ViewModel.Gpx.DoubleDipState, "DoubleDipFirstFinal", StringComparison.OrdinalIgnoreCase)) Then 'Ne stavaj suma za utnato ako e vo walkaway mod, togas e samo radi reda
            ViewModel.Gpx.QuestionForSume = CalculateIncorrectAnswer(ViewModel.Gpx.Question.LevelQ)
        End If

        MainGameMusicLayerObj.PlayIncorrectAnswer(ViewModel.Gpx.Question.LevelQ, ViewModel.Gpx.DoubleDipState)

        If ViewModel.Gpx.Question.LevelQ <> "888" And ViewModel.Gpx.Question.LevelQ <> "666" And Not (String.Equals(ViewModel.Gpx.DoubleDipState, "DoubleDipFirstFinal", StringComparison.OrdinalIgnoreCase)) Then
            ViewModel.Gpx.Question.LevelQ = "111"
        End If

    End Sub

    Private Sub RemoveQuestion_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveQuestion_Button.Click

        AremoveFF_Label.BackColor = Color.Yellow
        BremoveFF_Label.BackColor = Color.Yellow
        CremoveFF_Label.BackColor = Color.Yellow
        DremoveFF_Label.BackColor = Color.Yellow

        ViewModel.Gpx.Question.QuestionText = ""
        ViewModel.Gpx.Question.Answer1Text = ""
        ViewModel.Gpx.Question.Answer2Text = ""
        ViewModel.Gpx.Question.Answer3Text = ""
        ViewModel.Gpx.Question.Answer4Text = ""

        ViewModel.Gpx.Question.Pronunciation = ""
        ViewModel.Gpx.Question.Explanation = ""

        AnswerA_TextBox.BackColor = Color.White
        AnswerB_TextBox.BackColor = Color.White
        AnswerC_TextBox.BackColor = Color.White
        AnswerD_TextBox.BackColor = Color.White

        ViewModel.Gpx.FiftyFifty = ""
        ViewModel.Gpx.DoubleDipState = ""
        ViewModel.Gpx.DoubleDipFirstAnswer = ""

        TimerQuestionRemove.Stop() ''IZMENA!!! Dodadeno Timer2.Stop() bidejkji ako bese stisnato EmptyQuestion pred vreme, Timer2 pak go gasese posle izminatiot interval
        ViewModel.Gpx.MomentStatus = "EmptyQuestion_Fired" ''IZMENA!!

        '' ******* CASPARCG ******* CASPARCG *******
        '' ******* CASPARCG ******* CASPARCG *******
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            If ViewModel.Gpx.Question.LevelQ <> "888" Then
                GraphicsProcessingUnit.casparQA.Channels(0).CG.Remove(1)
                GraphicsProcessingUnit.cgDataQA.Clear()
            Else
                GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "QuestionHide_Label")
                GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "hideSwitchQmark")
            End If
        End If

        If ViewModel.Gpx.Question.LevelQ = "888" Then
            ViewModel.Gpx.Question.LevelQ = SwitchedQuestion.ToString
            STQ_X_Label_Click(STQ_X_Label, Nothing)
        End If

        Leveling()
        LoadQuestion()

        FinalA_Button.Text = "Final A: " + ViewModel.Gpx.Question.Answer1Text.Substring(0, Math.Min(20, ViewModel.Gpx.Question.Answer1Text.Length))
        FinalB_Button.Text = "Final B: " + ViewModel.Gpx.Question.Answer2Text.Substring(0, Math.Min(20, ViewModel.Gpx.Question.Answer2Text.Length))
        FinalC_Button.Text = "Final C: " + ViewModel.Gpx.Question.Answer3Text.Substring(0, Math.Min(20, ViewModel.Gpx.Question.Answer3Text.Length))
        FinalD_Button.Text = "Final D: " + ViewModel.Gpx.Question.Answer4Text.Substring(0, Math.Min(20, ViewModel.Gpx.Question.Answer4Text.Length))

        '' ******* CASPARCG ******* CASPARCG *******
        GraphicsProcessingUnit.resetVariables()
        DataLayer.DisposeATAvoteData()


    End Sub

    Private Function LoadQuestion()
        If ViewModel.Gpx.Question.LevelQ <> "111" Then
            QuestionLoad_Label_Click(QuestionLoad_Label, Nothing)

        End If
    End Function

    Private Sub WonPrizeReveal_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WonPrizeReveal_Button.Click
        WonPrizeReveal_Button.BackColor = Color.Gainsboro
        GraphicsProcessingUnit.WonPrizeReveal(ViewModel.Gpx.Question.LevelQ,
                                              Localizer.GetValueByKey("TOTALPRIZEWONTAG"),
                                              ViewModel.Gpx.QuestionForSume)

        If ViewModel.Gpx.Question.LevelQ = "111" Or ViewModel.Gpx.Question.LevelQ = "666" Or ViewModel.Gpx.Question.LevelQ = "16" Then
            HostContPresentationLayer.GamePlayStateSet("TOTALPRIZEWON")

        ElseIf ViewModel.Gpx.Question.LevelQ <> "888" Then
            'DO NOTHING
            'od prasanje na prasanje momentalen iznos
        End If


        ViewModel.Gpx.MomentStatus = "WonPrize_Fired" ''IZMENA!!


    End Sub

    Private Sub AremoveFF_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AremoveFF_Label.Click
        AremoveFF_Label.BackColor = Color.Blue

    End Sub

    Private Sub BremoveFF_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BremoveFF_Label.Click
        BremoveFF_Label.BackColor = Color.Blue

    End Sub

    Private Sub CremoveFF_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CremoveFF_Label.Click
        CremoveFF_Label.BackColor = Color.Blue

    End Sub

    Private Sub DremoveFF_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DremoveFF_Label.Click
        DremoveFF_Label.BackColor = Color.Blue

    End Sub

    Private Sub FiftyFiftyStart_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FiftyFiftyStart_Label.Click
        My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\40.50-50.wav", AudioPlayMode.Background)

        Dim ListRemoved As New List(Of Short)

        If RandomSelection_CheckBox.Checked Then
            'RANDOMLY SELECT TWO WRONG
            AremoveFF_Label.BackColor = Color.Yellow
            BremoveFF_Label.BackColor = Color.Yellow
            CremoveFF_Label.BackColor = Color.Yellow
            DremoveFF_Label.BackColor = Color.Yellow

            ListRemoved = New List(Of Short) From {1, 2, 3, 4}
            ListRemoved.Remove(ViewModel.Gpx.Question.CorrectAnswer)
            Dim randomNumber As Short = New Random().Next(0, ListRemoved.Count)
            ListRemoved.RemoveAt(randomNumber)
        End If

        'Razmisli zosto go napravi ova vaka
        If AremoveFF_Label.BackColor = Color.Blue OrElse ListRemoved.Contains(1) Then
            ViewModel.Gpx.Question.Answer1Text = ""
            ListRemoved.Add(1)
        End If
        If BremoveFF_Label.BackColor = Color.Blue OrElse ListRemoved.Contains(2) Then
            ViewModel.Gpx.Question.Answer2Text = ""
            ListRemoved.Add(2)
        End If
        If CremoveFF_Label.BackColor = Color.Blue OrElse ListRemoved.Contains(3) Then
            ViewModel.Gpx.Question.Answer3Text = ""
            ListRemoved.Add(3)
        End If
        If DremoveFF_Label.BackColor = Color.Blue OrElse ListRemoved.Contains(4) Then
            ViewModel.Gpx.Question.Answer4Text = ""
            ListRemoved.Add(4)
        End If

        GraphicsProcessingUnit.FiftyFiftyTake(ListRemoved)

        Dim HashListOfRemoves As New HashSet(Of Short)(ListRemoved)
        If HashListOfRemoves.Count = 2 Then
            ViewModel.Gpx.FiftyFifty = String.Format("{0}{1}", HashListOfRemoves.ElementAt(0), HashListOfRemoves.ElementAt(1))
            'HostContPresentationLayer.FiftyFiftyFire(HashListOfRemoves.ElementAt(0), HashListOfRemoves.ElementAt(1))
        End If

        ViewModel.Gpx.MomentStatus = "5050_Fire"

        GraphicsProcessingUnit.UpdateQAData()

        Me.TabControl2.SelectedTab = TabPage6

        FiftyFifty_X_Label_Click(FiftyFifty_X_Label, Nothing)

    End Sub

    Public Sub ataEndVote()

        Dim AtaPercentData() As String = DataLayer.GetATAvoteData()

        A_pub.Text = AtaPercentData(0)
        B_pub.Text = AtaPercentData(1)
        C_pub.Text = AtaPercentData(2)
        D_pub.Text = AtaPercentData(3)

        ViewModel.Gpx.AtaVotes = String.Format("{0};{1};{2};{3}", A_pub.Text, B_pub.Text, C_pub.Text, D_pub.Text)

        '**** CASPARCG
        GraphicsProcessingUnit.SetATAdata(A_pub.Text, B_pub.Text, C_pub.Text, D_pub.Text)
        '**** CASPARCG***** 17 18 20 22

        TimerATAsecDrop.Stop()
        TimerATAsecDrop.InitializeLifetimeService()

        MainGameMusicLayerObj.PlayLifelineSound("ATA", "DECIDE")

        A_pub.Text = A_pub.Text + "%"
        B_pub.Text = B_pub.Text + "%"
        C_pub.Text = C_pub.Text + "%"
        D_pub.Text = D_pub.Text + "%"

        Timer_PLAY.Start()
        Timer_PLAY.Interval = 800

        ATA_X_Label_Click(ATA_X_Label, Nothing)

        ViewModel.Gpx.MomentStatus = "AskAudience_EndVote"

        '**** CASPARCG*****

        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "ATAendVote")
        End If

        '**** CASPARCG*****
        Me.TabControl2.SelectedTab = TabPage6
    End Sub

    Private Sub Timer8_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerATAendVoteWithSec.Tick

        If ataVoteTime_TextBox.Text = "0" And AutoEndATA_CheckBox.CheckState = CheckState.Checked Then
            ataEndVote()

        End If

        TimerATAendVoteWithSec.Stop()
        TimerATAendVoteWithSec.InitializeLifetimeService()
    End Sub

    Private Sub ATAstart_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ATAstart_Label.Click
        Timer_PAUSE.Start()

        TimerATAsecDrop.Start()

        ataVoteTime_TextBox.Text = defaultATAvoteTime

        MainGameMusicLayerObj.PlayLifelineSound("ATA", "VOTE")

        TimerATAendVoteWithSec.Interval = Int(ataVoteTime_TextBox.Text) * 1000 + 5
        TimerATAendVoteWithSec.Start()

        ''CASPARCG***** CASPARCG****
        ShowHideAudienceGraph_Label_Click(ShowHideAudienceGraph_Label, Nothing)
        'showATAGraph

        ''CASPARCG***** CASPARCG****

        ViewModel.Gpx.MomentStatus = "AskAudience_Voting" ''IZMENA!!!

        GraphicsProcessingUnit.InteractiveWallScreenObj.AudienceVoting()

    End Sub

    Private Sub TimerPAFsecondDrop_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerPAFsecondDrop.Tick

        If PAFsec_TextBox.Text > 0 Then
            PAFsec_TextBox.Text -= 1
        Else
            TimerPAFsecondDrop.Stop()
            PAF_X_Label_Click(PAF_X_Label, Nothing)

            ViewModel.Gpx.MomentStatus = "PhoneFriend_End"

            Timer_PLAY.Interval = 1000
            Timer_PLAY.Start()

            '*** CASPARCG
            GraphicsProcessingUnit.PhoneAFriend("END")
            '*** CASPARCG

        End If

    End Sub

    Private Sub FourLifelinesStatus_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fourLifelinesStatus_Label.Click
        fourLifelinesStatus_Label.BackColor = Color.Orange
        threeLifelinesStatus_Label.BackColor = Color.Silver
        ViewModel.Gpx.Lifelines.ActiveLifelines = "4"
        GraphicsProcessingUnit.Activate4Lifelines()

    End Sub

    Private Sub ThreeLifelinesStatus_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles threeLifelinesStatus_Label.Click
        threeLifelinesStatus_Label.BackColor = Color.Orange
        fourLifelinesStatus_Label.BackColor = Color.Silver
        ViewModel.Gpx.Lifelines.ActiveLifelines = "3"
        GraphicsProcessingUnit.Activate3Lifelines()

    End Sub

    Private Sub LifelineRemind_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LifelineRemind_Button.Click
        GraphicsProcessingUnit.MarkCGlifelines(ViewModel.Gpx.Lifelines.Lifeline1Active, ViewModel.Gpx.Lifelines.Lifeline2Active, ViewModel.Gpx.Lifelines.Lifeline3Active, ViewModel.Gpx.Lifelines.Lifeline4Active)
        GraphicsProcessingUnit.LifelineRemind()

    End Sub

    Private Sub Label_SwitchQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label100.Click



        SwitchedQuestion = Val(ViewModel.Gpx.Question.LevelQ)

        'Dim vm As New ViewModel
        'vm.UpdateModel(Instance.Question.)

        ViewModel.Gpx.Question.LevelQ = "888"

        My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\102.Alternate Animate.wav", AudioPlayMode.Background)

        STQ_1_Label_Click(STQ_1_Label, Nothing)

        '' *** CASPARCG
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "showSwitchQmark")
        End If
        '' *** CASPARCG

        ViewModel.Gpx.MomentStatus = "SwitchTheQuestion_Progess"

        ''STQ_X_Label_Click(STQ_X_Label, Nothing)

        'Contestant.PictureBox9.BackgroundImage = Image.FromFile("C:\WWTBAM Removable Disc\Graphics\SwitchQ_X.png")
        'Contestant.PictureBox9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TabControl2.SelectedTab = TabPage6
    End Sub

    Private Sub WalkAwayStart_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WalkAwayStart_Button.Click
        SumeShow_CheckBox.Checked = False
        Empty_CheckBox.Checked = False

        If ViewModel.Gpx.Question.LevelQ = "1" Or ViewModel.Gpx.Question.LevelQ = "2" Or ViewModel.Gpx.Question.LevelQ = "3" Or ViewModel.Gpx.Question.LevelQ = "4" Or ViewModel.Gpx.Question.LevelQ = "5" Or ViewModel.Gpx.Question.LevelQ = "6" Or ViewModel.Gpx.Question.LevelQ = "7" Or ViewModel.Gpx.Question.LevelQ = "8" Or ViewModel.Gpx.Question.LevelQ = "9" Or ViewModel.Gpx.Question.LevelQ = "10" Then
            'LightDown.URL = "C:\WWTBAM Removable Disc\UK 2007\33.Little Quitter.wav"
        End If
        If ViewModel.Gpx.Question.LevelQ = "11" Or ViewModel.Gpx.Question.LevelQ = "12" Or ViewModel.Gpx.Question.LevelQ = "13" Or ViewModel.Gpx.Question.LevelQ = "14" Or ViewModel.Gpx.Question.LevelQ = "15" Or ViewModel.Gpx.Question.LevelQ = "16" Then
            'LightDown.URL = "C:\WWTBAM Removable Disc\UK 2007\34.Big Quitter.wav"
        End If

        Dim TextBox As String = "QSum" + Val(ViewModel.Gpx.Question.LevelQ - 1).ToString + "_TextBox"
        For Each tb As TextBox In Me.Controls.OfType(Of TextBox)()
            If String.Compare(TextBox, tb.Name, True) = 0 Then
                If ViewModel.Gpx.Question.LevelQ >= 2 And ViewModel.Gpx.Question.LevelQ <= 15 Then
                    ViewModel.Gpx.QuestionForSume = tb.Text
                End If
            End If
        Next

        ViewModel.Gpx.Question.LevelQ = "666"

        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "QuestionAnswersFlyOUT")
        End If

        PlayLXsound()
        Timer_STOP.Start()

        GraphicsProcessingUnit.InteractiveWallScreenObj.MotionBackgroundDuringQuestion(ViewModel.Gpx.Question.LevelQ)

        ViewModel.Gpx.MomentStatus = "Walkaway_Fired"

    End Sub

    Private Sub PAFstart_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAFstart_Label.Click

        PAFsec_TextBox.Text = "30"

        MainGameMusicLayerObj.PlayLifelineSound("PAF", "CLOCK")

        Timer_PAUSE.Start()

        TimerPAFsecondDrop.Start()

        '*** CASPARCG
        GraphicsProcessingUnit.PhoneAFriend("START")
        '*** CASPARCG

        PAF_1_Label_Click(PAF_1_Label, Nothing)

        ViewModel.Gpx.MomentStatus = "PhoneFriend_Progress" ''IZMENA!!!

    End Sub

    Private Sub PAFabort_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAFabort_Label.Click
        MainGameMusicLayerObj.PlayLifelineSound("PAF", "ABORT")

        TimerPAFsecondDrop.Stop()

        PAF_X_Label_Click(PAF_X_Label, Nothing)

        Timer_PLAY.Interval = 1500
        Timer_PLAY.Start()

        '*** CASPARCG
        GraphicsProcessingUnit.PhoneAFriend("ABORT")
        '*** CASPARCG

        ViewModel.Gpx.MomentStatus = "PhoneFriend_Interrupted" ''IZMENA!!!
        Me.TabControl2.SelectedTab = TabPage6
    End Sub

    Private Sub NewGame_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewGame_Label.Click
        SumeShow_CheckBox.Checked = True
        Empty_CheckBox.Checked = True

        Me.Lifeline3_PictureBox.BackgroundImage = Image.FromFile("C:\WWTBAM Removable Disc\Graphics\ATA_0.png")
        Me.Lifeline2_PictureBox.BackgroundImage = Image.FromFile("C:\WWTBAM Removable Disc\Graphics\DDIP_0.png")
        Me.Lifeline1_PictureBox.BackgroundImage = Image.FromFile("C:\WWTBAM Removable Disc\Graphics\5050_0.png")

        Me.Lifeline3_PictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Lifeline2_PictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Lifeline1_PictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch

        ViewModel.Gpx.Question.FinalAnswer = -1
        ViewModel.Gpx.DoubleDipFirstAnswer = ""
        VariableMilestone_TextBox.Text = ""

        If ViewModel.Gpx.Question.LevelQ = "111" Or ViewModel.Gpx.Question.LevelQ = "666" Then
            ViewModel.Gpx.Question.LevelQ = "1"

        End If

        ''SQL
        DataLayer.DisposeAnsweredGameQuestionsDB(1)
        ''SQL

        ViewModel.Gpx.MomentStatus = "NewGame_Fired" ''IZMENA!!!
        FiftyFifty_0_Label_Click(FiftyFifty_0_Label, Nothing)
        PAF_0_Label_Click(PAF_0_Label, Nothing)
        DDIP_0_Label_Click(DDIP_0_Label, Nothing)
        ATA_0_Label_Click(ATA_0_Label, Nothing)
        STQ_0_Label_Click(STQ_0_Label, Nothing)


        For index As Integer = 6 To 15 - 1
            Dim TextBox As String = "QSum" + index.ToString + "_TextBox"
            For Each tb As TextBox In Me.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    tb.BackColor = Color.White
                End If
            Next
        Next

        ViewModel.Gpx.Lifelines.Lifeline1Active = 1
        ViewModel.Gpx.Lifelines.Lifeline2Active = 1
        ViewModel.Gpx.Lifelines.Lifeline3Active = 1
        ViewModel.Gpx.Lifelines.Lifeline4Active = 1

        GraphicsProcessingUnit.CGMoneyTreeDataSet(QSum1_TextBox.Text, QSum2_TextBox.Text, QSum3_TextBox.Text, QSum4_TextBox.Text, QSum5_TextBox.Text, QSum6_TextBox.Text, QSum7_TextBox.Text, QSum8_TextBox.Text, QSum9_TextBox.Text, QSum10_TextBox.Text, QSum11_TextBox.Text, QSum12_TextBox.Text, QSum13_TextBox.Text, QSum14_TextBox.Text, QSum15_TextBox.Text)
        GraphicsProcessingUnit.removeSecondMilestone()
        GraphicsProcessingUnit.MoneyTreeFlyOut()

        GraphicsProcessingUnit.InteractiveWallScreenObj.AnyBackgroundLoop("GoodbyeToContestant", False)

    End Sub

    Private Sub FiftyFifty_X_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FiftyFifty_X_Label.Click
        AremoveFF_Label.BackColor = Color.Yellow
        BremoveFF_Label.BackColor = Color.Yellow
        CremoveFF_Label.BackColor = Color.Yellow
        DremoveFF_Label.BackColor = Color.Yellow

        GuiContext.SomethingToDoWithLifeline(ViewModel.Gpx.Lifelines.FiftyFiftyPosition, 0)

        Select Case ViewModel.Gpx.Lifelines.FiftyFiftyPosition
            Case 1
                ViewModel.Gpx.Lifelines.Lifeline1Active = 0
            Case 2
                ViewModel.Gpx.Lifelines.Lifeline2Active = 0
            Case 3
                ViewModel.Gpx.Lifelines.Lifeline3Active = 0
            Case 4
                ViewModel.Gpx.Lifelines.Lifeline4Active = 0
        End Select

    End Sub

    Private Sub TimerATAsecDrop_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerATAsecDrop.Tick

        If ataVoteTime_TextBox.Text <> "0" Then
            ataVoteTime_TextBox.Text = Val(ataVoteTime_TextBox.Text) - 1
        End If

        If ataVoteTime_TextBox.Text <> defaultATAvoteTime Then
            Try
                Dim VoteArray As String() = DataLayer.GetATAvoteData()
                A_pub.Text = VoteArray.ElementAt(0)
                B_pub.Text = VoteArray.ElementAt(1)
                C_pub.Text = VoteArray.ElementAt(2)
                D_pub.Text = VoteArray.ElementAt(3)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            A_pub.Text = A_pub.Text + "%"
            B_pub.Text = B_pub.Text + "%"
            C_pub.Text = C_pub.Text + "%"
            D_pub.Text = D_pub.Text + "%"

        End If

        '** REAL TIME VOTING


    End Sub

    Private Sub TimerABCDyellowLabels_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerABCDyellowLabels.Tick

        AnswerAappear_Label.BackColor = Color.Yellow
        AnswerBappear_Label.BackColor = Color.Yellow
        AnswerCappear_Label.BackColor = Color.Yellow
        AnswerDappear_Label.BackColor = Color.Yellow

        AnswersABCDappear_Label.BackColor = Color.Yellow

        TimerABCDyellowLabels.Stop()
        TimerABCDyellowLabels.InitializeLifetimeService()

    End Sub

    Private Sub PAF_X_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAF_X_Label.Click
        GuiContext.SomethingToDoWithLifeline(ViewModel.Gpx.Lifelines.PhoneAFriendPosition, 0)

        Select Case ViewModel.Gpx.Lifelines.PhoneAFriendPosition
            Case 1
                ViewModel.Gpx.Lifelines.Lifeline1Active = 0
            Case 2
                ViewModel.Gpx.Lifelines.Lifeline2Active = 0
            Case 3
                ViewModel.Gpx.Lifelines.Lifeline3Active = 0
            Case 4
                ViewModel.Gpx.Lifelines.Lifeline4Active = 0
        End Select

    End Sub

    Private Sub ATA_X_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ATA_X_Label.Click
        GuiContext.SomethingToDoWithLifeline(ViewModel.Gpx.Lifelines.AskTheAudiencePosition, 0)

        Select Case ViewModel.Gpx.Lifelines.AskTheAudiencePosition
            Case 1
                ViewModel.Gpx.Lifelines.Lifeline1Active = 0
            Case 2
                ViewModel.Gpx.Lifelines.Lifeline2Active = 0
            Case 3
                ViewModel.Gpx.Lifelines.Lifeline3Active = 0
            Case 4
                ViewModel.Gpx.Lifelines.Lifeline4Active = 0
        End Select

    End Sub

    Private Sub STQ_X_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STQ_X_Label.Click
        GuiContext.SomethingToDoWithLifeline(ViewModel.Gpx.Lifelines.SwitchTheQuestionPosition, 0)

        Select Case ViewModel.Gpx.Lifelines.SwitchTheQuestionPosition
            Case 1
                ViewModel.Gpx.Lifelines.Lifeline1Active = 0
            Case 2
                ViewModel.Gpx.Lifelines.Lifeline2Active = 0
            Case 3
                ViewModel.Gpx.Lifelines.Lifeline3Active = 0
            Case 4
                ViewModel.Gpx.Lifelines.Lifeline4Active = 0
        End Select


    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewModel.Gpx.Question.LevelQ = "1"

    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewModel.Gpx.Question.LevelQ = "2"
    End Sub

    Private Sub Label21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewModel.Gpx.Question.LevelQ = "14"
    End Sub

    Private Sub Label20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewModel.Gpx.Question.LevelQ = "13"
    End Sub

    Private Sub Label19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewModel.Gpx.Question.LevelQ = "12"
    End Sub

    Private Sub Label18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewModel.Gpx.Question.LevelQ = "11"
    End Sub

    Private Sub Label17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewModel.Gpx.Question.LevelQ = "10"
    End Sub

    Private Sub Label16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewModel.Gpx.Question.LevelQ = "9"
    End Sub

    Private Sub Label15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewModel.Gpx.Question.LevelQ = "8"
    End Sub

    Private Sub Label14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewModel.Gpx.Question.LevelQ = "7"
    End Sub

    Private Sub Label13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewModel.Gpx.Question.LevelQ = "6"
    End Sub

    Private Sub Label12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewModel.Gpx.Question.LevelQ = "5"
    End Sub

    Private Sub Label11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewModel.Gpx.Question.LevelQ = "4"
    End Sub

    Private Sub Label10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewModel.Gpx.Question.LevelQ = "3"
    End Sub

    Private Sub Label22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewModel.Gpx.Question.LevelQ = "15"
    End Sub

    Private Sub FiftyFifty_0_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FiftyFifty_0_Label.Click

        GuiContext.SomethingToDoWithLifeline(ViewModel.Gpx.Lifelines.FiftyFiftyPosition, 1)

        Select Case ViewModel.Gpx.Lifelines.FiftyFiftyPosition
            Case 1
                ViewModel.Gpx.Lifelines.Lifeline1Active = 1
            Case 2
                ViewModel.Gpx.Lifelines.Lifeline2Active = 1
            Case 3
                ViewModel.Gpx.Lifelines.Lifeline3Active = 1
            Case 4
                ViewModel.Gpx.Lifelines.Lifeline4Active = 1
        End Select

    End Sub

    Private Sub PAF_0_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAF_0_Label.Click
        GuiContext.SomethingToDoWithLifeline(ViewModel.Gpx.Lifelines.PhoneAFriendPosition, 1)

        Select Case ViewModel.Gpx.Lifelines.PhoneAFriendPosition
            Case 1
                ViewModel.Gpx.Lifelines.Lifeline1Active = 1
            Case 2
                ViewModel.Gpx.Lifelines.Lifeline2Active = 1
            Case 3
                ViewModel.Gpx.Lifelines.Lifeline3Active = 1
            Case 4
                ViewModel.Gpx.Lifelines.Lifeline4Active = 1
        End Select

    End Sub

    Private Sub ATA_0_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ATA_0_Label.Click

        GuiContext.SomethingToDoWithLifeline(ViewModel.Gpx.Lifelines.AskTheAudiencePosition, 1)

        Select Case ViewModel.Gpx.Lifelines.AskTheAudiencePosition
            Case 1
                ViewModel.Gpx.Lifelines.Lifeline1Active = 1
            Case 2
                ViewModel.Gpx.Lifelines.Lifeline2Active = 1
            Case 3
                ViewModel.Gpx.Lifelines.Lifeline3Active = 1
            Case 4
                ViewModel.Gpx.Lifelines.Lifeline4Active = 1
        End Select

    End Sub

    Private Sub STQ_0_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STQ_0_Label.Click

        GuiContext.SomethingToDoWithLifeline(ViewModel.Gpx.Lifelines.SwitchTheQuestionPosition, 1)

        Select Case ViewModel.Gpx.Lifelines.SwitchTheQuestionPosition
            Case 1
                ViewModel.Gpx.Lifelines.Lifeline1Active = 1
            Case 2
                ViewModel.Gpx.Lifelines.Lifeline2Active = 1
            Case 3
                ViewModel.Gpx.Lifelines.Lifeline3Active = 1
            Case 4
                ViewModel.Gpx.Lifelines.Lifeline4Active = 1
        End Select

    End Sub

    Private Sub FiftyFifty_1_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FiftyFifty_1_Label.Click

        GuiContext.SomethingToDoWithLifeline(ViewModel.Gpx.Lifelines.FiftyFiftyPosition, 2)

        Select Case ViewModel.Gpx.Lifelines.FiftyFiftyPosition
            Case 1
                ViewModel.Gpx.Lifelines.Lifeline1Active = 2
            Case 2
                ViewModel.Gpx.Lifelines.Lifeline2Active = 2
            Case 3
                ViewModel.Gpx.Lifelines.Lifeline3Active = 2
            Case 4
                ViewModel.Gpx.Lifelines.Lifeline4Active = 2
        End Select

    End Sub

    Private Sub PAF_1_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAF_1_Label.Click

        GuiContext.SomethingToDoWithLifeline(ViewModel.Gpx.Lifelines.PhoneAFriendPosition, 2)

        Select Case ViewModel.Gpx.Lifelines.PhoneAFriendPosition
            Case 1
                ViewModel.Gpx.Lifelines.Lifeline1Active = 2
            Case 2
                ViewModel.Gpx.Lifelines.Lifeline2Active = 2
            Case 3
                ViewModel.Gpx.Lifelines.Lifeline3Active = 2
            Case 4
                ViewModel.Gpx.Lifelines.Lifeline4Active = 2
        End Select

    End Sub

    Private Sub ATA_1_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ATA_1_Label.Click

        GuiContext.SomethingToDoWithLifeline(ViewModel.Gpx.Lifelines.AskTheAudiencePosition, 2)

        Select Case ViewModel.Gpx.Lifelines.AskTheAudiencePosition
            Case 1
                ViewModel.Gpx.Lifelines.Lifeline1Active = 2
            Case 2
                ViewModel.Gpx.Lifelines.Lifeline2Active = 2
            Case 3
                ViewModel.Gpx.Lifelines.Lifeline3Active = 2
            Case 4
                ViewModel.Gpx.Lifelines.Lifeline4Active = 2
        End Select

    End Sub

    Private Sub STQ_1_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STQ_1_Label.Click

        GuiContext.SomethingToDoWithLifeline(ViewModel.Gpx.Lifelines.SwitchTheQuestionPosition, 2)

        Select Case ViewModel.Gpx.Lifelines.SwitchTheQuestionPosition
            Case 1
                ViewModel.Gpx.Lifelines.Lifeline1Active = 2
            Case 2
                ViewModel.Gpx.Lifelines.Lifeline2Active = 2
            Case 3
                ViewModel.Gpx.Lifelines.Lifeline3Active = 2
            Case 4
                ViewModel.Gpx.Lifelines.Lifeline4Active = 2
        End Select

    End Sub
    Private Sub CorrectAnswerReveal_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CorrectAnswerReveal_Button.Click

        TimerQuestionRemove.Interval = Val(SecondsToDissolveAfterCorrectAnswer_TextBox.Text) * 1000
        Timer_STOP.Interval = 555
        Timer_SumeShow.Interval = 1140

        'If Instance.CurrentGamePlayLevels = "5" Then
        '    Timer_STOP.Start()

        'End If

        If Empty_CheckBox.Checked = True Then
            TimerQuestionRemove.Start()
        End If

        AnswerAappear_Label.BackColor = Color.Yellow
        AnswerBappear_Label.BackColor = Color.Yellow
        AnswerCappear_Label.BackColor = Color.Yellow
        AnswerDappear_Label.BackColor = Color.Yellow

        AremoveFF_Label.BackColor = Color.Yellow
        BremoveFF_Label.BackColor = Color.Yellow
        CremoveFF_Label.BackColor = Color.Yellow
        DremoveFF_Label.BackColor = Color.Yellow

        If ViewModel.Gpx.Question.IsGivenFinalAnswerCorrect = GivenAnswerStateEnum.Correct Then
            CorrectAnswerProcedure()
        ElseIf ViewModel.Gpx.Question.IsGivenFinalAnswerCorrect = GivenAnswerStateEnum.Incorrect Then
            IncorrectAnswerProcedure()
        Else
            Return
        End If

        If SumeShow_CheckBox.Checked = True Then
            Timer_SumeShow.Start()
        End If

        Select Case ViewModel.Gpx.Question.CorrectAnswer
            Case 1
                AnswerA_TextBox.BackColor = Color.Lime
            Case 2
                AnswerB_TextBox.BackColor = Color.Lime
            Case 3
                AnswerC_TextBox.BackColor = Color.Lime
            Case 4
                AnswerD_TextBox.BackColor = Color.Lime
        End Select

        '' ******* CASPARCG ******* CASPARCG *******
        GraphicsProcessingUnit.CorrectAnswer(ViewModel.Gpx.Question.CorrectAnswer)
        '' ******* CASPARCG ******* CASPARCG *******

        ViewModel.Gpx.MomentStatus = "CorrectAnswer_Fired"

    End Sub

    Sub Leveling()
        '' ******* CASPARCG ******* CASPARCG *******
        Dim MoneyTreeLevel As Int32 = ViewModel.Gpx.Question.LevelQ - 1
        GraphicsProcessingUnit.MoneyTreeLevel(MoneyTreeLevel)

        '' ******* CASPARCG ******* CASPARCG *******

        Dim qForSume, momentSume, incorrectSume As String
        If ViewModel.Gpx.Question.LevelQ >= 1 And ViewModel.Gpx.Question.LevelQ <= 15 Then

            qForSume = 0
            Dim TextBoxQfor As String = "QSum" + ViewModel.Gpx.Question.LevelQ.ToString + "_TextBox"
            For Each tb As TextBox In Me.Controls.OfType(Of TextBox)()
                If String.Compare(TextBoxQfor, tb.Name, True) = 0 Then
                    qForSume = tb.Text
                End If
            Next

            momentSume = 0
            Dim TextBoxSume As String = "QSum" + (MoneyTreeLevel).ToString + "_TextBox"
            For Each tb As TextBox In Me.Controls.OfType(Of TextBox)()
                If String.Compare(TextBoxSume, tb.Name, True) = 0 Then
                    momentSume = tb.Text
                End If
            Next

            Dim momentSumeInt As Integer = Integer.Parse(DirectCast(ViewModel.Gpx.CurrentGamePlayLevels.Where(Function(x) x.PositionLevel = MoneyTreeLevel).FirstOrDefault(), MainGameLevel).RealValueSume)

            incorrectSume = CalculateIncorrectAnswer(ViewModel.Gpx.Question.LevelQ)
            incorrectSume = IIf(incorrectSume = 0, 0, incorrectSume)

            Dim incorrectSumeInt As Integer = CalculateIncorrectAnswer(ViewModel.Gpx.Question.LevelQ, True) 'vo slucaj da ima bukvi brojki ili nesto vo stil '4 MILIONI' ja zema vistinskata vrednost-realvalue

            ViewModel.Gpx.CurentGameStatusData = $"{momentSume};{(incorrectSumeInt - momentSumeInt) * (-1)};{qForSume};{incorrectSume}"

        Else
            ViewModel.Gpx.CurentGameStatusData = "0;0;0;0"

        End If
    End Sub

    Private Sub WalkAwayQoppinion_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WalkAwayQoppinion_Label.Click

        ''IZMENA! Dodadeno zatoa sto ne vrakjase prasanje na GraphicsQuestion.vb
        ViewModel.Gpx.MomentStatus = "Question_AnswerD_Fired"
        GraphicsProcessingUnit.WalkAwayQoppinion()

    End Sub

    Private Sub Timer_STOP_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_STOP.Tick
        Timer_STOP.Interval = 300

        MainGameMusicLayerObj.StopHeartbeaLetsPlay()

        Timer_STOP.Stop()
        Timer_STOP.InitializeLifetimeService()
    End Sub

    Private Sub Timer_PAUSE_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_PAUSE.Tick

        MainGameMusicLayerObj.PauseHeartbeatMusic(ViewModel.Gpx.Question.LevelQ)

        Timer_PAUSE.Stop()
        Timer_PAUSE.InitializeLifetimeService()
    End Sub

    Private Sub Timer_PLAY_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_PLAY.Tick

        MainGameMusicLayerObj.PlayHeartbeatMusic(ViewModel.Gpx.Question.LevelQ)

        Timer_PLAY.Interval = 300

        Timer_PLAY.Stop()
        Timer_PLAY.InitializeLifetimeService()

    End Sub

    Private Sub ATAready_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ATAready_Label.Click
        Timer_PAUSE.Start()

        MainGameMusicLayerObj.PlayLifelineSound("ATA", "ASK")

        ATA_1_Label_Click(ATA_1_Label, Nothing)

        A_pub.Text = "0"
        B_pub.Text = "0"
        C_pub.Text = "0"
        D_pub.Text = "0"

        ViewModel.Gpx.MomentStatus = "AskAudience_Questioning" ''IZMENA!!!
        DataLayer.DisposeATAvoteData()

        GraphicsProcessingUnit.InteractiveWallScreenObj.AudienceAsking()

    End Sub

    Private Sub PAFCall_lbl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAFready_Label.Click
        Timer_PAUSE.Start()

        'LightDown.URL = "C:\WWTBAM Removable Disc\UK 2007\44.Phone a Friend.wav"
        MainGameMusicLayerObj.PlayLifelineSound("PAF", "CALL")

        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "QuestionAnswersFadeOUT")
        End If

        PAF_1_Label_Click(PAF_1_Label, Nothing)

        ViewModel.Gpx.MomentStatus = "PhoneFriend_Dialing" ''IZMENA!!!

    End Sub

    Private Sub Timer_SumeShow_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_SumeShow.Tick

        WonPrizeReveal_Button_Click(WonPrizeReveal_Button, Nothing)

        ViewModel.Gpx.MomentStatus = "WonPrize_Fired" ''IZMENA!!

        Timer_SumeShow.Stop()
        Timer_SumeShow.InitializeLifetimeService()

    End Sub

    Private Sub ShowHideAudienceGraph_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowHideAudienceGraph_Label.Click
        If GraphicsProcessingUnit.ShowHideATAGraph Then
            ShowHideAudienceGraph_Label.BackColor = Color.Blue
        Else
            ShowHideAudienceGraph_Label.BackColor = Color.Khaki
        End If
    End Sub

    Private Sub MoneyTreeSet_Label_Click(sender As Object, e As EventArgs) Handles MoneyTreeSet_Label.Click
        '' ******* CASPARCG *******

        GraphicsProcessingUnit.CGMoneyTreeDataSet(QSum1_TextBox.Text, QSum2_TextBox.Text, QSum3_TextBox.Text, QSum4_TextBox.Text, QSum5_TextBox.Text, QSum6_TextBox.Text, QSum7_TextBox.Text, QSum8_TextBox.Text, QSum9_TextBox.Text, QSum10_TextBox.Text, QSum11_TextBox.Text, QSum12_TextBox.Text, QSum13_TextBox.Text, QSum14_TextBox.Text, QSum15_TextBox.Text)
        GraphicsProcessingUnit.MoneyTreeSet()
        GraphicsProcessingUnit.MarkCGlifelines(ViewModel.Gpx.Lifelines.Lifeline1Active, ViewModel.Gpx.Lifelines.Lifeline2Active, ViewModel.Gpx.Lifelines.Lifeline3Active, ViewModel.Gpx.Lifelines.Lifeline4Active)

        MoneyTreeSet_Label.ForeColor = Color.Blue
        '' ******* CASPARCG ******* 

        GraphicsProcessingUnit.InteractiveWallScreenObj.AnyBackgroundLoop("ShowStartBackgroundLoop")

    End Sub

    Private Sub MoneyTreeShow_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoneyTreeShow_Label.Click
        '' ******* CASPARCG ******* CASPARCG *******
        GraphicsProcessingUnit.MoneyTreeFlyIn()
        '' ******* CASPARCG ******* CASPARCG *******
    End Sub

    Private Sub MoneyTreeHide_Label_Click(sender As Object, e As EventArgs) Handles MoneyTreeHide_Label.Click
        '' ******* CASPARCG ******* CASPARCG *******
        GraphicsProcessingUnit.MoneyTreeFlyOut()
        '' ******* CASPARCG ******* CASPARCG *******
    End Sub

    Private Sub Qfor_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QFor_Button.Click
        GraphicsProcessingUnit.QuestionForLozenge(ViewModel.Gpx.QuestionForSume)

    End Sub

    Private Sub AbcdHex_Show()
        ViewModel.Gpx.MomentStatus = "ABCDHex_Show"
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "QuestionAnswersFlyIN")
        End If
    End Sub

    Private Sub NextThing_Button_Click(sender As Object, e As EventArgs) Handles NextThing_Button.Click
        ''IZMENA!!! Celosna Izmena; Nov Label - NextThing_Label
        'MessageBox.Show(Instance.MomentStatus)

        If ViewModel.Gpx.MomentStatus = "QuestionAnswers_Load" Or ViewModel.Gpx.MomentStatus = "VariableMilestone_Set" Then
            QuestionAppear()
            Exit Sub
        End If

        If ViewModel.Gpx.MomentStatus = "Question_Fired" Then
            AnswerAappear_Label_Click(AnswerAappear_Label, Nothing)
            'AbcdHex_Show()
            Exit Sub
        End If

        If ViewModel.Gpx.MomentStatus = "ABCDHex_Show" And LimitedGame_RadioButton.Checked = False Then
            AnswerAappear_Label_Click(AnswerAappear_Label, Nothing)
            Exit Sub
        End If

        If ViewModel.Gpx.MomentStatus = "ABCDHex_Show" And LimitedGame_RadioButton.Checked = True And Val(ViewModel.Gpx.Question.LevelQ) <= 10 Then
            AnswersABCDappear_Label_Click(AnswersABCDappear_Label, Nothing)
            Exit Sub
        End If

        If ViewModel.Gpx.MomentStatus = "Question_AnswerA_Fired" Then
            AnswerBappear_Label_Click(AnswerBappear_Label, Nothing)
            Exit Sub
        End If

        If ViewModel.Gpx.MomentStatus = "Question_AnswerB_Fired" Then
            AnswerCappear_Label_Click(AnswerCappear_Label, Nothing)
            Exit Sub
        End If

        If ViewModel.Gpx.MomentStatus = "Question_AnswerC_Fired" Then
            AnswerDappear_Label_Click(AnswerDappear_Label, Nothing)
            Exit Sub
        End If

        If ViewModel.Gpx.MomentStatus = "AnswerA_Final_Fired" Then
            CorrectAnswerReveal_Button_Click(CorrectAnswerReveal_Button, Nothing)
            Exit Sub
        End If

        If ViewModel.Gpx.MomentStatus = "AnswerB_Final_Fired" Then
            CorrectAnswerReveal_Button_Click(CorrectAnswerReveal_Button, Nothing)
            Exit Sub
        End If

        If ViewModel.Gpx.MomentStatus = "AnswerC_Final_Fired" Then
            CorrectAnswerReveal_Button_Click(CorrectAnswerReveal_Button, Nothing)
            Exit Sub
        End If

        If ViewModel.Gpx.MomentStatus = "AnswerD_Final_Fired" Then
            CorrectAnswerReveal_Button_Click(CorrectAnswerReveal_Button, Nothing)
            Exit Sub
        End If

        If ViewModel.Gpx.MomentStatus = "DoubleDip_Final_Fired" Then
            DoubleDipRevealCorrect_Button_Click(DoubleDipRevealCorrect_Button, Nothing)
            Exit Sub
        End If

        If ViewModel.Gpx.MomentStatus = "CorrectAnswer_Fired" Then
            WonPrizeReveal_Button_Click(WonPrizeReveal_Button, Nothing)
            Exit Sub
        End If

        If ViewModel.Gpx.MomentStatus = "WonPrize_Fired" Then
            RemoveQuestion_Button_Click(RemoveQuestion_Button, Nothing)
            Exit Sub
        End If

        If ViewModel.Gpx.MomentStatus = "PhoneFriend_Dialing" Then
            PAFstart_Label_Click(PAFstart_Label, Nothing)
            Exit Sub
        End If

        If ViewModel.Gpx.MomentStatus = "PhoneFriend_Progress" Then
            PAFabort_Label_Click(PAFabort_Label, Nothing)
            Exit Sub
        End If

        If ViewModel.Gpx.MomentStatus = "AskAudience_Questioning" Then
            ATAstart_Label_Click(ATAstart_Label, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub CGConnection_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CGConnection_Button.Click
        Task.Run(Function()
                     GraphicsProcessingUnit.ConnectCG(CasparServerIP_TextBox.Text, CASPARCGLog_TextBox)
                     System.Threading.Thread.Sleep(1000)
                     MoneyTreeSet_Label_Click(MoneyTreeSet_Label, Nothing)
                 End Function)
    End Sub

    Private Sub ATAendVote_Label_Click(sender As Object, e As EventArgs) Handles ATAendVote_Label.Click
        ataEndVote()
        GraphicsProcessingUnit.InteractiveWallScreenObj.MotionBackgroundDuringQuestion(ViewModel.Gpx.Question.LevelQ)

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)
        ViewModel.Gpx.Question.Explanation = ""
        '(toGoToClient)Host.DirectorsChat_TextBox.Text = Instance.Quesion.Explanation
    End Sub

    Private Sub LimitedGame_RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles LimitedGame_RadioButton.CheckedChanged
        LimitedGameClockRestart_Label.Visible = True
        LimitedGameClockStop_Label.Visible = True
        LimitedGameClock_TextBox.Visible = True
        AnswersABCDappear_Label.Visible = True

    End Sub

    Private Sub UnlimitedGame_RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles UnlimitedGame_RadioButton.CheckedChanged
        LimitedGameClockRestart_Label.Visible = False
        LimitedGameClockStop_Label.Visible = False
        LimitedGameClock_TextBox.Visible = False
        AnswersABCDappear_Label.Visible = False


    End Sub



    Public Sub limitedClockStart()

        MainGameMusicLayerObj.LimitedClockPlay() 'Ctlcontrols.play()

        TimerLimitedGameSecDropClock.Start()


        If ViewModel.Gpx.Question.LevelQ <= 5 Then
            LimitedGameClock_TextBox.Text = "15"
        End If
        If ViewModel.Gpx.Question.LevelQ > 5 Then
            LimitedGameClock_TextBox.Text = "30"
        End If


        ''''
        GraphicsProcessingUnit.showClockBarCG(LimitedGameClock_TextBox.Text)
        ''''

    End Sub

    Public Sub limitedClockSecDrop()
        LimitedGameClock_TextBox.Text = LimitedGameClock_TextBox.Text - 1

        If LimitedGameClock_TextBox.Text = 0 Then
            TimerLimitedGameSecDropClock.Stop()
            MainGameMusicLayerObj.LimitedClockStop() 'Ctlcontrols.stop()
        End If

        If ViewModel.Gpx.Question.LevelQ <= 5 Then
            '' ******* CASPARCG ******* CASPARCG *******
            Dim fullClock As Integer = 15
            Dim remainingClock As Integer = 0
            remainingClock = fullClock - Int(LimitedGameClock_TextBox.Text)

            If GraphicsProcessingUnit.casparQA.IsConnected Then
                ''caspar_.Channels(0).CG.Invoke(1, "show1outOf15sClock")
                GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "show" + remainingClock.ToString + "outOf" + "15" + "sClock")
            End If
            '' ******* CASPARCG ******* CASPARCG *******

        End If
        If ViewModel.Gpx.Question.LevelQ > 5 Then
            '' ******* CASPARCG ******* CASPARCG *******
            Dim fullClock As Integer = 30
            Dim remainingClock As Integer = 0
            remainingClock = fullClock - Int(LimitedGameClock_TextBox.Text)

            ''MessageBox.Show("show" + remainingClock.ToString + "outOf" + "30" + "sClock")

            If GraphicsProcessingUnit.casparQA.IsConnected Then
                ''caspar_.Channels(0).CG.Invoke(1, "show1outOf30sClock")
                GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "show" + remainingClock.ToString + "outOf" + "30" + "sClock")
            End If
            '' ******* CASPARCG ******* CASPARCG *******

        End If

    End Sub

    Private Sub AnswersABCDappear_Label_Click(sender As Object, e As EventArgs) Handles AnswersABCDappear_Label.Click
        AnswerAappear_Label_Click(AnswerAappear_Label, Nothing)
        AnswerBappear_Label_Click(AnswerBappear_Label, Nothing)
        AnswerCappear_Label_Click(AnswerCappear_Label, Nothing)
        AnswerDappear_Label_Click(AnswerDappear_Label, Nothing)

        limitedClockStart()

        AnswersABCDappear_Label.BackColor = Color.Blue
        TimerABCDyellowLabels.Start()

    End Sub

    Private Sub TimerLimitedGameSecDropClock_Tick(sender As Object, e As EventArgs) Handles TimerLimitedGameSecDropClock.Tick
        limitedClockSecDrop()

    End Sub

    Private Sub LimitedGameClockStop_Label_Click(sender As Object, e As EventArgs) Handles LimitedGameClockStop_Label.Click
        TimerLimitedGameSecDropClock.Stop()
        MainGameMusicLayerObj.LimitedClockStop() 'Ctlcontrols.stop()

        If LimitedGame_RadioButton.Checked Then
            My.Computer.Audio.Play("C:\WWTBAM Removable Disc\US 2008\Stop The Clock.wav", AudioPlayMode.Background)
        End If

        ''''
        GraphicsProcessingUnit.hideClockBarCG(ViewModel.Gpx.Question.LevelQ, LimitedGame_RadioButton.Checked)
        ''''

    End Sub

    Private Sub LimitedGameClockRestart_Label_Click(sender As Object, e As EventArgs) Handles LimitedGameClockRestart_Label.Click
        TimerLimitedGameSecDropClock.Start() 'Ctlcontrols.play()

        My.Computer.Audio.Play("C:\WWTBAM Removable Disc\US 2008\Resume The Clock.wav", AudioPlayMode.Background)

        MainGameMusicLayerObj.LimitedClockPlay() 'Ctlcontrols.play()

        ''''
        GraphicsProcessingUnit.showClockBarCG(ViewModel.Gpx.Question.LevelQ)
        ''''

    End Sub

    Private Sub SoundLX_Button_Click(sender As Object, e As EventArgs) Handles SoundLX_Button.Click
        PlayLXsound()
        GraphicsProcessingUnit.InteractiveWallScreenObj.LightDownEffect(ViewModel.Gpx.Question.LevelQ)
    End Sub

    Public Sub PlayLXsound()
        Timer_STOP.Stop()
        MainGameMusicLayerObj.PlayLXSound(ViewModel.Gpx.Question.LevelQ, VariableMilestone_TextBox.Text)

    End Sub

    Private Sub QuizOperator_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown, WonPrizeReveal_Button.KeyDown, NextThing_Button.KeyDown, RemoveQuestion_Button.KeyDown, CorrectAnswerReveal_Button.KeyDown, FinalD_Button.KeyDown, FinalC_Button.KeyDown, FinalB_Button.KeyDown, FinalA_Button.KeyDown, SoundLX_Button.KeyDown
        If e.KeyCode = Keys.N Then
            NextThing_Button_Click(NextThing_Button, Nothing)
        End If
        If e.KeyCode = Keys.Q Then
            QuestionAppear()
        End If
        If e.KeyCode = Keys.D1 Then
            FinalA_Button_Click(FinalA_Button, Nothing)
        End If
        If e.KeyCode = Keys.D2 Then
            FinalB_Button_Click(FinalB_Button, Nothing)
        End If
        If e.KeyCode = Keys.D3 Then
            FinalC_Button_Click(FinalC_Button, Nothing)
        End If
        If e.KeyCode = Keys.D4 Then
            FinalD_Button_Click(FinalD_Button, Nothing)
        End If
        If e.KeyCode = Keys.R Then
            CorrectAnswerReveal_Button_Click(CorrectAnswerReveal_Button, Nothing)
        End If
        If e.KeyCode = Keys.S Then
            SoundLX_Button_Click(SoundLX_Button, Nothing)
        End If
        If e.KeyCode = Keys.Escape Then
            LimitedGameClockStop_Label_Click(LimitedGameClockStop_Label, Nothing)
        End If
        If (e.KeyCode And Not Keys.Modifiers) = Keys.Escape AndAlso e.Modifiers = Keys.Control Then
            LimitedGameClockRestart_Label_Click(LimitedGameClockRestart_Label, Nothing)
        End If

    End Sub

    Private Sub ExplanationClear_Label_Click(sender As Object, e As EventArgs) Handles ClearExplanation_Label.Click
        HostContPresentationLayer.OneTimeMessageSet("EXPLANATION-NONE")
        'HostContPresentationLayer.Instance.Quesion.ExplanationDissolve()
        'HostContPresentationLayer.Instance.Quesion.PronunciationDissolve()
    End Sub

    Private Sub Explanation_Label_Click(sender As Object, e As EventArgs) Handles SendExplanation_Label.Click
        HostContPresentationLayer.OneTimeMessageSet("EXPLANATION-FIRE")
        'HostContPresentationLayer.Instance.Quesion.ExplanationFire()
        'HostContPresentationLayer.Instance.Quesion.PronunciationFire()
    End Sub

    Private Sub VariableMilestoneSet_Button_Click(sender As Object, e As EventArgs) Handles VariableMilestoneSet_Button.Click
        VariableMilestone_TextBox.Text = ViewModel.Gpx.Question.LevelQ
        My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\29.QMilestone - Change.wav")

        Dim FirstMileStone As Short = 0
        If ViewModel.Gpx.CurrentGamePlayLevels.Where(Function(x) x.PositionLevel > 0).FirstOrDefault() IsNot Nothing Then
            FirstMileStone = ViewModel.Gpx.CurrentGamePlayLevels.Where(Function(x) x.PositionLevel > 0).FirstOrDefault().PositionLevel
        End If

        Dim TextBox As String 'Izbeli gi prvo
        For index As Integer = FirstMileStone To ViewModel.Gpx.CurrentGamePlayLevels.Count - 1
            TextBox = "QSum" + index.ToString + "_TextBox"
            For Each tb As TextBox In Me.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    tb.BackColor = Color.White
                End If
            Next
        Next

        'obelezi vtora sigurna
        For Each level As IMainGameLevel In ViewModel.Gpx.CurrentGamePlayLevels
            If level.PositionLevel = ViewModel.Gpx.Question.LevelQ Then
                level.SafeHeaven = True
                Exit For
            End If
        Next

        TextBox = "QSum" + ViewModel.Gpx.Question.LevelQ + "_TextBox"
        For Each tb As TextBox In Me.Controls.OfType(Of TextBox)()
            If String.Compare(TextBox, tb.Name, True) = 0 Then
                tb.BackColor = Color.Bisque
                GraphicsProcessingUnit.setSecondMilestoneAtQ(ViewModel.Gpx.Question.LevelQ)
                '' ******* CASPARCG ******* CASPARCG *******
                Exit For
            End If
        Next

        ViewModel.Gpx.MomentStatus = "VariableMilestone_Set"

    End Sub

    Private Sub MoneyTreeHop_Button_Click(sender As Object, e As EventArgs) Handles MoneyTreeHop_Button.Click
        MoneyTreeMoment_Textbox.Text += Val(MoneyTreeHop_TextBox.Text)
        If Val(MoneyTreeMoment_Textbox.Text) > 16 Then
            MoneyTreeMoment_Textbox.Text = "0"
        End If
        '' ******* CASPARCG ******* CASPARCG *******
        Dim MoneyTreeLevel As Int32 = Val(MoneyTreeMoment_Textbox.Text)
        GraphicsProcessingUnit.MoneyTreeLevel(MoneyTreeLevel)

        '' ******* CASPARCG ******* CASPARCG *******
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        If CorrectAnswer_TextBox.Visible = False Then
            CorrectAnswer_TextBox.Visible = True
            MoreQuestionInfoForHost_TextBox.Visible = True
        Else
            CorrectAnswer_TextBox.Visible = False
            MoreQuestionInfoForHost_TextBox.Visible = False
        End If
    End Sub

    Private Sub FiftyFiftyOptionsReset_Label_Click(sender As Object, e As EventArgs) Handles FiftyFiftyOptionsReset_Label.Click
        AremoveFF_Label.BackColor = Color.Yellow
        BremoveFF_Label.BackColor = Color.Yellow
        CremoveFF_Label.BackColor = Color.Yellow
        DremoveFF_Label.BackColor = Color.Yellow

    End Sub

    Private Sub Quiz_Operator_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        FastestFingerManaging.DisconnectFFFDevices("")
    End Sub

    Private Sub CasparServerIP_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CasparServerIP_TextBox.TextChanged
        My.Settings.casparHostName = CasparServerIP_TextBox.Text

    End Sub

    Private Sub DirectorIMessage_Button_Click(sender As Object, e As EventArgs) Handles DirectorIMessage_Button.Click
        If Not String.IsNullOrEmpty(DirectorIMessage_TextBox.Text.Trim) Then
            DirectorIMessages_TextBox.Text += DirectorIMessage_TextBox.Text + vbCrLf
            DirectorIMessage_TextBox.Text = ""
            HostContPresentationLayer.GetDirectorChat(DirectorIMessages_TextBox.Text, DirectorBlinkIMessage_TextBox.Text)
        Else
            DirectorIMessages_TextBox.Text = ""
            HostContPresentationLayer.DirectorChatReset()
            Return
        End If

    End Sub

    Private Sub Contestant_Textbox_TextChanged(sender As Object, e As EventArgs) Handles ContestantName_Textbox.TextChanged, ContestantLastName_Textbox.TextChanged, ContestantCity_Textbox.TextChanged, ContestantPartner_Textbox.TextChanged
        'HostContPresentationLayer.ContestantLoad(ContestantName_Textbox.Text, ContestantLastName_Textbox.Text, ContestantCity_Textbox.Text, ContestantPartner_Textbox.Text)

    End Sub

    Private Sub FFFStart_Label_Click(sender As Object, e As EventArgs) Handles FFFStart_Label.Click
        FFFOperator.Show()

    End Sub

    Private Sub Lifeline1_PictureBox_Click(sender As Object, e As EventArgs) Handles Lifeline1_PictureBox.Click
        GuiContext.PositionLifelineTab(1)
    End Sub

    Private Sub Lifeline2_PictureBox_Click(sender As Object, e As EventArgs) Handles Lifeline2_PictureBox.Click
        GuiContext.PositionLifelineTab(2)
    End Sub

    Private Sub Lifeline3_PictureBox_Click(sender As Object, e As EventArgs) Handles Lifeline3_PictureBox.Click
        GuiContext.PositionLifelineTab(3)
    End Sub

    Private Sub Lifeline4_PictureBox_Click(sender As Object, e As EventArgs) Handles Lifeline4_PictureBox.Click
        GuiContext.PositionLifelineTab(4)
    End Sub

    Private Sub FiftyFifty_PictureBox_Click(sender As Object, e As EventArgs) Handles FiftyFifty_PictureBox.Click, PAF_PictureBox.Click, ATA_PictureBox.Click, SwitchQ_PictureBox.Click
        Me.TabControl2.SelectedTab = TabPage6
    End Sub

    Private Sub LevelQset_ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LevelQset_ComboBox.SelectedIndexChanged
        ViewModel.Gpx.Question.LevelQ = Short.Parse(LevelQset_ComboBox.SelectedItem.ToString)
    End Sub

    Private Sub TenthQMilestoneSet_Button_Click(sender As Object, e As EventArgs) Handles TenthQMilestoneSet_Button.Click
        VariableMilestone_TextBox.Text = 10

        Dim TextBox As String 'Izbeli gi prvo
        For index As Integer = 6 To 15 - 1
            TextBox = "QSum" + index.ToString + "_TextBox"
            For Each tb As TextBox In Me.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    tb.BackColor = Color.White
                End If
            Next
        Next

        TextBox = "QSum" + "10" + "_TextBox"
        For Each tb As TextBox In Me.Controls.OfType(Of TextBox)()
            If String.Compare(TextBox, tb.Name, True) = 0 Then
                tb.BackColor = Color.Bisque
                '' ******* CASPARCG ******* CASPARCG *******
                GraphicsProcessingUnit.setSecondMilestoneAtQ("10")

            End If
        Next

        ViewModel.Gpx.MomentStatus = "VariableMilestone_Set"

    End Sub

    Private Sub DoubleDipReady_Label_Click(sender As Object, e As EventArgs) Handles DoubleDipReady_Label.Click
        ViewModel.Gpx.DoubleDipState = "DoubleDipFirstFinal"
        MainGameMusicLayerObj.PlayDoubleDipBackground()
        DDIP_1_Label_Click(DDIP_1_Label, Nothing)

        GraphicsProcessingUnit.InteractiveWallScreenObj.AnyBackgroundLoop("MotionBackgroundPulsingCircles")
    End Sub

    Private Sub DoubleDipCancel_Label_Click(sender As Object, e As EventArgs) Handles DoubleDipCancel_Label.Click
        ViewModel.Gpx.DoubleDipState = ""
        MainGameMusicLayerObj.StopDoubleDipBackground() 'Ctlcontrols.stop()
        DDIP_0_Label_Click(DDIP_0_Label, Nothing)
        'MainGameMusicLayerObj.PlayHeartbeatMusic()
    End Sub

    Private Sub DoubleDipRevealCorrect_Button_Click(sender As Object, e As EventArgs) Handles DoubleDipRevealCorrect_Button.Click

        ViewModel.Gpx.MomentStatus = "DoubleDipIsFirstInstance.Quesion.FinalAnswer_Correct_Fired"

        DDIP_X_Label_Click(DDIP_X_Label, Nothing)

        If (ViewModel.Gpx.Question.CorrectAnswer <> ViewModel.Gpx.Question.FinalAnswer) And String.Equals(ViewModel.Gpx.DoubleDipState, "DoubleDipFirstFinal", StringComparison.OrdinalIgnoreCase) Then
            Select Case ViewModel.Gpx.Question.FinalAnswer
                Case 1
                    AnswerA_TextBox.BackColor = Color.Gray
                Case 2
                    AnswerB_TextBox.BackColor = Color.Gray
                Case 3
                    AnswerC_TextBox.BackColor = Color.Gray
                Case 4
                    AnswerD_TextBox.BackColor = Color.Gray
            End Select

            IncorrectAnswerProcedure()
            ''CASPARCG
            GraphicsProcessingUnit.DobleDip(ViewModel.Gpx.Question.FinalAnswer)
            ''
            ViewModel.Gpx.DoubleDipState = "DoubleDipSecondFinal"

            SumeShow_CheckBox.Checked = True
            Empty_CheckBox.Checked = True

        ElseIf (ViewModel.Gpx.Question.CorrectAnswer <> ViewModel.Gpx.Question.FinalAnswer) Then
            CorrectAnswerReveal_Button_Click(CorrectAnswerReveal_Button, Nothing)

        ElseIf String.Equals(ViewModel.Gpx.Question.CorrectAnswer, ViewModel.Gpx.Question.FinalAnswer, StringComparison.OrdinalIgnoreCase) Then
            CorrectAnswerReveal_Button_Click(CorrectAnswerReveal_Button, Nothing)
        End If

        Me.TabControl2.SelectedTab = TabPage6

    End Sub

    Private Sub DoubleDipFinalA_Button_Click(sender As Object, e As EventArgs) Handles DoubleDipFinalA_Button.Click
        FinalA_Button_Click(FinalA_Button, Nothing)
        ViewModel.Gpx.MomentStatus = "DoubleDipAnswer_Final_Fired"
        ViewModel.Gpx.DoubleDipFirstAnswer = "1"
    End Sub
    Private Sub DoubleDipFinalB_Button_Click(sender As Object, e As EventArgs) Handles DoubleDipFinalB_Button.Click
        FinalB_Button_Click(FinalB_Button, Nothing)
        ViewModel.Gpx.MomentStatus = "DoubleDipAnswer_Final_Fired"
        ViewModel.Gpx.DoubleDipFirstAnswer = "2"
    End Sub
    Private Sub DoubleDipFinalC_Button_Click(sender As Object, e As EventArgs) Handles DoubleDipFinalC_Button.Click
        FinalC_Button_Click(FinalC_Button, Nothing)
        ViewModel.Gpx.MomentStatus = "DoubleDipAnswer_Final_Fired"
        ViewModel.Gpx.DoubleDipFirstAnswer = "3"
    End Sub
    Private Sub DoubleDipFinalD_Button_Click(sender As Object, e As EventArgs) Handles DoubleDipFinalD_Button.Click
        FinalD_Button_Click(FinalD_Button, Nothing)
        ViewModel.Gpx.MomentStatus = "DoubleDipAnswer_Final_Fired"
        ViewModel.Gpx.DoubleDipFirstAnswer = "4"
    End Sub

    Private Sub DDIP_0_Label_Click(sender As Object, e As EventArgs) Handles DDIP_0_Label.Click
        GuiContext.SomethingToDoWithLifeline(ViewModel.Gpx.Lifelines.DoubleDipPosition, 1)
        ViewModel.Gpx.Lifelines.Lifeline2Active = 1
    End Sub

    Private Sub DDIP_1_Label_Click(sender As Object, e As EventArgs) Handles DDIP_1_Label.Click
        GuiContext.SomethingToDoWithLifeline(ViewModel.Gpx.Lifelines.DoubleDipPosition, 2)
        ViewModel.Gpx.Lifelines.Lifeline2Active = 2
    End Sub

    Private Sub DDIP_X_Label_Click(sender As Object, e As EventArgs) Handles DDIP_X_Label.Click
        GuiContext.SomethingToDoWithLifeline(ViewModel.Gpx.Lifelines.DoubleDipPosition, 0)
        ViewModel.Gpx.Lifelines.Lifeline2Active = 0
    End Sub

    Public ReadOnly Property NextActivity() As String
        Get
            'Select Case Instance.MomentStatus
            '    Case ""
            '        Return ""
            '    Case Else
            '        Return ""
            'End Select
            'Return ""
            If ViewModel.Gpx.MomentStatus = "QuestionAnswers_Load" Or ViewModel.Gpx.MomentStatus = "VariableMilestone_Set" Then
                Return "Question_Fired"
            End If
            If ViewModel.Gpx.MomentStatus = "Question_Fired" Then
                Return "Question_AnswerA_Fired"
            End If
            If ViewModel.Gpx.MomentStatus = "ABCDHex_Show" And LimitedGame_RadioButton.Checked = False Then
                Return "Question_AnswerA_Fired"
            End If
            If ViewModel.Gpx.MomentStatus = "ABCDHex_Show" And LimitedGame_RadioButton.Checked = True And Val(ViewModel.Gpx.Question.LevelQ) <= 10 Then
                'AnswersABCDappear_Label_Click(AnswersABCDappear_Label, Nothing)
                Return "Question_AnswerA_Fired"
            End If
            If ViewModel.Gpx.MomentStatus = "Question_AnswerA_Fired" Then
                Return "Question_AnswerB_Fired"
            End If
            If ViewModel.Gpx.MomentStatus = "Question_AnswerB_Fired" Then
                Return "Question_AnswerC_Fired"
            End If
            If ViewModel.Gpx.MomentStatus = "Question_AnswerC_Fired" Then
                Return "Question_AnswerD_Fired"
            End If
            If ViewModel.Gpx.MomentStatus = "AnswerA_Final_Fired" Then
                Return "CorrectAnswer_Fired"
            End If
            If ViewModel.Gpx.MomentStatus = "AnswerB_Final_Fired" Then
                Return "CorrectAnswer_Fired"
            End If
            If ViewModel.Gpx.MomentStatus = "AnswerC_Final_Fired" Then
                Return "CorrectAnswer_Fired"
            End If
            If ViewModel.Gpx.MomentStatus = "AnswerD_Final_Fired" Then
                Return "CorrectAnswer_Fired"
            End If
            If ViewModel.Gpx.MomentStatus = "DoubleDip_Final_Fired" Then
                'DoubleDipRevealCorrect_Button_Click(DoubleDipRevealCorrect_Button, Nothing)
            End If
            If ViewModel.Gpx.MomentStatus = "CorrectAnswer_Fired" Then
                'WonPrizeReveal_Button_Click(WonPrizeReveal_Button, Nothing)
                Return "WonPrize_Fired"
            End If
            If ViewModel.Gpx.MomentStatus = "WonPrize_Fired" Then
                Return "QuestionAnswers_Load"
            End If
            If ViewModel.Gpx.MomentStatus = "PhoneFriend_Dialing" Then
                'PAFstart_Label_Click(PAFstart_Label, Nothing)
            End If
            If ViewModel.Gpx.MomentStatus = "PhoneFriend_Progress" Then
                'PAFabort_Label_Click(PAFabort_Label, Nothing)
            End If
            If ViewModel.Gpx.MomentStatus = "AskAudience_Questioning" Then
                'ATAstart_Label_Click(ATAstart_Label, Nothing)
            End If
            Return ""
        End Get
    End Property

    Private Sub SoundStop_Button_Click(sender As Object, e As EventArgs) Handles SoundStop_Button.Click
        MainGameMusicLayerObj.StopAll()
    End Sub

    Private Sub SoundPlay_Button_Click(sender As Object, e As EventArgs) Handles SoundPlay_Button.Click
        Try
            Dim soundToPlay As Xml2CSharp.SOUND = MusicList_ComboBox.SelectedItem
            MainGameMusicLayerObj.PlayArbitrarySound(soundToPlay)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public m_IsGameGoingLive As Boolean = True
    Public Property IsGameGoingLive As Boolean
        Get
            Return m_IsGameGoingLive
        End Get
        Set(value As Boolean)
            m_IsGameGoingLive = value
            LiveRehearselViewButton(m_IsGameGoingLive)
        End Set
    End Property
    Private Sub RehearselLiveLock_Button_Click(sender As Object, e As EventArgs) Handles RehearselLiveLock_Button.Click
        If IsGameGoingLive Then
            IsGameGoingLive = False
        Else
            IsGameGoingLive = True
        End If
    End Sub
    Private Sub LiveRehearselViewButton(m_IsGameGoingLive As Boolean)
        If m_IsGameGoingLive Then
            RehearselLiveLock_Button.BackColor = Color.Red
            RehearselLiveLock_Button.Text = "YOU ARE LIVE"
        Else
            RehearselLiveLock_Button.BackColor = Color.Silver
            RehearselLiveLock_Button.Text = "REHEARSEL TIME"
        End If
    End Sub

    Private Sub SoundMute_Button_Click(sender As Object, e As EventArgs) Handles SoundMute_Button.Click
        If SoundMute_Button.BackColor = Color.Gainsboro Then
            SoundMute_Button.BackColor = Color.Red
            MainGameMusicLayerObj.Mute = True
            MainGameMusicLayerObj.StopAll()
        Else
            SoundMute_Button.BackColor = Color.Gainsboro
            MainGameMusicLayerObj.Mute = False
        End If

    End Sub

    Private Sub QuestionText_Event(sender As Object, e As MouseEventArgs) Handles Question_TextBox.MouseDoubleClick
        ViewModel.Gpx.Question.QuestionText = Question_TextBox.Text
    End Sub

    Private Sub StarApacheServer_Button_Click(sender As Object, e As EventArgs) Handles StarApacheServer_Button.Click
        Try
            Dim proc As Process = Process.Start("C:\\Users\\mihai\\Documents\\laragon-lite\\laragon.exe")
            'https://stackoverflow.com/questions/9679375/run-an-exe-from-c-sharp-code
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ReloadConfiguration_Button_Click(sender As Object, e As EventArgs) Handles ReloadConfiguration_Button.Click
        LoadConfiguration()
        HostContPresentationLayer.OneTimeMessageSet("CONFIGURATION-RESET")
    End Sub

    Dim ContestantClickStateID As String = ""
    Private Sub GetContestantClicks_CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles GetContestantClicks_CheckBox.CheckedChanged

        HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostContestantClickData.php?ClickType=-1&ClickValue=-1")

        If GetContestantClicks_CheckBox.Checked Then
            Task.Run(Sub()
                         While GetContestantClicks_CheckBox.Checked
                             If Me.InvokeRequired Then
                                 Me.Invoke(New Action(AddressOf GetContestantClicks))
                             Else
                                 GetContestantClicks()
                             End If
                             System.Threading.Thread.Sleep(500)
                         End While
                     End Sub)
        End If

    End Sub

    Sub GetContestantClicks()
        Dim ContClicksText As String = HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/GetContestantClickData.php")
        Dim ContClicksReader As System.IO.TextReader = New System.IO.StringReader(ContClicksText)

        Dim serializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(Xml2CSharp.WWTBAMCONTESTANTCLICKSDATA))
        Dim WwtbamClicks As Xml2CSharp.WWTBAMCONTESTANTCLICKSDATA
        WwtbamClicks = serializer.Deserialize(ContClicksReader)

        If Not String.Equals(ContestantClickStateID, WwtbamClicks.STATEID, StringComparison.OrdinalIgnoreCase) Then
            ContestantClickStateID = WwtbamClicks.STATEID

            If String.Equals("FINALANSWER", WwtbamClicks.CLICKTYPE, StringComparison.OrdinalIgnoreCase) Then
                Select Case WwtbamClicks.CLICKVALUE
                    Case "1"
                        FinalA_Button_Click(FinalA_Button, Nothing)
                    Case "2"
                        FinalB_Button_Click(FinalB_Button, Nothing)
                    Case "3"
                        FinalC_Button_Click(FinalC_Button, Nothing)
                    Case "4"
                        FinalD_Button_Click(FinalD_Button, Nothing)
                End Select
            End If
        End If
    End Sub

End Class