Imports System.Threading.Tasks

Public Class Quiz_Operator
    Dim RulesQcount As Integer

    Dim defaultATAvoteTime As String = 6

    Public Shared finalAnswerGiven As Boolean = False

    Public Shared SwitchedQuestion As Integer
    Public Shared questionID As String

    Public MainGameMusicLayerObj As New MainGameMusicLayer

#Region "PROPERTIЕS"
    Dim _MomentStatus As String = ""
    Public Property MomentStatus() As String
        Get
            Return _MomentStatus
        End Get
        Set(value As String)
            _MomentStatus = value
            MomentStatusCUEorder_TextBox.Text = value
            HostContPresentationLayer.GamePlayStateSet(value)
            GUIOperatorStateSet(_MomentStatus)
        End Set
    End Property

    Dim LifelinesState1 As String = ""
    Public ReadOnly Property LifelinesState As String
        Get
            Dim AllLifelines As String = String.Format("{0};{1};{2};{3}", Helpers.ConvertLifelineStateToReadable(Lifeline1Active), Helpers.ConvertLifelineStateToReadable(Lifeline2Active), Helpers.ConvertLifelineStateToReadable(Lifeline3Active), Helpers.ConvertLifelineStateToReadable(Lifeline4Active))
            Return AllLifelines
        End Get
    End Property

    Dim lifeline1Active1 As Short = 1
    Public Property Lifeline1Active As Short
        Get
            Return lifeline1Active1
        End Get
        Set(value As Short)
            lifeline1Active1 = value
            GraphicsProcessingUnit.MarkCGlifelines(lifeline1Active1, Lifeline2Active, Lifeline3Active, Lifeline4Active)
            HostContPresentationLayer.GamePlayStateSet("LIFELINE_UPDATE")
        End Set
    End Property

    Dim lifeline2Active1 As Short = 1
    Public Property Lifeline2Active As Short
        Get
            Return lifeline2Active1
        End Get
        Set(value As Short)
            lifeline2Active1 = value
            GraphicsProcessingUnit.MarkCGlifelines(lifeline1Active1, Lifeline2Active, Lifeline3Active, Lifeline4Active)
            HostContPresentationLayer.GamePlayStateSet("LIFELINE_UPDATE")
        End Set
    End Property

    Dim lifeline3Active1 As Short = 1
    Public Property Lifeline3Active As Short
        Get
            Return lifeline3Active1
        End Get
        Set(value As Short)
            lifeline3Active1 = value
            GraphicsProcessingUnit.MarkCGlifelines(lifeline1Active1, Lifeline2Active, Lifeline3Active, Lifeline4Active)
            HostContPresentationLayer.GamePlayStateSet("LIFELINE_UPDATE")
        End Set
    End Property

    Dim lifeline4Active1 As Short = 1
    Public Property Lifeline4Active As Short
        Get
            Return lifeline4Active1
        End Get
        Set(value As Short)
            lifeline4Active1 = value
            GraphicsProcessingUnit.MarkCGlifelines(lifeline1Active1, Lifeline2Active, Lifeline3Active, Lifeline4Active)
            HostContPresentationLayer.GamePlayStateSet("LIFELINE_UPDATE")
        End Set
    End Property

    Dim doubleDipState1 As String = ""
    Public Property DoubleDipState As String
        Get
            Return doubleDipState1
        End Get
        Set(value As String)
            doubleDipState1 = value
            GraphicsProcessingUnit.MarkCGlifelines(lifeline1Active1, Lifeline2Active, Lifeline3Active, Lifeline4Active)
        End Set
    End Property

    Dim doubleDipFirstAnswer1 As String = ""
    Public Property DoubleDipFirstAnswer As String
        Get
            Return doubleDipFirstAnswer1
        End Get
        Set(value As String)
            doubleDipFirstAnswer1 = value
        End Set
    End Property

    Dim QuestionText1 As String = ""
    Public Property QuestionText As String
        Get
            Return QuestionText1
        End Get
        Set(value As String)
            QuestionText1 = value
            Question_TextBox.Text = value.Replace(vbCr, " ").Replace(vbLf, " ")
        End Set
    End Property

    Dim Answer1Text1 As String = ""
    Public Property Answer1Text As String
        Get
            Return Answer1Text1
        End Get
        Set(value As String)
            Answer1Text1 = value
            AnswerA_TextBox.Text = value
        End Set
    End Property

    Dim Answer2Text1 As String = ""
    Public Property Answer2Text As String
        Get
            Return Answer2Text1
        End Get
        Set(value As String)
            Answer2Text1 = value
            AnswerB_TextBox.Text = value
        End Set
    End Property

    Dim Answer3Text1 As String = ""
    Public Property Answer3Text As String
        Get
            Return Answer3Text1
        End Get
        Set(value As String)
            Answer3Text1 = value
            AnswerC_TextBox.Text = value
        End Set
    End Property

    Dim Answer4Text1 As String = ""
    Public Property Answer4Text As String
        Get
            Return Answer4Text1
        End Get
        Set(value As String)
            Answer4Text1 = value
            AnswerD_TextBox.Text = value
        End Set
    End Property

    Dim FinalAnswer1 As String = ""
    Public Property FinalAnswer As String
        Get
            Return FinalAnswer1
        End Get
        Set(value As String)
            FinalAnswer1 = value
            FinalAnswer_TextBox.Text = Helpers.Convert1234ToABCD(value)
        End Set
    End Property

    Dim CorrectAnswer1 As String = ""
    Public Property CorrectAnswer As String
        Get
            Return CorrectAnswer1
        End Get
        Set(value As String)
            CorrectAnswer1 = value
            CorrectAnswer_TextBox.Text = value
        End Set
    End Property

    Dim Explanation1 As String = ""
    Public Property Explanation As String
        Get
            Return Explanation1
        End Get
        Set(value As String)
            Explanation1 = value
            MoreQuestionInfoForHost_TextBox.Text = value
        End Set
    End Property

    Dim Pronunciation1 As String = ""
    Public Property Pronunciation As String
        Get
            Return Pronunciation1
        End Get
        Set(value As String)
            Pronunciation1 = value
            Pronunciation_TextBox.Text = value
        End Set
    End Property

    Dim LevelQ1 As String = 0
    Public Property LevelQ As String
        Get
            Return LevelQ1
        End Get
        Set(value As String)
            LevelQ1 = value
            LevelQ_TextBox.Text = value
        End Set
    End Property

    Dim CurentGameStatusData1 As String = ""
    Public Property CurentGameStatusData As String
        Get
            Return CurentGameStatusData1
        End Get
        Set(value As String)
            CurentGameStatusData1 = value
            HostContPresentationLayer.GamePlayStateSet("")
        End Set
    End Property

    Dim ActiveLifelines1 As String = "4"
    Public Property ActiveLifelines As String
        Get
            Return ActiveLifelines1
        End Get
        Set(value As String)
            ActiveLifelines1 = value
            HostContPresentationLayer.GamePlayStateSet("")
        End Set
    End Property

    Dim FiftyFifty1 As String = ""
    Public Property FiftyFifty As String
        Get
            Return FiftyFifty1
        End Get
        Set(value As String)
            FiftyFifty1 = value
        End Set
    End Property

    Dim AtaVotes1 As String = ""
    Public Property AtaVotes As String
        Get
            Return AtaVotes1
        End Get
        Set(value As String)
            AtaVotes1 = value
        End Set
    End Property

    Dim QuestionForSume1 As String = ""
    Public Property QuestionForSume As String
        Get
            Return QuestionForSume1
        End Get
        Set(value As String)
            QuestionForSume1 = value
        End Set
    End Property

    Public FiftyFiftyPosition As Short = 0
    Public PhoneAFriendPosition As Short = 0
    Public AskTheAudiencePosition As Short = 0
    Public SwitchTheQuestionPosition As Short = 0
    Public DoubleDipPosition As Short = 0
    Public AskTheHostPosition As Short = 0
    Public AskTheExpertPosition As Short = 0

    Dim Lifelines_ As String
    Public Property Lifelines As String
        Get
            Return Lifelines_
        End Get
        Set(value As String)
            Lifelines_ = value.ToUpper
            Try
                Dim Lifelines() As String = Lifelines_.Split(";")
                FiftyFiftyPosition = Array.IndexOf(Lifelines, "5050") + 1
                PhoneAFriendPosition = Array.IndexOf(Lifelines, "PAF") + 1
                AskTheAudiencePosition = Array.IndexOf(Lifelines, "ATA") + 1
                SwitchTheQuestionPosition = Array.IndexOf(Lifelines, "STQ") + 1
                DoubleDipPosition = Array.IndexOf(Lifelines, "DDIP") + 1
                AskTheHostPosition = Array.IndexOf(Lifelines, "ATH") + 1
                AskTheExpertPosition = Array.IndexOf(Lifelines, "ATE") + 1
            Catch ex As Exception
            End Try
        End Set
    End Property

    Dim MoneyTree As New List(Of Decimal)

