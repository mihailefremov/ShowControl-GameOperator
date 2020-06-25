Public Class MainGameMusicLayer

    Private Q1to5AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Private Q6AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Private Q7AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Private Q8AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Private Q9AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Private Q10AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Private Q11AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Private Q12AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Private Q13AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Private Q14AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Private Q15AxWindowsMediaPlayer1 As New System.Windows.Media.MediaPlayer
    Private FinalAnswer611MediaPlayer As New System.Windows.Media.MediaPlayer
    Private FinalAnswer712MediaPlayer As New System.Windows.Media.MediaPlayer
    Private FinalAnswer813MediaPlayer As New System.Windows.Media.MediaPlayer
    Private FinalAnswer914MediaPlayer As New System.Windows.Media.MediaPlayer
    Private FinalAnswer1015MediaPlayer As New System.Windows.Media.MediaPlayer

    Private CorrectAnswerQ1MediaPlayer As New System.Windows.Media.MediaPlayer
    Private CorrectAnswerQ5MediaPlayer As New System.Windows.Media.MediaPlayer
    Private CorrectAnswerQ6MediaPlayer As New System.Windows.Media.MediaPlayer
    Private CorrectAnswerQ7MediaPlayer As New System.Windows.Media.MediaPlayer
    Private CorrectAnswerQ8MediaPlayer As New System.Windows.Media.MediaPlayer
    Private CorrectAnswerQ9MediaPlayer As New System.Windows.Media.MediaPlayer
    Private CorrectAnswerQ10MediaPlayer As New System.Windows.Media.MediaPlayer
    Private CorrectAnswerQ11MediaPlayer As New System.Windows.Media.MediaPlayer
    Private CorrectAnswerQ12MediaPlayer As New System.Windows.Media.MediaPlayer
    Private CorrectAnswerQ13MediaPlayer As New System.Windows.Media.MediaPlayer
    Private CorrectAnswerQ14MediaPlayer As New System.Windows.Media.MediaPlayer
    Private CorrectAnswerQ15MediaPlayer As New System.Windows.Media.MediaPlayer

    Private IncorrectAnswerQMediaPlayer As New System.Windows.Media.MediaPlayer

    Private LimitedClock_WindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Private DoubleDipBackground_WindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Private LetsPLAYQ1to5AxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Private LetsPLAYQ6AxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Private LetsPLAYQ7AxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Private LetsPLAYQ8AxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Private LetsPLAYQ9AxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Private LetsPLAYQ10AxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Private WalkAwayLXAxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer

    Private LifelineSoundWindowsMediaPlayer As New System.Windows.Media.MediaPlayer

    Private AnyMusicLXAxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Private ArbitrarySoundMediaPlayer As New Windows.Media.MediaPlayer
    Private TopLevelQuestionPosition As Short = 15

    Public WwtbamMusicPlaylistConfig As New Xml2CSharp.MUSICPLAYLISTCONFIGURATION
    Public Mute As Boolean = False

    Sub New(TopLevelQuestionPosition As Short)
        'Music configuration

        Me.TopLevelQuestionPosition = TopLevelQuestionPosition

        Dim ConfigurationMusicPath As String = GameConfiguration.Default.DefaultGameConfigurationPath
        Dim MusicConfigurationText As String = System.IO.File.ReadAllText(String.Format("{0}\{1}", ConfigurationMusicPath, "MusicPlaylistConfiguration.xml"))

        Dim MusicConfigurationReader As System.IO.TextReader = New System.IO.StringReader(MusicConfigurationText)

        Dim serializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(Xml2CSharp.MUSICPLAYLISTCONFIGURATION))
        Me.WwtbamMusicPlaylistConfig = serializer.Deserialize(MusicConfigurationReader)

        Q1to5AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(28).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(28).TITLE)))
        Q1to5AxWindowsMediaPlayer1.Stop()

        Q6AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(58).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(58).TITLE)))
        'Q6AxWindowsMediaPlayer1.Open(New Uri("C:\WWTBAM Removable Disc\UK 2007\120.Q6 - Heartbeat Loop.wav"))
        Q6AxWindowsMediaPlayer1.Stop()

        Q7AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(63).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(63).TITLE)))
        'Q7AxWindowsMediaPlayer1.Open(New Uri("C\WWTBAM Removable Disc\UK 2007\121.Q7 - Heartbeat Loop.wav"))
        Q7AxWindowsMediaPlayer1.Stop()

        Q8AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(68).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(68).TITLE)))
        'Q8AxWindowsMediaPlayer1.Open(New Uri("C:\WWTBAM Removable Disc\UK 2007\122.Q8 - Heartbeat Loop.wav"))
        Q8AxWindowsMediaPlayer1.Stop()

        Q9AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(73).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(73).TITLE)))
        'Q9AxWindowsMediaPlayer1.Open(New Uri("C\WWTBAM Removable Disc\UK 2007\123.Q9 - Heartbeat Loop.wav"))
        Q9AxWindowsMediaPlayer1.Stop()

        Q10AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(78).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(78).TITLE)))
        'Q10AxWindowsMediaPlayer1.Open(New Uri("C:\WWTBAM Removable Disc\UK 2007\124.Q10 - Heartbeat Loop.wav"))
        Q10AxWindowsMediaPlayer1.Stop()

        Q11AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(83).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(83).TITLE)))
        'Q11AxWindowsMediaPlayer1.Open(New Uri("C\WWTBAM Removable Disc\UK 2007\125.Q11 - Heartbeat Loop.wav"))
        Q11AxWindowsMediaPlayer1.Stop()

        Q12AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(87).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(87).TITLE)))
        'Q12AxWindowsMediaPlayer1.Open(New Uri("C:\WWTBAM Removable Disc\UK 2007\126.Q12 - Heartbeat Loop.wav"))
        Q12AxWindowsMediaPlayer1.Stop()

        Q13AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(91).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(91).TITLE)))
        'Q13AxWindowsMediaPlayer1.Open(New Uri("C\WWTBAM Removable Disc\UK 2007\127.Q13 - Heartbeat Loop.wav"))
        Q13AxWindowsMediaPlayer1.Stop()

        Q14AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(95).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(95).TITLE)))
        'Q14AxWindowsMediaPlayer1.Open(New Uri("C:\WWTBAM Removable Disc\UK 2007\128.Q14 - Heartbeat Loop.wav"))
        Q14AxWindowsMediaPlayer1.Stop()

        Q15AxWindowsMediaPlayer1.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(99).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(99).TITLE)))
        'Q15AxWindowsMediaPlayer1.Open(New Uri("C\WWTBAM Removable Disc\UK 2007\129.Q15 - Heartbeat Loop.wav"))
        Q15AxWindowsMediaPlayer1.Stop()

        DoubleDipBackground_WindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(107).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(107).TITLE)))
        DoubleDipBackground_WindowsMediaPlayer.Stop()

        LetsPLAYQ1to5AxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(27).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(27).TITLE)))
        LetsPLAYQ1to5AxWindowsMediaPlayer.Stop()

        LetsPLAYQ6AxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(32).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(32).TITLE)))
        LetsPLAYQ6AxWindowsMediaPlayer.Stop()

        LetsPLAYQ7AxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(62).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(62).TITLE)))
        LetsPLAYQ7AxWindowsMediaPlayer.Stop()

        LetsPLAYQ8AxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(67).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(67).TITLE)))
        LetsPLAYQ8AxWindowsMediaPlayer.Stop()

        LetsPLAYQ9AxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(72).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(72).TITLE)))
        LetsPLAYQ9AxWindowsMediaPlayer.Stop()

        LetsPLAYQ10AxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(77).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(77).TITLE)))
        LetsPLAYQ10AxWindowsMediaPlayer.Stop()

        'LimitedClock_WindowsMediaPlayer.Open(New Uri("C:\WWTBAM Removable Disc\US 2008\Clock 100 Seconds.wav"))
        'LimitedClock_WindowsMediaPlayer.Stop()

        WalkAwayLXAxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(34).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(34).TITLE)))
        WalkAwayLXAxWindowsMediaPlayer.Stop()

        FinalAnswer611MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(59).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(59).TITLE)))
        FinalAnswer611MediaPlayer.Stop()

        FinalAnswer712MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(64).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(64).TITLE)))
        FinalAnswer712MediaPlayer.Stop()

        FinalAnswer813MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(69).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(69).TITLE)))
        FinalAnswer813MediaPlayer.Stop()

        FinalAnswer914MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(74).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(74).TITLE)))
        FinalAnswer914MediaPlayer.Stop()

        FinalAnswer1015MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(79).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(79).TITLE)))
        FinalAnswer1015MediaPlayer.Stop()

        CorrectAnswerQ1MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(29).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(29).TITLE)))
        CorrectAnswerQ5MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(31).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(31).TITLE)))
        CorrectAnswerQ6MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(61).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(61).TITLE)))
        CorrectAnswerQ7MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(66).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(66).TITLE)))
        CorrectAnswerQ8MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(71).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(71).TITLE)))
        CorrectAnswerQ9MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(76).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(76).TITLE)))
        CorrectAnswerQ10MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(81).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(81).TITLE)))
        CorrectAnswerQ11MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(85).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(85).TITLE)))
        CorrectAnswerQ12MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(89).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(89).TITLE)))
        CorrectAnswerQ13MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(93).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(93).TITLE)))
        CorrectAnswerQ14MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(97).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(97).TITLE)))
        CorrectAnswerQ15MediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(101).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(101).TITLE)))

        StopCorrectAnswer()

    End Sub

    Sub StopHeartbeaLetsPlay()
        Me.Q1to5AxWindowsMediaPlayer1.Stop()
        Me.Q6AxWindowsMediaPlayer1.Stop()
        Me.Q7AxWindowsMediaPlayer1.Stop()
        Me.Q8AxWindowsMediaPlayer1.Stop()
        Me.Q9AxWindowsMediaPlayer1.Stop()
        Me.Q10AxWindowsMediaPlayer1.Stop()
        Me.Q11AxWindowsMediaPlayer1.Stop()
        Me.Q12AxWindowsMediaPlayer1.Stop()
        Me.Q13AxWindowsMediaPlayer1.Stop()
        Me.Q14AxWindowsMediaPlayer1.Stop()
        Me.Q15AxWindowsMediaPlayer1.Stop()
        Me.LimitedClock_WindowsMediaPlayer.Stop()
        Me.DoubleDipBackground_WindowsMediaPlayer.Stop()
        StopLetsPlay()
    End Sub

    Sub StopLetsPlay()
        Me.LimitedClock_WindowsMediaPlayer.Stop()
        Me.DoubleDipBackground_WindowsMediaPlayer.Stop()
        Me.LetsPLAYQ1to5AxWindowsMediaPlayer.Stop()
        Me.LetsPLAYQ6AxWindowsMediaPlayer.Stop()
        Me.LetsPLAYQ7AxWindowsMediaPlayer.Stop()
        Me.LetsPLAYQ8AxWindowsMediaPlayer.Stop()
        Me.LetsPLAYQ9AxWindowsMediaPlayer.Stop()
        Me.LetsPLAYQ10AxWindowsMediaPlayer.Stop()
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
        If Mute Then Return
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
        If Mute Then Return
        StopHeartbeaLetsPlay()
        DoubleDipBackground_WindowsMediaPlayer.Play()
    End Sub

    Sub StopDoubleDipBackground()
        DoubleDipBackground_WindowsMediaPlayer.Stop()
    End Sub


    Sub PlayFinalAnswerSound(LevelQ As String, DoubleDipState As String)

        If Not (LevelQ = "1" Or LevelQ = "2" Or LevelQ = "3" Or LevelQ = "4" Or LevelQ = "5") Then
            StopAll()
        End If

        If Mute Then Return

        StopFinalAnswer()

        If String.Equals(DoubleDipState, "DoubleDipFirstFinal", StringComparison.OrdinalIgnoreCase) Then
            My.Computer.Audio.Play(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(105).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(105).TITLE), AudioPlayMode.Background)

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
            My.Computer.Audio.Play(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(24).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(24).TITLE), AudioPlayMode.Background)
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
        If Mute Then Return

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
            My.Computer.Audio.Play(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(101).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(101).TITLE), AudioPlayMode.Background)

        End If
    End Sub

    Sub PlayIncorrectAnswer(LevelQ, DoubleDipState)
        If Mute Then Return

        StopAll()

        If String.Equals(DoubleDipState, "DoubleDipFirstFinal", StringComparison.OrdinalIgnoreCase) Then
            IncorrectAnswerQMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(106).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(106).TITLE)))

        ElseIf LevelQ = "6" Or LevelQ = "11" Then
            IncorrectAnswerQMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(60).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(60).TITLE)))

        ElseIf LevelQ = "7" Or LevelQ = "12" Then
            IncorrectAnswerQMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(65).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(65).TITLE)))

        ElseIf LevelQ = "8" Or LevelQ = "13" Then
            IncorrectAnswerQMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(70).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(70).TITLE)))

        ElseIf LevelQ = "9" Or LevelQ = "14" Then
            IncorrectAnswerQMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(75).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(75).TITLE)))

        ElseIf LevelQ = "10" Then
            IncorrectAnswerQMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(80).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(80).TITLE)))

        ElseIf LevelQ = "1" Or LevelQ = "2" Or LevelQ = "3" Or LevelQ = "4" Or LevelQ = "5" Or LevelQ = "666" Or LevelQ = "888" Then
            IncorrectAnswerQMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(30).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(30).TITLE)))

        ElseIf LevelQ = "15" Then
            IncorrectAnswerQMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(100).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(100).TITLE)))

        End If

        IncorrectAnswerQMediaPlayer.Play()

    End Sub

    Sub StopIncorrectAnswer()
        IncorrectAnswerQMediaPlayer.Stop()
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
        StopLetsPlay()
        If Mute Then Return
        If String.IsNullOrWhiteSpace(VariableMilestone_TextBox) Then Return

        If Val(LevelQ_TextBox) >= 1 And Val(LevelQ_TextBox) <= 5 Then
            LetsPLAYQ1to5AxWindowsMediaPlayer.Play()
        ElseIf Val(LevelQ_TextBox) = 11 And (Val(VariableMilestone_TextBox) <> 10) Then
            LetsPLAYQ6AxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(119).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(119).TITLE)))
            LetsPLAYQ6AxWindowsMediaPlayer.Play()
        ElseIf Val(LevelQ_TextBox) = 6 Or (Val(LevelQ_TextBox) = 11 And Val(VariableMilestone_TextBox) = 10) Then
            LetsPLAYQ6AxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(32).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(32).TITLE)))
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
        StopHeartbeaLetsPlay()
        StopFinalAnswer()
        StopCorrectAnswer()
        StopIncorrectAnswer()
        My.Computer.Audio.Stop()
        ArbitrarySoundMediaPlayer.Stop()
    End Sub

    Sub PlayLifelineSound(Lifeline As String, State As String)
        Lifeline = Lifeline.ToUpper
        State = State.ToUpper

        LifelineSoundWindowsMediaPlayer.Stop()

        Select Case Lifeline + " " + State
            Case "ATA ASK"
                LifelineSoundWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(41).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(41).TITLE)))
            Case "ATA VOTE"
                LifelineSoundWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(42).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(42).TITLE)))
            Case "ATA DECIDE"
                LifelineSoundWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(43).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(43).TITLE)))
            Case "PAF CALL"
                LifelineSoundWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(44).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(44).TITLE)))
            Case "PAF CLOCK"
                LifelineSoundWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(45).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(45).TITLE)))
            Case "PAF ABORT"
                LifelineSoundWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(46).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(46).TITLE)))
        End Select

        LifelineSoundWindowsMediaPlayer.Play()

    End Sub

    Sub PlayArbitrarySound(Sound As Xml2CSharp.SOUND)
        ArbitrarySoundMediaPlayer.Stop()
        If Mute Then Return

        ArbitrarySoundMediaPlayer.Open(New Uri(String.Format("{0}\{1}", Sound.LOCATION, Sound.TITLE)))
        ArbitrarySoundMediaPlayer.Play()
    End Sub

    Sub LimitedClockPlay()

    End Sub

    Sub LimitedClockStop()

    End Sub

End Class
