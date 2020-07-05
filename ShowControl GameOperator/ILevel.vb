Public Interface ILevel
    Property PositionLevel As Short
    Property Difficulty As Short
    Function SetLevel(Level As Short) As Short
    Function SetDifficulty(Difficulty As Short) As Short
    Property PreviewValueSume As String
    Property RealValueSume As Decimal
    Property SafeHeaven As Boolean
    Function SetSafeHeaven(IsSafeHeaven As Boolean) As Boolean
End Interface
