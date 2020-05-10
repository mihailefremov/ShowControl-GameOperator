Public Class FFFMusicLayer

    Private FFFQuestionAxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Private FFFastestFingerFirstAxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Private FFRightOrderAxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Private FF1stinOrderAxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Private FF2ndinOrderAxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Private FF3rdinOrderAxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Private FF4thinOrderAxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer
    Private FFWhoIsCorrectNamesAxWindowsMediaPlayer As New System.Windows.Media.MediaPlayer

    Public WwtbamMusicPlaylistConfig As New Xml2CSharp.MUSICPLAYLISTCONFIGURATION

    Public Sub New()
        'Music configuration
        Dim ConfigurationMusicPath As String = GameConfiguration.Default.DefaultGameConfigurationPath
        Dim MusicConfigurationText As String = System.IO.File.ReadAllText(String.Format("{0}\{1}", ConfigurationMusicPath, "MusicPlaylistConfiguration.xml"))

        Dim MusicConfigurationReader As System.IO.TextReader = New System.IO.StringReader(MusicConfigurationText)

        Dim serializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(Xml2CSharp.MUSICPLAYLISTCONFIGURATION))
        Me.WwtbamMusicPlaylistConfig = serializer.Deserialize(MusicConfigurationReader)

        FFFQuestionAxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(10).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(10).TITLE)))
        FFFQuestionAxWindowsMediaPlayer.Stop()

        FFFastestFingerFirstAxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(12).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(12).TITLE)))
        FFFastestFingerFirstAxWindowsMediaPlayer.Stop()

        FFRightOrderAxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(13).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(13).TITLE)))
        FFRightOrderAxWindowsMediaPlayer.Stop()

        FF1stinOrderAxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(14).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(14).TITLE)))
        FF1stinOrderAxWindowsMediaPlayer.Stop()

        FF2ndinOrderAxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(15).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(15).TITLE)))
        FF2ndinOrderAxWindowsMediaPlayer.Stop()

        FF3rdinOrderAxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(16).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(16).TITLE)))
        FF3rdinOrderAxWindowsMediaPlayer.Stop()

        FF4thinOrderAxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(17).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(17).TITLE)))
        FF4thinOrderAxWindowsMediaPlayer.Stop()

        FFWhoIsCorrectNamesAxWindowsMediaPlayer.Open(New Uri(String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(18).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(18).TITLE)))
        FFWhoIsCorrectNamesAxWindowsMediaPlayer.Stop()

    End Sub

    Sub FFFQuestionPlay()
        FFFQuestionAxWindowsMediaPlayer.Stop()
        FFFQuestionAxWindowsMediaPlayer.Play()
    End Sub

    Sub FFFastestFingerFirstPlay()
        FFFastestFingerFirstAxWindowsMediaPlayer.Stop()
        FFFastestFingerFirstAxWindowsMediaPlayer.Play()

    End Sub

    Sub FFFastestFingerFirstStop()
        FFFastestFingerFirstAxWindowsMediaPlayer.Stop()
    End Sub

    Sub FF1stinOrderPlay()
        FF1stinOrderAxWindowsMediaPlayer.Stop()
        FF1stinOrderAxWindowsMediaPlayer.Play()
    End Sub

    Sub FF2ndinOrderPlay()
        FF2ndinOrderAxWindowsMediaPlayer.Stop()
        FF2ndinOrderAxWindowsMediaPlayer.Play()
    End Sub

    Sub FF3rdinOrderPlay()
        FF3rdinOrderAxWindowsMediaPlayer.Stop()
        FF3rdinOrderAxWindowsMediaPlayer.Play()
    End Sub

    Sub FF4thinOrderPlay()
        FF4thinOrderAxWindowsMediaPlayer.Stop()
        FF4thinOrderAxWindowsMediaPlayer.Play()
    End Sub

    Sub FFWhoIsCorrectNamesPlay()
        FFWhoIsCorrectNamesAxWindowsMediaPlayer.Stop()
        FFWhoIsCorrectNamesAxWindowsMediaPlayer.Play()
    End Sub

    Sub FFRightOrderStop()
        FFRightOrderAxWindowsMediaPlayer.Stop()
    End Sub

    Sub FFRightOrderPlay()
        FFRightOrderAxWindowsMediaPlayer.Stop()
        FFRightOrderAxWindowsMediaPlayer.Play()
    End Sub

End Class
