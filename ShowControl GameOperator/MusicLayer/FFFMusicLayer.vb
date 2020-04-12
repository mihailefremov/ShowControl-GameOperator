Public Class FFFMusicLayer

    Public FFFQuestionAxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public FFFastestFingerFirstAxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public FFRightOrderAxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public FF1stinOrderAxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public FF2ndinOrderAxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public FF3rdinOrderAxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public FF4thinOrderAxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public FFWhoIsCorrectNamesAxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer

    Public WwtbamMusicPlaylistConfig As New Xml2CSharp.MUSICPLAYLISTCONFIGURATION

    Public Sub New()
        'Music configuration
        Dim ConfigurationMusicPath As String = GameConfiguration.Default.DefaultGameConfigurationPath
        Dim MusicConfigurationText As String = System.IO.File.ReadAllText(String.Format("{0}\{1}", ConfigurationMusicPath, "MusicPlaylistConfiguration.xml"))

        Dim MusicConfigurationReader As System.IO.TextReader = New System.IO.StringReader(MusicConfigurationText)

        Dim serializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(Xml2CSharp.MUSICPLAYLISTCONFIGURATION))
        Me.WwtbamMusicPlaylistConfig = serializer.Deserialize(MusicConfigurationReader)

        FFFQuestionAxWindowsMediaPlayer.CreateControl()
        FFFQuestionAxWindowsMediaPlayer.URL = (String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(10).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(10).TITLE))
        FFFQuestionAxWindowsMediaPlayer.Ctlcontrols.stop()

        FFFastestFingerFirstAxWindowsMediaPlayer.CreateControl()
        FFFastestFingerFirstAxWindowsMediaPlayer.URL = (String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(12).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(12).TITLE))
        FFFastestFingerFirstAxWindowsMediaPlayer.Ctlcontrols.stop()

        FFRightOrderAxWindowsMediaPlayer.CreateControl()
        FFRightOrderAxWindowsMediaPlayer.URL = (String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(13).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(13).TITLE))
        FFRightOrderAxWindowsMediaPlayer.Ctlcontrols.stop()

        FF1stinOrderAxWindowsMediaPlayer.CreateControl()
        FF1stinOrderAxWindowsMediaPlayer.URL = (String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(14).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(14).TITLE))
        FF1stinOrderAxWindowsMediaPlayer.Ctlcontrols.stop()

        FF2ndinOrderAxWindowsMediaPlayer.CreateControl()
        FF2ndinOrderAxWindowsMediaPlayer.URL = (String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(15).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(15).TITLE))
        FF2ndinOrderAxWindowsMediaPlayer.Ctlcontrols.stop()

        FF3rdinOrderAxWindowsMediaPlayer.CreateControl()
        FF3rdinOrderAxWindowsMediaPlayer.URL = (String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(16).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(16).TITLE))
        FF3rdinOrderAxWindowsMediaPlayer.Ctlcontrols.stop()

        FF4thinOrderAxWindowsMediaPlayer.CreateControl()
        FF4thinOrderAxWindowsMediaPlayer.URL = (String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(17).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(17).TITLE))
        FF4thinOrderAxWindowsMediaPlayer.Ctlcontrols.stop()

        FFWhoIsCorrectNamesAxWindowsMediaPlayer.CreateControl()
        FFWhoIsCorrectNamesAxWindowsMediaPlayer.URL = (String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.GetSoundByNumber(18).LOCATION, WwtbamMusicPlaylistConfig.GetSoundByNumber(18).TITLE))
        FFWhoIsCorrectNamesAxWindowsMediaPlayer.Ctlcontrols.stop()

    End Sub

End Class