#End Region

    Private Sub LoadConfiguration()

        Try
            Dim ConfigurationPath As String = GameConfiguration.Default.DefaultGameConfigurationPath
            Dim BasicConfigurationText As String = System.IO.File.ReadAllText(String.Format("{0}\{1}", ConfigurationPath, "BasicGameConfiguration.xml"))

            Dim BasicConfigurationReader As System.IO.TextReader = New System.IO.StringReader(BasicConfigurationText)

            Dim serializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(Xml2CSharp.BASICGAMECONFIGURATIONS))
            Dim WwtbamConfiguraiton As Xml2CSharp.BASICGAMECONFIGURATIONS

            WwtbamConfiguraiton = serializer.Deserialize(BasicConfigurationReader)

#Region "MONEYTREE PREVIEW VALUE"
            QSum1_TextBox.Text = WwtbamConfiguraiton.MONEYTREE.Q1.PREVIEWVALUE
            QSum2_TextBox.Text = WwtbamConfiguraiton.MONEYTREE.Q2.PREVIEWVALUE
            QSum3_TextBox.Text = WwtbamConfiguraiton.MONEYTREE.Q3.PREVIEWVALUE
            QSum4_TextBox.Text = WwtbamConfiguraiton.MONEYTREE.Q4.PREVIEWVALUE
            QSum5_TextBox.Text = WwtbamConfiguraiton.MONEYTREE.Q5.PREVIEWVALUE

            QSum6_TextBox.Text = WwtbamConfiguraiton.MONEYTREE.Q6.PREVIEWVALUE
            QSum7_TextBox.Text = WwtbamConfiguraiton.MONEYTREE.Q7.PREVIEWVALUE
            QSum8_TextBox.Text = WwtbamConfiguraiton.MONEYTREE.Q8.PREVIEWVALUE
            QSum9_TextBox.Text = WwtbamConfiguraiton.MONEYTREE.Q9.PREVIEWVALUE
            QSum10_TextBox.Text = WwtbamConfiguraiton.MONEYTREE.Q10.PREVIEWVALUE

            QSum11_TextBox.Text = WwtbamConfiguraiton.MONEYTREE.Q11.PREVIEWVALUE
            QSum12_TextBox.Text = WwtbamConfiguraiton.MONEYTREE.Q12.PREVIEWVALUE
            QSum13_TextBox.Text = WwtbamConfiguraiton.MONEYTREE.Q13.PREVIEWVALUE
            QSum14_TextBox.Text = WwtbamConfiguraiton.MONEYTREE.Q14.PREVIEWVALUE
            QSum15_TextBox.Text = WwtbamConfiguraiton.MONEYTREE.Q15.PREVIEWVALUE
#End Region

#Region "MONEYTREE REAL VALUE"

            MoneyTree.Add(WwtbamConfiguraiton.MONEYTREE.Q1.REALVALUE)
            MoneyTree.Add(WwtbamConfiguraiton.MONEYTREE.Q2.REALVALUE)
            MoneyTree.Add(WwtbamConfiguraiton.MONEYTREE.Q3.REALVALUE)
            MoneyTree.Add(WwtbamConfiguraiton.MONEYTREE.Q4.REALVALUE)
            MoneyTree.Add(WwtbamConfiguraiton.MONEYTREE.Q5.REALVALUE)
            MoneyTree.Add(WwtbamConfiguraiton.MONEYTREE.Q6.REALVALUE)
            MoneyTree.Add(WwtbamConfiguraiton.MONEYTREE.Q7.REALVALUE)
            MoneyTree.Add(WwtbamConfiguraiton.MONEYTREE.Q8.REALVALUE)
            MoneyTree.Add(WwtbamConfiguraiton.MONEYTREE.Q9.REALVALUE)
            MoneyTree.Add(WwtbamConfiguraiton.MONEYTREE.Q10.REALVALUE)
            MoneyTree.Add(WwtbamConfiguraiton.MONEYTREE.Q11.REALVALUE)
            MoneyTree.Add(WwtbamConfiguraiton.MONEYTREE.Q12.REALVALUE)
            MoneyTree.Add(WwtbamConfiguraiton.MONEYTREE.Q13.REALVALUE)
            MoneyTree.Add(WwtbamConfiguraiton.MONEYTREE.Q14.REALVALUE)
            MoneyTree.Add(WwtbamConfiguraiton.MONEYTREE.Q15.REALVALUE)
            MoneyTree.Sort()

#End Region

            HostContPresentationLayer.ConfigurationMoneyTreeSet()

            If WwtbamConfiguraiton.LIFELINES.DEFAULTLIFELINECOUNT.Equals(3) Then
                ThreeLifelinesStatus_Label_Click(threeLifelinesStatus_Label, Nothing)
            ElseIf WwtbamConfiguraiton.LIFELINES.DEFAULTLIFELINECOUNT.Equals(4) Then
                FourLifelinesStatus_Label_Click(fourLifelinesStatus_Label, Nothing)
            End If

            Lifelines = $"{WwtbamConfiguraiton.LIFELINES.LIFELINE1};{WwtbamConfiguraiton.LIFELINES.LIFELINE2};{WwtbamConfiguraiton.LIFELINES.LIFELINE3};{WwtbamConfiguraiton.LIFELINES.LIFELINE4};{WwtbamConfiguraiton.LIFELINES.LIFELINE5}"
            HostContPresentationLayer.ConfigurationLifelines($"{WwtbamConfiguraiton.LIFELINES.LIFELINE1}", $"{WwtbamConfiguraiton.LIFELINES.LIFELINE2}", $"{WwtbamConfiguraiton.LIFELINES.LIFELINE3}", $"{WwtbamConfiguraiton.LIFELINES.LIFELINE4}", $"{WwtbamConfiguraiton.LIFELINES.LIFELINE5}")

            GuiContext.ResetAll()

            MusicList_ComboBox.DataSource = MainGameMusicLayerObj.WwtbamMusicPlaylistConfig.SOUND
            MusicList_ComboBox.DisplayMember = "TITLE"

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try

    End Sub


    Private Sub Quiz_Operator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LoadConfiguration()
        LiveRehearselViewButton(m_IsGameGoingLive)

        TimerPAFsecondDrop.Interval = 1000
        TimerEndPAF.Interval = 30000
        Timer_PLAY.Interval = 300
        If (Not Debugger.IsAttached) Or (Not Debugger.IsAttached And My.Settings.DebugMode = True) Then
            Timer_STOP.Start()
        End If

    End Sub

    Private Sub QuestionLoad_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuestionLoad_Label.Click

        Dim CorrectAnswerInt As Integer

        If LevelQ <> "111" Or LevelQ <> "666" Then
            Using selectedQuestionTable As DataTable = DataLayer.SelectSuitableQuestion(LevelQ)
                With selectedQuestionTable
                    If selectedQuestionTable.Rows.Count > 0 Then
                        QuestionText = .Rows(0)("Question").ToString().Replace("|", vbCrLf)
                        Answer1Text = .Rows(0)("Answer1").ToString()
                        Answer2Text = .Rows(0)("Answer2").ToString()
                        Answer3Text = .Rows(0)("Answer3").ToString()
                        Answer4Text = .Rows(0)("Answer4").ToString()
                        CorrectAnswer = .Rows(0)("CorrectAnswer").ToString()
                        CorrectAnswerInt = .Rows(0)("CorrectAnswer")
                        Explanation = .Rows(0)("MoreInformation").ToString()
                        Pronunciation = .Rows(0)("Pronunciation").ToString()
                        questionID = .Rows(0)("QuestionID").ToString()
                    End If
                End With
            End Using
        End If

        Try
            If RandomizeAnswers_CheckBox.Checked = True Then

                Dim QuestionAnswers As String() = {Answer1Text, Answer2Text, Answer3Text, Answer4Text}
                Dim CorrectAnswerNormal As String = QuestionAnswers.ElementAt(CorrectAnswerInt - 1)

                Dim r As Random = New Random()
                Dim QuestionAnswersRandom As String() = {Answer1Text, Answer2Text, Answer3Text, Answer4Text}
                QuestionAnswersRandom = QuestionAnswersRandom.OrderBy(Function(x) r.[Next]()).ToArray()

                Dim CorrectAnswerAfterRandom As Integer = Array.IndexOf(QuestionAnswersRandom, CorrectAnswerNormal)

                Answer1Text = QuestionAnswersRandom.ElementAt(0)
                Answer2Text = QuestionAnswersRandom.ElementAt(1)
                Answer3Text = QuestionAnswersRandom.ElementAt(2)
                Answer4Text = QuestionAnswersRandom.ElementAt(3)
                CorrectAnswer = Helpers.Convert1234ToABCD(CorrectAnswerAfterRandom + 1)

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


        GuiContext.FiftyFiftyResetOptionOperator(CorrectAnswer)

        MomentStatus = "QuestionAnswers_Load"
        '' ******* CASPARCG *******
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.CGQuestionSet(QuestionText, Answer1Text, Answer2Text, Answer3Text, Answer4Text, QuestionForSume)
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Add(1, My.Settings.questionFlashTempl, True, GraphicsProcessingUnit.cgDataQA)

        End If
        '' ******* CASPARCG ******* 

    End Sub

    Private Sub QuestionAppear()
        MomentStatus = "Question_Fired"

        finalAnswerGiven = False

        ''Mark All Yelow
        For index As Integer = 8 To 22
            For Each lb As Label In Me.Controls.OfType(Of Label)()
                Dim Label As String = "Label" + index.ToString
                If String.Compare(Label, lb.Name, True) = 0 Then
                    lb.BackColor = Color.Yellow
                End If
            Next
        Next

        ''First Five Questions
        If Val(LevelQ) >= 1 And Val(LevelQ) <= 5 Then
            Dim TextBox As String = "QSum" + LevelQ + "_TextBox"
            For Each tb As TextBox In Me.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    QuestionForSume = tb.Text
                End If
            Next
            MainGameMusicLayerObj.PlayHeartbeatMusic(LevelQ)
        End If

        For Each lb As Label In Me.Controls.OfType(Of Label)()
            Dim Label As String = "Label" + Val(Val(LevelQ) + 7).ToString
            If String.Compare(Label, lb.Name, True) = 0 Then
                lb.BackColor = Color.Orange
            End If
        Next

        ''Second Ten Questions
        If Val(LevelQ) >= 6 And Val(LevelQ) <= 15 Then
            Dim TextBox As String = "QSum" + LevelQ + "_TextBox"
            For Each tb As TextBox In Me.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    QuestionForSume = tb.Text
                End If
            Next
            MainGameMusicLayerObj.PlayHeartbeatMusic(LevelQ)
        End If

        FinalAnswer = "T"

        AnswerDappear_Label.BackColor = Color.Yellow

        TimerQuestionRemove.Interval = Val(SecondsToDissolveAfterCorrectAnswer_TextBox.Text) * 1000

        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.CGQuestionSet(QuestionText, Answer1Text, Answer2Text, Answer3Text, Answer4Text, QuestionForSume)
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Update(1, GraphicsProcessingUnit.cgDataQA)
            'casparQA_.Channels(0).CG.Play(1)
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "QuestionFlyIN")
        End If

        DataLayer.DisposeATAvoteData()

        GraphicsProcessingUnit.InteractiveWallScreenObj.MotionBackgroundDuringQuestion(LevelQ)

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

        MomentStatus = "Question_AnswerA_Fired" ''IZMENA!!

        MainGameMusicLayerObj.StopLetsPlay()

        ''SQL
        DataLayer.MarkQuestionFiredDB(questionID, Me.IsGameGoingLive)
        DataLayer.MarkQuestionAnsweredDB(questionID, Me.IsGameGoingLive)
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

        MomentStatus = "Question_AnswerB_Fired" ''IZMENA!!

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

        MomentStatus = "Question_AnswerC_Fired" ''IZMENA!!

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

        MomentStatus = "Question_AnswerD_Fired" ''IZMENA!!

    End Sub


