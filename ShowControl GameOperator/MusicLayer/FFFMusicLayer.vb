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
        FFFQuestionAxWindowsMediaPlayer.URL = (String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND10.LOCATION, WwtbamMusicPlaylistConfig.SOUND10.TITLE))
        FFFQuestionAxWindowsMediaPlayer.Ctlcontrols.stop()

        FFFastestFingerFirstAxWindowsMediaPlayer.CreateControl()
        FFFastestFingerFirstAxWindowsMediaPlayer.URL = (String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND12.LOCATION, WwtbamMusicPlaylistConfig.SOUND12.TITLE))
        FFFastestFingerFirstAxWindowsMediaPlayer.Ctlcontrols.stop()

        FFRightOrderAxWindowsMediaPlayer.CreateControl()
        FFRightOrderAxWindowsMediaPlayer.URL = (String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND13.LOCATION, WwtbamMusicPlaylistConfig.SOUND13.TITLE))
        FFRightOrderAxWindowsMediaPlayer.Ctlcontrols.stop()

        FF1stinOrderAxWindowsMediaPlayer.CreateControl()
        FF1stinOrderAxWindowsMediaPlayer.URL = (String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND14.LOCATION, WwtbamMusicPlaylistConfig.SOUND14.TITLE))
        FF1stinOrderAxWindowsMediaPlayer.Ctlcontrols.stop()

        FF2ndinOrderAxWindowsMediaPlayer.CreateControl()
        FF2ndinOrderAxWindowsMediaPlayer.URL = (String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND15.LOCATION, WwtbamMusicPlaylistConfig.SOUND15.TITLE))
        FF2ndinOrderAxWindowsMediaPlayer.Ctlcontrols.stop()

        FF3rdinOrderAxWindowsMediaPlayer.CreateControl()
        FF3rdinOrderAxWindowsMediaPlayer.URL = (String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND16.LOCATION, WwtbamMusicPlaylistConfig.SOUND16.TITLE))
        FF3rdinOrderAxWindowsMediaPlayer.Ctlcontrols.stop()

        FF4thinOrderAxWindowsMediaPlayer.CreateControl()
        FF4thinOrderAxWindowsMediaPlayer.URL = (String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND17.LOCATION, WwtbamMusicPlaylistConfig.SOUND17.TITLE))
        FF4thinOrderAxWindowsMediaPlayer.Ctlcontrols.stop()

        FFWhoIsCorrectNamesAxWindowsMediaPlayer.CreateControl()
        FFWhoIsCorrectNamesAxWindowsMediaPlayer.URL = (String.Format("{0}\{1}", WwtbamMusicPlaylistConfig.SOUND18.LOCATION, WwtbamMusicPlaylistConfig.SOUND18.TITLE))
        FFWhoIsCorrectNamesAxWindowsMediaPlayer.Ctlcontrols.stop()

    End Sub

End Class
