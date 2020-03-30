Imports System.Configuration
Imports System.Threading.Tasks
Imports Svt.Caspar

Public Class FFFOperator

    Dim casparQA As New CasparDevice
    Dim casparRO As New CasparDevice
    Dim casparWAC As New CasparDevice
    Dim casparFNS As New CasparDevice
    Dim cgDataQA, cgDataRO, cgDataWAC, cgDataFNS As New CasparCGDataCollection
    Dim questionID As String = "0"
    Dim MomentStatus As String
    Dim checkConnection As Short = 0
    Dim MusicFF As FFFMusicLayer

    Dim ListOfActiveContetants As New List(Of String)
    Dim ListOfActiveContestantsTime As New List(Of String)

    Dim ListOfAllContestants As New List(Of String)
    Dim ListOfAllContestantsTime As New List(Of String)

    Dim ElapsedTimes(10) As Double
    Dim IndexPositionOfFastestContestantByTime As Int16 = -10

    Dim NextRevealOrder As Int16 = 1
#Region "EVENT HANDLERS"
    Private Sub LoadFFFQuestion_Button_Click(sender As Object, e As EventArgs) Handles LoadFFFQuestion_Button.Click
        MomentStatus = "LoadedQuestion_Fired"
        ResetContestantScores()
        Using selectedQuestionTable As DataTable = DataLayer.SelectSuitableQuestion(LevelQFF_TextBox.Text, "2")
            With selectedQuestionTable
                If selectedQuestionTable.Rows.Count > 0 Then
                    QuestionFFF_TextBox.Text = .Rows(0)("Question").ToString().Replace("|", vbCrLf)
                    AnswerAFFF_TextBox.Text = .Rows(0)("Answer1").ToString()
                    AnswerBFFF_TextBox.Text = .Rows(0)("Answer2").ToString()
                    AnswerCFFF_TextBox.Text = .Rows(0)("Answer3").ToString()
                    AnswerDFFF_TextBox.Text = .Rows(0)("Answer4").ToString()
                    CorrectAnswer_TextBox.Text = .Rows(0)("CorrectAnswer").ToString()
                    questionID = .Rows(0)("QuestionID").ToString()
                End If
            End With
        End Using
        Dim tFFUnits As Task = Task.Run(Sub()
                                            FastestFingerManaging.QuestionLoad(QuestionFFF_TextBox.Text, AnswerAFFF_TextBox.Text, AnswerBFFF_TextBox.Text, AnswerCFFF_TextBox.Text, AnswerDFFF_TextBox.Text)
                                        End Sub)
        If casparQA.IsConnected Then
            Try
                CGQuestionSet()
                If cgDataQA.DataPairs.Count > 0 Then
                    casparQA.Channels(0).CG.Add(1, My.Settings.questionFlashTempl, True, cgDataQA)
                End If
            Catch ex As Exception
                MessageBox.Show("LoadedQuestion_Fired|" + ex.Message)
            End Try
        End If
    End Sub

    Private Sub QuestionFFFReveal_Button_Click(sender As Object, e As EventArgs) Handles QuestionFFFReveal_Button.Click
        MomentStatus = "QuestionFFF_Fired"
        ResetContestantScores()
        If casparQA.IsConnected Then
            CGQuestionSet()
            casparQA.Channels(0).CG.Invoke(1, "QuestionFlyIN") 'casparQA_.Channels(0).CG.Play(1)
        End If
        FastestFingerManaging.QuestionLoad(QuestionFFF_TextBox.Text, AnswerAFFF_TextBox.Text, AnswerBFFF_TextBox.Text, AnswerCFFF_TextBox.Text, AnswerDFFF_TextBox.Text)
        FastestFingerManaging.QuestionFire()
        DataLayer.MarkQuestionAnsweredDB(questionID)
        My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\10.Standby for Q.wav", AudioPlayMode.BackgroundLoop)
        GraphicsProcessingUnit.InteractiveWallScreenObj.MotionBackgroundDuringQuestion("2001")
    End Sub

    Private Sub AnswersABCDFFFReveal_Button_Click(sender As Object, e As EventArgs) Handles AnswersABCDFFFReveal_Button.Click
        ''CASPARCG:    ''QuestionAnswersFlyIN   ''showAnswersABCD
        MomentStatus = "AnswersABCDFFF_Fired"
        If casparQA.IsConnected Then
            casparQA.Channels(0).CG.Invoke(1, "QuestionAnswersFlyIN")
            casparQA.Channels(0).CG.Invoke(1, "showAnswersABCD")
        End If
        Dim tFFUnits As Task = Task.Run(Sub()
                                            FastestFingerManaging.FastestFingerFirstFire()
                                        End Sub)
        DataLayer.MarkQuestionFiredDB(questionID)
        My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\11.Three Beeps.wav", AudioPlayMode.Background)
        MusicFF.FFFastestFingerFirstAxWindowsMediaPlayer.Ctlcontrols.play()

        For i As Integer = 1 To Int(FFNumberOfDevicesToConnect_TextBox.Text)
            FastestFingerManaging.ConnectFFDevice(i.ToString, FFLog_TextBox.Text)
        Next

        For Each FF In FastestFingerManaging.Devices
            Try
                'FF.ThreadListener.Resume()
                'FF.ThreadDataPopulator.Resume()
                FF.ListenerEvent.Set()
                FF.DataPopulatorEvent.Set()
            Catch ex As Exception
            End Try
        Next

        Timer_RefreshFFFBoard.Start()
    End Sub

    Private Sub DissolveQAFFF_Label_Click(sender As Object, e As EventArgs) Handles DissolveQAFFF_Label.Click
        For Each FF In FastestFingerManaging.Devices
            Try
                'FF.ThreadListener.Suspend()
                'FF.ThreadDataPopulator.Suspend()
                FF.ListenerEvent.Reset()
                FF.DataPopulatorEvent.Reset()
                FastestFingerManaging.ResetFFFgame()

            Catch ex As Exception
            End Try
        Next
        Timer_RefreshFFFBoard.Stop()
        CGDissolveQA()
        UpdateClocksAnswers()
    End Sub

    Private Sub RightOrderReady_Label_Click(sender As Object, e As EventArgs) Handles RightOrderReady_Label.Click
        MomentStatus = "RightOrderReady"
        If casparRO.IsConnected Then
            Try
                CGSetRightOrder()
            Catch ex As Exception
                MessageBox.Show("RightOrderReady|" + ex.Message)
            End Try
        End If
        NextRevealOrder = 1
    End Sub

    Private Sub NextRevealRightOrder_Label_Click(sender As Object, e As EventArgs) Handles NextRevealRightOrder_Label.Click
        Select Case NextRevealOrder
            Case 1
                MusicFF.FF1stinOrderAxWindowsMediaPlayer.Ctlcontrols.play()
                MusicFF.FFFastestFingerFirstAxWindowsMediaPlayer.Ctlcontrols.stop()
                CGNextOrder(1)
            Case 2
                MusicFF.FF2ndinOrderAxWindowsMediaPlayer.Ctlcontrols.play()
                CGNextOrder(2)
            Case 3
                MusicFF.FF3rdinOrderAxWindowsMediaPlayer.Ctlcontrols.play()
                CGNextOrder(3)
            Case 4
                MusicFF.FF4thinOrderAxWindowsMediaPlayer.Ctlcontrols.play()
                CGNextOrder(4)
                MomentStatus = "NextRevealRightOrder"
        End Select
        NextRevealOrder += 1
    End Sub

    Private Sub WhoAnswerCorrectReady_Label_Click(sender As Object, e As EventArgs) Handles WhoAnswerCorrectReady_Label.Click
        MomentStatus = "WhoAnsweredCorrectlyReady"
        Try
            SetWhoAnsweredCorrectly()
            If casparWAC.IsConnected Then
                CGWhoAnsweredCorrectlySet()
                casparWAC.Channels(0).CG.Add(3, My.Settings.whoIsCorrectFlashTempl, True, cgDataWAC)
            End If
        Catch ex As Exception
            MessageBox.Show("WhoAnsweredCorrectlyReady|" + ex.Message)
        End Try
    End Sub

    Private Sub WhoAnswerCorrectGreen_Label_Click(sender As Object, e As EventArgs) Handles WhoAnswerCorrectGreen_Label.Click
        MomentStatus = "WhoAnsweredCorrectlyGreen"
        If casparWAC.IsConnected Then
            Try
                casparWAC.Channels(0).CG.Invoke(3, "FFTheFastest_Correct")
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        If IndexPositionOfFastestContestantByTime > -1 Then
            MusicFF.FFWhoIsCorrectNamesAxWindowsMediaPlayer.Ctlcontrols.play()
        End If
    End Sub

    Private Sub WhoAnswerCorrectBlink_Label_Click(sender As Object, e As EventArgs) Handles WhoAnswerCorrectBlink_Label.Click
        MomentStatus = "WhoAnsweredCorrectlyBlink"
        If casparWAC.IsConnected Then
            Try
                casparWAC.Channels(0).CG.Invoke(3, "FFTheFastest_Blink")
            Catch ex As Exception
                MessageBox.Show("WhoAnsweredCorrectlyBlink|" + ex.Message)
            End Try
        End If
        If IndexPositionOfFastestContestantByTime > -1 Then
            My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\Q5Win.wav", AudioPlayMode.Background)
        End If
        MusicFF.FFRightOrderAxWindowsMediaPlayer.Ctlcontrols.stop()
    End Sub
    Private Sub UpdateFFF_Button_Click(sender As Object, e As EventArgs) Handles UpdateFFFNames_Button.Click
        UpdateNames()
    End Sub

    Private Sub NextActivity_Button_Click(sender As Object, e As EventArgs) Handles NextActivity_Button.Click
        Select Case MomentStatus
            Case "QuestionFFF_Fired"
                AnswersABCDFFFReveal_Button_Click(AnswersABCDFFFReveal_Button, Nothing)
            Case "AnswersABCDFFF_Fired"
                'CGDissolveQA()
                'CGSetRightOrder()
                DissolveQAFFF_Label_Click(DissolveQAFFF_Label, Nothing)
            Case "QuestionAnswers_Dissolved"
                RightOrderReady_Label_Click(RightOrderReady_Label, Nothing)
            Case "RightOrderReady"
                RightOrderFlyIN_Label_Click(RightOrderFlyIN_Label, Nothing)
            Case "RightOrderFlyIN"
                NextRevealRightOrder_Label_Click(NextRevealRightOrder_Label, Nothing)
            Case "NextRevealRightOrder"
                WhoAnswerCorrectReady_Label_Click(WhoAnswerCorrectReady_Label, Nothing)
            Case "WhoAnsweredCorrectlyReady"
                WhoAnswerCorrectGreen_Label_Click(WhoAnswerCorrectGreen_Label, Nothing)
            Case "WhoAnsweredCorrectlyGreen"
                WhoAnswerCorrectBlink_Label_Click(WhoAnswerCorrectBlink_Label, Nothing)
            Case "WhoAnsweredCorrectlyBlink"
                WinnerClock_Label_Click(WinnerClock_Label, Nothing)
            Case "WinnerClockName"
                ClearGUI_Button_Click(ClearGUI_Button, Nothing)
            Case "LoadedQuestion_Fired"
                QuestionFFFReveal_Button_Click(QuestionFFFReveal_Button, Nothing)
        End Select
    End Sub

    Private Sub ContestantIsActive_CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles _
            ContestantIsActive1_CheckBox.CheckedChanged, ContestantIsActive2_CheckBox.CheckedChanged, ContestantIsActive3_CheckBox.CheckedChanged, ContestantIsActive4_CheckBox.CheckedChanged, ContestantIsActive5_CheckBox.CheckedChanged,
        ContestantIsActive6_CheckBox.CheckedChanged, ContestantIsActive7_CheckBox.CheckedChanged, ContestantIsActive8_CheckBox.CheckedChanged, ContestantIsActive9_CheckBox.CheckedChanged, ContestantIsActive10_CheckBox.CheckedChanged

        UpdateActiveStatus()
    End Sub

    Private Sub CGConnection_Button_Click(sender As Object, e As EventArgs) Handles CGConnection_Button.Click
        Try
            My.Settings.casparHostName = CasparServerIP_TextBox.Text

            casparQA.Settings.Hostname = My.Settings.casparHostName
            casparQA.Settings.Port = My.Settings.casparPort
            casparQA.Connect()

            System.Threading.Thread.Sleep(500)

            casparRO.Settings.Hostname = My.Settings.casparHostName
            casparRO.Settings.Port = My.Settings.casparPort
            casparRO.Connect()

            System.Threading.Thread.Sleep(500)

            casparWAC.Settings.Hostname = My.Settings.casparHostName
            casparWAC.Settings.Port = My.Settings.casparPort
            casparWAC.Connect()

            System.Threading.Thread.Sleep(500)

            casparFNS.Settings.Hostname = My.Settings.casparHostName
            casparFNS.Settings.Port = My.Settings.casparPort
            casparFNS.Connect()

            System.Threading.Thread.Sleep(250)

            If casparQA.IsConnected Then
                CASPARCGLog_TextBox.Text += "Connected with CasparCG!" + vbCrLf
            Else
                casparQA.Settings.Hostname = My.Settings.casparHostName
                casparQA.Settings.Port = My.Settings.casparPort
                If Not casparQA.IsConnected Then CASPARCGLog_TextBox.Text += "NOT Connected with CasparCG!" + vbCrLf
            End If
            '''''
            If casparRO.IsConnected Then
                CASPARCGLog_TextBox.Text += "Connected with CasparCG-RO!" + vbCrLf
            Else
                casparRO.Settings.Hostname = My.Settings.casparHostName
                casparRO.Settings.Port = My.Settings.casparPort
                If Not casparRO.IsConnected Then CASPARCGLog_TextBox.Text += "NOT Connected with CasparCG-RO!" + vbCrLf
            End If
            '''''
            If casparWAC.IsConnected Then
                CASPARCGLog_TextBox.Text += "Connected with CasparCG-WAC!" + vbCrLf
            Else
                casparWAC.Settings.Hostname = My.Settings.casparHostName
                casparWAC.Settings.Port = My.Settings.casparPort
                If Not casparWAC.IsConnected Then CASPARCGLog_TextBox.Text += "NOT Connected with CasparCG-WAC!" + vbCrLf
            End If
            '''''
            If casparFNS.IsConnected Then
                CASPARCGLog_TextBox.Text += "Connected with CasparCG-FNS!" + vbCrLf
            Else
                casparFNS.Settings.Hostname = My.Settings.casparHostName
                casparFNS.Settings.Port = My.Settings.casparPort
                If Not casparFNS.IsConnected Then CASPARCGLog_TextBox.Text += "NOT Connected with CasparCG-FNS!" + vbCrLf

            End If
            checkConnection += 1
            'If checkConnection < 5 Then
            '    CGConnection_Button_Click(CGConnection_Button, Nothing)
            'End If
        Catch ex As Exception
            CASPARCGLog_TextBox.Text += "CGConnection_Button_Click|" + ex.Message + vbCrLf
        End Try

        System.Threading.Thread.Sleep(250)
    End Sub

    Private Sub FFFConnect_Button_Click(sender As Object, e As EventArgs)
        'FFFManagement.ConnectFFFDevices(FFLog_TextBox.Text)
        'FFFManagement.ConnectFFFCommands(FFLog_TextBox.Text)
    End Sub

#End Region

#Region "METHODS"
    Sub ResetContestantScores()
        Dim p As String = HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/ResetFFFPlayData.php")
        For i As Int16 = 1 To 10
            IndexPositionOfFastestContestantByTime = -10
            TheFastestContestantIndex_TextBox.Text = ""
            TheFastestContestantName_TextBox.Text = ""
            FastestFingerManaging.Devices.ElementAt(i - 1).Order = String.Empty
            FastestFingerManaging.Devices.ElementAt(i - 1).Position = i.ToString
            FastestFingerManaging.Devices.ElementAt(i - 1).ElapsedTime = String.Empty
            FastestFingerManaging.Devices.ElementAt(i - 1).IsCorrect = False
        Next
        For i As Int16 = 1 To 10
            Dim TextBox As String = "ContestantName" + i.ToString + "_TextBox"
            For Each tb As TextBox In TabPage1.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    tb.BackColor = Color.White
                End If
            Next
        Next
        For i As Int16 = 1 To 10
            Dim TextBox As String = "ContestantCity" + i.ToString + "_TextBox"
            For Each tb As TextBox In TabPage1.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    tb.BackColor = Color.White
                End If
            Next
        Next
        For i As Int16 = 1 To 10
            Dim TextBox As String = "ContestantElapsedTime" + i.ToString + "_TextBox"
            For Each tb As TextBox In TabPage1.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    tb.Text = "0.000"
                    tb.BackColor = Color.White
                End If
            Next
        Next
        For i As Int16 = 1 To 10
            Dim TextBox As String = "ContestantOrder" + i.ToString + "_TextBox"
            For Each tb As TextBox In TabPage1.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    tb.Text = "0000"
                    tb.BackColor = Color.White
                End If
            Next
        Next
    End Sub

    Sub SetWhoAnsweredCorrectly()
        ListOfActiveContetants.Clear()
        ListOfActiveContestantsTime.Clear()

        For Each Contestant As FFFDataFromDevice In FastestFingerManaging.Devices
            If Contestant.IsActive Then
                ListOfActiveContetants.Add(Contestant.NameLastName)
                If Contestant.IsCorrect Then
                    ListOfActiveContestantsTime.Add(Contestant.ElapsedTime)
                Else
                    ListOfActiveContestantsTime.Add(String.Empty)
                End If
            End If
        Next

    End Sub

    Sub CreateUpdateListOfAllContestantNamesClocks()
        ListOfAllContestants.Clear()
        ListOfAllContestantsTime.Clear()
        For i As Int16 = 1 To 10
            Dim NameTextBox As String = "ContestantName" + i.ToString + "_TextBox"
            Dim ElapsedTimeTextBox As String = "ContestantElapsedTime" + i.ToString + "_TextBox"
            For Each tb As TextBox In TabPage1.Controls.OfType(Of TextBox)()
                If String.Compare(NameTextBox, tb.Name, True) = 0 Then
                    ListOfAllContestants.Add(tb.Text)
                ElseIf String.Compare(ElapsedTimeTextBox, tb.Name, True) = 0 Then
                    ListOfAllContestantsTime.Add(tb.Text)
                End If
            Next
        Next
    End Sub

    Private Sub FFFBoardRefresh()
        IndexPositionOfFastestContestantByTime = -10
        For i As Int16 = 1 To 10
            Dim TextBox As String = "ContestantOrder" + i.ToString + "_TextBox"
            For Each tb As TextBox In TabPage1.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    tb.Text = FastestFingerManaging.Devices.ElementAt(i - 1).Order
                End If
            Next
        Next
        For i As Int16 = 1 To 10
            Dim TextBox As String = "ContestantElapsedTime" + i.ToString + "_TextBox"
            For Each tb As TextBox In TabPage1.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    tb.Text = FastestFingerManaging.Devices.ElementAt(i - 1).ElapsedTime
                End If
            Next
        Next
        GreenIfCorrectAndUpdateCorrectStatus()
    End Sub

    Sub UpdateNames()
        For i As Int16 = 1 To 10
            Dim TextBox As String = "ContestantName" + i.ToString + "_TextBox"
            For Each tb As TextBox In TabPage1.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    Dim city As String = String.Empty
                    For Each tb1 As TextBox In TabPage1.Controls.OfType(Of TextBox)()
                        If String.Compare("ContestantCity" + i.ToString + "_TextBox", tb1.Name, True) = 0 Then
                            city = tb1.Text
                        End If
                    Next
                    FastestFingerManaging.Devices.ElementAt(i - 1).NameLastName = tb.Text
                    FastestFingerManaging.Devices.ElementAt(i - 1).City = city
                    FastestFingerManaging.ContestantLoad(i.ToString, tb.Text, city)
                End If
            Next
        Next
        CreateUpdateListOfAllContestantNamesClocks()
    End Sub

    Sub ActivateXNumberOfContestantsAtStartUp()
        FFNumberOfDevicesToConnect_TextBox.Text = FFFOperatorSettings.Default.StartUpNumberOfFFFContestants
        For i As Short = 1 To FFFOperatorSettings.Default.StartUpNumberOfFFFContestants
            Dim CheckBox As String = "ContestantIsActive" + i.ToString + "_CheckBox"
            For Each tb As CheckBox In TabPage1.Controls.OfType(Of CheckBox)()
                If String.Compare(CheckBox, tb.Name, True) = 0 Then
                    tb.Checked = True
                End If
            Next
        Next
    End Sub

    Sub UpdateClocksAnswers()
        IndexPositionOfFastestContestantByTime = -10
        For index As Integer = 1 To 10
            Dim ContestantOrderTextBox As String = "ContestantOrder" + index.ToString + "_TextBox"
            Dim ContestantElapsedTimeTextBox As String = "ContestantElapsedTime" + index.ToString + "_TextBox"
            For Each tb As TextBox In TabPage1.Controls.OfType(Of TextBox)()
                If String.Compare(ContestantOrderTextBox, tb.Name, True) = 0 Then
                    FastestFingerManaging.Devices.ElementAt(index - 1).Order = tb.Text
                End If
                If String.Compare(ContestantElapsedTimeTextBox, tb.Name, True) = 0 Then
                    FastestFingerManaging.Devices.ElementAt(index - 1).ElapsedTime = tb.Text
                End If
            Next
        Next
        GreenIfCorrectAndUpdateCorrectStatus()
        CreateUpdateListOfAllContestantNamesClocks()
    End Sub

    Sub UpdateActiveStatus()
        For i As Int16 = 1 To 10
            Dim CheckBox As String = "ContestantIsActive" + i.ToString + "_CheckBox"
            For Each cb As CheckBox In TabPage1.Controls.OfType(Of CheckBox)()
                If String.Compare(CheckBox, cb.Name, True) = 0 Then
                    If cb.Checked = True Then
                        FastestFingerManaging.Devices.ElementAt(i - 1).IsActive = True
                    ElseIf cb.Checked = False Then
                        FastestFingerManaging.Devices.ElementAt(i - 1).IsActive = False
                    End If
                End If
            Next
        Next
    End Sub

    Sub GreenIfCorrectAndUpdateCorrectStatus()
        For i As Int16 = 1 To 10
            Dim ContestantOrderTextBox As String = "ContestantOrder" + i.ToString + "_TextBox"
            Dim ContestantNameTextBox As String = "ContestantName" + i.ToString + "_TextBox"
            Dim ContestantCityTextBox As String = "ContestantCity" + i.ToString + "_TextBox"
            Dim ContestantElapsedTimeTextBox As String = "ContestantElapsedTime" + i.ToString + "_TextBox"
            For Each tb As TextBox In TabPage1.Controls.OfType(Of TextBox)()
                If String.Compare(ContestantOrderTextBox, tb.Name, True) = 0 Then
                    If CorrectAnswer_TextBox.Text = FastestFingerManaging.Devices.ElementAt(i - 1).Order And FastestFingerManaging.Devices.ElementAt(i - 1).IsActive Then
                        FastestFingerManaging.Devices.ElementAt(i - 1).IsCorrect = True
                        ReturnThePositionOfFastest()
                        tb.BackColor = Color.PaleGreen
                        ChangeColorOfContestantRow(ContestantNameTextBox, Color.PaleGreen)
                        ChangeColorOfContestantRow(ContestantCityTextBox, Color.PaleGreen)
                        ChangeColorOfContestantRow(ContestantElapsedTimeTextBox, Color.PaleGreen)
                    Else
                        FastestFingerManaging.Devices.ElementAt(i - 1).IsCorrect = False
                        tb.BackColor = Color.White
                        ChangeColorOfContestantRow(ContestantNameTextBox, Color.White)
                        ChangeColorOfContestantRow(ContestantCityTextBox, Color.White)
                        ChangeColorOfContestantRow(ContestantElapsedTimeTextBox, Color.White)
                    End If
                End If
            Next
        Next


    End Sub

    Sub ChangeColorOfContestantRow(textBoxName As String, color As System.Drawing.Color)
        For Each tb As TextBox In TabPage1.Controls.OfType(Of TextBox)()
            If String.Compare(textBoxName, tb.Name, True) = 0 Then
                tb.BackColor = color
            End If
        Next
    End Sub

    Function ReturnThePositionOfFastest()
        If IndexPositionOfFastestContestantByTime = -10 Then
            For j As Int16 = 1 To 10
                Double.TryParse(FastestFingerManaging.Devices.ElementAt(j - 1).ElapsedTime, ElapsedTimes(j - 1))
                If FastestFingerManaging.Devices.ElementAt(j - 1).Order = CorrectAnswer_TextBox.Text And FastestFingerManaging.Devices.ElementAt(j - 1).IsActive Then
                    ElapsedTimes(j - 1) -= 1000
                End If
                IndexPositionOfFastestContestantByTime = Array.IndexOf(ElapsedTimes, ElapsedTimes.Min)
                TheFastestContestantIndex_TextBox.Text = IndexPositionOfFastestContestantByTime + 1
                TheFastestContestantName_TextBox.Text = ListOfAllContestants(IndexPositionOfFastestContestantByTime)
            Next
        End If
    End Function

    Sub ResetWholeTable()
        For i As Int16 = 1 To 10
            Dim TextBox As String = "ContestantName" + i.ToString + "_TextBox"
            For Each tb As TextBox In TabPage1.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    tb.Text = "ContestantName" + i.ToString
                    tb.BackColor = Color.White
                End If
            Next
        Next
        For i As Int16 = 1 To 10
            Dim TextBox As String = "ContestantCity" + i.ToString + "_TextBox"
            For Each tb As TextBox In TabPage1.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    tb.Text = "City"
                    tb.BackColor = Color.White
                End If
            Next
        Next
        For i As Int16 = 1 To 10
            Dim TextBox As String = "ContestantElapsedTime" + i.ToString + "_TextBox"
            For Each tb As TextBox In TabPage1.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    tb.Text = "0.000"
                    tb.BackColor = Color.White
                End If
            Next
        Next
        For i As Int16 = 1 To 10
            Dim TextBox As String = "ContestantOrder" + i.ToString + "_TextBox"
            For Each tb As TextBox In TabPage1.Controls.OfType(Of TextBox)()
                If String.Compare(TextBox, tb.Name, True) = 0 Then
                    tb.Text = "0000"
                    tb.BackColor = Color.White
                End If
            Next
        Next
        For i As Int16 = 1 To 10
            Dim CheckBox As String = "ContestantIsActive" + i.ToString + "_CheckBox"
            For Each cb As CheckBox In TabPage1.Controls.OfType(Of CheckBox)()
                If String.Compare(CheckBox, cb.Name, True) = 0 Then
                    cb.Checked = True
                End If
            Next
        Next
    End Sub
    Sub CGDissolveQA()
        MomentStatus = "QuestionAnswers_Dissolved"
        Try
            If casparQA.IsConnected Then
                casparQA.Channels(0).CG.Remove(1)
            End If
        Catch ex As Exception
            MessageBox.Show("QuestionAnswers_Dissolved|" + ex.Message)
        End Try
    End Sub
    Sub CGSetRightOrder()
        Try
            If casparRO.IsConnected Then
                CGRightOrderSet()
                If cgDataRO.DataPairs.Count > 0 Then
                    casparRO.Channels(0).CG.Add(3, My.Settings.rightOrderFlashTempl, True, cgDataRO)
                Else
                    casparRO.Channels(0).CG.Add(3, My.Settings.rightOrderFlashTempl, True)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("CGSetRightOrder|" + ex.Message)
        End Try
    End Sub

    Private Sub Timer_RefreshFFFBoard_Tick(sender As Object, e As EventArgs) Handles Timer_RefreshFFFBoard.Tick
        FFFBoardRefresh()
    End Sub

    Private Sub StandByContestant_Button_Click(sender As Object, e As EventArgs) Handles StandByContestant_Button.Click
        UpdateNames()
        ResetContestantScores()
        FastestFingerManaging.StandByContestantName()
    End Sub

    Private Sub FFFConnectSingleDevice_Button_Click(sender As Object, e As EventArgs) Handles FFFConnectSingleDevice_Button.Click
        FastestFingerManaging.ConnectFFDevice(FFNumberDevice_TextBox.Text, FFLog_TextBox.Text)
    End Sub

    Private Sub FFFConnectMultipleDevices_Click(sender As Object, e As EventArgs) Handles FFFConnectMultipleDevices_Button.Click
        For i As Integer = 1 To Int(FFNumberOfDevicesToConnect_TextBox.Text)
            FastestFingerManaging.ConnectFFDevice(i.ToString, FFLog_TextBox.Text)
        Next
    End Sub

    Private Sub FFFOperator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UpdateNames()
        UpdateActiveStatus()
        ActivateXNumberOfContestantsAtStartUp()
        MusicFF = New FFFMusicLayer
    End Sub

    Private Sub FFFOperator_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            FastestFingerManaging.DisconnectFFFDevices("")
        Catch ex As Exception
        End Try
    End Sub

    Sub CGNextOrder(Row As String)
        If casparRO.IsConnected Then
            Try
                casparRO.Channels(0).CG.Invoke(3, "FFFNum" + Row + "FlyIN")
            Catch ex As Exception
                MessageBox.Show("CGNextOrder|" + ex.Message)
            End Try
        End If
    End Sub

    Private Sub ClearGUI_Button_Click(sender As Object, e As EventArgs) Handles ClearGUI_Button.Click
        If casparQA.IsConnected Then
            Try
                casparQA.Channels(0).CG.Remove(4)
            Catch ex As Exception
                MessageBox.Show("ClearGUI_Label_Click|" + ex.Message)
            End Try
        End If
    End Sub

    Public Sub CGQuestionSet()
        '' ******* CASPARCG ******* CASPARCG ******* CASPARCG ******* ******* CASPARCG ******* CASPARCG ******* CASPARCG *******
        Try
            Dim Question_Text As String
            Question_Text = QuestionFFF_TextBox.Text

            'cgDataQA.Clear()

            If Question_Text.Contains(vbLf) Or Question_Text.Contains(vbCr) Or Question_Text.Contains("\n") Or Question_Text.Contains("\r\n") Then
                cgDataQA.SetData("QuestionML_TextField", QuestionFFF_TextBox.Text)
                cgDataQA.SetData("QuestionSL_TextField", "")
            Else
                cgDataQA.SetData("QuestionSL_TextField", QuestionFFF_TextBox.Text)
                cgDataQA.SetData("QuestionML_TextField", "")
            End If

            cgDataQA.SetData("AnswerA_TextField", AnswerAFFF_TextBox.Text)
            cgDataQA.SetData("AnswerB_TextField", AnswerBFFF_TextBox.Text)
            cgDataQA.SetData("AnswerC_TextField", AnswerCFFF_TextBox.Text)
            cgDataQA.SetData("AnswerD_TextField", AnswerDFFF_TextBox.Text)

            cgDataQA.SetData("MarkA_TextField", "A:")
            cgDataQA.SetData("MarkB_TextField", "B:")
            cgDataQA.SetData("MarkC_TextField", "C:")
            cgDataQA.SetData("MarkD_TextField", "D:")
        Catch ex As Exception
            MessageBox.Show("CGQuestionSet|" + ex.Message)
        End Try

    End Sub

    Private Sub WinnerClock_Label_Click(sender As Object, e As EventArgs) Handles WinnerClock_Label.Click
        '' ******* CASPARCG ******* CASPARCG ******* CASPARCG ******* ******* CASPARCG ******* CASPARCG ******* CASPARCG *******
        MomentStatus = "WinnerClockName"
        Try
            If casparQA.IsConnected Then
                casparQA.Channels(0).CG.Remove(3)
            End If

            If IndexPositionOfFastestContestantByTime <= -1 Then Return

            If casparFNS.IsConnected Then
                cgDataFNS.SetData("FFName_TextField", ListOfAllContestants.Item(IndexPositionOfFastestContestantByTime))
                cgDataFNS.SetData("FFClock_TextField", ListOfAllContestantsTime.Item(IndexPositionOfFastestContestantByTime))
                casparFNS.Channels(0).CG.Add(4, My.Settings.fastestNameClokStrapFlashTempl, True, cgDataFNS)
            End If
        Catch ex As Exception
            MessageBox.Show("CGWinnerClock|" + ex.Message)
        End Try

        'FFName_TextField
        'FFClock_TextField
    End Sub

    Private Sub UpdateFFFClock_Button_Click(sender As Object, e As EventArgs) Handles UpdateFFFClock_Button.Click
        UpdateClocksAnswers()
    End Sub

    Private Sub ConfigurationFFF_Button_Click(sender As Object, e As EventArgs) Handles ConfigurationFFF_Button.Click

    End Sub

    Private Sub RightOrderFlyIN_Label_Click(sender As Object, e As EventArgs) Handles RightOrderFlyIN_Label.Click
        MomentStatus = "RightOrderFlyIN"
        If casparRO.IsConnected Then
            Try
                casparRO.Channels(0).CG.Invoke(3, "FFFOrderFlyIN")
            Catch ex As Exception
                MessageBox.Show("RightOrderFlyIN|" + ex.Message)
            End Try
        End If
        MusicFF.FFRightOrderAxWindowsMediaPlayer.Ctlcontrols.play()
    End Sub

    Public Sub CGRightOrderSet()
        '' ******* CASPARCG ******* CASPARCG ******* CASPARCG ******* ******* CASPARCG ******* CASPARCG ******* CASPARCG *******
        Try
            Dim CorrectOrder = CorrectAnswer_TextBox.Text.Replace("1", "A").Replace("2", "B").Replace("3", "C").Replace("4", "D")
            If CorrectOrder.Length < 4 Then Return

            cgDataRO.SetData("Question_TextField", QuestionFFF_TextBox.Text)

            If CorrectOrder(0) = "A" Then
                cgDataRO.SetData("Answer1_TextField", AnswerAFFF_TextBox.Text)
            ElseIf CorrectOrder(0) = "B" Then
                cgDataRO.SetData("Answer1_TextField", AnswerBFFF_TextBox.Text)
            ElseIf CorrectOrder(0) = "C" Then
                cgDataRO.SetData("Answer1_TextField", AnswerCFFF_TextBox.Text)
            ElseIf CorrectOrder(0) = "D" Then
                cgDataRO.SetData("Answer1_TextField", AnswerDFFF_TextBox.Text)
            End If
            If CorrectOrder(1) = "A" Then
                cgDataRO.SetData("Answer2_TextField", AnswerAFFF_TextBox.Text)
            ElseIf CorrectOrder(1) = "B" Then
                cgDataRO.SetData("Answer2_TextField", AnswerBFFF_TextBox.Text)
            ElseIf CorrectOrder(1) = "C" Then
                cgDataRO.SetData("Answer2_TextField", AnswerCFFF_TextBox.Text)
            ElseIf CorrectOrder(1) = "D" Then
                cgDataRO.SetData("Answer2_TextField", AnswerDFFF_TextBox.Text)
            End If
            If CorrectOrder(2) = "A" Then
                cgDataRO.SetData("Answer3_TextField", AnswerAFFF_TextBox.Text)
            ElseIf CorrectOrder(2) = "B" Then
                cgDataRO.SetData("Answer3_TextField", AnswerBFFF_TextBox.Text)
            ElseIf CorrectOrder(2) = "C" Then
                cgDataRO.SetData("Answer3_TextField", AnswerCFFF_TextBox.Text)
            ElseIf CorrectOrder(2) = "D" Then
                cgDataRO.SetData("Answer3_TextField", AnswerDFFF_TextBox.Text)
            End If
            If CorrectOrder(3) = "A" Then
                cgDataRO.SetData("Answer4_TextField", AnswerAFFF_TextBox.Text)
            ElseIf CorrectOrder(3) = "B" Then
                cgDataRO.SetData("Answer4_TextField", AnswerBFFF_TextBox.Text)
            ElseIf CorrectOrder(3) = "C" Then
                cgDataRO.SetData("Answer4_TextField", AnswerCFFF_TextBox.Text)
            ElseIf CorrectOrder(3) = "D" Then
                cgDataRO.SetData("Answer4_TextField", AnswerDFFF_TextBox.Text)
            End If

            cgDataRO.SetData("Mark1_TextField", CorrectOrder(0) + ":")
            cgDataRO.SetData("Mark2_TextField", CorrectOrder(1) + ":")
            cgDataRO.SetData("Mark3_TextField", CorrectOrder(2) + ":")
            cgDataRO.SetData("Mark4_TextField", CorrectOrder(3) + ":")

        Catch ex As Exception
            MessageBox.Show("CGRightOrderSet|" + ex.Message)
        End Try

    End Sub

    Public Sub CGWhoAnsweredCorrectlySet()
        '' ******* CASPARCG ******* CASPARCG ******* CASPARCG ******* ******* CASPARCG ******* CASPARCG ******* CASPARCG *******
        Try
            Dim Corrects As String = ""

            cgDataWAC.Clear()
            For Each contestant In ListOfActiveContetants
                cgDataWAC.SetData("FF" + (ListOfActiveContetants.IndexOf(contestant) + 1).ToString + "Name_TextField", contestant)
            Next

            Dim positionHere As Short = 0
            Dim index As Short = 1
            For Each clock In ListOfActiveContestantsTime
                cgDataWAC.SetData("FF" + index.ToString + "Clock_TextField", clock)
                If Not String.IsNullOrEmpty(clock.Trim) Then
                    Corrects += positionHere.ToString
                End If
                positionHere += 1
                index += 1
            Next

            'Potrebno e zatoa sto ako e najbrz na pozicija 7 a pred nego nema igraci, na CG da go pokaze kako prv bidejkji tamu se trgaat od nadolu i indexite se menuvaat
            Dim CountOfInactiveContestantsPositionedBeforeTheFastest = 0
            If IndexPositionOfFastestContestantByTime > -1 Then
                For i As Integer = 0 To IndexPositionOfFastestContestantByTime
                    If Not FastestFingerManaging.Devices.ElementAt(i).IsActive Then
                        CountOfInactiveContestantsPositionedBeforeTheFastest += 1
                    End If
                Next
            End If

            cgDataWAC.SetData("FFPresent_TextField", ListOfActiveContetants.Count.ToString)
            cgDataWAC.SetData("FFTheCorrect_TextField", Corrects.ToString)
            cgDataWAC.SetData("FFTheFastest_TextField", (IndexPositionOfFastestContestantByTime - CountOfInactiveContestantsPositionedBeforeTheFastest).ToString)

        Catch ex As Exception
            MessageBox.Show("CGWhoAnsweredCorrectlySet|" + ex.Message)
        End Try

    End Sub

#End Region

End Class