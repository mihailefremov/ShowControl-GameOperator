Imports Svt.Caspar

Public Class InteractiveWallScreen

    Public casparWallScreen As New CasparDevice
    Public cgDataWallScreen As New CasparCGDataCollection

    Public IsOnWallScreen As Boolean = My.Settings.IsInteractiveScreenOn

    Public Sub Connect()
        If Not IsOnWallScreen Then
            Return
        End If
        Try
            System.Threading.Thread.Sleep(300)
            casparWallScreen.Settings.Hostname = My.Settings.casparHostName
            casparWallScreen.Settings.Port = My.Settings.casparPort
            casparWallScreen.Connect()
        Catch ex As Exception
        End Try

    End Sub

    Sub LightDownEffect(Level As String)
        If Not IsOnWallScreen Then
            Return
        End If
        Try
            Level = CType(Level, Short)
            Dim LevelX As String = ""
            Dim randomGen As New Random
            Dim versionNumber As Short
            If Level >= 6 And Level <= 10 Then
                LevelX = "6to10"
                versionNumber = randomGen.Next(1, 8)
            ElseIf Level <= 5 Then
                LevelX = "1to5"
                versionNumber = randomGen.Next(1, 4)
            ElseIf Level >= 10 Then
                LevelX = "11to15"
                versionNumber = randomGen.Next(1, 9)
            Else
                Return
            End If

            If casparWallScreen.IsConnected Then
                casparWallScreen.Channels(0).Clear(0)
                casparWallScreen.Channels(0).LoadBG(20, $"LightDown{LevelX}v{versionNumber.ToString}", False)
                casparWallScreen.Channels(0).Play(20)
            End If
        Catch ex As Exception
        End Try

    End Sub

    Public Sub MotionBackgroundDuringQuestion(Level As String)
        If Not IsOnWallScreen Then
            Return
        End If
        Try
            Level = CType(Level, Short)
            Dim LevelX As String = ""

            Dim isRepeatMotion As Boolean = True

            Dim randomGen As New Random
            Dim versionNumber As Short
            If Level >= 1 And Level <= 5 Then
                LevelX = "1to5"
                versionNumber = randomGen.Next(1, 3)
                isRepeatMotion = False
            ElseIf Level >= 6 And Level <= 10 Then
                LevelX = "6to10"
                versionNumber = randomGen.Next(1, 6)
            ElseIf Level >= 11 And Level <= 14 Then 'od 11-14
                LevelX = "11to15"
                versionNumber = randomGen.Next(1, 8)
            ElseIf Level = 15 Then '15
                LevelX = "15"

            ElseIf Level = 2001 Then
                LevelX = "FastetFingerFirst"
                versionNumber = 1
            ElseIf Level = 2002 Then
                LevelX = "Other"
                versionNumber = 1
            ElseIf Level = 666 Then
                LevelX = "WalkAway"
                versionNumber = 1
            Else
                Return
            End If

            If casparWallScreen.IsConnected Then
                casparWallScreen.Channels(0).Clear(0)
                casparWallScreen.Channels(0).LoadBG(20, $"MotionBackgroundQuestion{LevelX}", isRepeatMotion)
                casparWallScreen.Channels(0).Play(20)
            End If

        Catch ex As Exception
        End Try

    End Sub

    Public Sub CorrectAnswer(Level As String)
        If Not IsOnWallScreen Then
            Return
        End If
        Try
            Level = CType(Level, Short)
            Level -= 1
            If Level < 5 Or Level > 15 Then
                Return
            End If

            Dim LevelX As String = ""
            Dim randomGen As New Random
            Dim versionNumber As Short
            Dim isLoop As Boolean = False

            If Level >= 5 And Level <= 9 Then 'od 5-9
                LevelX = "6to10"
                versionNumber = randomGen.Next(1, 4)
            ElseIf Level < 5 Then 'od 1-5
                LevelX = "1to5"
                versionNumber = randomGen.Next(1, 3)
            ElseIf Level = 10 Then '10
                LevelX = "10"
                versionNumber = 1
            ElseIf Level >= 11 And Level <= 14 Then 'od 11-14
                LevelX = "11to15"
                versionNumber = 1
            ElseIf Level = 15 Then 'od 15
                LevelX = "15"
                versionNumber = 1
                isLoop = True
            Else
                Return
            End If

            If casparWallScreen.IsConnected Then
                casparWallScreen.Channels(0).Clear(0)
                casparWallScreen.Channels(0).LoadBG(20, $"Correct{LevelX}v{versionNumber.ToString}", isLoop)
                casparWallScreen.Channels(0).SetVolume(20, 0, 8, Easing.EaseInBack)
                casparWallScreen.Channels(0).Play(20)
            End If
        Catch ex As Exception
        End Try

    End Sub

    Public Sub AudienceAsking()
        If Not IsOnWallScreen Then
            Return
        End If
        Try
            If casparWallScreen.IsConnected Then
                casparWallScreen.Channels(0).Clear(0)
                casparWallScreen.Channels(0).LoadBG(20, $"AudienceAsking", False)
                casparWallScreen.Channels(0).SetVolume(20, 0, 8, Easing.EaseInBack)
                casparWallScreen.Channels(0).Play(20)
            End If
        Catch ex As Exception
        End Try

    End Sub

    Public Sub AudienceVoting()
        If Not IsOnWallScreen Then
            Return
        End If
        Try
            If casparWallScreen.IsConnected Then
                casparWallScreen.Channels(0).Clear(0)
                casparWallScreen.Channels(0).LoadBG(20, $"AudienceVoting", False)
                casparWallScreen.Channels(0).SetVolume(20, 0, 8, Easing.EaseInBack)
                casparWallScreen.Channels(0).Play(20)
            End If
        Catch ex As Exception
        End Try

    End Sub

    Public Sub AnyBackgroundLoop(bed As String, Optional isLoop As Boolean = True)
        If Not IsOnWallScreen Then
            Return
        End If
        Try
            If casparWallScreen.IsConnected Then
                casparWallScreen.Channels(0).Clear(0)
                casparWallScreen.Channels(0).LoadBG(20, $"{bed}", isLoop)
                casparWallScreen.Channels(0).Play(20)
            End If
        Catch ex As Exception
        End Try

    End Sub


End Class