#Region "FINALANSWERS"

    Public Sub HostFinalAnswerData()

        LimitedGameClockStop_Label_Click(LimitedGameClockStop_Label, Nothing)

        If Not FinalAnswer = CorrectAnswer Then
            SumeShow_CheckBox.Checked = False
            Empty_CheckBox.Checked = False
        End If

        MainGameMusicLayerObj.PlayFinalAnswerSound(LevelQ_TextBox.Text, DoubleDipState)

        If Val(LevelQ) >= 6 And Val(LevelQ) <= 15 Then
            Timer_STOP.Interval = 500
            Timer_STOP.Start()

        End If

        If GraphicsProcessingUnit.flagATAgraph = True Then
            GraphicsProcessingUnit.ShowHideATAGraph()
        End If

        finalAnswerGiven = True

        GraphicsProcessingUnit.CGgleenQAstop()

    End Sub

    Private Sub FinalA_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinalA_Button.Click, AnswerA_TextBox.DoubleClick

        AnswerA_TextBox.BackColor = Color.Yellow

        FinalAnswer = Helpers.Convert1234ToABCD(1)


        '' ******* CASPARCG ******* CASPARCG *******
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "finalAnswerA")
        End If
        '' ******* CASPARCG ******* CASPARCG *******

        HostFinalAnswerData()

        MomentStatus = "AnswerA_Final_Fired" ''IZMENA!!

    End Sub

    Private Sub FinalB_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinalB_Button.Click, AnswerB_TextBox.DoubleClick

        AnswerB_TextBox.BackColor = Color.Yellow

        FinalAnswer = Helpers.Convert1234ToABCD(2)

        '' ******* CASPARCG ******* CASPARCG *******
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "finalAnswerB")
        End If
        '' ******* CASPARCG ******* CASPARCG *******

        HostFinalAnswerData()

        MomentStatus = "AnswerB_Final_Fired" ''IZMENA!!

    End Sub

    Private Sub FinalC_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinalC_Button.Click, AnswerC_TextBox.DoubleClick

        AnswerC_TextBox.BackColor = Color.Yellow

        FinalAnswer = Helpers.Convert1234ToABCD(3)

        If LevelQ = "6" Or LevelQ = "7" Or LevelQ = "8" Or LevelQ = "9" Or LevelQ = "10" Or LevelQ = "11" Or LevelQ = "12" Or LevelQ = "13" Or LevelQ = "14" Or LevelQ = "15" Then
            Timer_STOP.Interval = 500
            Timer_STOP.Start()
        End If

        '' ******* CASPARCG ******* CASPARCG *******
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "finalAnswerC")
        End If
        '' ******* CASPARCG ******* CASPARCG *******

        HostFinalAnswerData()

        MomentStatus = "AnswerC_Final_Fired" ''IZMENA!!

    End Sub

    Private Sub FinalD_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinalD_Button.Click, AnswerD_TextBox.DoubleClick

        AnswerD_TextBox.BackColor = Color.Yellow

        FinalAnswer = Helpers.Convert1234ToABCD(4)

        '' ******* CASPARCG ******* CASPARCG *******
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "finalAnswerD")
        End If
        '' ******* CASPARCG ******* CASPARCG *******

        HostFinalAnswerData()

        MomentStatus = "AnswerD_Final_Fired" ''IZMENA!!

    End Sub

