Imports System
Imports System.Xml.Serialization
Imports System.Collections.Generic

Namespace Xml2CSharp
    <XmlRoot(ElementName:="SOUND")>
    Public Class SOUND
        <XmlElement(ElementName:="NUMBER")>
        Public Property NUMBER As String
        <XmlElement(ElementName:="TITLE")>
        Public Property TITLE As String
        <XmlElement(ElementName:="LOCATION")>
        Public Property LOCATION As String
        <XmlElement(ElementName:="DESCRIPTION")>
        Public Property DESCRIPTION As String
        <XmlElement(ElementName:="LOOP")>
        Public Property [LOOP] As String
    End Class

    <XmlRoot(ElementName:="MUSICPLAYLISTCONFIGURATION")>
    Public Class MUSICPLAYLISTCONFIGURATION
        <XmlElement(ElementName:="SOUND")>
        Public Property SOUND As List(Of SOUND)
        Public Function GetSoundByNumber(Number As String) As SOUND
            For Each SOUND1 In SOUND
                If SOUND1.NUMBER = Number Then
                    Return SOUND1
                End If
            Next
            Return SOUND.ElementAt(0) 'TODO return test sound
        End Function
    End Class
End Namespace
