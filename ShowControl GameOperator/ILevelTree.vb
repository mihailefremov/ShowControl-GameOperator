Public Interface ILevelTree
    Inherits ICollection(Of ILevel)
    Property CurrentLevel As Short
    Sub GoToNextLevel()
    Function GetLevel() As ILevel
    Function GetMainGameLevel() As IMainGameLevel
End Interface
