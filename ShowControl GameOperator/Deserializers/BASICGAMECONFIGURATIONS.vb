Imports System
Imports System.Xml.Serialization
Imports System.Collections.Generic

Namespace Xml2CSharp
    <XmlRoot(ElementName:="Q15")>
    Public Class Q15
        <XmlElement(ElementName:="REALVALUE")>
        Public Property REALVALUE As String
        <XmlElement(ElementName:="PREVIEWVALUE")>
        Public Property PREVIEWVALUE As String
    End Class

    <XmlRoot(ElementName:="Q14")>
    Public Class Q14
        <XmlElement(ElementName:="REALVALUE")>
        Public Property REALVALUE As String
        <XmlElement(ElementName:="PREVIEWVALUE")>
        Public Property PREVIEWVALUE As String
    End Class

    <XmlRoot(ElementName:="Q13")>
    Public Class Q13
        <XmlElement(ElementName:="REALVALUE")>
        Public Property REALVALUE As String
        <XmlElement(ElementName:="PREVIEWVALUE")>
        Public Property PREVIEWVALUE As String
    End Class

    <XmlRoot(ElementName:="Q12")>
    Public Class Q12
        <XmlElement(ElementName:="REALVALUE")>
        Public Property REALVALUE As String
        <XmlElement(ElementName:="PREVIEWVALUE")>
        Public Property PREVIEWVALUE As String
    End Class

    <XmlRoot(ElementName:="Q11")>
    Public Class Q11
        <XmlElement(ElementName:="REALVALUE")>
        Public Property REALVALUE As String
        <XmlElement(ElementName:="PREVIEWVALUE")>
        Public Property PREVIEWVALUE As String
    End Class

    <XmlRoot(ElementName:="Q10")>
    Public Class Q10
        <XmlElement(ElementName:="REALVALUE")>
        Public Property REALVALUE As String
        <XmlElement(ElementName:="PREVIEWVALUE")>
        Public Property PREVIEWVALUE As String
    End Class

    <XmlRoot(ElementName:="Q9")>
    Public Class Q9
        <XmlElement(ElementName:="REALVALUE")>
        Public Property REALVALUE As String
        <XmlElement(ElementName:="PREVIEWVALUE")>
        Public Property PREVIEWVALUE As String
    End Class

    <XmlRoot(ElementName:="Q8")>
    Public Class Q8
        <XmlElement(ElementName:="REALVALUE")>
        Public Property REALVALUE As String
        <XmlElement(ElementName:="PREVIEWVALUE")>
        Public Property PREVIEWVALUE As String
    End Class

    <XmlRoot(ElementName:="Q7")>
    Public Class Q7
        <XmlElement(ElementName:="REALVALUE")>
        Public Property REALVALUE As String
        <XmlElement(ElementName:="PREVIEWVALUE")>
        Public Property PREVIEWVALUE As String
    End Class

    <XmlRoot(ElementName:="Q6")>
    Public Class Q6
        <XmlElement(ElementName:="REALVALUE")>
        Public Property REALVALUE As String
        <XmlElement(ElementName:="PREVIEWVALUE")>
        Public Property PREVIEWVALUE As String
    End Class

    <XmlRoot(ElementName:="Q5")>
    Public Class Q5
        <XmlElement(ElementName:="REALVALUE")>
        Public Property REALVALUE As String
        <XmlElement(ElementName:="PREVIEWVALUE")>
        Public Property PREVIEWVALUE As String
    End Class

    <XmlRoot(ElementName:="Q4")>
    Public Class Q4
        <XmlElement(ElementName:="REALVALUE")>
        Public Property REALVALUE As String
        <XmlElement(ElementName:="PREVIEWVALUE")>
        Public Property PREVIEWVALUE As String
    End Class

    <XmlRoot(ElementName:="Q3")>
    Public Class Q3
        <XmlElement(ElementName:="REALVALUE")>
        Public Property REALVALUE As String
        <XmlElement(ElementName:="PREVIEWVALUE")>
        Public Property PREVIEWVALUE As String
    End Class

    <XmlRoot(ElementName:="Q2")>
    Public Class Q2
        <XmlElement(ElementName:="REALVALUE")>
        Public Property REALVALUE As String
        <XmlElement(ElementName:="PREVIEWVALUE")>
        Public Property PREVIEWVALUE As String
    End Class

    <XmlRoot(ElementName:="Q1")>
    Public Class Q1
        <XmlElement(ElementName:="REALVALUE")>
        Public Property REALVALUE As String
        <XmlElement(ElementName:="PREVIEWVALUE")>
        Public Property PREVIEWVALUE As String
    End Class

    <XmlRoot(ElementName:="MONEYTREE")>
    Public Class MONEYTREE
        <XmlElement(ElementName:="NUMBEROFQUESTIONS")>
        Public Property NUMBEROFQUESTIONS As String
        <XmlElement(ElementName:="DECIMALSEPARATOR")>
        Public Property DECIMALSEPARATOR As String
        <XmlElement(ElementName:="SAFETYNETS")>
        Public Property SAFETYNETS As String
        <XmlElement(ElementName:="VARIABLESAFETYNET")>
        Public Property VARIABLESAFETYNET As String
        <XmlElement(ElementName:="LIFELINEDEPENDANTSAFETYNET")>
        Public Property LIFELINEDEPENDANTSAFETYNET As String
        <XmlElement(ElementName:="Q15")>
        Public Property Q15 As Q15
        <XmlElement(ElementName:="Q14")>
        Public Property Q14 As Q14
        <XmlElement(ElementName:="Q13")>
        Public Property Q13 As Q13
        <XmlElement(ElementName:="Q12")>
        Public Property Q12 As Q12
        <XmlElement(ElementName:="Q11")>
        Public Property Q11 As Q11
        <XmlElement(ElementName:="Q10")>
        Public Property Q10 As Q10
        <XmlElement(ElementName:="Q9")>
        Public Property Q9 As Q9
        <XmlElement(ElementName:="Q8")>
        Public Property Q8 As Q8
        <XmlElement(ElementName:="Q7")>
        Public Property Q7 As Q7
        <XmlElement(ElementName:="Q6")>
        Public Property Q6 As Q6
        <XmlElement(ElementName:="Q5")>
        Public Property Q5 As Q5
        <XmlElement(ElementName:="Q4")>
        Public Property Q4 As Q4
        <XmlElement(ElementName:="Q3")>
        Public Property Q3 As Q3
        <XmlElement(ElementName:="Q2")>
        Public Property Q2 As Q2
        <XmlElement(ElementName:="Q1")>
        Public Property Q1 As Q1
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
