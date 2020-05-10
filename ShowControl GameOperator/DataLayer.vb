Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class DataLayer

    Private Shared audienceVotesConnString As String = $"server={My.Settings.gameQuestionsServer};database={My.Settings.audienceVotesDatabase};integrated security=true;"

    Private Shared ReadOnly ConnectionStringQuestionDB As String = audienceVotesConnString
    Private Shared ReadOnly ConnectionStringQuestionDBMSSQL As String = $"server={My.Settings.gameQuestionsServer};database={My.Settings.audienceVotesDatabase};integrated security=true;"

    Private Shared _QASqlConnVar As New SqlConnection With {
                .ConnectionString = ConnectionStringQuestionDB
            }

#Region "QUESTION DATA"
    Friend Shared Function SelectSuitableQuestion(questionLevel As String, Optional typeQ As String = "1", Optional isReplacement As Boolean = False) As DataTable
        Dim dbDataSet As New DataTable
        Try
            Using con As New SqlConnection(ConnectionStringQuestionDBMSSQL)
                Using cmd As New SqlCommand("[dbo].[proc_GetLiveStackQuestion]", con)
                    cmd.Connection.Open()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add(New SqlParameter("@Level", questionLevel))
                    cmd.Parameters.Add(New SqlParameter("@Type", typeQ))
                    cmd.Parameters.Add(New SqlParameter("@IsReplacement", IIf(isReplacement, 1, 0)))
                    Using rdr As New SqlDataAdapter(cmd)
                        rdr.Fill(dbDataSet)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try

        Return dbDataSet

    End Function

    Friend Shared Sub MarkQuestionAnsweredDB(questionID As String, IsGameGoingLive As Boolean)
        Return
    End Sub

    Friend Shared Sub MarkQuestionFiredDB(questionID As String, IsGameGoingLive As Boolean, Optional qtype As String = "1")
        If Not IsGameGoingLive Then Return

        'Paralelno mssql
        Try
            Using con As New SqlConnection(ConnectionStringQuestionDBMSSQL)
                Using cmd As New SqlCommand("update gamequestions SET TimesAnswered='1', LastDateAnswered=@LDate where QuestionID=@QID", con)
                    cmd.Connection.Open()
                    cmd.Parameters.Add("@QID", SqlDbType.VarChar).Value = questionID
                    cmd.Parameters.Add("@LDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Shared Sub DisposeAnsweredGameQuestionsDB(QuestionType As String)
        Return
    End Sub

#End Region

#Region "AUDIENCE DATA"
    Friend Shared Function GetATAvoteData() As String()

        If My.Settings.UseMySqlForAta Then
            Return GetATAvoteDataMySql()
            'not ready yet because of voting application
        End If

        Dim conn As New SqlConnection
        Dim myConnectionString As String = audienceVotesConnString
        Dim cmda, cmdb, cmdc, cmdd As New SqlCommand
        Dim percentA, percentB, percentC, percentD As Decimal

        Try
            conn.ConnectionString = myConnectionString
            conn.Open()
            'MsgBox("You are now connected")

            cmda.CommandText = "SELECT COUNT(GivenAnswer) AS '1' FROM " + My.Settings.audienceVotesTable + " where GivenAnswer=1"
            cmda.Connection = conn
            Dim countA = cmda.ExecuteScalar

            cmdb.CommandText = "SELECT COUNT(GivenAnswer) AS '2' FROM " + My.Settings.audienceVotesTable + " where GivenAnswer=2"
            cmdb.Connection = conn
            Dim countB = cmdb.ExecuteScalar

            cmdc.CommandText = "SELECT COUNT(GivenAnswer) AS '3' FROM " + My.Settings.audienceVotesTable + " where GivenAnswer=3"
            cmdc.Connection = conn
            Dim countC = cmdc.ExecuteScalar

            cmdd.CommandText = "SELECT COUNT(GivenAnswer) AS '4' FROM " + My.Settings.audienceVotesTable + " where GivenAnswer=4"
            cmdd.Connection = conn
            Dim countD = cmdd.ExecuteScalar

            Dim countTotal As Integer = countA + countB + countC + countD

            If countTotal <> 0 Then
                percentA = Math.Round(countA / countTotal * 100)
                percentB = Math.Round(countB / countTotal * 100)
                percentC = Math.Round(countC / countTotal * 100)
                percentD = Math.Round(countD / countTotal * 100)
            End If

            conn.Close()
            conn.Dispose()

        Catch ex As SqlException
            Throw ex
        End Try

        Return {percentA, percentB, percentC, percentD}

    End Function

    Shared Sub DisposeATAvoteData()

        If My.Settings.UseMySqlForAta Then
            DisposeATAvoteDataMySql()
            Return
        End If
        'not ready yet because of voting application

        Try
            Using con As New SqlConnection(ConnectionStringQuestionDBMSSQL)
                Using cmd As New SqlCommand("delete from " + My.Settings.audienceVotesTable, con)
                    cmd.Connection.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#Region "MYSQL-ATA"
    Private Shared ReadOnly Property audienceVotesConnectionMySql As String
        Get
            Return "server=" + My.Settings.audienceVotesServer + ";" _
                   & "uid=" + My.Settings.mySqlUser + ";" _
                   & "pwd=" + My.Settings.mySqlPassword + ";" _
                   & "database=" + My.Settings.audienceVotesDatabase + ";"
        End Get
    End Property
    Private Shared Function GetATAvoteDataMySql() As String()
        Dim conn As New MySql.Data.MySqlClient.MySqlConnection
        Dim myConnectionString As String = audienceVotesConnectionMySql
        Dim cmda, cmdb, cmdc, cmdd As New MySqlCommand
        Dim percentA, percentB, percentC, percentD As Decimal

        Try
            conn.ConnectionString = myConnectionString
            conn.Open()

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

            Dim countTotal As Integer = countA + countB + countC + countD

            If countTotal <> 0 Then
                percentA = Math.Round(countA / countTotal * 100)
                percentB = Math.Round(countB / countTotal * 100)
                percentC = Math.Round(countC / countTotal * 100)
                percentD = Math.Round(countD / countTotal * 100)
            End If

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Throw
        End Try

        Return {percentA, percentB, percentC, percentD}

    End Function
    Private Shared Sub DisposeATAvoteDataMySql()
        Dim conn As New MySql.Data.MySqlClient.MySqlConnection
        Dim myConnectionString As String = audienceVotesConnectionMySql
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

        Catch ex As Exception
            Throw
        End Try

    End Sub
#End Region

    Shared Function getContGuestVoteData() As String
        'todo da se preraboti
        Dim conn As New SqlConnection
        Dim myConnectionString As String = audienceVotesConnString
        Dim cmda As New SqlCommand
        Dim rda As SqlDataReader
        Dim retrieve As String = ""

        Try
            conn.ConnectionString = myConnectionString
            conn.Open()

            cmda.CommandText = "Select GivenAnswer from " + My.Settings.audienceVotesTable + " where username='cg1'"
            cmda.Connection = conn

            rda = cmda.ExecuteReader
            If rda.Read() Then
                retrieve = rda("GivenAnswer").ToString
            End If

            rda.Close()
            rda.Dispose()

            conn.Close()
            conn.Dispose()

        Catch ex As SqlException
            Throw ex
        End Try

        Return ConvertToABCD(retrieve)
    End Function
    Private Shared Function ConvertToABCD(ByVal P As String) As String

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
