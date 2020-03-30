Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Threading.Tasks

Public Class FFFDataFromDevice
    'GI SLUSA PORAKITE OD KEYPADS-ite na FFF IGRACITE
    Public Delegate Sub UpdateTextDelegate(TB As TextBox, txt As String)
    Private Shared txtBox As New TextBox

    Private QuizOperatorDataToExecute As String = ""

    Public NameLastName As String = String.Empty
    Public City As String = String.Empty

    Public ipAddress As String = String.Empty
    Public Position As String = String.Empty

    Public UpdateTimeStamp As String = String.Empty

    Public Order As String = String.Empty
    Public ElapsedTime As String = String.Empty
    Public IsCorrect As Boolean = False
    Public IsActive As Boolean = True

    Public IsNameCorrect As Boolean = True

    Public FFFDeviceData As String

    Public ThreadListener As New Thread(AddressOf ListenAllTime)
    Public ThreadDataPopulator As New Thread(AddressOf PopulateWithData)

    Public ListenerEvent As New ManualResetEvent(False)
    Public DataPopulatorEvent As New ManualResetEvent(False)

    Public Sub ConnectFFF(Position As String)
        Me.ipAddress = My.Settings.StateServerIPAddress
        Me.Position = Position

        ThreadListener.Start()
        ThreadDataPopulator.Start()
    End Sub

    Public Sub ListenAllTime()
        While True
            ListenerEvent.WaitOne()
            While IsActive
                Listen(ipAddress)
                Thread.Sleep(200)
            End While
            Thread.Sleep(1000)
        End While
    End Sub

    Public Sub Listen(IPAddressIn As String)
        Dim unused = HttpApiRequests.GetPostRequests.Get($"https://{IPAddressIn}/wwtbam-state/GetFFFPlayData.php?ContestantPosition={Position}")
        FFFDeviceData = unused
    End Sub

    Public Sub PopulateWithData()
        While True
            DataPopulatorEvent.WaitOne()
            While IsActive
                If Not IsNothing(FFFDeviceData) Then
                    OnLineReceivedQuizOperator(FFFDeviceData.Trim)
                End If
                Thread.Sleep(150)
            End While
            Thread.Sleep(1000)
        End While
    End Sub


    ' UPDATE TEXTBOX
    Sub UpdateExecuteCommandText(TB As TextBox, txt As String)
        If TB.InvokeRequired Then
            TB.Invoke(New UpdateTextDelegate(AddressOf UpdateExecuteCommandText), New Object() {TB, txt})
        Else
            If txt IsNot Nothing Then
                'TB.AppendText(txt & vbCrLf)
                Try
                    ExecuteClientMessage(txt)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        End If
    End Sub

    ' UPDATE TEXT WHEN DATA IS RECEIVED
    Sub OnLineReceivedQuizOperator(Data As String)
        QuizOperatorDataToExecute = Data

        Dim TimeStampFromData As String = GetValueStringBetweenTags(QuizOperatorDataToExecute, "<UPDATETIMESTAMP>", "</UPDATETIMESTAMP>")
        If UpdateTimeStamp = TimeStampFromData Then
            Return
        End If

        UpdateTimeStamp = TimeStampFromData
        UpdateExecuteCommandText(txtBox, QuizOperatorDataToExecute)

    End Sub

    Sub ExecuteClientMessage(Message As String)
        If Message.Trim() = "" Then Exit Sub

        Dim MessageType As String
        If True Then
            MessageType = "FFFCONFIRMORDER"
        Else
            MessageType = GetValueStringBetweenTags(Message, "<MESSAGE-TYPE>", "</MESSAGE-TYPE>")
            MessageType = MessageType.ToUpper
        End If

        Select Case MessageType
            Case "FFFCONFIRMORDER"
                ConfirmOrder(Message)
            Case "FFFCONFIRMDECLINENAME"
                ConfirmDeclineName(Message)
        End Select

        FFFDeviceData = String.Empty

    End Sub

    Private Sub ConfirmOrder(message As String)
        Order = GetValueStringBetweenTags(message, "<GIVENANSWER>", "</GIVENANSWER>")
        ElapsedTime = GetValueStringBetweenTags(message, "<TIMEOFANSWER>", "</TIMEOFANSWER>")
        Dim RoundedTime As Double = 0
        Double.TryParse(ElapsedTime, RoundedTime)
        RoundedTime = Math.Round(RoundedTime, My.MySettings.Default.FFFClockDecimalPlaces)
        ElapsedTime = RoundedTime.ToString

        Dim FFFsecondPartClock As String = ElapsedTime.Substring(ElapsedTime.LastIndexOf(".") + 1)
        FFFsecondPartClock = FFFsecondPartClock.PadRight(My.MySettings.Default.FFFClockDecimalPlaces, "0")
        Dim Index = ElapsedTime.LastIndexOf(".")

        Dim FFFfirstPartClock As String = "0"

        If (Index > 0) Then
            FFFfirstPartClock = ElapsedTime.Substring(0, Index)
        End If

        ElapsedTime = String.Format("{0}.{1}", FFFfirstPartClock, FFFsecondPartClock)
    End Sub

    Private Sub ConfirmDeclineName(message As String)
        Dim Result = GetValueStringBetweenTags(message, "<CONFIRMDECLINE>", "</CONFIRMDECLINE>")
        Boolean.TryParse(Result, IsNameCorrect)

    End Sub
    Public Function GetValueStringBetweenTags(value As String, startTag As String, endTag As String) As String
        Try
            If value.ToUpper.Contains(startTag) And value.ToUpper.Contains(endTag) Then
                Dim Index As Integer = value.IndexOf(startTag) + startTag.Length
                Return value.Substring(Index, value.IndexOf(endTag) - Index)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


End Class
