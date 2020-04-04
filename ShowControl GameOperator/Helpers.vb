Public Class Helpers

    Public Shared Function Convert1234ToABCD(answerNumber As Object) As String
        Try
            Select Case answerNumber
                Case 1
                    Return "A"
                Case 2
                    Return "B"
                Case 3
                    Return "C"
                Case 4
                    Return "D"
                Case Else
                    Return UCase(answerNumber.ToString)
            End Select
        Catch ex As Exception
            Return answerNumber.ToString
        End Try
    End Function

    Public Shared Function ConvertABCDTo1234(answerText As String) As Integer
        Try
            Select Case UCase(answerText.Trim)
                Case "A"
                    Return 1
                Case "B"
                    Return 2
                Case "C"
                    Return 3
                Case "D"
                    Return 4
                Case Else
                    If IsNumeric(answerText) Then
                        Return Int(answerText)
                    End If
                    Return 0
            End Select
        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Shared Function ConvertLifelineStateToReadable(lifelineState As Short) As String
        Try
            Select Case lifelineState
                Case 0
                    Return "USED"
                Case 1
                    Return "UNUSED"
                Case 2
                    Return "INUSE"
                Case -1
                    Return "DISABLED"
                Case Else
                    Return "UNDEFINED"
            End Select
        Catch ex As Exception
            Return "UNDEFINED"
        End Try

    End Function
    Public Shared Function Base64Encode(ByVal plainText As String) As String
        Dim plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText)
        Return System.Convert.ToBase64String(plainTextBytes)
    End Function

    Public Shared Function RemoveLetters(s As String) As Integer
        Dim rxNonDigits As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex("[^\d]+")

        If String.IsNullOrEmpty(s) Then Return s
        Dim cleaned As String = rxNonDigits.Replace(s, "")
        Return Integer.Parse(cleaned)
    End Function

End Class
