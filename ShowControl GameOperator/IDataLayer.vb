Public Interface IDataLayer
    Sub DisposeAnsweredGameQuestionsDB(QuestionType As String)
    Sub DisposeATAvoteData()
    Sub MarkQuestionAnsweredDB(questionID As String, IsGameGoingLive As Boolean)
    Sub MarkQuestionFiredDB(questionID As String, IsGameGoingLive As Boolean, Optional qtype As String = "1")
    Function GetATAvoteData() As String()
    Function getContGuestVoteData() As String
    Function SelectSuitableQuestion(questionLevel As String, Optional typeQ As String = "1", Optional isReplacement As Boolean = False) As DataTable
End Interface