#End Region
    Private Sub CorrectAnswerProcedure()

        MainGameMusicLayerObj.StopIncorrectAnswer()
        MainGameMusicLayerObj.PlayCorrectAnswer(LevelQ, VariableMilestone_TextBox.Text)

        If LevelQ <> "888" And LevelQ <> "666" Then
            LevelQ = Val(LevelQ) + 1
        End If

        Leveling()

        GraphicsProcessingUnit.InteractiveWallScreenObj.CorrectAnswer(LevelQ)

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

        Dim reval As String = ""
        If Val(levelq) >= 1 And levelq <= 5 Then
            reval = "0"
        End If

        If Val(levelq) >= 6 And levelq <= 15 Then
            reval = SetSecondMilestonePrize("5")
            If realvalue Then
                reval = MoneyTree.ElementAt(5 - 1)
            End If
            If (Val(levelq) > Val(VariableMilestone_TextBox.Text)) And Not String.IsNullOrEmpty(VariableMilestone_TextBox.Text.Trim) Then
                reval = SetSecondMilestonePrize(VariableMilestone_TextBox.Text)
                If realvalue Then
                    reval = MoneyTree.ElementAt(VariableMilestone_TextBox.Text - 1)
                End If
            End If
        End If

        Return reval

    End Function

    Private Sub IncorrectAnswerProcedure()

        If (LevelQ <> "666") And Not (String.Equals(DoubleDipState, "DoubleDipFirstFinal", StringComparison.OrdinalIgnoreCase)) Then 'Ne stavaj suma za utnato ako e vo walkaway mod, togas e samo radi reda
            QuestionForSume = CalculateIncorrectAnswer(LevelQ)
        End If

        If (LevelQ <> "666") And Not (String.Equals(DoubleDipState, "DoubleDipFirstFinal", StringComparison.OrdinalIgnoreCase)) Then 'Ne stavaj suma za utnato ako e vo walkaway mod, togas e samo radi reda
            QuestionForSume = CalculateIncorrectAnswer(LevelQ)
        End If

        MainGameMusicLayerObj.PlayIncorrectAnswer(LevelQ, DoubleDipState)

        If LevelQ <> "888" And LevelQ <> "666" And Not (String.Equals(DoubleDipState, "DoubleDipFirstFinal", StringComparison.OrdinalIgnoreCase)) Then
            LevelQ = "111"
        End If

    End Sub

    Private Sub RemoveQuestion_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveQuestion_Button.Click

        AremoveFF_Label.BackColor = Color.Yellow
        BremoveFF_Label.BackColor = Color.Yellow
        CremoveFF_Label.BackColor = Color.Yellow
        DremoveFF_Label.BackColor = Color.Yellow

        QuestionText = ""
        Answer1Text = ""
        Answer2Text = ""
        Answer3Text = ""
        Answer4Text = ""

        Pronunciation = ""
        Explanation = ""

        AnswerA_TextBox.BackColor = Color.White
        AnswerB_TextBox.BackColor = Color.White
        AnswerC_TextBox.BackColor = Color.White
        AnswerD_TextBox.BackColor = Color.White

        FiftyFifty = ""
        DoubleDipState = ""
        DoubleDipFirstAnswer = ""

        TimerQuestionRemove.Stop() ''IZMENA!!! Dodadeno Timer2.Stop() bidejkji ako bese stisnato EmptyQuestion pred vreme, Timer2 pak go gasese posle izminatiot interval
        MomentStatus = "EmptyQuestion_Fired" ''IZMENA!!

        '' ******* CASPARCG ******* CASPARCG *******
        '' ******* CASPARCG ******* CASPARCG *******
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            If LevelQ <> "888" Then
                GraphicsProcessingUnit.casparQA.Channels(0).CG.Remove(1)
                GraphicsProcessingUnit.cgDataQA.Clear()
            Else
                GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "QuestionHide_Label")
                GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "hideSwitchQmark")
            End If
        End If

        If LevelQ = "888" Then
            LevelQ = SwitchedQuestion.ToString
            STQ_X_Label_Click(STQ_X_Label, Nothing)
        End If

        Leveling()
        LoadQuestion()

        FinalA_Button.Text = "Final A: " + Answer1Text.Substring(0, Math.Min(20, Answer1Text.Length))
        FinalB_Button.Text = "Final B: " + Answer2Text.Substring(0, Math.Min(20, Answer2Text.Length))
        FinalC_Button.Text = "Final C: " + Answer3Text.Substring(0, Math.Min(20, Answer3Text.Length))
        FinalD_Button.Text = "Final D: " + Answer4Text.Substring(0, Math.Min(20, Answer4Text.Length))

        '' ******* CASPARCG ******* CASPARCG *******
        GraphicsProcessingUnit.resetVariables()
        DataLayer.DisposeATAvoteData()


    End Sub

    Private Function LoadQuestion()
        If LevelQ <> "111" Then
            QuestionLoad_Label_Click(QuestionLoad_Label, Nothing)

        End If
    End Function

    Private Sub WonPrizeReveal_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WonPrizeReveal_Button.Click
        WonPrizeReveal_Button.BackColor = Color.Gainsboro
        GraphicsProcessingUnit.WonPrizeReveal(LevelQ, QuestionForSume)

        If LevelQ = "111" Or LevelQ = "666" Then
            HostContPresentationLayer.GamePlayStateSet("TOTALPRIZEWON")

        ElseIf LevelQ <> "888" Then
            'DO NOTHING
            'od prasanje na prasanje momentalen iznos
        End If


        MomentStatus = "WonPrize_Fired" ''IZMENA!!


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
            ListRemoved.Remove(Helpers.ConvertABCDTo1234(CorrectAnswer))
            Dim randomNumber As Short = New Random().Next(0, ListRemoved.Count)
            ListRemoved.RemoveAt(randomNumber)
        End If

        'Razmisli zosto go napravi ova vaka
        If AremoveFF_Label.BackColor = Color.Blue OrElse ListRemoved.Contains(1) Then
            Answer1Text = ""
            ListRemoved.Add(1)
        End If
        If BremoveFF_Label.BackColor = Color.Blue OrElse ListRemoved.Contains(2) Then
            Answer2Text = ""
            ListRemoved.Add(2)
        End If
        If CremoveFF_Label.BackColor = Color.Blue OrElse ListRemoved.Contains(3) Then
            Answer3Text = ""
            ListRemoved.Add(3)
        End If
        If DremoveFF_Label.BackColor = Color.Blue OrElse ListRemoved.Contains(4) Then
            Answer4Text = ""
            ListRemoved.Add(4)
        End If

        GraphicsProcessingUnit.FiftyFiftyTake(ListRemoved)

        Dim HashListOfRemoves As New HashSet(Of Short)(ListRemoved)
        If HashListOfRemoves.Count = 2 Then
            FiftyFifty = String.Format("{0}{1}", HashListOfRemoves.ElementAt(0), HashListOfRemoves.ElementAt(1))
            'HostContPresentationLayer.FiftyFiftyFire(HashListOfRemoves.ElementAt(0), HashListOfRemoves.ElementAt(1))
        End If

        MomentStatus = "5050_Fire"

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

        AtaVotes = String.Format("{0};{1};{2};{3}", A_pub.Text, B_pub.Text, C_pub.Text, D_pub.Text)

        '**** CASPARCG
        GraphicsProcessingUnit.SetATAdata(A_pub.Text, B_pub.Text, C_pub.Text, D_pub.Text)
        '**** CASPARCG***** 17 18 20 22

        TimerATAsecDrop.Stop()
        TimerATAsecDrop.InitializeLifetimeService()

        My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\43.Audience Decide.wav", AudioPlayMode.Background)

        A_pub.Text = A_pub.Text + "%"
        B_pub.Text = B_pub.Text + "%"
        C_pub.Text = C_pub.Text + "%"
        D_pub.Text = D_pub.Text + "%"

        Timer_PLAY.Start()
        Timer_PLAY.Interval = 800

        ATA_X_Label_Click(ATA_X_Label, Nothing)

        MomentStatus = "AskAudience_EndVote"

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

        My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\42.Audience Keyboards.wav", AudioPlayMode.Background)

        TimerATAendVoteWithSec.Interval = Int(ataVoteTime_TextBox.Text) * 1000 + 5
        TimerATAendVoteWithSec.Start()

        ''CASPARCG***** CASPARCG****
        ShowHideAudienceGraph_Label_Click(ShowHideAudienceGraph_Label, Nothing)
        'showATAGraph

        ''CASPARCG***** CASPARCG****

        MomentStatus = "AskAudience_Voting" ''IZMENA!!!

        GraphicsProcessingUnit.InteractiveWallScreenObj.AudienceVoting()

    End Sub

    Private Sub TimerPAFsecondDrop_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerPAFsecondDrop.Tick

        PAFsec_TextBox.Text -= 1

    End Sub

    Private Sub FourLifelinesStatus_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fourLifelinesStatus_Label.Click
        fourLifelinesStatus_Label.BackColor = Color.Orange
        threeLifelinesStatus_Label.BackColor = Color.Silver
        ActiveLifelines = "4"
        GraphicsProcessingUnit.Activate4Lifelines()

    End Sub

    Private Sub ThreeLifelinesStatus_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles threeLifelinesStatus_Label.Click
        threeLifelinesStatus_Label.BackColor = Color.Orange
        fourLifelinesStatus_Label.BackColor = Color.Silver
        ActiveLifelines = "3"
        GraphicsProcessingUnit.Activate3Lifelines()

    End Sub

    Private Sub LifelineRemind_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LifelineRemind_Button.Click
        GraphicsProcessingUnit.MarkCGlifelines(lifeline1Active1, Lifeline2Active, Lifeline3Active, Lifeline4Active)
        GraphicsProcessingUnit.LifelineRemind()

    End Sub

    Private Sub Label_SwitchQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label100.Click

        SwitchedQuestion = Val(LevelQ)

        LevelQ = "888"

        My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\102.Alternate Animate.wav", AudioPlayMode.Background)

        STQ_1_Label_Click(STQ_1_Label, Nothing)

        '' *** CASPARCG
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "showSwitchQmark")
        End If
        '' *** CASPARCG

        MomentStatus = "SwitchTheQuestion_Progess"

        ''STQ_X_Label_Click(STQ_X_Label, Nothing)

        'Contestant.PictureBox9.BackgroundImage = Image.FromFile("C:\WWTBAM Removable Disc\Graphics\SwitchQ_X.png")
        'Contestant.PictureBox9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TabControl2.SelectedTab = TabPage6
    End Sub

    Private Sub WalkAwayStart_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WalkAwayStart_Button.Click
        SumeShow_CheckBox.Checked = False
        Empty_CheckBox.Checked = False

        If LevelQ = "1" Or LevelQ = "2" Or LevelQ = "3" Or LevelQ = "4" Or LevelQ = "5" Or LevelQ = "6" Or LevelQ = "7" Or LevelQ = "8" Or LevelQ = "9" Or LevelQ = "10" Then
            'LightDown.URL = "C:\WWTBAM Removable Disc\UK 2007\33.Little Quitter.wav"
        End If
        If LevelQ = "11" Or LevelQ = "12" Or LevelQ = "13" Or LevelQ = "14" Or LevelQ = "15" Or LevelQ = "16" Then
            'LightDown.URL = "C:\WWTBAM Removable Disc\UK 2007\34.Big Quitter.wav"
        End If

        Dim TextBox As String = "QSum" + Val(LevelQ - 1).ToString + "_TextBox"
        For Each tb As TextBox In Me.Controls.OfType(Of TextBox)()
            If String.Compare(TextBox, tb.Name, True) = 0 Then
                If LevelQ >= 2 And LevelQ <= 15 Then
                    QuestionForSume = tb.Text
                End If
            End If
        Next

        LevelQ = "666"

        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "QuestionAnswersFlyOUT")
        End If

        PlayLXsound()
        Timer_STOP.Start()

        GraphicsProcessingUnit.InteractiveWallScreenObj.MotionBackgroundDuringQuestion(LevelQ)

        MomentStatus = "Walkaway_Fired"

    End Sub

    Private Sub PAFstart_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAFstart_Label.Click

        PAFsec_TextBox.Text = "30"

        My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\45.Friend Clock.wav", AudioPlayMode.Background)

        Timer_PAUSE.Start()

        TimerPAFsecondDrop.Start()
        TimerEndPAF.Start()

        '*** CASPARCG
        GraphicsProcessingUnit.PhoneAFriend("START")
        '*** CASPARCG

        PAF_1_Label_Click(PAF_1_Label, Nothing)

        MomentStatus = "PhoneFriend_Progress" ''IZMENA!!!

    End Sub

    Private Sub TimerEndPAF_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerEndPAF.Tick

        PAFsec_TextBox.Text = "0"

        PAF_X_Label_Click(PAF_X_Label, Nothing)

        Timer_PLAY.Start()

        Timer_PLAY.Interval = 1000

        TimerPAFsecondDrop.Stop()
        TimerPAFsecondDrop.InitializeLifetimeService()

        TimerEndPAF.Stop()
        TimerEndPAF.InitializeLifetimeService()

        PAF_X_Label_Click(PAF_X_Label, Nothing)

        '*** CASPARCG
        GraphicsProcessingUnit.PhoneAFriend("END")
        '*** CASPARCG

    End Sub

    Private Sub PAFabort_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAFabort_Label.Click
        My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\46.Interrupted Clock.wav", AudioPlayMode.Background)

        PAF_X_Label_Click(PAF_X_Label, Nothing)

        Timer_PLAY.Start()
        Timer_PLAY.Interval = 1500

        TimerPAFsecondDrop.Stop()
        TimerPAFsecondDrop.InitializeLifetimeService()

        TimerEndPAF.Stop()
        TimerEndPAF.InitializeLifetimeService()

        '*** CASPARCG
        GraphicsProcessingUnit.PhoneAFriend("ABORT")
        '*** CASPARCG

        MomentStatus = "PhoneFriend_Interrupted" ''IZMENA!!!
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

        FinalAnswer = "T"
        DoubleDipFirstAnswer = ""
        VariableMilestone_TextBox.Text = ""

        If LevelQ = "111" Or LevelQ = "666" Then
            LevelQ = "1"

        End If

        ''SQL
        DataLayer.DisposeAnsweredGameQuestionsDB(1)
        ''SQL

        MomentStatus = "NewGame_Fired" ''IZMENA!!!
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

        Lifeline1Active = 1
        Lifeline2Active = 1
        Lifeline3Active = 1
        Lifeline4Active = 1

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

        GuiContext.SomethingToDoWithLifeline(FiftyFiftyPosition, 0)

        Select Case FiftyFiftyPosition
            Case 1
                Lifeline1Active = 0
            Case 2
                Lifeline2Active = 0
            Case 3
                Lifeline3Active = 0
            Case 4
                Lifeline4Active = 0
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
        GuiContext.SomethingToDoWithLifeline(PhoneAFriendPosition, 0)

        Select Case PhoneAFriendPosition
            Case 1
                Lifeline1Active = 0
            Case 2
                Lifeline2Active = 0
            Case 3
                Lifeline3Active = 0
            Case 4
                Lifeline4Active = 0
        End Select

    End Sub

    Private Sub ATA_X_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ATA_X_Label.Click
        GuiContext.SomethingToDoWithLifeline(AskTheAudiencePosition, 0)

        Select Case AskTheAudiencePosition
            Case 1
                Lifeline1Active = 0
            Case 2
                Lifeline2Active = 0
            Case 3
                Lifeline3Active = 0
            Case 4
                Lifeline4Active = 0
        End Select

    End Sub

    Private Sub STQ_X_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STQ_X_Label.Click
        GuiContext.SomethingToDoWithLifeline(SwitchTheQuestionPosition, 0)

        Select Case SwitchTheQuestionPosition
            Case 1
                Lifeline1Active = 0
            Case 2
                Lifeline2Active = 0
            Case 3
                Lifeline3Active = 0
            Case 4
                Lifeline4Active = 0
        End Select


    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LevelQ = "1"

    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LevelQ = "2"
    End Sub

    Private Sub Label21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LevelQ = "14"
    End Sub

    Private Sub Label20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LevelQ = "13"
    End Sub

    Private Sub Label19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LevelQ = "12"
    End Sub

    Private Sub Label18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LevelQ = "11"
    End Sub

    Private Sub Label17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LevelQ = "10"
    End Sub

    Private Sub Label16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LevelQ = "9"
    End Sub

    Private Sub Label15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LevelQ = "8"
    End Sub

    Private Sub Label14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LevelQ = "7"
    End Sub

    Private Sub Label13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LevelQ = "6"
    End Sub

    Private Sub Label12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LevelQ = "5"
    End Sub

    Private Sub Label11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LevelQ = "4"
    End Sub

    Private Sub Label10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LevelQ = "3"
    End Sub

    Private Sub Label22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LevelQ = "15"
    End Sub

    Private Sub FiftyFifty_0_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FiftyFifty_0_Label.Click

        GuiContext.SomethingToDoWithLifeline(FiftyFiftyPosition, 1)

        Select Case FiftyFiftyPosition
            Case 1
                Lifeline1Active = 1
            Case 2
                Lifeline2Active = 1
            Case 3
                Lifeline3Active = 1
            Case 4
                Lifeline4Active = 1
        End Select

    End Sub

    Private Sub PAF_0_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAF_0_Label.Click
        GuiContext.SomethingToDoWithLifeline(PhoneAFriendPosition, 1)

        Select Case PhoneAFriendPosition
            Case 1
                Lifeline1Active = 1
            Case 2
                Lifeline2Active = 1
            Case 3
                Lifeline3Active = 1
            Case 4
                Lifeline4Active = 1
        End Select

    End Sub

    Private Sub ATA_0_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ATA_0_Label.Click

        GuiContext.SomethingToDoWithLifeline(AskTheAudiencePosition, 1)

        Select Case AskTheAudiencePosition
            Case 1
                Lifeline1Active = 1
            Case 2
                Lifeline2Active = 1
            Case 3
                Lifeline3Active = 1
            Case 4
                Lifeline4Active = 1
        End Select

    End Sub

    Private Sub STQ_0_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STQ_0_Label.Click

        GuiContext.SomethingToDoWithLifeline(SwitchTheQuestionPosition, 1)

        Select Case SwitchTheQuestionPosition
            Case 1
                Lifeline1Active = 1
            Case 2
                Lifeline2Active = 1
            Case 3
                Lifeline3Active = 1
            Case 4
                Lifeline4Active = 1
        End Select

    End Sub

    Private Sub FiftyFifty_1_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FiftyFifty_1_Label.Click

        GuiContext.SomethingToDoWithLifeline(FiftyFiftyPosition, 2)

        Select Case FiftyFiftyPosition
            Case 1
                Lifeline1Active = 2
            Case 2
                Lifeline2Active = 2
            Case 3
                Lifeline3Active = 2
            Case 4
                Lifeline4Active = 2
        End Select

    End Sub

    Private Sub PAF_1_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAF_1_Label.Click

        GuiContext.SomethingToDoWithLifeline(PhoneAFriendPosition, 2)

        Select Case PhoneAFriendPosition
            Case 1
                Lifeline1Active = 2
            Case 2
                Lifeline2Active = 2
            Case 3
                Lifeline3Active = 2
            Case 4
                Lifeline4Active = 2
        End Select

    End Sub

    Private Sub ATA_1_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ATA_1_Label.Click

        GuiContext.SomethingToDoWithLifeline(AskTheAudiencePosition, 2)

        Select Case AskTheAudiencePosition
            Case 1
                Lifeline1Active = 2
            Case 2
                Lifeline2Active = 2
            Case 3
                Lifeline3Active = 2
            Case 4
                Lifeline4Active = 2
        End Select

    End Sub

    Private Sub STQ_1_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STQ_1_Label.Click

        GuiContext.SomethingToDoWithLifeline(SwitchTheQuestionPosition, 2)

        Select Case SwitchTheQuestionPosition
            Case 1
                Lifeline1Active = 2
            Case 2
                Lifeline2Active = 2
            Case 3
                Lifeline3Active = 2
            Case 4
                Lifeline4Active = 2
        End Select

    End Sub
    Private Sub CorrectAnswerReveal_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CorrectAnswerReveal_Button.Click

        TimerQuestionRemove.Interval = Val(SecondsToDissolveAfterCorrectAnswer_TextBox.Text) * 1000

        Timer_STOP.Interval = 555

        Timer_SumeShow.Interval = 1140

        If LevelQ = "5" Then
            Timer_STOP.Start()

        End If

        If Empty_CheckBox.Checked = True Then
            TimerQuestionRemove.Start()
        End If

        If SumeShow_CheckBox.Checked = True Then
            Timer_SumeShow.Start()
        End If

        AnswerAappear_Label.BackColor = Color.Yellow
        AnswerBappear_Label.BackColor = Color.Yellow
        AnswerCappear_Label.BackColor = Color.Yellow
        AnswerDappear_Label.BackColor = Color.Yellow

        AremoveFF_Label.BackColor = Color.Yellow
        BremoveFF_Label.BackColor = Color.Yellow
        CremoveFF_Label.BackColor = Color.Yellow
        DremoveFF_Label.BackColor = Color.Yellow

        If CorrectAnswer = FinalAnswer Then
            CorrectAnswerProcedure()
        ElseIf CorrectAnswer <> FinalAnswer Then
            IncorrectAnswerProcedure()
        End If

        Select Case Helpers.ConvertABCDTo1234(CorrectAnswer)
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
        GraphicsProcessingUnit.CorrectAnswer(CorrectAnswer)
        '' ******* CASPARCG ******* CASPARCG *******

        MomentStatus = "CorrectAnswer_Fired"

    End Sub

    Sub Leveling()
        '' ******* CASPARCG ******* CASPARCG *******
        Dim MoneyTreeLevel As Int32 = Val(LevelQ) - 1
        GraphicsProcessingUnit.MoneyTreeLevel(MoneyTreeLevel)

        '' ******* CASPARCG ******* CASPARCG *******

        Dim qForSume, momentSume, incorrectSume As String
        If LevelQ >= 1 And LevelQ <= 15 Then

            qForSume = 0
            Dim TextBoxQfor As String = "QSum" + LevelQ + "_TextBox"
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

            Dim momentSumeInt As Integer = Integer.Parse(MoneyTree.ElementAt(Math.Max((MoneyTreeLevel - 1), 0)))

            incorrectSume = CalculateIncorrectAnswer(LevelQ)
            incorrectSume = IIf(incorrectSume = 0, 0, incorrectSume)

            Dim incorrectSumeInt As Integer = CalculateIncorrectAnswer(LevelQ, True) 'vo slucaj da ima bukvi brojki ili nesto vo stil '4 MILIONI' ja zema vistinskata vrednost-realvalue

            CurentGameStatusData = $"{momentSume};{(incorrectSumeInt - momentSumeInt) * (-1)};{qForSume};{incorrectSume}"

        Else
            CurentGameStatusData = "0;0;0;0"

        End If
    End Sub

    Private Sub WalkAwayQoppinion_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WalkAwayQoppinion_Label.Click

        ''IZMENA! Dodadeno zatoa sto ne vrakjase prasanje na GraphicsQuestion.vb
        MomentStatus = "Question_AnswerD_Fired"
        GraphicsProcessingUnit.WalkAwayQoppinion()

    End Sub

    Private Sub Timer_STOP_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_STOP.Tick
        Timer_STOP.Interval = 300

        MainGameMusicLayerObj.StopHeartbeaLetsPlay()

        Timer_STOP.Stop()
        Timer_STOP.InitializeLifetimeService()
    End Sub

    Private Sub Timer_PAUSE_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_PAUSE.Tick

        MainGameMusicLayerObj.PauseHeartbeatMusic(LevelQ)

        Timer_PAUSE.Stop()
        Timer_PAUSE.InitializeLifetimeService()
    End Sub

    Private Sub Timer_PLAY_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_PLAY.Tick

        MainGameMusicLayerObj.PlayHeartbeatMusic(LevelQ)

        Timer_PLAY.Interval = 300

        Timer_PLAY.Stop()
        Timer_PLAY.InitializeLifetimeService()

    End Sub

    Private Sub ATAready_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ATAready_Label.Click
        Timer_PAUSE.Start()

        My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\41.Ask the Audience.wav", AudioPlayMode.BackgroundLoop)

        ATA_1_Label_Click(ATA_1_Label, Nothing)

        A_pub.Text = "0"
        B_pub.Text = "0"
        C_pub.Text = "0"
        D_pub.Text = "0"

        MomentStatus = "AskAudience_Questioning" ''IZMENA!!!
        DataLayer.DisposeATAvoteData()

        GraphicsProcessingUnit.InteractiveWallScreenObj.AudienceAsking()

    End Sub

    Private Sub PAFCall_lbl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAFready_Label.Click
        Timer_PAUSE.Start()

        'LightDown.URL = "C:\WWTBAM Removable Disc\UK 2007\44.Phone a Friend.wav"
        My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\44.Phone a Friend.wav", AudioPlayMode.BackgroundLoop)

        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "QuestionAnswersFadeOUT")
        End If

        PAF_1_Label_Click(PAF_1_Label, Nothing)

        MomentStatus = "PhoneFriend_Dialing" ''IZMENA!!!

    End Sub

    Private Sub Timer_SumeShow_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_SumeShow.Tick

        WonPrizeReveal_Button_Click(WonPrizeReveal_Button, Nothing)

        MomentStatus = "WonPrize_Fired" ''IZMENA!!

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
        GraphicsProcessingUnit.MarkCGlifelines(lifeline1Active1, Lifeline2Active, Lifeline3Active, Lifeline4Active)

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
        GraphicsProcessingUnit.QuestionForLozenge(QuestionForSume)

    End Sub

    Private Sub AbcdHex_Show()
        MomentStatus = "ABCDHex_Show"
        If GraphicsProcessingUnit.casparQA.IsConnected Then
            GraphicsProcessingUnit.casparQA.Channels(0).CG.Invoke(1, "QuestionAnswersFlyIN")
        End If
    End Sub

    Private Sub NextThing_Button_Click(sender As Object, e As EventArgs) Handles NextThing_Button.Click
        ''IZMENA!!! Celosna Izmena; Nov Label - NextThing_Label
        'MessageBox.Show(MomentStatus)

        If MomentStatus = "QuestionAnswers_Load" Or MomentStatus = "VariableMilestone_Set" Then
            QuestionAppear()
            Exit Sub
        End If

        If MomentStatus = "Question_Fired" Then
            AnswerAappear_Label_Click(AnswerAappear_Label, Nothing)
            'AbcdHex_Show()
            Exit Sub
        End If

        If MomentStatus = "ABCDHex_Show" And LimitedGame_RadioButton.Checked = False Then
            AnswerAappear_Label_Click(AnswerAappear_Label, Nothing)
            Exit Sub
        End If

        If MomentStatus = "ABCDHex_Show" And LimitedGame_RadioButton.Checked = True And Val(LevelQ) <= 10 Then
            AnswersABCDappear_Label_Click(AnswersABCDappear_Label, Nothing)
            Exit Sub
        End If

        If MomentStatus = "Question_AnswerA_Fired" Then
            AnswerBappear_Label_Click(AnswerBappear_Label, Nothing)
            Exit Sub
        End If

        If MomentStatus = "Question_AnswerB_Fired" Then
            AnswerCappear_Label_Click(AnswerCappear_Label, Nothing)
            Exit Sub
        End If

        If MomentStatus = "Question_AnswerC_Fired" Then
            AnswerDappear_Label_Click(AnswerDappear_Label, Nothing)
            Exit Sub
        End If

        If MomentStatus = "AnswerA_Final_Fired" Then
            CorrectAnswerReveal_Button_Click(CorrectAnswerReveal_Button, Nothing)
            Exit Sub
        End If

        If MomentStatus = "AnswerB_Final_Fired" Then
            CorrectAnswerReveal_Button_Click(CorrectAnswerReveal_Button, Nothing)
            Exit Sub
        End If

        If MomentStatus = "AnswerC_Final_Fired" Then
            CorrectAnswerReveal_Button_Click(CorrectAnswerReveal_Button, Nothing)
            Exit Sub
        End If

        If MomentStatus = "AnswerD_Final_Fired" Then
            CorrectAnswerReveal_Button_Click(CorrectAnswerReveal_Button, Nothing)
            Exit Sub
        End If

        If MomentStatus = "DoubleDip_Final_Fired" Then
            DoubleDipRevealCorrect_Button_Click(DoubleDipRevealCorrect_Button, Nothing)
            Exit Sub
        End If

        If MomentStatus = "CorrectAnswer_Fired" Then
            WonPrizeReveal_Button_Click(WonPrizeReveal_Button, Nothing)
            Exit Sub
        End If

        If MomentStatus = "WonPrize_Fired" Then
            RemoveQuestion_Button_Click(RemoveQuestion_Button, Nothing)
            Exit Sub
        End If

        If MomentStatus = "PhoneFriend_Dialing" Then
            PAFstart_Label_Click(PAFstart_Label, Nothing)
            Exit Sub
        End If

        If MomentStatus = "PhoneFriend_Progress" Then
            PAFabort_Label_Click(PAFabort_Label, Nothing)
            Exit Sub
        End If

        If MomentStatus = "AskAudience_Questioning" Then
            ATAstart_Label_Click(ATAstart_Label, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub CGConnection_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CGConnection_Button.Click
        GraphicsProcessingUnit.ConnectCG(CasparServerIP_TextBox.Text, CASPARCGLog_TextBox)
        System.Threading.Thread.Sleep(1000)
        MoneyTreeSet_Label_Click(MoneyTreeSet_Label, Nothing)
    End Sub

    Private Sub ATAendVote_Label_Click(sender As Object, e As EventArgs) Handles ATAendVote_Label.Click
        ataEndVote()
        GraphicsProcessingUnit.InteractiveWallScreenObj.MotionBackgroundDuringQuestion(LevelQ)

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)
        Explanation = ""
        '(toGoToClient)Host.DirectorsChat_TextBox.Text = Explanation
    End Sub

    Private Sub AudienceFriendAnswer_Label_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub CorrectAnswer_TextBox_TextChanged(sender As Object, e As EventArgs) Handles CorrectAnswer_TextBox.TextChanged
        CorrectAnswer = Helpers.Convert1234ToABCD(CorrectAnswer)

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


        If LevelQ <= 5 Then
            LimitedGameClock_TextBox.Text = "15"
        End If
        If LevelQ > 5 Then
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

        If LevelQ <= 5 Then
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
        If LevelQ > 5 Then
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
        GraphicsProcessingUnit.hideClockBarCG(LevelQ, LimitedGame_RadioButton.Checked)
        ''''

    End Sub

    Private Sub LimitedGameClockRestart_Label_Click(sender As Object, e As EventArgs) Handles LimitedGameClockRestart_Label.Click
        TimerLimitedGameSecDropClock.Start() 'Ctlcontrols.play()

        My.Computer.Audio.Play("C:\WWTBAM Removable Disc\US 2008\Resume The Clock.wav", AudioPlayMode.Background)

        MainGameMusicLayerObj.LimitedClockPlay() 'Ctlcontrols.play()

        ''''
        GraphicsProcessingUnit.showClockBarCG(LevelQ)
        ''''

    End Sub

    Private Sub SoundLX_Button_Click(sender As Object, e As EventArgs) Handles SoundLX_Button.Click
        PlayLXsound()
        GraphicsProcessingUnit.InteractiveWallScreenObj.LightDownEffect(LevelQ)
    End Sub

    Public Sub PlayLXsound()
        Timer_STOP.Stop()
        MainGameMusicLayerObj.PlayLXSound(LevelQ, VariableMilestone_TextBox.Text)

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

    Private Sub ClearExplanation_Label_Click(sender As Object, e As EventArgs) Handles ClearExplanation_Label.Click
        HostContPresentationLayer.OneTimeMessageSet("EXPLANATION-NONE")
        'HostContPresentationLayer.ExplanationDissolve()
        'HostContPresentationLayer.PronunciationDissolve()
    End Sub

    Private Sub SendExplanation_Label_Click(sender As Object, e As EventArgs) Handles SendExplanation_Label.Click
        HostContPresentationLayer.OneTimeMessageSet("EXPLANATION-FIRE")
        'HostContPresentationLayer.ExplanationFire()
        'HostContPresentationLayer.PronunciationFire()
    End Sub

    Private Sub VariableMilestoneSet_Button_Click(sender As Object, e As EventArgs) Handles VariableMilestoneSet_Button.Click
        VariableMilestone_TextBox.Text = LevelQ
        My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\29.QMilestone - Change.wav")

        Dim TextBox As String 'Izbeli gi prvo
        For index As Integer = 6 To 15 - 1
            TextBox = "QSum" + index.ToString + "_TextBox"
            For Each tb As TextBox In Me.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    tb.BackColor = Color.White
                End If
            Next
        Next

        TextBox = "QSum" + LevelQ + "_TextBox"
        For Each tb As TextBox In Me.Controls.OfType(Of TextBox)()
            If String.Compare(TextBox, tb.Name, True) = 0 Then
                tb.BackColor = Color.Bisque
                '' ******* CASPARCG ******* CASPARCG *******
                GraphicsProcessingUnit.setSecondMilestoneAtQ(LevelQ)

            End If
        Next

        MomentStatus = "VariableMilestone_Set"

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
        LevelQ = LevelQset_ComboBox.SelectedItem
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

        MomentStatus = "VariableMilestone_Set"

    End Sub

    Private Sub DoubleDipReady_Label_Click(sender As Object, e As EventArgs) Handles DoubleDipReady_Label.Click
        DoubleDipState = "DoubleDipFirstFinal"
        MainGameMusicLayerObj.PlayDoubleDipBackground()
        DDIP_1_Label_Click(DDIP_1_Label, Nothing)

        GraphicsProcessingUnit.InteractiveWallScreenObj.AnyBackgroundLoop("MotionBackgroundPulsingCircles")
    End Sub

    Private Sub DoubleDipCancel_Label_Click(sender As Object, e As EventArgs) Handles DoubleDipCancel_Label.Click
        DoubleDipState = ""
        MainGameMusicLayerObj.StopDoubleDipBackground() 'Ctlcontrols.stop()
        DDIP_0_Label_Click(DDIP_0_Label, Nothing)
        'MainGameMusicLayerObj.PlayHeartbeatMusic()
    End Sub

    Private Sub DoubleDipRevealCorrect_Button_Click(sender As Object, e As EventArgs) Handles DoubleDipRevealCorrect_Button.Click

        MomentStatus = "DoubleDipIsFirstFinalAnswer_Correct_Fired"

        DDIP_X_Label_Click(DDIP_X_Label, Nothing)

        If (CorrectAnswer <> FinalAnswer) And String.Equals(DoubleDipState, "DoubleDipFirstFinal", StringComparison.OrdinalIgnoreCase) Then
            Select Case Helpers.ConvertABCDTo1234(FinalAnswer)
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
            GraphicsProcessingUnit.DobleDip(FinalAnswer)
            ''
            DoubleDipState = "DoubleDipSecondFinal"

            SumeShow_CheckBox.Checked = True
            Empty_CheckBox.Checked = True

        ElseIf (CorrectAnswer <> FinalAnswer) Then
            CorrectAnswerReveal_Button_Click(CorrectAnswerReveal_Button, Nothing)

        ElseIf String.Equals(CorrectAnswer, FinalAnswer, StringComparison.OrdinalIgnoreCase) Then
            CorrectAnswerReveal_Button_Click(CorrectAnswerReveal_Button, Nothing)
        End If

        Me.TabControl2.SelectedTab = TabPage6

    End Sub

    Private Sub DoubleDipFinalA_Button_Click(sender As Object, e As EventArgs) Handles DoubleDipFinalA_Button.Click
        FinalA_Button_Click(FinalA_Button, Nothing)
        MomentStatus = "DoubleDipAnswer_Final_Fired"
        DoubleDipFirstAnswer = "1"
    End Sub
    Private Sub DoubleDipFinalB_Button_Click(sender As Object, e As EventArgs) Handles DoubleDipFinalB_Button.Click
        FinalB_Button_Click(FinalB_Button, Nothing)
        MomentStatus = "DoubleDipAnswer_Final_Fired"
        DoubleDipFirstAnswer = "2"
    End Sub
    Private Sub DoubleDipFinalC_Button_Click(sender As Object, e As EventArgs) Handles DoubleDipFinalC_Button.Click
        FinalC_Button_Click(FinalC_Button, Nothing)
        MomentStatus = "DoubleDipAnswer_Final_Fired"
        DoubleDipFirstAnswer = "3"
    End Sub
    Private Sub DoubleDipFinalD_Button_Click(sender As Object, e As EventArgs) Handles DoubleDipFinalD_Button.Click
        FinalD_Button_Click(FinalD_Button, Nothing)
        MomentStatus = "DoubleDipAnswer_Final_Fired"
        DoubleDipFirstAnswer = "4"
    End Sub

    Private Sub DDIP_0_Label_Click(sender As Object, e As EventArgs) Handles DDIP_0_Label.Click
        GuiContext.SomethingToDoWithLifeline(DoubleDipPosition, 1)
        Lifeline2Active = 1
    End Sub

    Private Sub DDIP_1_Label_Click(sender As Object, e As EventArgs) Handles DDIP_1_Label.Click
        GuiContext.SomethingToDoWithLifeline(DoubleDipPosition, 2)
        Lifeline2Active = 2
    End Sub

    Private Sub DDIP_X_Label_Click(sender As Object, e As EventArgs) Handles DDIP_X_Label.Click
        GuiContext.SomethingToDoWithLifeline(DoubleDipPosition, 0)
        Lifeline2Active = 0
    End Sub

    Public Sub GUIOperatorStateSet(MomentStatus As String)
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
        '"DoubleDipIsFirstFinalAnswer_Correct_Fired"
        '"DoubleDipAnswer_Final_Fired"

        'FinalA_Button.Visible = True
        'FinalB_Button.Visible = True
        'FinalC_Button.Visible = True
        'FinalD_Button.Visible = True
        'QuestionAppear_Button.Visible = True
        'WonPrizeReveal_Button.Visible = True


        Select Case MomentStatus
            Case "EmptyQuestion_Fired", "QuestionAnswers_Load"
                FinalA_Button.Visible = False
                FinalB_Button.Visible = False
                FinalC_Button.Visible = False
                FinalD_Button.Visible = False
                CorrectAnswerReveal_Button.Visible = False
                WonPrizeReveal_Button.Visible = False
                SoundLX_Button.Visible = True
                LifelineRemind_Button.Visible = False
                QFor_Button.Visible = False
                VariableMilestoneSet_Button.Visible = True
                VariableMilestone_TextBox.Visible = True
                WalkAwayStart_Button.Visible = False
                WalkAwayQoppinion_Label.Visible = False
                Lifeline1_PictureBox.Visible = False
                Lifeline2_PictureBox.Visible = False
                Lifeline3_PictureBox.Visible = False
                Lifeline4_PictureBox.Visible = False

            Case "Question_Fired"
                FinalA_Button.Visible = False
                FinalB_Button.Visible = False
                FinalC_Button.Visible = False
                FinalD_Button.Visible = False
                CorrectAnswerReveal_Button.Visible = False
                WonPrizeReveal_Button.Visible = False
                SoundLX_Button.Visible = False
                LifelineRemind_Button.Visible = False
                QFor_Button.Visible = True
                VariableMilestoneSet_Button.Visible = False
                VariableMilestone_TextBox.Visible = False
                WalkAwayStart_Button.Visible = False
                WalkAwayQoppinion_Label.Visible = False
                Lifeline1_PictureBox.Visible = False
                Lifeline2_PictureBox.Visible = False
                Lifeline3_PictureBox.Visible = False
                Lifeline4_PictureBox.Visible = False

            Case "AnswerA_Final_Fired",
                 "AnswerB_Final_Fired",
                 "AnswerC_Final_Fired",
                 "AnswerD_Final_Fired"

                FinalA_Button.Visible = False
                FinalB_Button.Visible = False
                FinalC_Button.Visible = False
                FinalD_Button.Visible = False
                WonPrizeReveal_Button.Visible = False
                CorrectAnswerReveal_Button.Visible = True
                SoundLX_Button.Visible = False
                LifelineRemind_Button.Visible = False
                QFor_Button.Visible = True
                VariableMilestoneSet_Button.Visible = False
                VariableMilestone_TextBox.Visible = False
                WalkAwayStart_Button.Visible = False
                Lifeline1_PictureBox.Visible = False
                Lifeline2_PictureBox.Visible = False
                Lifeline3_PictureBox.Visible = False
                Lifeline4_PictureBox.Visible = False

            Case "Question_AnswerD_Fired", "DoubleDipAnswer_Final_Fired" 'prikazi konecen
                FinalA_Button.Visible = True
                FinalB_Button.Visible = True
                FinalC_Button.Visible = True
                FinalD_Button.Visible = True
                WonPrizeReveal_Button.Visible = False
                SoundLX_Button.Visible = False
                LifelineRemind_Button.Visible = True
                QFor_Button.Visible = True
                VariableMilestoneSet_Button.Visible = False
                VariableMilestone_TextBox.Visible = False
                WalkAwayStart_Button.Visible = True
                WalkAwayQoppinion_Label.Visible = False
                Lifeline1_PictureBox.Visible = True
                Lifeline2_PictureBox.Visible = True
                Lifeline3_PictureBox.Visible = True
                Lifeline4_PictureBox.Visible = True

            Case "WonPrize_Fired"
                WonPrizeReveal_Button.Visible = False

            Case "CorrectAnswer_Fired"
                FinalA_Button.Visible = False
                FinalB_Button.Visible = False
                FinalC_Button.Visible = False
                FinalD_Button.Visible = False
                CorrectAnswerReveal_Button.Visible = False
                WonPrizeReveal_Button.Visible = True
                SoundLX_Button.Visible = True
                LifelineRemind_Button.Visible = False
                QFor_Button.Visible = False
                VariableMilestoneSet_Button.Visible = True
                VariableMilestone_TextBox.Visible = True
                WalkAwayStart_Button.Visible = False
                WalkAwayQoppinion_Label.Visible = False
                Lifeline1_PictureBox.Visible = False
                Lifeline2_PictureBox.Visible = False
                Lifeline3_PictureBox.Visible = False
                Lifeline4_PictureBox.Visible = False

            Case "DoubleDipIsFirstFinalAnswer_Correct_Fired"
                FinalA_Button.Visible = True
                FinalB_Button.Visible = True
                FinalC_Button.Visible = True
                FinalD_Button.Visible = True
                WonPrizeReveal_Button.Visible = False
                CorrectAnswerReveal_Button.Visible = False
                SoundLX_Button.Visible = False
                LifelineRemind_Button.Visible = False
                QFor_Button.Visible = True
                VariableMilestoneSet_Button.Visible = False
                VariableMilestone_TextBox.Visible = False
                WalkAwayStart_Button.Visible = False
                WalkAwayQoppinion_Label.Visible = False

            Case "Walkaway_Fired", "JustOpinion_Fired"
                WonPrizeReveal_Button.Visible = True
                CorrectAnswerReveal_Button.Visible = False
                LifelineRemind_Button.Visible = False
                QFor_Button.Visible = True
                WalkAwayStart_Button.Visible = False
                WalkAwayQoppinion_Label.Visible = True

        End Select

        NextThing_Button.Text = "CUE NEXT..." + NextActivity
    End Sub

    Public ReadOnly Property NextActivity() As String
        Get
            'Select Case MomentStatus
            '    Case ""
            '        Return ""
            '    Case Else
            '        Return ""
            'End Select
            'Return ""
            If MomentStatus = "QuestionAnswers_Load" Or MomentStatus = "VariableMilestone_Set" Then
                Return "Question_Fired"
            End If
            If MomentStatus = "Question_Fired" Then
                Return "Question_AnswerA_Fired"
            End If
            If MomentStatus = "ABCDHex_Show" And LimitedGame_RadioButton.Checked = False Then
                Return "Question_AnswerA_Fired"
            End If
            If MomentStatus = "ABCDHex_Show" And LimitedGame_RadioButton.Checked = True And Val(LevelQ) <= 10 Then
                'AnswersABCDappear_Label_Click(AnswersABCDappear_Label, Nothing)
                Return "Question_AnswerA_Fired"
            End If
            If MomentStatus = "Question_AnswerA_Fired" Then
                Return "Question_AnswerB_Fired"
            End If
            If MomentStatus = "Question_AnswerB_Fired" Then
                Return "Question_AnswerC_Fired"
            End If
            If MomentStatus = "Question_AnswerC_Fired" Then
                Return "Question_AnswerD_Fired"
            End If
            If MomentStatus = "AnswerA_Final_Fired" Then
                Return "CorrectAnswer_Fired"
            End If
            If MomentStatus = "AnswerB_Final_Fired" Then
                Return "CorrectAnswer_Fired"
            End If
            If MomentStatus = "AnswerC_Final_Fired" Then
                Return "CorrectAnswer_Fired"
            End If
            If MomentStatus = "AnswerD_Final_Fired" Then
                Return "CorrectAnswer_Fired"
            End If
            If MomentStatus = "DoubleDip_Final_Fired" Then
                'DoubleDipRevealCorrect_Button_Click(DoubleDipRevealCorrect_Button, Nothing)
            End If
            If MomentStatus = "CorrectAnswer_Fired" Then
                'WonPrizeReveal_Button_Click(WonPrizeReveal_Button, Nothing)
                Return "WonPrize_Fired"
            End If
            If MomentStatus = "WonPrize_Fired" Then
                Return "QuestionAnswers_Load"
            End If
            If MomentStatus = "PhoneFriend_Dialing" Then
                'PAFstart_Label_Click(PAFstart_Label, Nothing)
            End If
            If MomentStatus = "PhoneFriend_Progress" Then
                'PAFabort_Label_Click(PAFabort_Label, Nothing)
            End If
            If MomentStatus = "AskAudience_Questioning" Then
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

    Private Sub UpdateQuestionText_Event(sender As Object, e As MouseEventArgs) Handles Question_TextBox.MouseDoubleClick
        QuestionText = Question_TextBox.Text
    End Sub

    Private Sub StarApacheServer_Button_Click(sender As Object, e As EventArgs) Handles StarApacheServer_Button.Click
        Try
            Dim proc As Process = Process.Start("C:\\Users\\mihai\\Documents\\laragon-lite\\laragon.exe")
            'https://stackoverflow.com/questions/9679375/run-an-exe-from-c-sharp-code
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class