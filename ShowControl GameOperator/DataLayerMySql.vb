Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient


Public Class DataLayerMySql

    Public Shared audienceVotesConnection As String = "server=" + My.Settings.audienceVotesServer + ";" _
                   & "uid=" + My.Settings.mySqlUser + ";" _
                   & "pwd=" + My.Settings.mySqlPassword + ";" _
                   & "database=" + My.Settings.audienceVotesDatabase + ";"

    Private Shared ReadOnly ConnectionStringQuestionDB As String = audienceVotesConnection

    Private Shared ReadOnly ConnectionStringQuestionDBMSSQL As String = $"server={My.Settings.gameQuestionsServer};database={My.Settings.audienceVotesDatabase};integrated security=true;"

    Public Shared percentA, percentB, percentC, percentD As Decimal

    Private Shared _QAMySqlConnVar As New MySqlConnection With {
                .ConnectionString = ConnectionStringQuestionDB
            }
    'Public Shared ReadOnly Property QADbConnectionProperty As MySqlConnection
    '    Get
    '        Try
    '            If IsNothing(_QAMySqlConnVar) Then
    '                _QAMySqlConnVar = New MySqlConnection
    '                _QAMySqlConnVar.ConnectionString = ConnectionStringQuestionDB
    '                _QAMySqlConnVar.Open()
    '                Return _QAMySqlConnVar
    '            Else
    '                If _QAMySqlConnVar.State = ConnectionState.Open Then
    '                    Return _QAMySqlConnVar

    '                ElseIf _QAMySqlConnVar.State = ConnectionState.Closed OrElse _QAMySqlConnVar.State = ConnectionState.Broken Then
    '                    _QAMySqlConnVar = New MySqlConnection
    '                    _QAMySqlConnVar.ConnectionString = ConnectionStringQuestionDB
    '                    _QAMySqlConnVar.Open()
    '                    Return _QAMySqlConnVar
    '                Else
    '                    Return _QAMySqlConnVar
    '                End If
    '            End If
    '        Catch ex As Exception
    '        End Try
    '    End Get
    'End Property


