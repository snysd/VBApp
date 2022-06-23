Imports MySql.Data.MySqlClient
Imports System.Data

Public Class Form1
    Const DB_Source As String = "127.0.0.1"
    Const DB_Port As String = "3306"
    Const DB_Name As String = "testdb"
    Const DB_Id As String = "root"
    Const DB_Pw As String = "sql_pwd"
    Dim Conn As New MySqlConnection("Database=" + DB_Name _
                                        + ";Data Source=" + DB_Source _
                                        + ";Port=" + DB_Port _
                                        + ";User Id=" + DB_Id _
                                        + ";Password=" + DB_Pw _
                                        + ";sqlservermode=True;")
    Private Sub ButtonInsert_Click(sender As Object, e As EventArgs) Handles ButtonInsert.Click
        Dim name As String = Me.TextBoxName.Text
        Dim age As String = Me.TextBoxAge.Text
        Dim gender As String = Me.TextBoxGender.Text

        Dim query As String = $"INSERT INTO memberlist VALUES ('{name}','{gender}',{age})"
        Dim cmd As MySqlCommand = New MySqlCommand(query, Conn)
        cmd.ExecuteNonQuery()


    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Conn.Open()
    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Conn.Close()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Conn.Close()
    End Sub

    Private Sub ListBoxDB_MouseEnter(sender As Object, e As EventArgs)
        Conn.Close()
        Conn.Open()
        Dim query As String = "SELECT * FROM memberlist;"
        Dim cmd As MySqlCommand = New MySqlCommand(query, Conn)
        Dim dr As MySqlDataReader = cmd.ExecuteReader()
        Dim dt As DataTable = New DataTable()
        dt.Load(dr)
    End Sub

    Private Sub ButtonFileInsert_Click(sender As Object, e As EventArgs) Handles ButtonFileInsert.Click

    End Sub
End Class
