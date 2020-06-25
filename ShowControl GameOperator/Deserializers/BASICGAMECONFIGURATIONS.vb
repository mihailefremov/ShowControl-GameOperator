Imports System
Imports System.Xml.Serialization
Imports System.Collections.Generic

Namespace Xml2CSharp
    <XmlRoot(ElementName:="LEVEL")>
    Public Class LEVEL
        <XmlElement(ElementName:="POSITION")>
        Public Property POSITION As String
        <XmlElement(ElementName:="REALVALUE")>
        Public Property REALVALUE As String
        <XmlElement(ElementName:="PREVIEWVALUE")>
        Public Property PREVIEWVALUE As String
        <XmlElement(ElementName:="SAFEHEAVEN")>
        Public Property SAFEHEAVEN As String
    End Class

    <XmlRoot(ElementName:="MONEYTREE")>
    Public Class MONEYTREE
        <XmlElement(ElementName:="NUMBEROFQUESTIONS")>
        Public Property NUMBEROFQUESTIONS As String
        <XmlElement(ElementName:="DECIMALSEPARATOR")>
        Public Property DECIMALSEPARATOR As String
        <XmlElement(ElementName:="VARIABLESAFETYNET")>
        Public Property VARIABLESAFETYNET As String
        <XmlElement(ElementName:="LIFELINEDEPENDANTSAFETYNET")>
        Public Property LIFELINEDEPENDANTSAFETYNET As String
        <XmlElement(ElementName:="LEVEL")>
        Public Property LEVEL As List(Of LEVEL)
        Public Function GetLevelByPosition(Position As Short) As LEVEL
            If LEVEL.Where(Function(t) t.POSITION = Position).FirstOrDefault() IsNot Nothing Then
                Return LEVEL.Where(Function(t) t.POSITION = Position).FirstOrDefault()
            End If
            Return Nothing
        End Function
    End Class

    <XmlRoot(ElementName:="LIFELINES")>
    Public Class LIFELINES
        <XmlElement(ElementName:="DEFAULTLIFELINECOUNT")>
        Public Property DEFAULTLIFELINECOUNT As String
        <XmlElement(ElementName:="LIFELINE1")>
        Public Property LIFELINE1 As String
        <XmlElement(ElementName:="LIFELINE2")>
        Public Property LIFELINE2 As String
        <XmlElement(ElementName:="LIFELINE3")>
        Public Property LIFELINE3 As String
        <XmlElement(ElementName:="LIFELINE4")>
        Public Property LIFELINE4 As String
        <XmlElement(ElementName:="LIFELINE5")>
        Public Property LIFELINE5 As String
    End Class

    <XmlRoot(ElementName:="BASICGAMECONFIGURATIONS")>
    Public Class BASICGAMECONFIGURATIONS
        <XmlElement(ElementName:="MONEYTREE")>
        Public Property MONEYTREE As MONEYTREE
        <XmlElement(ElementName:="LIFELINES")>
        Public Property LIFELINES As LIFELINES
    End Class
End Namespace
