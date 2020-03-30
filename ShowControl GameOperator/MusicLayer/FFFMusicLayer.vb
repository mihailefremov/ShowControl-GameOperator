Public Class FFFMusicLayer

    Public FFFastestFingerFirstAxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public FFRightOrderAxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public FF1stinOrderAxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public FF2ndinOrderAxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public FF3rdinOrderAxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public FF4thinOrderAxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public FFWhoIsCorrectNamesAxWindowsMediaPlayer As New AxWMPLib.AxWindowsMediaPlayer
    Public Sub New()
        FFFastestFingerFirstAxWindowsMediaPlayer.CreateControl()
        FFFastestFingerFirstAxWindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\UK 2007\12.Fastest finger first.wav"
        FFFastestFingerFirstAxWindowsMediaPlayer.Ctlcontrols.stop()

        FFRightOrderAxWindowsMediaPlayer.CreateControl()
        FFRightOrderAxWindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\UK 2007\13.What's the Order.wav"
        FFRightOrderAxWindowsMediaPlayer.Ctlcontrols.stop()

        FF1stinOrderAxWindowsMediaPlayer.CreateControl()
        FF1stinOrderAxWindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\UK 2007\14.1st in Order.wav"
        FF1stinOrderAxWindowsMediaPlayer.Ctlcontrols.stop()

        FF2ndinOrderAxWindowsMediaPlayer.CreateControl()
        FF2ndinOrderAxWindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\UK 2007\15.2nd in Order.wav"
        FF2ndinOrderAxWindowsMediaPlayer.Ctlcontrols.stop()

        FF3rdinOrderAxWindowsMediaPlayer.CreateControl()
        FF3rdinOrderAxWindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\UK 2007\16.3rd in Order.wav"
        FF3rdinOrderAxWindowsMediaPlayer.Ctlcontrols.stop()

        FF4thinOrderAxWindowsMediaPlayer.CreateControl()
        FF4thinOrderAxWindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\UK 2007\17.4th in Order.wav"
        FF4thinOrderAxWindowsMediaPlayer.Ctlcontrols.stop()

        FFWhoIsCorrectNamesAxWindowsMediaPlayer.CreateControl()
        FFWhoIsCorrectNamesAxWindowsMediaPlayer.URL = "C:\WWTBAM Removable Disc\UK 2007\18.Correct Answers.wav"
        FFWhoIsCorrectNamesAxWindowsMediaPlayer.Ctlcontrols.stop()

    End Sub

End Class
