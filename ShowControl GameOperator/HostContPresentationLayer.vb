﻿Imports System.Threading.Tasks

Public Class HostContPresentationLayer

    Private Shared Message As New System.Text.StringBuilder

#Region "MESSAGE-METHODS"

    Public Shared Sub GamePlayStateSet(Optional MomentStatus As String = "")

        'HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?QuestionText={Quiz_Operator.QuestionText}&Answer1Text={Quiz_Operator.Answer1Text}&Answer2Text={Quiz_Operator.Answer2Text}&Answer3Text={Quiz_Operator.Answer3Text}&Answer4Text={Quiz_Operator.Answer4Text}&FinalAnswer={Helpers.ConvertABCDTo1234(Quiz_Operator.FinalAnswer)}&CorrectAnswer={Helpers.ConvertABCDTo1234(Quiz_Operator.CorrectAnswer)}&ExplanationText={Quiz_Operator.Explanation}&PronunciationText={Quiz_Operator.Pronunciation}&ContestantNameCity={String.Format("{0} {1}", Quiz_Operator.ContestantName_Textbox.Text, Quiz_Operator.ContestantLastName_Textbox.Text)}&PartnerName={String.Format("{0}", Quiz_Operator.ContestantPartner_Textbox.Text)}&BankDropIfCorrectIfWrong={Quiz_Operator.CurentGameStatusData}&QLevel={Math.Max(Quiz_Operator.LevelQ - 1, 0)}&LifelinesState={Quiz_Operator.LifelinesState}&FiftyFifty={Quiz_Operator.FiftyFifty}&AtaPercents={Quiz_Operator.AtaVotes}&SecondMilestoneAt={Quiz_Operator.VariableMilestone_TextBox.Text}&DoubleDipState={Quiz_Operator.DoubleDipState}&DoubleDipFirstAnswer={Quiz_Operator.DoubleDipFirstAnswer}&ActiveLifelines={Quiz_Operator.ActiveLifelines}")
        HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?QuestionText={Uri.EscapeDataString(Quiz_Operator.QuestionText)}&Answer1Text={Uri.EscapeDataString(Quiz_Operator.Answer1Text)}&Answer2Text={Uri.EscapeDataString(Quiz_Operator.Answer2Text)}&Answer3Text={Uri.EscapeDataString(Quiz_Operator.Answer3Text)}&Answer4Text={Uri.EscapeDataString(Quiz_Operator.Answer4Text)}&FinalAnswer={Helpers.ConvertABCDTo1234(Quiz_Operator.FinalAnswer)}&CorrectAnswer={Helpers.ConvertABCDTo1234(Quiz_Operator.CorrectAnswer)}&ExplanationText={Uri.EscapeDataString(Quiz_Operator.Explanation)}&PronunciationText={Uri.EscapeDataString(Quiz_Operator.Pronunciation)}&ContestantNameCity={String.Format("{0} {1}", Uri.EscapeDataString(Quiz_Operator.ContestantName_Textbox.Text), Uri.EscapeDataString(Quiz_Operator.ContestantLastName_Textbox.Text))}&PartnerName={String.Format("{0}", Uri.EscapeDataString(Quiz_Operator.ContestantPartner_Textbox.Text))}&BankDropIfCorrectIfWrong={Quiz_Operator.CurentGameStatusData}&QLevel={Math.Max(Quiz_Operator.LevelQ - 1, 0)}&LifelinesState={Quiz_Operator.LifelinesState}&FiftyFifty={Quiz_Operator.FiftyFifty}&AtaPercents={Quiz_Operator.AtaVotes}&SecondMilestoneAt={Quiz_Operator.VariableMilestone_TextBox.Text}&DoubleDipState={Quiz_Operator.DoubleDipState}&DoubleDipFirstAnswer={Quiz_Operator.DoubleDipFirstAnswer}&ActiveLifelines={Quiz_Operator.ActiveLifelines}")
        If MomentStatus.Trim = String.Empty Then Return
        Select Case MomentStatus.ToUpper
            Case "QUESTIONANSWERS_LOAD" 'QuestionAnswersState=

            Case "EMPTYQUESTION_FIRED"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?QuestionAnswersState=None&AtaState=None&Paf=None")
            Case "QUESTION_FIRED"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?QuestionAnswersState=ReadQ&AtaState=None&Paf=None")
            Case "QUESTION_ANSWERA_FIRED"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?QuestionAnswersState=ReadQ1&AtaState=None&Paf=None")
            Case "QUESTION_ANSWERB_FIRED"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?QuestionAnswersState=ReadQ12&AtaState=None&Paf=None")
            Case "QUESTION_ANSWERC_FIRED"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?QuestionAnswersState=ReadQ123&AtaState=None&Paf=None")
            Case "QUESTION_ANSWERD_FIRED"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?QuestionAnswersState=ReadQ1234&AtaState=None&Paf=None")
            Case "ANSWERA_FINAL_FIRED", "ANSWERB_FINAL_FIRED", "ANSWERC_FINAL_FIRED", "ANSWERD_FINAL_FIRED", "DOUBLEDIPANSWER_FINAL_FIRED"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?QuestionAnswersState=FinalAnswerGiven&FinalAnswer={Helpers.ConvertABCDTo1234(Quiz_Operator.FinalAnswer)}")
            Case "CORRECTANSWER_FIRED", "DOUBLEDIPISFIRSTFINALANSWER_CORRECT_FIRED"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?QuestionAnswersState=CorrectAnswerReveal")
            Case "EMPTYQUESTION_FIRED"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?QuestionAnswersState=None")
            'Case "LIFELINE_UPDATE"
            '    HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?LifelinesState={Quiz_Operator.LifelinesState}")
            Case "5050_FIRE"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?FiftyFifty={Quiz_Operator.FiftyFifty}")

            Case "PHONEFRIEND_DIALING"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?Paf=None")

            Case "PHONEFRIEND_PROGRESS"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?Paf=Start")

            Case "PHONEFRIEND_INTERRUPTED"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?Paf=None")

            Case "ASKAUDIENCE_QUESTIONING"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?AtaState=NONE")

            Case "ASKAUDIENCE_VOTING"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?AtaState=ClearDiagram")

            Case "ASKAUDIENCE_ENDVOTE"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?AtaState=DiagramWithPercentage")

            Case "TOTALPRIZEWON"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostMainGamePlayState.php?QuestionAnswersState=TOTALPRIZEWON|{Quiz_Operator.QuestionForSume.ToString}")

        End Select
    End Sub

    Public Shared Sub OneTimeMessageSet(Optional OneTimeMessage As String = "")

        If OneTimeMessage.Trim = String.Empty Then Return
        Select Case OneTimeMessage.ToUpper
            Case "EXPLANATION-FIRE"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostOneTimeMessage.php?MessageType=Explanation&State=Fired")
                'http://127.0.0.1/wwtbam-state/PostOneTimeMessage.php?MessageType=Explanation&State=Fired
            Case "EXPLANATION-NONE"
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostOneTimeMessage.php?MessageType=Explanation&State=None")
                'http://127.0.0.1/wwtbam-state/PostOneTimeMessage.php?MessageType=Explanation&State=None
        End Select

    End Sub

    Friend Shared Sub ExplanationFire()
        Return
        Message.Clear()
        Message.Append("<HOSTCONT-MESSAGE>")
        Message.Append("<MESSAGE-TYPE>EXPLANATIONFIRE</MESSAGE-TYPE>")
        Message.Append("</HOSTCONT-MESSAGE>")

        'SendMessage(Message.ToString)
    End Sub

    Friend Shared Sub ExplanationDissolve()
        Return
        Message.Clear()
        Message.Append("<HOSTCONT-MESSAGE>")
        Message.Append("<MESSAGE-TYPE>EXPLANATIONDISSOLVE</MESSAGE-TYPE>")
        Message.Append("</HOSTCONT-MESSAGE>")

        'SendMessage(Message.ToString)
    End Sub

    Friend Shared Sub GetDirectorChat(imessage As String, blink As String)
        Dim ChatState As String = "NONE"
        Dim intBlink As Integer
        If Not Integer.TryParse(blink, intBlink) Then
            intBlink = 0
        End If
        If intBlink > 0 Then
            ChatState = $"BLINK{intBlink.ToString}"
        End If
        HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostProducerChatState.php?ProducerChatState={ChatState}&ProducerChatText={Uri.EscapeDataString(imessage)}")

        Return
        'Message.Clear()
        'Message.Append("<HOSTCONT-MESSAGE>")
        'Message.Append("<MESSAGE-TYPE>GETDIRECTORCHAT</MESSAGE-TYPE>")
        'Message.Append(String.Format("<IMESSAGE>{0}</IMESSAGE>", imessage))
        'Message.Append(String.Format("<BLINK>{0}</BLINK>", blink))
        'Message.Append("</HOSTCONT-MESSAGE>")

        'SendMessage(Message.ToString)
    End Sub

    Friend Shared Sub DirectorChatReset()
        HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostProducerChatState.php?ProducerChatState=&ProducerChatText=")
        Return

        'SendMessage(Message.ToString)
    End Sub
#End Region
End Class