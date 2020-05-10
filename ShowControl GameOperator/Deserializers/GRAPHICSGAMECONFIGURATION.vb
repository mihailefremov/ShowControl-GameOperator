Imports System
Imports System.Xml.Serialization
Imports System.Collections.Generic

Namespace Xml2CSharp
    <XmlRoot(ElementName:="AUTOREMOVEQUESTIONDEFAULT")>
    Public Class AUTOREMOVEQUESTIONDEFAULT
        <XmlElement(ElementName:="SECONDS")>
        Public Property SECONDS As String
    End Class

    <XmlRoot(ElementName:="AUTOREMOVEQUESTIONSPECIFIC")>
    Public Class AUTOREMOVEQUESTIONSPECIFIC
        <XmlElement(ElementName:="LEVEL")>
        Public Property LEVEL As String
        <XmlElement(ElementName:="SECONDS")>
        Public Property SECONDS As String
    End Class

    <XmlRoot(ElementName:="AUTOSHOWCURRENTPRIZEWONDEFAULT")>
    Public Class AUTOSHOWCURRENTPRIZEWONDEFAULT
        <XmlElement(ElementName:="LEVEL")>
        Public Property LEVEL As String
        <XmlElement(ElementName:="SECONDS")>
        Public Property SECONDS As String
    End Class

    <XmlRoot(ElementName:="AUTOSHOWCURRENTPRIZEWONSPECIFIC")>
    Public Class AUTOSHOWCURRENTPRIZEWONSPECIFIC
        <XmlElement(ElementName:="LEVEL")>
        Public Property LEVEL As String
        <XmlElement(ElementName:="SECONDS")>
        Public Property SECONDS As String
    End Class

    <XmlRoot(ElementName:="GRAPHICSGAMECONFIGURATION")>
    Public Class GRAPHICSGAMECONFIGURATION
        <XmlElement(ElementName:="AUTOREMOVEQUESTIONIFCORRECT")>
        Public Property AUTOREMOVEQUESTIONIFCORRECT As String
        <XmlElement(ElementName:="AUTOREMOVEQUESTIONDEFAULT")>
        Public Property AUTOREMOVEQUESTIONDEFAULT As AUTOREMOVEQUESTIONDEFAULT
        <XmlElement(ElementName:="AUTOREMOVEQUESTIONSPECIFIC")>
        Public Property AUTOREMOVEQUESTIONSPECIFIC As List(Of AUTOREMOVEQUESTIONSPECIFIC)
        <XmlElement(ElementName:="AUTOSHOWCURRENTPRIZEWON")>
        Public Property AUTOSHOWCURRENTPRIZEWON As String
        <XmlElement(ElementName:="AUTOSHOWCURRENTPRIZEWONDEFAULT")>
        Public Property AUTOSHOWCURRENTPRIZEWONDEFAULT As AUTOSHOWCURRENTPRIZEWONDEFAULT
        <XmlElement(ElementName:="AUTOSHOWCURRENTPRIZEWONSPECIFIC")>
        Public Property AUTOSHOWCURRENTPRIZEWONSPECIFIC As List(Of AUTOSHOWCURRENTPRIZEWONSPECIFIC)
    End Class
End Namespace