#Region "QUESTION DATA"
    Friend Shared Function SelectSuitableQuestion(questionLevel As String, Optional typeQ As String = "1") As DataTable
        Dim MySqlConn As MySqlConnection
        MySqlConn = New MySqlConnection With {
                .ConnectionString = ConnectionStringQuestionDB
            }
        Dim dbDataSet As New DataTable

        Try
            MySqlConn.Open()
            Dim Query As String
            Query = "select QuestionID, Question, Answer1, Answer2, Answer3, Answer4, 
                        CorrectAnswer, MoreInformation, Pronunciation
                        from questionsforcontestant where Type=" + typeQ + " and Level=" + questionLevel
            Dim COMM As MySqlCommand
            COMM = New MySqlCommand(Query, MySqlConn)
            Dim SDA As New MySqlDataAdapter With {
                    .SelectCommand = COMM
                }
            SDA.Fill(dbDataSet)
            MySqlConn.Close()

        Catch ex As Exception
            'ErrorLog_Textbox.Text += ex.Message + vbCrLf
        Finally
            MySqlConn.Dispose()
        End Try

        Return dbDataSet
    End Function

    'Friend Shared Function SelectSuitableQuestion(questionLevel As String, Optional typeQ As String = "1") As DataTable
    '    Dim MySqlConn As MySqlConnection
    '    MySqlConn = QADbConnectionProperty
    '    Dim dbDataSet As New DataTable
    '    Try
    '        'MySqlConn.Open()
    '        Dim Query As String
    '        Query = "select QuestionID, Question, Answer1, Answer2, Answer3, Answer4, 
    '                    CorrectAnswer, MoreInformation, Pronunciation
    '                    from questionsforcontestant where Type=" + typeQ + " and Level=" + questionLevel
    '        Dim COMM As MySqlCommand
    '        COMM = New MySqlCommand(Query, MySqlConn)
    '        Dim SDA As New MySqlDataAdapter With {
    '                .SelectCommand = COMM
    '            }
    '        SDA.Fill(dbDataSet)
    '        'MySqlConn.Close()
    '    Catch ex As Exception
    '        'ErrorLog_Textbox.Text += ex.Message + vbCrLf
    '    Finally
    '        'MySqlConn.Dispose()
    '    End Try

    '    Return dbDataSet
    'End Function

    Friend Shared Sub MarkQuestionAnsweredDB(questionID As String, IsGameGoingLive As Boolean)
        If Not IsGameGoingLive Then Return

        Dim MySqlConn As MySqlConnection
        MySqlConn = New MySqlConnection
        MySqlConn.ConnectionString = ConnectionStringQuestionDB
        Dim READER As MySqlDataReader

        Try
            MySqlConn.Open()
            Dim Query As String
            Query = "update questionsforcontestant SET Answered = 1 where QuestionID='" & questionID & "'"
            Dim COMM As MySqlCommand
            COMM = New MySqlCommand(Query, MySqlConn)
            READER = COMM.ExecuteReader

            READER.Close()
            READER.Dispose()
            MySqlConn.Close()

        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        Finally
            MySqlConn.Dispose()
        End Try

        'Paralelno mssql
        'Try
        '    Using con As New SqlConnection(ConnectionStringQuestionDBMSSQL)
        '        Using cmd As New SqlCommand("update questionsforcontestant SET Answered='1' where QuestionID=@QID", con)
        '            cmd.Parameters.Add("@QID", SqlDbType.VarChar).Value = questionID
        '            con.Open()
        '            cmd.ExecuteNonQuery()
        '        End Using
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

    End Sub

    'Friend Shared Sub MarkQuestionAnsweredDB(questionID As String)
    '    Dim MySqlConn As MySqlConnection = QADbConnectionProperty
    '    Dim READER As MySqlDataReader
    '    Try
    '        'MySqlConn.Open()
    '        Dim Query As String
    '        Query = "update questionsforcontestant SET Answered = 1 where QuestionID='" & questionID & "'"
    '        Dim COMM As MySqlCommand
    '        COMM = New MySqlCommand(Query, MySqlConn)
    '        READER = COMM.ExecuteReader

    '        READER.Close()
    '        READER.Dispose()
    '        'MySqlConn.Close()

    '    Catch ex As Exception
    '        'MessageBox.Show(ex.Message)
    '    Finally
    '        'MySqlConn.Dispose()
    '    End Try

    'End Sub

    Friend Shared Sub MarkQuestionFiredDB(questionID As String, IsGameGoingLive As Boolean, Optional qtype As String = "1")
        If Not IsGameGoingLive Then Return

        Dim MySqlConn As MySqlConnection
        MySqlConn = New MySqlConnection
        MySqlConn.ConnectionString = ConnectionStringQuestionDB
        Dim READER As MySqlDataReader

        Try
            MySqlConn.Open()
            Dim Query As String     ''update gamequestions SET TimesAnswered = TimesAnswered + 1 where ID=
            Dim qTable As String = "gamequestions"
            Query = $"update {qTable} SET TimesAnswered = '1', 
                     LastDateAnswered='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' where QuestionID='{questionID}'"
            Dim COMM As MySqlCommand
            COMM = New MySqlCommand(Query, MySqlConn)
            READER = COMM.ExecuteReader

            READER.Close()
            READER.Dispose()
            MySqlConn.Close()

        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        Finally
            MySqlConn.Dispose()
        End Try

        'Paralelno mssql
        Try
            Using con As New SqlConnection(ConnectionStringQuestionDBMSSQL)
                Using cmd As New SqlCommand("update gamequestions SET TimesAnswered='1', LastDateAnswered=@LDate where QuestionID=@QID", con)
                    cmd.Parameters.Add("@QID", SqlDbType.VarChar).Value = questionID
                    cmd.Parameters.Add("@LDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    'Friend Shared Sub MarkQuestionFiredDB(questionID As String, Optional qtype As String = "1")
    '    Dim MySqlConn As MySqlConnection
    '    MySqlConn = QADbConnectionProperty
    '    Dim READER As MySqlDataReader

    '    Try
    '        Dim Query As String     ''update gamequestions SET TimesAnswered = TimesAnswered + 1 where ID=
    '        Dim qTable As String = "gamequestions"
    '        Query = $"update {qTable} SET TimesAnswered = '1', 
    '                 LastDateAnswered='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' where QuestionID='{questionID}'"
    '        Dim COMM As MySqlCommand
    '        COMM = New MySqlCommand(Query, MySqlConn)
    '        READER = COMM.ExecuteReader

    '        READER.Close()
    '        READER.Dispose()

    '    Catch ex As Exception
    '        'MessageBox.Show(ex.Message)
    '    Finally
    '        'MySqlConn.Dispose()
    '    End Try

    'End Sub

    Shared Sub DisposeAnsweredGameQuestionsDB(QuestionType As String)
        Dim conn As New MySql.Data.MySqlClient.MySqlConnection
        Dim myConnectionString As String = ConnectionStringQuestionDB
        Dim cmd As New MySqlCommand
        Dim td As New DataTable

        Try
            conn.ConnectionString = myConnectionString
            conn.Open()
            cmd.CommandText = "delete from " + "wwtbam.questionsforcontestant" + " where Answered=1 and Type=" + QuestionType
            cmd.Connection = conn
            cmd.ExecuteNonQuery()

            conn.Close()
            conn.Dispose()

        Catch ex As MySql.Data.MySqlClient.MySqlException
            'MessageBox.Show(ex.Message)

            conn.Close()
            conn.Dispose()
        End Try

        'Paralelno mssql
        Try
            Using con As New SqlConnection(ConnectionStringQuestionDBMSSQL)
                Using cmd1 As New SqlCommand("delete from " + "questionsforcontestant" + " where Answered='1' and Type=" + QuestionType, con)
                    con.Open()
                    cmd1.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
        End Try

    End Sub

    'Shared Sub DisposeAnsweredGameQuestionsDB(QuestionType As String)
    '    Dim conn As MySql.Data.MySqlClient.MySqlConnection = QADbConnectionProperty
    '    Dim myConnectionString As String = ConnectionStringQuestionDB
    '    Dim cmd As New MySqlCommand
    '    Dim td As New DataTable

    '    Try
    '        conn.ConnectionString = myConnectionString
    '        cmd.CommandText = "delete from " + "wwtbam.questionsforcontestant" + " where Answered=1 and Type=" + QuestionType
    '        cmd.Connection = conn
    '        cmd.ExecuteNonQuery()

    '    Catch ex As MySql.Data.MySqlClient.MySqlException
    '        'MessageBox.Show(ex.Message)

    '    End Try

    'End Sub
#End Region

#Region "AUDIENCE DATA"
    Friend Shared Function GetATAvoteData() As String()
        Dim conn As New MySql.Data.MySqlClient.MySqlConnection
        Dim myConnectionString As String = audienceVotesConnection
        Dim cmda, cmdb, cmdc, cmdd As New MySqlCommand
        Dim rda, rdb, rdc, rdd As MySqlDataReader

        Try
            conn.ConnectionString = myConnectionString
            conn.Open()
            'MsgBox("You are now connected")

            cmda.CommandText = "SELECT COUNT(GivenAnswer) AS '1' FROM " + My.Settings.audienceVotesDatabase + "." + My.Settings.audienceVotesTable + " where GivenAnswer=1"
            cmda.Connection = conn
            Dim countA = cmda.ExecuteScalar

            cmdb.CommandText = "SELECT COUNT(GivenAnswer) AS '2' FROM " + My.Settings.audienceVotesDatabase + "." + My.Settings.audienceVotesTable + " where GivenAnswer=2"
            cmdb.Connection = conn
            Dim countB = cmdb.ExecuteScalar

            cmdc.CommandText = "SELECT COUNT(GivenAnswer) AS '3' FROM " + My.Settings.audienceVotesDatabase + "." + My.Settings.audienceVotesTable + " where GivenAnswer=3"
            cmdc.Connection = conn
            Dim countC = cmdc.ExecuteScalar

            cmdd.CommandText = "SELECT COUNT(GivenAnswer) AS '4' FROM " + My.Settings.audienceVotesDatabase + "." + My.Settings.audienceVotesTable + " where GivenAnswer=4"
            cmdd.Connection = conn
            Dim countD = cmdd.ExecuteScalar

            'cmdd.Connection = conn
            'rdd = cmdd.ExecuteReader
            'Dim countD = GetCount(rdd)
            'rdd.Close()
            'rdd.Dispose()

            Dim countTotal As Integer = countA + countB + countC + countD

            If countTotal <> 0 Then
                percentA = countA / countTotal * 100
                percentB = countB / countTotal * 100
                percentC = countC / countTotal * 100
                percentD = countD / countTotal * 100
                percentA = Math.Round(percentA)
                percentB = Math.Round(percentB)
                percentC = Math.Round(percentC)
                percentD = Math.Round(percentD)
            End If

            conn.Close()
            conn.Dispose()

        Catch ex As MySql.Data.MySqlClient.MySqlException
            'MessageBox.Show(ex.Message)

        End Try

        Return {percentA, percentB, percentC, percentD}

    End Function

    Shared Sub DisposeATAvoteData()
        Dim conn As New MySql.Data.MySqlClient.MySqlConnection
        Dim myConnectionString As String = audienceVotesConnection
        Dim cmd As New MySqlCommand
        Dim td As New DataTable

        Try
            conn.ConnectionString = myConnectionString
            conn.Open()
            cmd.CommandText = "delete from " + My.Settings.audienceVotesDatabase + "." + My.Settings.audienceVotesTable
            cmd.Connection = conn
            cmd.ExecuteNonQuery()

            conn.Close()
            conn.Dispose()

        Catch ex As MySql.Data.MySqlClient.MySqlException
            'MessageBox.Show(ex.Message)

            conn.Close()
            conn.Dispose()
        End Try

        percentA = 0
        percentB = 0
        percentC = 0
        percentD = 0
    End Sub

    Shared Function getContGuestVoteData() As String
        Dim conn As New MySql.Data.MySqlClient.MySqlConnection
        Dim myConnectionString As String = audienceVotesConnection
        Dim cmda As New MySqlCommand
        Dim rda As MySqlDataReader
        Dim retrieve As String = ""

        Try
            conn.ConnectionString = myConnectionString
            conn.Open()

            cmda.CommandText = "Select GivenAnswer from " + My.Settings.audienceVotesDatabase + "." + My.Settings.audienceVotesTable + " where username='cg1'"
            cmda.Connection = conn

            rda = cmda.ExecuteReader
            If rda.Read() Then
                retrieve = rda("GivenAnswer").ToString
            End If

            rda.Close()
            rda.Dispose()

            conn.Close()
            conn.Dispose()

        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message)

            conn.Close()
            conn.Dispose()
        End Try

        Return ConvertToABCD(retrieve)
    End Function

    Shared Function ConvertToABCD(ByVal P As String) As String

        If P = "1" Then
            Return "A"

        ElseIf P = "2" Then
            Return "B"

        ElseIf P = "3" Then
            Return "C"

        ElseIf P = "4" Then
            Return "D"

        Else
            Return ""

        End If

    End Function

#End Region

End Class
