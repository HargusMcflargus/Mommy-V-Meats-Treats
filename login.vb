Public Class login
    Public connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db.mdb"
    Public user = ""
    Private connection As OleDb.OleDbConnection

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim command As New OleDb.OleDbCommand("SELECT * FROM [users] WHERE [Username]='" & usernameField.Text & "' AND [Password]='" & passwordField.Text & "'", connection)
        Dim reader = command.ExecuteReader
        If reader.HasRows Then
            MessageBox.Show("Welcome " & usernameField.Text)
            user = usernameField.Text
            usernameField.Clear()
            passwordField.Clear()
            Hide()
            Form1.Show()
        End If
    End Sub

    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connection = New OleDb.OleDbConnection(connectionString)
        Try
            connection.Open()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class