Public Interface IMainGameLevel
    Inherits ILevel
    Property PreviewValueSume As String
    Property RealValueSume As Decimal
    Property SafeHeaven As Boolean
    Function SetSafeHeaven(IsSafeHeaven As Boolean) As Boolean
End Interface