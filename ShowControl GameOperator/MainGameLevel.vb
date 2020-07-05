Public Class MainGameLevel
    Implements ILevel

    Public Sub New(PositionLevel As Short, PreviewValueSume As String, RealValueSume As Decimal, SafeHeaven As String)
        Me.PositionLevel = PositionLevel
        Me.PreviewValueSume = PreviewValueSume
        Me.RealValueSume = RealValueSume
        Select Case SafeHeaven
            Case 1
                Me.SafeHeaven = True
            Case 0
                Me.SafeHeaven = False
            Case Else
                Me.SafeHeaven = False
        End Select
    End Sub

    Public Property PositionLevel As Short Implements ILevel.PositionLevel
    Public Property Difficulty As Short Implements ILevel.Difficulty
    Public Property PreviewValueSume As String Implements ILevel.PreviewValueSume
    Public Property RealValueSume As Decimal Implements ILevel.RealValueSume
    Public Property SafeHeaven As Boolean Implements ILevel.SafeHeaven
    Private Function SetLevel(Level As Short) As Short Implements ILevel.SetLevel
        If Level < 0 OrElse Level > 15 Then
            Me.PositionLevel = Level
            SetDifficulty(Level)
        End If
    End Function
    Private Function SetDifficulty(Level As Short) As Short Implements ILevel.SetDifficulty
        Throw New NotImplementedException
    End Function
    Private Function SetSafeHeaven(IsSafeHeaven As Boolean) As Boolean Implements ILevel.SetSafeHeaven
        Me.SafeHeaven = SafeHeaven
    End Function
End Class
