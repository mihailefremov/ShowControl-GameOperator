Public Class Lifelines

    Dim LifelinesState1 As String = ""
    Public ReadOnly Property LifelinesState As String
        Get
            Dim AllLifelines As String = String.Format("{0};{1};{2};{3}", Helpers.ConvertLifelineStateToReadable(Lifeline1Active), Helpers.ConvertLifelineStateToReadable(Lifeline2Active), Helpers.ConvertLifelineStateToReadable(Lifeline3Active), Helpers.ConvertLifelineStateToReadable(Lifeline4Active))
            Return AllLifelines
        End Get
    End Property

    Dim lifeline1Active1 As Short = 1
    Public Property Lifeline1Active As Short
        Get
            Return lifeline1Active1
        End Get
        Set(value As Short)
            lifeline1Active1 = value
            GraphicsProcessingUnit.MarkCGlifelines(lifeline1Active1, Lifeline2Active, Lifeline3Active, Lifeline4Active)
            HostContPresentationLayer.GamePlayStateSet("LIFELINE_UPDATE")
        End Set
    End Property

    Dim lifeline2Active1 As Short = 1
    Public Property Lifeline2Active As Short
        Get
            Return lifeline2Active1
        End Get
        Set(value As Short)
            lifeline2Active1 = value
            GraphicsProcessingUnit.MarkCGlifelines(lifeline1Active1, Lifeline2Active, Lifeline3Active, Lifeline4Active)
            HostContPresentationLayer.GamePlayStateSet("LIFELINE_UPDATE")
        End Set
    End Property

    Dim lifeline3Active1 As Short = 1
    Public Property Lifeline3Active As Short
        Get
            Return lifeline3Active1
        End Get
        Set(value As Short)
            lifeline3Active1 = value
            GraphicsProcessingUnit.MarkCGlifelines(lifeline1Active1, Lifeline2Active, Lifeline3Active, Lifeline4Active)
            HostContPresentationLayer.GamePlayStateSet("LIFELINE_UPDATE")
        End Set
    End Property

    Dim lifeline4Active1 As Short = 1
    Public Property Lifeline4Active As Short
        Get
            Return lifeline4Active1
        End Get
        Set(value As Short)
            lifeline4Active1 = value
            GraphicsProcessingUnit.MarkCGlifelines(lifeline1Active1, Lifeline2Active, Lifeline3Active, Lifeline4Active)
            HostContPresentationLayer.GamePlayStateSet("LIFELINE_UPDATE")
        End Set
    End Property

    Dim ActiveLifelines1 As String = "4"
    Public Property ActiveLifelines As String
        Get
            Return ActiveLifelines1
        End Get
        Set(value As String)
            ActiveLifelines1 = value
            HostContPresentationLayer.GamePlayStateSet("")
        End Set
    End Property

    Public FiftyFiftyPosition As Short = 0
    Public PhoneAFriendPosition As Short = 0
    Public AskTheAudiencePosition As Short = 0
    Public SwitchTheQuestionPosition As Short = 0
    Public DoubleDipPosition As Short = 0
    Public AskTheHostPosition As Short = 0
    Public AskTheExpertPosition As Short = 0

    Dim ThisGameLifelines_ As String
    Public Property ThisGameLifelines As String
        Get
            Return ThisGameLifelines_
        End Get
        Set(value As String)
            ThisGameLifelines_ = value.ToUpper
            Try
                Dim Lifelines() As String = ThisGameLifelines_.Split(";")
                FiftyFiftyPosition = Array.IndexOf(Lifelines, "5050") + 1
                PhoneAFriendPosition = Array.IndexOf(Lifelines, "PAF") + 1
                AskTheAudiencePosition = Array.IndexOf(Lifelines, "ATA") + 1
                SwitchTheQuestionPosition = Array.IndexOf(Lifelines, "STQ") + 1
                DoubleDipPosition = Array.IndexOf(Lifelines, "DDIP") + 1
                AskTheHostPosition = Array.IndexOf(Lifelines, "ATH") + 1
                AskTheExpertPosition = Array.IndexOf(Lifelines, "ATE") + 1
            Catch ex As Exception
            End Try
        End Set
    End Property

End Class
