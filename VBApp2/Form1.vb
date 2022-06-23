Imports Microsoft.VisualBasic.FileIO
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
    Private Sub Insert_Query(name As String, gender As String, age As String)
        Dim query As String = $"INSERT INTO memberlist VALUES ('{name}','{gender}',{age})"
        Dim cmd As MySqlCommand = New MySqlCommand(query, Conn)
        cmd.ExecuteNonQuery()
    End Sub
    Private Sub ButtonInsert_Click(sender As Object, e As EventArgs) Handles ButtonInsert.Click
        Dim name As String = Me.TextBoxName.Text
        Dim age As String = Me.TextBoxAge.Text
        Dim gender As String = Me.TextBoxGender.Text
        Insert_Query(name, gender, age)
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
        'OpenFileDialogクラスのインスタンスを作成
        Dim ofd As New OpenFileDialog()

        'はじめのファイル名を指定する
        'はじめに「ファイル名」で表示される文字列を指定する
        ofd.FileName = "default.csv"
        'はじめに表示されるフォルダを指定する
        '指定しない（空の文字列）の時は、現在のディレクトリが表示される
        ofd.InitialDirectory = "C:\"
        '[ファイルの種類]に表示される選択肢を指定する
        '指定しないとすべてのファイルが表示される
        ofd.Filter = "CSV Files (*.csv)|*.csv"
        '[ファイルの種類]ではじめに選択されるものを指定する
        '2番目の「すべてのファイル」が選択されているようにする
        ofd.FilterIndex = 2
        'タイトルを設定する
        ofd.Title = "開くファイルを選択してください"
        'ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
        ofd.RestoreDirectory = True
        '存在しないファイルの名前が指定されたとき警告を表示する
        'デフォルトでTrueなので指定する必要はない
        ofd.CheckFileExists = True
        '存在しないパスが指定されたとき警告を表示する
        'デフォルトでTrueなので指定する必要はない
        ofd.CheckPathExists = True

        'ダイアログを表示する
        If ofd.ShowDialog() = DialogResult.OK Then
            Dim parser As New TextFieldParser(ofd.FileName, System.Text.Encoding.GetEncoding("UTF-8"))
            parser.TextFieldType = FieldType.Delimited
            parser.SetDelimiters(",") ' 区切り文字はコンマ
            Dim cnt As Integer = 0
            While Not parser.EndOfData
                cnt = cnt + 1
                Dim row As String() = parser.ReadFields() ' 1行読み込み
                If cnt = 1 Then
                    Continue While
                End If
                Insert_Query(row(0), row(1), row(2))
                ' 配列rowの要素は読み込んだ行の各フィールドの値
            End While
        End If
    End Sub
End Class
