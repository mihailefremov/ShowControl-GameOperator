Public Class WwtbamLocalizer

    Private LocalizationConfiguration As Xml2CSharp.LOCALIZATIONCONFIGURATION

    Public Sub New()
        Dim ConfigurationPath As String = GameConfiguration.Default.DefaultGameConfigurationPath
        Dim LocalizationConfigurationText As String = System.IO.File.ReadAllText(String.Format("{0}\{1}", ConfigurationPath, "LocalizationConfiguration.xml"))
        Dim LocalizationConfigurationReader As System.IO.TextReader = New System.IO.StringReader(LocalizationConfigurationText)
        Dim serializerLocalizer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(Xml2CSharp.LOCALIZATIONCONFIGURATION))
        LocalizationConfiguration = serializerLocalizer.Deserialize(LocalizationConfigurationReader)
    End Sub

    Public Function GetValueByKey(Key As String) As String
        For Each t In LocalizationConfiguration.TAG
            If t.KEY = Key.Trim Then
                Return t.VALUE
            End If
        Next
        Return Key
    End Function

    Public Function GetCharArrayByKey(Key As String) As String
        Return GetValueByKey(Key).ToCharArray()
    End Function

    Public Function LocalizeControl(Form As Quiz_Operator)
        Form.AnswerAappear_Label.Text = GetValueByKey("ANSWERMARKS").ToCharArray.ElementAtOrDefault(0) + ":"
        Form.AnswerBappear_Label.Text = GetValueByKey("ANSWERMARKS").ToCharArray.ElementAtOrDefault(1) + ":"
        Form.AnswerCappear_Label.Text = GetValueByKey("ANSWERMARKS").ToCharArray.ElementAtOrDefault(2) + ":"
        Form.AnswerDappear_Label.Text = GetValueByKey("ANSWERMARKS").ToCharArray.ElementAtOrDefault(3) + ":"
        'Throw New NotImplementedException
    End Function

End Class
