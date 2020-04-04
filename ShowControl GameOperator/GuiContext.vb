Public Class GuiContext

    Public Shared ReadOnly Property GuiLifelines As String()
        Get
            Dim lfl(5) As String
            lfl = (Quiz_Operator.Lifelines + ";;;;").Split(";")
            Return lfl
        End Get
    End Property

    Friend Shared Sub SomethingToDoWithLifeline(lifelinePosition As Short, action As Short)
        Select Case action
            Case 0
                XmarkLifeline(lifelinePosition)
            Case 1
                UnusedLifeline(lifelinePosition)
            Case 2
                InUsemarkLifeline(lifelinePosition)
        End Select
    End Sub

    Friend Shared Sub ResetAll()
        UnusedLifeline(1)
        UnusedLifeline(2)
        UnusedLifeline(3)
        UnusedLifeline(4)
    End Sub

    Friend Shared Sub UnusedLifeline(lifelinePosition As Integer)
        If lifelinePosition = 1 Then
            Quiz_Operator.Lifeline1_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(0).ToUpper}_0")
        ElseIf lifelinePosition = 2 Then
            Quiz_Operator.Lifeline2_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(1).ToUpper}_0")
        ElseIf lifelinePosition = 3 Then
            Quiz_Operator.Lifeline3_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(2).ToUpper}_0")
        ElseIf lifelinePosition = 4 Then
            Quiz_Operator.Lifeline4_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(3).ToUpper}_0")
        End If
    End Sub


    Friend Shared Sub InUsemarkLifeline(lifelinePosition As Integer)
        If lifelinePosition = 1 Then
            Quiz_Operator.Lifeline1_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(0).ToUpper}_1")
        ElseIf lifelinePosition = 2 Then
            Quiz_Operator.Lifeline2_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(1).ToUpper}_1")
        ElseIf lifelinePosition = 3 Then
            Quiz_Operator.Lifeline3_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(2).ToUpper}_1")
        ElseIf lifelinePosition = 4 Then
            Quiz_Operator.Lifeline4_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(3).ToUpper}_1")
        End If
    End Sub

    Friend Shared Sub XmarkLifeline(lifelinePosition As Integer)
        If lifelinePosition = 1 Then
            Quiz_Operator.Lifeline1_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(0).ToUpper}_X")
        ElseIf lifelinePosition = 2 Then
            Quiz_Operator.Lifeline2_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(1).ToUpper}_X")
        ElseIf lifelinePosition = 3 Then
            Quiz_Operator.Lifeline3_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(2).ToUpper}_X")
        ElseIf lifelinePosition = 4 Then
            Quiz_Operator.Lifeline4_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{GuiLifelines.ElementAt(3).ToUpper}_X")
        End If
    End Sub
End Class
