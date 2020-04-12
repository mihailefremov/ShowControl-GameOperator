Public Class MainGameMusicLayer

    Public Q1to5AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Public Q6AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Public Q7AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Public Q8AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Public Q9AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Public Q10AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Public Q11AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Public Q12AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Public Q13AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Public Q14AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Public Q15AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Public FinalAnswer611MediaPlayer As New System.Windows.Media.MediaPlayer
    Public FinalAnswer712MediaPlayer As New System.Windows.Media.MediaPlayer
    Public FinalAnswer813MediaPlayer As New System.Windows.Media.MediaPlayer
    Public FinalAnswer914MediaPlayer As New System.Windows.Media.MediaPlayer
    Public FinalAnswer1015MediaPlayer As New System.Windows.Media.MediaPlayer

    Public CorrectAnswerQ1MediaPlayer As New System.Windows.Media.MediaPlayer
    Public CorrectAnswerQ5MediaPlayer As New System.Windows.Media.MediaPlayer
    Public CorrectAnswerQ6MediaPlayer As New System.Windows.Media.MediaPlayer
    Public CorrectAnswerQ7MediaPlayer As New System.Windows.Media.MediaPlayer
    Public CorrectAnswerQ8MediaPlayer As New System.Windows.Media.MediaPlayer
    Public CorrectAnswerQ9MediaPlayer As New System.Windows.Media.MediaPlayer
    Public CorrectAnswerQ10MediaPlayer As New System.Windows.Media.MediaPlayer
    Public CorrectAnswerQ11MediaPlayer As New System.Windows.Media.MediaPlayer
    Public CorrectAnswerQ12MediaPlayer As New System.Windows.Media.MediaPlayer
    Public CorrectAnswerQ13MediaPlayer As New System.Windows.Media.MediaPlayer
    Public CorrectAnswerQ14MediaPlayer As New System.Windows.Media.MediaPlayer
    Public CorrectAnswerQ15MediaPlayer As New System.Windows.Media.MediaPlayer

    Public LimitedClock_WindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Public DoubleDipBackground_WindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public LetsPLAYQ1to5AxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Public LetsPLAYQ6AxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Public LetsPLAYQ7AxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Public LetsPLAYQ8AxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Public LetsPLAYQ9AxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Public LetsPLAYQ10AxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Public WalkAwayLXAxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer

    Public AnyMusicLXAxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer

    Public WwtbamMusicPlaylistConfig As New Xml2CSharp.MUSICPLAYLISTCONFIGURATION

    Sub New()
        'Music configuration
        Dim ConfigurationMusicPath As String = GameConfiguration.Default.DefaultGameConfigurationPath
        Dim MusicConfigurationText As String = System.IO.File.ReadAllText(String.Format("{0}\{1}", ConfigurationMusicPath, "MusicPlaylistConfiguration.xml"))

        Dim MusicConfigurationReader As System.IO.TextReader = New System.IO.StringReader(MusicConfigurationText)

        Dim serializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(Xml2CSharp.MUSICPLAYLISTCONFIGURATION))
        Me.WwtbamMusicPlaylistConfig = serializer.Deserialize(MusicConfigurationReader)

        Q1to5AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND28.LOCATION, WwtbamMusicPlaylistConfig.SOUND28.TITLE)))
        Q1to5AxWindowsMediaPlayer1.Stop()

        Q6AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND58.LOCATION, WwtbamMusicPlaylistConfig.SOUND58.TITLE)))
        'Q6AxWindowsMediaPlayer1.Open(New Uri("C:\WWTBAM Removable Disc\UK 2007\120.Q6 - Heartbeat Loop.wav"))
        Q6AxWindowsMediaPlayer1.Stop()

        Q7AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND63.LOCATION, WwtbamMusicPlaylistConfig.SOUND63.TITLE)))
        'Q7AxWindowsMediaPlayer1.Open(New Uri("C\WWTBAM Removable Disc\UK 2007\121.Q7 - Heartbeat Loop.wav"))
        Q7AxWindowsMediaPlayer1.Stop()

        Q8AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND68.LOCATION, WwtbamMusicPlaylistConfig.SOUND68.TITLE)))
        'Q8AxWindowsMediaPlayer1.Open(New Uri("C:\WWTBAM Removable Disc\UK 2007\122.Q8 - Heartbeat Loop.wav"))
        Q8AxWindowsMediaPlayer1.Stop()

        Q9AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND73.LOCATION, WwtbamMusicPlaylistConfig.SOUND73.TITLE)))
        'Q9AxWindowsMediaPlayer1.Open(New Uri("C\WWTBAM Removable Disc\UK 2007\123.Q9 - Heartbeat Loop.wav"))
        Q9AxWindowsMediaPlayer1.Stop()

        Q10AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND78.LOCATION, WwtbamMusicPlaylistConfig.SOUND78.TITLE)))
        'Q10AxWindowsMediaPlayer1.Open(New Uri("C:\WWTBAM Removable Disc\UK 2007\124.Q10 - Heartbeat Loop.wav"))
        Q10AxWindowsMediaPlayer1.Stop()

        Q11AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND83.LOCATION, WwtbamMusicPlaylistConfig.SOUND83.TITLE)))
        'Q11AxWindowsMediaPlayer1.Open(New Uri("C\WWTBAM Removable Disc\UK 2007\125.Q11 - Heartbeat Loop.wav"))
        Q11AxWindowsMediaPlayer1.Stop()

        Q12AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND87.LOCATION, WwtbamMusicPlaylistConfig.SOUND87.TITLE)))
        'Q12AxWindowsMediaPlayer1.Open(New Uri("C:\WWTBAM Removable Disc\UK 2007\126.Q12 - Heartbeat Loop.wav"))
        Q12AxWindowsMediaPlayer1.Stop()

        Q13AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND91.LOCATION, WwtbamMusicPlaylistConfig.SOUND91.TITLE)))
        'Q13AxWindowsMediaPlayer1.Open(New Uri("C\WWTBAM Removable Disc\UK 2007\127.Q13 - Heartbeat Loop.wav"))
        Q13AxWindowsMediaPlayer1.Stop()

        Q14AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND95.LOCATION, WwtbamMusicPlaylistConfig.SOUND95.TITLE)))
        'Q14AxWindowsMediaPlayer1.Open(New Uri("C:\WWTBAM Removable Disc\UK 2007\128.Q14 - Heartbeat Loop.wav"))
        Q14AxWindowsMediaPlayer1.Stop()

        Q15AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND99.LOCATION, WwtbamMusicPlaylistConfig.SOUND99.TITLE)))
        'Q15AxWindowsMediaPlayer1.Open(New Uri("C\WWTBAM Removable Disc\UK 2007\129.Q15 - Heartbeat Loop.wav"))
        Q15AxWindowsMediaPlayer1.Stop()

        DoubleDipBackground_WindowsMediaPlayer.CreateControl()
        DoubleDipBackground_WindowsMediaPlayer.URL = String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND107.LOCATION, WwtbamMusicPlaylistConfig.SOUND107.TITLE)
        DoubleDipBackground_WindowsMediaPlayer.Ctlcontrols.stop()

        LetsPLAYQ1to5AxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND27.LOCATION, WwtbamMusicPlaylistConfig.SOUND27.TITLE)))
        LetsPLAYQ1to5AxWindowsMediaPlayer.Stop()

        LetsPLAYQ6AxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND32.LOCATION, WwtbamMusicPlaylistConfig.SOUND32.TITLE)))
        LetsPLAYQ6AxWindowsMediaPlayer.Stop()

        LetsPLAYQ7AxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND62.LOCATION, WwtbamMusicPlaylistConfig.SOUND62.TITLE)))
        LetsPLAYQ7AxWindowsMediaPlayer.Stop()

        LetsPLAYQ8AxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND67.LOCATION, WwtbamMusicPlaylistConfig.SOUND67.TITLE)))
        LetsPLAYQ8AxWindowsMediaPlayer.Stop()

        LetsPLAYQ9AxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND72.LOCATION, WwtbamMusicPlaylistConfig.SOUND72.TITLE)))
        LetsPLAYQ9AxWindowsMediaPlayer.Stop()

        LetsPLAYQ10AxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND77.LOCATION, WwtbamMusicPlaylistConfig.SOUND77.TITLE)))
        LetsPLAYQ10AxWindowsMediaPlayer.Stop()

        'LimitedClock_WindowsMediaPlayer.Open(New Uri("C:\WWTBAM Removable Disc\US 2008\Clock 100 Seconds.wav"))
        'LimitedClock_WindowsMediaPlayer.Stop()

        WalkAwayLXAxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND34.LOCATION, WwtbamMusicPlaylistConfig.SOUND34.TITLE)))
        WalkAwayLXAxWindowsMediaPlayer.Stop()

        FinalAnswer611MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND59.LOCATION, WwtbamMusicPlaylistConfig.SOUND59.TITLE)))
        FinalAnswer611MediaPlayer.Stop()

        FinalAnswer712MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND64.LOCATION, WwtbamMusicPlaylistConfig.SOUND64.TITLE)))
        FinalAnswer712MediaPlayer.Stop()

        FinalAnswer813MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND69.LOCATION, WwtbamMusicPlaylistConfig.SOUND69.TITLE)))
        FinalAnswer813MediaPlayer.Stop()

        FinalAnswer914MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND74.LOCATION, WwtbamMusicPlaylistConfig.SOUND74.TITLE)))
        FinalAnswer914MediaPlayer.Stop()

        FinalAnswer1015MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND79.LOCATION, WwtbamMusicPlaylistConfig.SOUND79.TITLE)))
        FinalAnswer1015MediaPlayer.Stop()

        CorrectAnswerQ1MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND29.LOCATION, WwtbamMusicPlaylistConfig.SOUND29.TITLE)))
        CorrectAnswerQ5MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND31.LOCATION, WwtbamMusicPlaylistConfig.SOUND31.TITLE)))
        CorrectAnswerQ6MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND61.LOCATION, WwtbamMusicPlaylistConfig.SOUND61.TITLE)))
        CorrectAnswerQ7MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND66.LOCATION, WwtbamMusicPlaylistConfig.SOUND66.TITLE)))
        CorrectAnswerQ8MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND71.LOCATION, WwtbamMusicPlaylistConfig.SOUND71.TITLE)))
        CorrectAnswerQ9MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND76.LOCATION, WwtbamMusicPlaylistConfig.SOUND76.TITLE)))
        CorrectAnswerQ10MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND81.LOCATION, WwtbamMusicPlaylistConfig.SOUND81.TITLE)))
        CorrectAnswerQ11MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND85.LOCATION, WwtbamMusicPlaylistConfig.SOUND85.TITLE)))
        CorrectAnswerQ12MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND89.LOCATION, WwtbamMusicPlaylistConfig.SOUND89.TITLE)))
        CorrectAnswerQ13MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND93.LOCATION, WwtbamMusicPlaylistConfig.SOUND93.TITLE)))
        CorrectAnswerQ14MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND97.LOCATION, WwtbamMusicPlaylistConfig.SOUND97.TITLE)))
        CorrectAnswerQ15MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND101.LOCATION, WwtbamMusicPlaylistConfig.SOUND101.TITLE)))

        StopCorrectAnswer()

    End Sub

    Sub StopHeartbeaLetsPlay()
        Me.Q1to5AxWindowsMediaPlayer1.Stop() 'Ctlcontrols.stop()
        Me.Q6AxWindowsMediaPlayer1.Stop() 'Ctlcontrols.stop()
        Me.Q7AxWindowsMediaPlayer1.Stop() 'Ctlcontrols.stop()
        Me.Q8AxWindowsMediaPlayer1.Stop() 'Ctlcontrols.stop()
        Me.Q9AxWindowsMediaPlayer1.Stop() 'Ctlcontrols.stop()
        Me.Q10AxWindowsMediaPlayer1.Stop() 'Ctlcontrols.stop()
        Me.Q11AxWindowsMediaPlayer1.Stop() 'Ctlcontrols.stop()
        Me.Q12AxWindowsMediaPlayer1.Stop() 'Ctlcontrols.stop()
        Me.Q13AxWindowsMediaPlayer1.Stop() 'Ctlcontrols.stop()
        Me.Q14AxWindowsMediaPlayer1.Stop() 'Ctlcontrols.stop()
        Me.Q15AxWindowsMediaPlayer1.Stop() 'Ctlcontrols.stop()
        Me.LimitedClock_WindowsMediaPlayer.Stop() 'Ctlcontrols.stop()
        Me.DoubleDipBackground_WindowsMediaPlayer.Ctlcontrols.stop() 'Ctlcontrols.stop()
        Me.LetsPLAYQ1to5AxWindowsMediaPlayer.Stop() 'Ctlcontrols.stop()
        Me.LetsPLAYQ6AxWindowsMediaPlayer.Stop() 'Ctlcontrols.stop()
        Me.LetsPLAYQ7AxWindowsMediaPlayer.Stop() 'Ctlcontrols.stop()
        Me.LetsPLAYQ8AxWindowsMediaPlayer.Stop() 'Ctlcontrols.stop()
        Me.LetsPLAYQ9AxWindowsMediaPlayer.Stop() 'Ctlcontrols.stop()
        Me.LetsPLAYQ10AxWindowsMediaPlayer.Stop() 'Ctlcontrols.stop()
    End Sub

    Sub PauseHeartbeatMusic(LevelQ As String)
        Select Case LevelQ
            Case "1", "2", "3", "4", "5"
                Me.Q1to5AxWindowsMediaPlayer1.Pause()
            Case "6"
                Me.Q6AxWindowsMediaPlayer1.Pause()
            Case "7"
                Me.Q7AxWindowsMediaPlayer1.Pause()
            Case "8"
                Me.Q8AxWindowsMediaPlayer1.Pause()
            Case "9"
                Me.Q9AxWindowsMediaPlayer1.Pause()
            Case "10"
                Me.Q10AxWindowsMediaPlayer1.Pause()
            Case "11"
                Me.Q11AxWindowsMediaPlayer1.Pause()
            Case "12"
                Me.Q12AxWindowsMediaPlayer1.Pause()
            Case "13"
                Me.Q13AxWindowsMediaPlayer1.Pause()
            Case "14"
                Me.Q14AxWindowsMediaPlayer1.Pause()
            Case "15"
                Me.Q15AxWindowsMediaPlayer1.Pause()
        End Select

    End Sub

    Sub PlayHeartbeatMusic(level As String)

        Select Case level
            Case "1", "2", "3", "4", "5"
                Q1to5AxWindowsMediaPlayer1.Play()
            Case "6"
                Me.Q6AxWindowsMediaPlayer1.Play()
            Case "7"
                Me.Q7AxWindowsMediaPlayer1.Play()
            Case "8"
                Me.Q8AxWindowsMediaPlayer1.Play()
            Case "9"
                Me.Q9AxWindowsMediaPlayer1.Play()
            Case "10"
                Me.Q10AxWindowsMediaPlayer1.Play()
            Case "11"
                Me.Q11AxWindowsMediaPlayer1.Play()
            Case "12"
                Me.Q12AxWindowsMediaPlayer1.Play()
            Case "13"
                Me.Q13AxWindowsMediaPlayer1.Play()
            Case "14"
                Me.Q14AxWindowsMediaPlayer1.Play()
            Case "15"
                Me.Q15AxWindowsMediaPlayer1.Play()
        End Select

    End Sub

    Sub PlayDoubleDipBackground()
        StopHeartbeaLetsPlay()
        DoubleDipBackground_WindowsMediaPlayer.Ctlcontrols.play()
    End Sub

    Sub PlayFinalAnswerSound(LevelQ As String, DoubleDipState As String)
        StopFinalAnswer()

        If String.Equals(DoubleDipState, "DoubleDipFirstFinal", StringComparison.OrdinalIgnoreCase) Then
            My.Computer.Audio.Play(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND105.LOCATION, WwtbamMusicPlaylistConfig.SOUND105.TITLE), AudioPlayMode.Background)

        ElseIf LevelQ = "6" Or LevelQ = "11" Then
            FinalAnswer611MediaPlayer.Play()

        ElseIf LevelQ = "7" Or LevelQ = "12" Then
            FinalAnswer712MediaPlayer.Play()

        ElseIf LevelQ = "8" Or LevelQ = "13" Then
            FinalAnswer813MediaPlayer.Play()

        ElseIf LevelQ = "9" Or LevelQ = "14" Then
            FinalAnswer914MediaPlayer.Play()

        ElseIf LevelQ = "10" Or LevelQ = "15" Then
            FinalAnswer1015MediaPlayer.Play()

        ElseIf LevelQ = "666" Or LevelQ = "888" Or LevelQ = "STQ1" Or LevelQ = "STQ2" Then
            My.Computer.Audio.Play(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND24.LOCATION, WwtbamMusicPlaylistConfig.SOUND24.TITLE), AudioPlayMode.Background)
        End If

    End Sub

    Sub StopFinalAnswer()
        My.Computer.Audio.Stop()
        FinalAnswer611MediaPlayer.Stop()
        FinalAnswer712MediaPlayer.Stop()
        FinalAnswer813MediaPlayer.Stop()
        FinalAnswer914MediaPlayer.Stop()
        FinalAnswer1015MediaPlayer.Stop()
    End Sub

    Sub PlayCorrectAnswer(LevelQ As String, VariableMilestone As String)
        StopFinalAnswer()
        StopCorrectAnswer()

        If LevelQ = "5" Then
            CorrectAnswerQ5MediaPlayer.Play()

            ''VARIABLE MILESTONE
        ElseIf LevelQ = VariableMilestone Then
            'My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\81.R32000 - Winner.wav")
            CorrectAnswerQ10MediaPlayer.Play()
            ''VARIABLE MILESTONE
        ElseIf VariableMilestone <> "10" And LevelQ = "10" Then
            My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\76.Q10 NoMilestone - Yes.wav")

        ElseIf LevelQ = "6" Then
            'My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\61.Q6 - Yes.wav")
            CorrectAnswerQ6MediaPlayer.Play()

        ElseIf LevelQ = "7" Then
            'My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\66.Q7 - Yes.wav")
            CorrectAnswerQ7MediaPlayer.Play()

        ElseIf LevelQ = "8" Then
            'My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\71.Q8 - Yes.wav", AudioPlayMode.Background)
            CorrectAnswerQ8MediaPlayer.Play()

        ElseIf LevelQ = "9" Then
            'My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\76.Q9 - Yes.wav", AudioPlayMode.Background)
            CorrectAnswerQ9MediaPlayer.Play()

        ElseIf LevelQ = "11" Then
            'My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\85.Q11 - Yes.wav", AudioPlayMode.Background)
            CorrectAnswerQ11MediaPlayer.Play()

        ElseIf LevelQ = "12" Then
            'My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\89.Q12 - Yes.wav", AudioPlayMode.Background)
            CorrectAnswerQ12MediaPlayer.Play()

        ElseIf LevelQ = "13" Then
            'My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\93.Q13 - Yes.wav", AudioPlayMode.Background)
            CorrectAnswerQ13MediaPlayer.Play()

        ElseIf LevelQ = "14" Then
            'My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\97.Q14 - Yes.wav", AudioPlayMode.Background)
            CorrectAnswerQ14MediaPlayer.Play()

        ElseIf LevelQ = "1" Or LevelQ = "2" Or LevelQ = "3" Or LevelQ = "4" Or LevelQ = "666" Or LevelQ = "888" Then
            'My.Computer.Audio.Play("C:\WWTBAM Removable Disc\UK 2007\29.Q1-5 - Yes.wav")
            CorrectAnswerQ1MediaPlayer.Play()

        End If

        If LevelQ = "15" Then
            CorrectAnswerQ15MediaPlayer.Stop()
            My.Computer.Audio.Play(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND101.LOCATION, WwtbamMusicPlaylistConfig.SOUND101.TITLE), AudioPlayMode.Background)

        End If
    End Sub

    Sub PlayIncorrectAnswer(LevelQ, DoubleDipState)

        StopAll()

        Dim IncorrectAnswerQ15MediaPlayer As New System.Windows.Media.MediaPlayer
        IncorrectAnswerQ15MediaPlayer.Stop()

        If String.Equals(DoubleDipState, "DoubleDipFirstFinal", StringComparison.OrdinalIgnoreCase) Then
            IncorrectAnswerQ15MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND106.LOCATION, WwtbamMusicPlaylistConfig.SOUND106.TITLE)))

        ElseIf LevelQ = "6" Or LevelQ = "11" Then
            IncorrectAnswerQ15MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND60.LOCATION, WwtbamMusicPlaylistConfig.SOUND60.TITLE)))

        ElseIf LevelQ = "7" Or LevelQ = "12" Then
            IncorrectAnswerQ15MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND65.LOCATION, WwtbamMusicPlaylistConfig.SOUND65.TITLE)))

        ElseIf LevelQ = "8" Or LevelQ = "13" Then
            IncorrectAnswerQ15MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND70.LOCATION, WwtbamMusicPlaylistConfig.SOUND70.TITLE)))

        ElseIf LevelQ = "9" Or LevelQ = "14" Then
            IncorrectAnswerQ15MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND75.LOCATION, WwtbamMusicPlaylistConfig.SOUND75.TITLE)))

        ElseIf LevelQ = "10" Then
            IncorrectAnswerQ15MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND80.LOCATION, WwtbamMusicPlaylistConfig.SOUND80.TITLE)))

        ElseIf LevelQ = "1" Or LevelQ = "2" Or LevelQ = "3" Or LevelQ = "4" Or LevelQ = "5" Or LevelQ = "666" Or LevelQ = "888" Then
            IncorrectAnswerQ15MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND30.LOCATION, WwtbamMusicPlaylistConfig.SOUND30.TITLE)))

        ElseIf LevelQ = "15" Then
            IncorrectAnswerQ15MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND100.LOCATION, WwtbamMusicPlaylistConfig.SOUND100.TITLE)))

        End If

        IncorrectAnswerQ15MediaPlayer.Play()

    End Sub

    Sub StopCorrectAnswer()
        CorrectAnswerQ1MediaPlayer.Stop()
        CorrectAnswerQ5MediaPlayer.Stop()
        CorrectAnswerQ6MediaPlayer.Stop()
        CorrectAnswerQ7MediaPlayer.Stop()
        CorrectAnswerQ8MediaPlayer.Stop()
        CorrectAnswerQ9MediaPlayer.Stop()
        CorrectAnswerQ10MediaPlayer.Stop()
        CorrectAnswerQ11MediaPlayer.Stop()
        CorrectAnswerQ12MediaPlayer.Stop()
        CorrectAnswerQ13MediaPlayer.Stop()
        CorrectAnswerQ14MediaPlayer.Stop()
        CorrectAnswerQ15MediaPlayer.Stop()
    End Sub

    Sub PlayLXSound(LevelQ_TextBox As String, VariableMilestone_TextBox As String)
        If Val(LevelQ_TextBox) >= 1 And Val(LevelQ_TextBox) <= 5 Then
            LetsPLAYQ1to5AxWindowsMediaPlayer.Play()
        ElseIf Val(LevelQ_TextBox) = 11 And (Val(VariableMilestone_TextBox) <> 10) Then
            LetsPLAYQ6AxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND119.LOCATION, WwtbamMusicPlaylistConfig.SOUND119.TITLE)))
            LetsPLAYQ6AxWindowsMediaPlayer.Play()
        ElseIf Val(LevelQ_TextBox) = 6 Or (Val(LevelQ_TextBox) = 11 And Val(VariableMilestone_TextBox) = 10) Then
            LetsPLAYQ6AxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND32.LOCATION, WwtbamMusicPlaylistConfig.SOUND32.TITLE)))
            LetsPLAYQ6AxWindowsMediaPlayer.Play()
        ElseIf Val(LevelQ_TextBox) = 7 Or Val(LevelQ_TextBox) = 12 Then
            LetsPLAYQ7AxWindowsMediaPlayer.Play()
        ElseIf Val(LevelQ_TextBox) = 8 Or Val(LevelQ_TextBox) = 13 Then
            LetsPLAYQ8AxWindowsMediaPlayer.Play()
        ElseIf Val(LevelQ_TextBox) = 9 Or Val(LevelQ_TextBox) = 14 Then
            LetsPLAYQ9AxWindowsMediaPlayer.Play()
        ElseIf Val(LevelQ_TextBox) = 10 Or Val(LevelQ_TextBox) = 15 Then
            LetsPLAYQ10AxWindowsMediaPlayer.Play()
        ElseIf LevelQ_TextBox = "666" Then
            WalkAwayLXAxWindowsMediaPlayer.Stop()
            WalkAwayLXAxWindowsMediaPlayer.Play()
        End If
    End Sub

    Sub StopAll()
        Me.StopHeartbeaLetsPlay()
        Me.StopFinalAnswer()
        Me.StopCorrectAnswer()
        My.Computer.Audio.Stop()
    End Sub

    'Sub Iterate()

    '    Dim Objekti As New List(Of String)
    '    Try
    '        For Each obj As Object In WwtbamMusicPlaylistConfig.GetType().GetProperties()
    '            If obj.Name.ToString.StartsWith("SOUND") Then
    '                Dim oh As System.Runtime.Remoting.ObjectHandle = Activator.CreateInstanceFrom(Reflection.Assembly.GetEntryAssembly().CodeBase, "ShowControl_GameOperator.Xml2CSharp." + obj.Name.ToString)
    '                Dim object1 = oh.Unwrap()
    '                object1 = obj
    '                Objekti.Add(object1.Title)
    '            End If
    '        Next
    '    Catch ex As Exception
    '    End Try

    '    Dim p = ""

    'End Sub

End Class
