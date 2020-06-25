Public Class GamePlayContext

#Region "PROPERTIЕS"
    Private _MomentStatus As String = ""
    Public Property MomentStatus() As String
        Get
            Return _MomentStatus
        End Get
        Set(value As String)
            _MomentStatus = value
            HostContPresentationLayer.GamePlayStateSet(value)
            GuiContext.MomentStatus(value)
            UpdateView()
        End Set
    End Property

    Private _question As New Question
    Public Property Question As Question
        Get
            Return _question
        End Get
        Set(value As Question)
            _question = value
            UpdateView()
        End Set
    End Property

    Private _lifelines As New Lifelines
    Public Property Lifelines As Lifelines
        Get
            Return _lifelines
        End Get
        Set(value As Lifelines)
            _lifelines = value
            UpdateView()
        End Set
    End Property

    Private _currentGamePlayLevels As ILevelTree = New LevelTree
    Public Property CurrentGamePlayLevels As ILevelTree
        Get
            Return _currentGamePlayLevels
        End Get
        Set(value As ILevelTree)
            _currentGamePlayLevels = value
            UpdateView()
        End Set
    End Property

    Private doubleDipState1 As String = ""
    Public Property DoubleDipState As String
        Get
            Return doubleDipState1
        End Get
        Set(value As String)
            doubleDipState1 = value
            GraphicsProcessingUnit.MarkCGlifelines(Lifelines.Lifeline1Active, Lifelines.Lifeline2Active, Lifelines.Lifeline3Active, Lifelines.Lifeline4Active)
            UpdateView()
        End Set
    End Property

    Private doubleDipFirstAnswer1 As String = ""
    Public Property DoubleDipFirstAnswer As String
        Get
            Return doubleDipFirstAnswer1
        End Get
        Set(value As String)
            doubleDipFirstAnswer1 = value
            UpdateView()
        End Set
    End Property

    Private CurentGameStatusData1 As String = ""
    Public Property CurentGameStatusData As String
        Get
            Return CurentGameStatusData1
        End Get
        Set(value As String)
            CurentGameStatusData1 = value
            HostContPresentationLayer.GamePlayStateSet("")
            UpdateView()
        End Set
    End Property

    Private FiftyFifty1 As String = ""
    Public Property FiftyFifty As String
        Get
            Return FiftyFifty1
        End Get
        Set(value As String)
            FiftyFifty1 = value
            UpdateView()
        End Set
    End Property

    Private AtaVotes1 As String = ""
    Public Property AtaVotes As String
        Get
            Return AtaVotes1
        End Get
        Set(value As String)
            AtaVotes1 = value
            UpdateView()
        End Set
    End Property

    Private QuestionForSume1 As String = ""
    Public Property QuestionForSume As String
        Get
            Return QuestionForSume1
        End Get
        Set(value As String)
            QuestionForSume1 = value
            UpdateView()
        End Set
    End Property

    Private _answerMarks As String = "ABCD"
    Public Property AnswerMarks As String
        Get
            Return _answerMarks
        End Get
        Set(value As String)
            _answerMarks = value
            UpdateView()
        End Set
    End Property


#End Region

#Region "OBSEERVER"

    Private s As GamePlayContextSubject = New GamePlayContextSubject()
    Private _gpx As GamePlayContext = Me

    Private StateId As Long = 0

    Public Property Gpx As GamePlayContext
        Get
            Return _gpx
        End Get
        Set(value As GamePlayContext)
            _gpx = value
        End Set
    End Property

    Public Sub UpdateView()
        ''Update Subject
        s.SubjectState = Gpx
        s.Notify()
    End Sub

    Public Sub Run()

        s.Attach(New ConcreteObserver(s, "X"))
        s.Attach(New ConcreteObserver(s, "Y"))
        s.Attach(New ConcreteObserver(s, "Z"))

        ''Update Subject
        s.SubjectState = Gpx
        s.Notify()

    End Sub

    MustInherit Class Subject
        Private _observers As List(Of Observer) = New List(Of Observer)()

        Public Sub Attach(ByVal observer As Observer)
            _observers.Add(observer)
        End Sub

        Public Sub Detach(ByVal observer As Observer)
            _observers.Remove(observer)
        End Sub

        Public Sub Notify()
            For Each o As Observer In _observers
                o.Update()
            Next
        End Sub
    End Class

    Class GamePlayContextSubject
        Inherits Subject

        Private _subjectState As GamePlayContext

        Public Property SubjectState As GamePlayContext
            Get
                Return _subjectState
            End Get
            Set(ByVal value As GamePlayContext)
                _subjectState = value
            End Set
        End Property
    End Class

    MustInherit Class Observer
        Public MustOverride Sub Update()
    End Class

    Class ConcreteObserver
        Inherits Observer

        Private _name As String
        Private _observerState As GamePlayContext
        Private _subject As GamePlayContextSubject

        Public Sub New(ByVal subject As GamePlayContextSubject, ByVal name As String)
            Me._subject = subject
            Me._name = name
        End Sub

        Public Overrides Sub Update()
            _observerState = _subject.SubjectState
            MessageBox.Show(_observerState.MomentStatus + " " + _name)
        End Sub

        Public Property Subject As GamePlayContextSubject
            Get
                Return _subject
            End Get
            Set(ByVal value As GamePlayContextSubject)
                _subject = value
            End Set
        End Property
    End Class


#End Region

End Class
