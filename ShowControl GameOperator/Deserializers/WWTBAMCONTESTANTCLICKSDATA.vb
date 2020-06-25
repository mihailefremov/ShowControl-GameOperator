Imports System
Imports System.Xml.Serialization
Imports System.Collections.Generic

Namespace Xml2CSharp
    <XmlRoot(ElementName:="WWTBAM-CONTESTANTCLICKS-DATA")>
    Public Class WWTBAMCONTESTANTCLICKSDATA
        <XmlElement(ElementName:="STATEID")>
        Public Property STATEID As String
        <XmlElement(ElementName:="CLICKTYPE")>
        Public Property CLICKTYPE As String
        <XmlElement(ElementName:="CLICKVALUE")>
        Public Property CLICKVALUE As String
    End Class
End Namespace