Public Class Log
#Region "ERRORS"
    Public Shared Sub LogWrite(message As String)
        Try
            Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\ShowControl-QuizOperator-Logs"
            If Not IO.Directory.Exists(path) Then
                Dim di As IO.DirectoryInfo = IO.Directory.CreateDirectory(path)
            End If
            Dim file As String = path + "\QuizOperatorLog" + Date.Now.ToString("yyyy-MM-dd") + ".txt"

            Dim createText As String = String.Format(Date.Now.ToString("yyyy-MM-dd HH:mm:ss.ff") + " | {0} ", message)

            Using sw As IO.StreamWriter = New IO.StreamWriter(file, True)
                sw.WriteLine(createText)
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub LogWrite(message As String, filename As String)
        Try
            Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\ShowControl-QuizOperator-Logs"
            If Not IO.Directory.Exists(path) Then
                Dim di As IO.DirectoryInfo = IO.Directory.CreateDirectory(path)
            End If
            Dim file As String = path + "\" + filename + Date.Now.ToString("yyyy-MM-dd") + ".txt"

            Dim createText As String = String.Format(Date.Now.ToString("yyyy-MM-dd HH:mm:ss.ff") + " | {0} ", message)

            Using sw As IO.StreamWriter = New IO.StreamWriter(file, True)
                sw.WriteLine(createText)
            End Using
        Catch ex As Exception
        End Try
    End Sub

#End Region
End Class
