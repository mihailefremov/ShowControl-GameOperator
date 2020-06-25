Imports System
Imports System.Xml.Serialization
Imports System.Collections.Generic

Namespace Xml2CSharp
    <XmlRoot(ElementName:="TAG")>
    Public Class TAG
        <XmlElement(ElementName:="KEY")>
        Public Property KEY As String
        <XmlElement(ElementName:="VALUE")>
        Public Property VALUE As String
    End Class

    <XmlRoot(ElementName:="LOCALIZATIONCONFIGURATION")>
    Public Class LOCALIZATIONCONFIGURATION
        <XmlElement(ElementName:="TAG")>
        Public Property TAG As List(Of TAG)
    End Class
End Namespace