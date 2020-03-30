Public Class MainGameMusicLayer

    Public Q1to5AxWindowsMediaPlayer1 As New AxWMPLib.AxWindowsMediaPlayer
    Public Q6AxWindowsMediaPlayer1 As New AxWMPLib.AxWindowsMediaPlayer
    Public Q7AxWindowsMediaPlayer1 As New AxWMPLib.AxWindowsMediaPlayer
    Public Q8AxWindowsMediaPlayer1 As New AxWMPLib.AxWindowsMediaPlayer
    Public Q9AxWindowsMediaPlayer1 As New AxWMPLib.AxWindowsMediaPlayer
    Public Q10AxWindowsMediaPlayer1 As New AxWMPLib.AxWindowsMediaPlayer
    Public Q11AxWindowsMediaPlayer1 As New AxWMPLib.AxWindowsMediaPlayer
    Public Q12AxWindowsMediaPlayer1 As New AxWMPLib.AxWindowsMediaPlayer
    Public Q13AxWindowsMediaPlayer1 As New AxWMPLib.AxWindowsMediaPlayer
    Public Q14AxWindowsMediaPlayer1 As New AxWMPLib.AxWindowsMediaPlayer
    Public Q15AxWindowsMediaPlayer1 As New AxWMPLib.AxWindowsMediaPlayer
    Public LimitedClock_WindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public DoubleDipBackground_WindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public LetsPLAYQ1to5AxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public LetsPLAYQ6AxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public LetsPLAYQ7AxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public LetsPLAYQ8AxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public LetsPLAYQ9AxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public LetsPLAYQ10AxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public WalkAwayLXAxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer

    Sub New()
        Q1to5AxWindowsMediaPlayer1.CreateControl()
        Q1to5AxWindowsMediaPlayer1.URL = "C:\WWTBAM Removable Disc\UK 2007\28.Q1-5 Heartbeat.wav"
        Q1to5AxWindowsMediaPlayer1.Ctlcontrols.stop()

        Q6AxWindowsMediaPlayer1.CreateControl()
        Q6AxWindowsMediaPlayer1.URL = "C:\WWTBAM Removable Disc\UK 2007\58.Q6 - Heartbeat.wav"
        Q6AxWindowsMediaPlayer1.Ctlcontrols.stop()

        Q7AxWindowsMediaPlayer1.CreateControl()
        Q7AxWindowsMediaPlayer1.URL = "C:\WWTBAM Removable Disc\UK 2007\63.Q7 - Heartbeat.wav"
        Q7AxWindowsMediaPlayer1.Ctlcontrols.stop()

        Q8AxWindowsMediaPlayer1.CreateControl()
        Q8AxWindowsMediaPlayer1.URL = "C:\WWTBAM Removable Disc\UK 2007\68.Q8 - Heartbeat.wav"
        Q8AxWindowsMediaPlayer1.Ctlcontrols.stop()

        Q9AxWindowsMediaPlayer1.CreateControl()
        Q9AxWindowsMediaPlayer1.URL = "C:\WWTBAM Removable Disc\UK 2007\73.Q9 - Heartbeat.wav"
        Q9AxWindowsMediaPlayer1.Ctlcontrols.stop()

        Q10AxWindowsMediaPlayer1.CreateControl()
        Q10AxWindowsMediaPlayer1.URL = "C:\WWTBAM Removable Disc\UK 2007\78.Q10 - Heartbeat.wav"
        Q10AxWindowsMediaPlayer1.Ctlcontrols.stop()

        Q11AxWindowsMediaPlayer1.CreateControl()
        Q11AxWindowsMediaPlayer1.URL = "C:\WWTBAM Removable Disc\UK 2007\83.Q11 - Heartbeat.wav"
        Q11AxWindowsMediaPlayer1.Ctlcontrols.stop()

        Q12AxWindowsMediaPlayer1.CreateControl()
        Q12AxWindowsMediaPlayer1.URL = "C:\WWTBAM Removable Disc\UK 2007\87.Q12 - Heartbeat.wav"
        Q12AxWindowsMediaPlayer1.Ctlcontrols.stop()

        Q13AxWindowsMediaPlayer1.CreateControl()
        Q13AxWindowsMediaPlayer1.URL = "C:\WWTBAM Removable Disc\UK 2007\91.Q13 - Heartbeat.wav"
        Q13AxWindowsMediaPlayer1.Ctlcontrols.stop()

        Q14AxWindowsMediaPlayer1.CreateControl()
        Q14AxWindowsMediaPlayer1.URL = "C:\WWTBAM Removable Disc\UK 2007\95.Q14 - Heartbeat.wav"
        Q14AxWindowsMediaPlayer1.Ctlcontrols.stop()

        Q15AxWindowsMediaPlayer1.CreateControl()
        Q15AxWindowsMediaPlayer1.URL = "C:\WWTBAM Removable Disc\UK 2007\99.Q15 - Heartbeat (R1 000 000).wav"
        Q15AxWindowsMediaPlayer1.Ctlcontrols.stop()

        DoubleDipBackground_WindowsMediaPlayer.CreateControl()
        DoubleDipBackground_WindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\UK 2007\107.Double Dip First Chance.wav"
        DoubleDipBackground_WindowsMediaPlayer.Ctlcontrols.stop()

        LetsPLAYQ1to5AxWindowsMediaPlayer.CreateControl()
        LetsPLAYQ1to5AxWindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\UK 2007\27.Let's See Q1.wav"
        LetsPLAYQ1to5AxWindowsMediaPlayer.Ctlcontrols.stop()

        LetsPLAYQ6AxWindowsMediaPlayer.CreateControl()
        LetsPLAYQ6AxWindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\UK 2007\32.Q5 - Lx.wav"
        LetsPLAYQ6AxWindowsMediaPlayer.Ctlcontrols.stop()

        LetsPLAYQ7AxWindowsMediaPlayer.CreateControl()
        LetsPLAYQ7AxWindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\UK 2007\62.Q6 - Lx.wav"
        LetsPLAYQ7AxWindowsMediaPlayer.Ctlcontrols.stop()

        LetsPLAYQ8AxWindowsMediaPlayer.CreateControl()
        LetsPLAYQ8AxWindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\UK 2007\67.Q7-12 - Lx.wav"
        LetsPLAYQ8AxWindowsMediaPlayer.Ctlcontrols.stop()

        LetsPLAYQ9AxWindowsMediaPlayer.CreateControl()
        LetsPLAYQ9AxWindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\UK 2007\72.Q8-13 - Lx.wav"
        LetsPLAYQ9AxWindowsMediaPlayer.Ctlcontrols.stop()

        LetsPLAYQ10AxWindowsMediaPlayer.CreateControl()
        LetsPLAYQ10AxWindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\UK 2007\77.Q9-14 - Lx.wav"
        LetsPLAYQ10AxWindowsMediaPlayer.Ctlcontrols.stop()

        LimitedClock_WindowsMediaPlayer.CreateControl()
        LimitedClock_WindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\US 2008\Clock 100 Seconds.wav"
        LimitedClock_WindowsMediaPlayer.Ctlcontrols.stop()

        WalkAwayLXAxWindowsMediaPlayer.CreateControl()
        WalkAwayLXAxWindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\UK 2007\34.Big Quitter.wav"
        WalkAwayLXAxWindowsMediaPlayer.Ctlcontrols.stop()
    End Sub

    Sub PlayHeartbeatMusic(level As Short)

        Select Case level
            Case 6
                Q6AxWindowsMediaPlayer1.Ctlcontrols.play()
            Case 7
                Q7AxWindowsMediaPlayer1.Ctlcontrols.play()
            Case 8
                Q8AxWindowsMediaPlayer1.Ctlcontrols.play()
            Case 9
                Q9AxWindowsMediaPlayer1.Ctlcontrols.play()
            Case 10
                Q10AxWindowsMediaPlayer1.Ctlcontrols.play()
            Case 11
                Q11AxWindowsMediaPlayer1.Ctlcontrols.play()
            Case 12
                Q12AxWindowsMediaPlayer1.Ctlcontrols.play()
            Case 13
                Q13AxWindowsMediaPlayer1.Ctlcontrols.play()
            Case 14
                Q14AxWindowsMediaPlayer1.Ctlcontrols.play()
            Case 15
                Q15AxWindowsMediaPlayer1.Ctlcontrols.play()
        End Select

    End Sub

    Sub PlayDoubleDipBackground()
        Q6AxWindowsMediaPlayer1.Ctlcontrols.stop()
        Q7AxWindowsMediaPlayer1.Ctlcontrols.stop()
        Q8AxWindowsMediaPlayer1.Ctlcontrols.stop()
        Q9AxWindowsMediaPlayer1.Ctlcontrols.stop()
        Q10AxWindowsMediaPlayer1.Ctlcontrols.stop()
        Q11AxWindowsMediaPlayer1.Ctlcontrols.stop()
        Q12AxWindowsMediaPlayer1.Ctlcontrols.stop()
        Q13AxWindowsMediaPlayer1.Ctlcontrols.stop()
        Q14AxWindowsMediaPlayer1.Ctlcontrols.stop()
        Q15AxWindowsMediaPlayer1.Ctlcontrols.stop()
        DoubleDipBackground_WindowsMediaPlayer.Ctlcontrols.stop()

        DoubleDipBackground_WindowsMediaPlayer.Ctlcontrols.play()
    End Sub

    Sub PlayLXSound(LevelQ_TextBox As String, VariableMilestone_TextBox As String)
        If Val(LevelQ_TextBox) >= 1 And Val(LevelQ_TextBox) <= 5 Then
            LetsPLAYQ1to5AxWindowsMediaPlayer.Ctlcontrols.play()
        ElseIf Val(LevelQ_TextBox) = 11 And (Val(VariableMilestone_TextBox) <> 10) Then
            LetsPLAYQ6AxWindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\UK 2007\82.Q10 NoMilestone - Lx.wav"
            LetsPLAYQ6AxWindowsMediaPlayer.Ctlcontrols.play()
        ElseIf Val(LevelQ_TextBox) = 6 Or (Val(LevelQ_TextBox) = 11 And Val(VariableMilestone_TextBox) = 10) Then
            LetsPLAYQ6AxWindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\UK 2007\32.Q5 - Lx.wav"
            LetsPLAYQ6AxWindowsMediaPlayer.Ctlcontrols.play()
        ElseIf Val(LevelQ_TextBox) = 7 Or Val(LevelQ_TextBox) = 12 Then
            LetsPLAYQ7AxWindowsMediaPlayer.Ctlcontrols.play()
        ElseIf Val(LevelQ_TextBox) = 8 Or Val(LevelQ_TextBox) = 13 Then
            LetsPLAYQ8AxWindowsMediaPlayer.Ctlcontrols.play()
        ElseIf Val(LevelQ_TextBox) = 9 Or Val(LevelQ_TextBox) = 14 Then
            LetsPLAYQ9AxWindowsMediaPlayer.Ctlcontrols.play()
        ElseIf Val(LevelQ_TextBox) = 10 Or Val(LevelQ_TextBox) = 15 Then
            LetsPLAYQ10AxWindowsMediaPlayer.Ctlcontrols.play()
        ElseIf LevelQ_TextBox = "666" Then
            WalkAwayLXAxWindowsMediaPlayer.Ctlcontrols.play()
        End If
    End Sub

End Class
