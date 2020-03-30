Imports System.Threading.Tasks

Public Class FastestFingerManaging

    Public Shared FFFOrders()
    Public Shared FFFClocks()
    Public Shared FFFIsCorrect() As Boolean

    Public Shared strHostName As String = System.Net.Dns.GetHostName()
    Public Shared strIPAddress As String = My.Settings.QuizOperatorIPAddress ' System.Net.Dns.Resolve(strHostName).AddressList(0).ToString()

    Public Shared Devices As List(Of FFFDataFromDevice) = New List(Of FFFDataFromDevice) From {
     New FFFDataFromDevice(), New FFFDataFromDevice(), New FFFDataFromDevice(), New FFFDataFromDevice(), New FFFDataFromDevice(),
          New FFFDataFromDevice(), New FFFDataFromDevice(), New FFFDataFromDevice(), New FFFDataFromDevice(), New FFFDataFromDevice()
    }

#Region "LISTEN TO KEYPADS"

    Public Shared Sub ConnectFFDevice(i As Int16, ByRef out As String)
        Try
            Devices.ElementAt(i - 1).ConnectFFF(i.ToString)
            out += (i).ToString + ": Device Connected" + vbCrLf
        Catch ex As Exception
            out += ex.Message + vbCrLf
        End Try
    End Sub

    Public Shared Sub DisconnectFFFDevices(ByRef out As String)
        For Each FF In Devices
            Try
                FF.ThreadListener.Abort()
                FF.ThreadDataPopulator.Abort()
                out += "Disconnected FFF Devices" + vbCrLf
            Catch ex As Exception
                out += ex.Message + vbCrLf
            End Try
        Next
        Log.LogWrite(out, "QuizOperatorLog")
    End Sub

#End Region

#Region "methods"
    Shared Sub ContestantLoad(position As String, nameLastname As String, city As String)
        HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostFFFPlayData.php?ContestantPosition={position}&ContestantName={nameLastname}&ContestantTown={city}")

    End Sub

    Shared Sub QuestionLoad(question As String, answer1 As String, answer2 As String, answer3 As String, answer4 As String)

        HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostFFFPlayState.php?FFFPlayState=QUESTIONLOAD&QuestionText={question}&Answer1Text={answer1}&Answer2Text={answer2}&Answer3Text={answer3}&Answer4Text={answer4}")

    End Sub

    Shared Sub QuestionFire()

        HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostFFFPlayState.php?FFFPlayState=ReadQ")

    End Sub

    Shared Sub ThreeBeepsFire()

        HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostFFFPlayState.php?FFFPlayState=ReadQ1234Ready")

    End Sub

    Shared Sub FastestFingerFirstFire()

        HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostFFFPlayState.php?FFFPlayState=ReadQ1234")

    End Sub

    Shared Sub ResetFFFgame()
        HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostFFFPlayState.php?FFFPlayState=None")

    End Sub

    Shared Sub StandByContestantName()

        HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.StateServerIPAddress}/wwtbam-state/PostFFFPlayState.php?FFFPlayState=ContestantNameTown")

    End Sub

    Shared Sub StandByFFFGame()

    End Sub

#End Region


End Class
