Public Class Question

    Public Sub New()
    End Sub

    Public Sub New(row As DataTable)
        With row
            ID = .Rows(0)("QuestionID")
            QuestionText = .Rows(0)("Question").ToString().Replace("|", vbCrLf)
            Answer1Text = .Rows(0)("Answer1").ToString()
            Answer2Text = .Rows(0)("Answer2").ToString()
            Answer3Text = .Rows(0)("Answer3").ToString()
            Answer4Text = .Rows(0)("Answer4").ToString()
            CorrectAnswer = .Rows(0)("CorrectAnswer").ToString()
            Explanation = .Rows(0)("MoreInformation").ToString()
            Pronunciation = .Rows(0)("Pronunciation").ToString()
        End With
    End Sub

    Private _levelQ As String
    Public Property LevelQ As String
        Get
            If String.IsNullOrWhiteSpace(_levelQ) Then
                Return "0"
            End If
            Return _levelQ
        End Get
        Set(value As String)
            _levelQ = value
        End Set
    End Property

    Public ID As String

    Dim QuestionText1 As String = ""
    Public Property QuestionText As String
        Get
            Return QuestionText1
        End Get
        Set(value As String)
            QuestionText1 = value
        End Set
    End Property

    Dim Answer1Text1 As String = ""
    Public Property Answer1Text As String
        Get
            Return Answer1Text1
        End Get
        Set(value As String)
            Answer1Text1 = value
        End Set
    End Property

    Dim Answer2Text1 As String = ""
    Public Property Answer2Text As String
        Get
            Return Answer2Text1
        End Get
        Set(value As String)
            Answer2Text1 = value
        End Set
    End Property

    Dim Answer3Text1 As String = ""
    Public Property Answer3Text As String
        Get
            Return Answer3Text1
        End Get
        Set(value As String)
            Answer3Text1 = value
        End Set
    End Property

    Dim Answer4Text1 As String = ""
    Public Property Answer4Text As String
        Get
            Return Answer4Text1
        End Get
        Set(value As String)
            Answer4Text1 = value
        End Set
    End Property

    Dim FinalAnswer1 As Short = 0
    Public Property FinalAnswer As Short
        Get
            Return FinalAnswer1
        End Get
        Set(value As Short)
            FinalAnswer1 = value
        End Set
    End Property

    Public ReadOnly Property FinalAnswerLetter As String
        Get
            Return Helpers.Convert1234ToABCD(FinalAnswer)
        End Get
    End Property

    Dim CorrectAnswer1 As Short = 0
    Public Property CorrectAnswer As Short
        Get
            Return CorrectAnswer1
        End Get
        Set(value As Short)
            CorrectAnswer1 = value
        End Set
    End Property

    Public ReadOnly Property CorrectAnswerLetter As String
        Get
            Return Helpers.Convert1234ToABCD(CorrectAnswer)
        End Get
    End Property

    Dim Explanation1 As String = ""
    Public Property Explanation As String
        Get
            Return Explanation1
        End Get
        Set(value As String)
            Explanation1 = value
        End Set
    End Property

    Dim Pronunciation1 As String = ""
    Public Property Pronunciation As String
        Get
            Return Pronunciation1
        End Get
        Set(value As String)
            Pronunciation1 = value
        End Set
    End Property

    Public Function IsGivenFinalAnswerCorrect() As GivenAnswerStateEnum
        If FinalAnswer + CorrectAnswer < 0 Then
            Return GivenAnswerStateEnum.Inconclusive
        ElseIf FinalAnswer = CorrectAnswer Then
            Return GivenAnswerStateEnum.Correct
        Else
            Return GivenAnswerStateEnum.Incorrect
        End If
    End Function

    Public Sub RandomizeAnswers()

        Try
            Dim QuestionAnswers As String() = {Answer1Text, Answer2Text, Answer3Text, Answer4Text}
            Dim CorrectAnswerNormal As String = QuestionAnswers.ElementAt(CorrectAnswer - 1)

            Dim r As Random = New Random()
            Dim QuestionAnswersRandom As String() = {Answer1Text, Answer2Text, Answer3Text, Answer4Text}
            QuestionAnswersRandom = QuestionAnswersRandom.OrderBy(Function(x) r.[Next]()).ToArray()

            Dim CorrectAnswerAfterRandom As Integer = Array.IndexOf(QuestionAnswersRandom, CorrectAnswerNormal)

            Answer1Text = QuestionAnswersRandom.ElementAt(0)
            Answer2Text = QuestionAnswersRandom.ElementAt(1)
            Answer3Text = QuestionAnswersRandom.ElementAt(2)
            Answer4Text = QuestionAnswersRandom.ElementAt(3)
            CorrectAnswer = CorrectAnswerAfterRandom + 1
        Catch
        End Try

    End Sub

End Class

Public Enum GivenAnswerStateEnum
    Correct = 1
    Incorrect = 2
    Inconclusive = 3
End Enum