Public Class LevelTree
    Implements ILevelTree

    Private Levels As IList(Of ILevel)
    Private ReadOnly Property TopLevelPosition As Short
        Get
            Return Levels.Count
        End Get
    End Property
    Public Property CurrentLevel As Short = 0 Implements ILevelTree.CurrentLevel

    Public Sub New()
        If Levels Is Nothing Then
            Levels = New List(Of ILevel)
        End If
    End Sub

    Public ReadOnly Property Count As Integer Implements ILevelTree.Count
        Get
            Return Levels.Count
        End Get
    End Property

    Public ReadOnly Property IsReadOnly As Boolean Implements ILevelTree.IsReadOnly
        Get
            Return Levels.IsReadOnly
        End Get
    End Property

    Private Sub Add(item As ILevel) Implements ICollection(Of ILevel).Add
        DirectCast(Levels, ICollection(Of ILevel)).Add(item)
    End Sub

    Public Sub Clear() Implements ILevelTree.Clear
        DirectCast(Levels, ICollection(Of ILevel)).Clear()
    End Sub

    Public Function Contains(item As ILevel) As Boolean Implements ILevelTree.Contains
        Return Levels.Contains(item)
    End Function

    Public Sub CopyTo(array() As ILevel, arrayIndex As Integer) Implements ILevelTree.CopyTo
        DirectCast(Levels, ICollection(Of ILevel)).CopyTo(array, arrayIndex)
    End Sub

    Public Function Remove(item As ILevel) As Boolean Implements ILevelTree.Remove
        Return Levels.Remove(item)
    End Function

    Public Sub GoToNextLevel() Implements ILevelTree.GoToNextLevel
        If CurrentLevel = TopLevelPosition Then
            CurrentLevel = 0
        Else
            CurrentLevel += 1
        End If
    End Sub

    Public Function GetLevel() As ILevel Implements ILevelTree.GetLevel
        If Levels.Count > 0 Then Return Nothing
        If TypeOf Levels.ElementAt(CurrentLevel) Is ILevel Then
            Dim CurrentMainGameLevel As ILevel = Levels.ElementAt(CurrentLevel)
            Return CurrentMainGameLevel
        End If
        Return Nothing
    End Function

    Public Function GetMainGameLevel() As IMainGameLevel Implements ILevelTree.GetMainGameLevel
        If Levels.Count > 0 Then Return Nothing
        If TypeOf Levels.ElementAt(CurrentLevel) Is IMainGameLevel Then
            Dim CurrentMainGameLevel As IMainGameLevel = Levels.ElementAt(CurrentLevel)
            Return CurrentMainGameLevel
        End If
        Return Nothing
    End Function

    Public Function GetEnumerator() As IEnumerator(Of ILevel) Implements ILevelTree.GetEnumerator
        Return Levels.GetEnumerator()
    End Function

    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return DirectCast(Levels, IEnumerable).GetEnumerator()
    End Function

End Class
