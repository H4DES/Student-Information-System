﻿Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class Form2

    Dim signup As String


    Private Sub btnToSignin_Click(sender As Object, e As EventArgs) Handles btnToSignin.Click
        Panel_login.Show()
        Panel_Signup.Hide()
        txtUsername.Focus()
        txtCreateFname.Clear()
        txtCreateMname.Clear()
        txtCreateLname.Clear()
        txtCreateUser.Clear()
        txtCreatePass.Clear()
    End Sub

    Private Sub btnSignin_Click(sender As Object, e As EventArgs) Handles btnSignin.Click
        MSconnection = New SqlConnection(MSconnectionString)
        'MYconnection = New MySqlConnection(MYconnectionString)
        Dim MSreader As SqlDataReader
        Dim MYreader As MySqlDataReader

        If signin = "MSSQL" Then
            Using MSconnection As New SqlConnection(MSconnectionString)
                Try
                    MSconnection.Open()
                    Dim query As String = "SELECT * FROM tbl_admin WHERE username = @Username AND password = @Password"

                    Dim MScommand As New SqlCommand(query, MSconnection)
                    MScommand.Parameters.AddWithValue("@Username", txtUsername.Text)
                    MScommand.Parameters.AddWithValue("@Password", txtPassword.Text)
                    MSreader = MScommand.ExecuteReader()

                    If MSreader.Read() Then
                        adminID = MSreader("admin_id").ToString()
                        Username = MSreader("username").ToString()
                        Password = MSreader("password").ToString()

                    End If
                    If txtUsername.Text = "" And txtPassword.Text = "" Then
                        MessageBox.Show("Please fill up!")
                    Else
                        If MSreader.HasRows Then
                            msgbox_signin.Show()
                            Form3.Show()
                            Me.Hide()
                            txtPassword.Clear()
                        Else
                            MessageBox.Show("Incorrect Username or Password!")
                        End If
                    End If
                    MSconnection.Close()

                Catch ex As SqlException
                    MessageBox.Show(ex.Message)
                Finally
                    MSconnection.Dispose()
                End Try
            End Using

        ElseIf signin = "MYSQL" Then
            Using MYconnection As New MySqlConnection(MYconnectionString)
                Try
                    MYconnection.Open()
                    Dim query As String = "SELECT * FROM tbl_admin WHERE username = @Username AND password = @Password"

                    Dim MYcommand As New MySqlCommand(query, MYconnection)
                    MYcommand.Parameters.AddWithValue("@Username", txtUsername.Text)
                    MYcommand.Parameters.AddWithValue("@Password", txtPassword.Text)
                    MYreader = MYcommand.ExecuteReader()

                    If MYreader.Read() Then
                        adminID = MYreader("admin_id").ToString()
                        Username = MYreader("username").ToString()
                        Password = MYreader("password").ToString()

                    End If

                    If txtUsername.Text = "" And txtPassword.Text = "" Then
                        MessageBox.Show("Fill Up!")
                    Else
                        If MYreader.HasRows Then
                            msgbox_signin.Show()
                            Form3.Show()
                            Me.Hide()
                            txtPassword.Clear()
                        Else
                            MessageBox.Show("Error!")
                        End If
                    End If
                    MYconnection.Close()

                Catch ex As MySqlException
                    MessageBox.Show(ex.Message)
                Finally
                    MYconnection.Dispose()
                End Try
            End Using
        End If

    End Sub

    Private Sub btnToCreateAcc_Click(sender As Object, e As EventArgs) Handles btnToCreateAcc.Click
        Panel_login.Hide()
        Panel_Signup.Show()
        txtCreateUser.Focus()
        txtUsername.Clear()
        txtPassword.Clear()
    End Sub

    Private Sub btnSignup_Click(sender As Object, e As EventArgs) Handles btnSignup.Click
        MSconnection = New SqlConnection(MSconnectionString)
        MYconnection = New MySqlConnection(MYconnectionString)

        If signup = "MSSQL" Then
            Try
                MSconnection.Open()

                Dim checkQuery As String = "SELECT COUNT(*) FROM tbl_admin WHERE username = @Username"
                Dim checkCommand As New SqlCommand(checkQuery, MSconnection)
                checkCommand.Parameters.AddWithValue("@Username", txtCreateUser.Text)

                If Convert.ToInt32(checkCommand.ExecuteScalar()) > 0 Then
                    MessageBox.Show("Username already exists. Please choose a different username.")
                Else
                    ' Check if any text boxes are empty
                    If String.IsNullOrEmpty(txtCreateUser.Text) OrElse
               String.IsNullOrEmpty(txtCreatePass.Text) OrElse
               String.IsNullOrEmpty(txtCreateFname.Text) OrElse
               String.IsNullOrEmpty(txtCreateLname.Text) Then
                        MessageBox.Show("Please fill in all the required fields.")
                    Else
                        'When all accounts is ready
                        Dim query As String = "INSERT INTO tbl_admin (username, password, firstname, middlename, lastname, date_time) VALUES (@Username, @Password, @Fname, @Mname, @Lname, GETDATE())"
                        Dim MScommand As New SqlCommand(query, MSconnection)
                        MScommand.Parameters.AddWithValue("@Username", txtCreateUser.Text)
                        MScommand.Parameters.AddWithValue("@Password", txtCreatePass.Text)
                        MScommand.Parameters.AddWithValue("@Fname", txtCreateFname.Text)
                        MScommand.Parameters.AddWithValue("@Mname", txtCreateMname.Text)
                        MScommand.Parameters.AddWithValue("@Lname", txtCreateLname.Text)

                        MScommand.ExecuteNonQuery()
                        msgbox_signup.Show()
                        txtCreateFname.Clear()
                        txtCreateMname.Clear()
                        txtCreateLname.Clear()
                        txtCreateUser.Clear()
                        txtCreatePass.Clear()
                    End If
                End If
            Catch ex As SqlException
                MessageBox.Show(ex.Message)
            Finally
                MSconnection.Dispose()
            End Try

        ElseIf signup = "MYSQL" Then
            Try
                MYconnection.Open()

                'FOR CHECKING DUPLICATE USERNAME
                Dim checkQuery As String = "SELECT COUNT(*) FROM tbl_admin WHERE username = @Username"
                Dim checkCommand As New MySqlCommand(checkQuery, MYconnection)
                checkCommand.Parameters.AddWithValue("@Username", txtCreateUser.Text)

                If Convert.ToInt32(checkCommand.ExecuteScalar()) > 0 Then
                    MessageBox.Show("Username already exists. Please choose a different username.")
                Else
                    ' Check if any text boxes are empty
                    If String.IsNullOrEmpty(txtCreateUser.Text) OrElse
               String.IsNullOrEmpty(txtCreatePass.Text) OrElse
               String.IsNullOrEmpty(txtCreateFname.Text) OrElse
               String.IsNullOrEmpty(txtCreateMname.Text) OrElse
               String.IsNullOrEmpty(txtCreateLname.Text) Then
                        MessageBox.Show("Please fill in all the required fields.")
                    Else

                        Dim query As String = "INSERT INTO tbl_admin (username, password, firstname, middlename, lastname, date_time) VALUES (@Username, @Password, @Fname, @Mname, @Lname, NOW())"
                        Dim MYcommand As New MySqlCommand(query, MYconnection)
                        MYcommand.Parameters.AddWithValue("@Username", txtCreateUser.Text)
                        MYcommand.Parameters.AddWithValue("@Password", txtCreatePass.Text)
                        MYcommand.Parameters.AddWithValue("@Fname", txtCreateFname.Text)
                        MYcommand.Parameters.AddWithValue("@Mname", txtCreateMname.Text)
                        MYcommand.Parameters.AddWithValue("@Lname", txtCreateLname.Text)

                        MYcommand.ExecuteNonQuery()
                        msgbox_signup.Show()
                        txtCreateFname.Clear()
                        txtCreateMname.Clear()
                        txtCreateLname.Clear()
                        txtCreateUser.Clear()
                        txtCreatePass.Clear()
                    End If
                End If
            Catch ex As MySqlException
                MessageBox.Show(ex.Message)
            Finally
                MYconnection.Dispose()
            End Try

        End If

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel_Signup.Hide()
        ' Attach the common event handler to the KeyDown event of the textboxes
        AddHandler txtUsername.KeyDown, AddressOf Form2_KeyDown
        AddHandler txtPassword.KeyDown, AddressOf Form2_KeyDown
        AddHandler rdo_MY_login.KeyDown, AddressOf Form2_KeyDown
    End Sub

    Private Sub rdo_MS_signup_CheckedChanged(sender As Object, e As EventArgs) Handles rdo_MS_signup.CheckedChanged
        signup = "MSSQL"
    End Sub

    Private Sub rdo_MY_signup_CheckedChanged(sender As Object, e As EventArgs) Handles rdo_MY_signup.CheckedChanged
        signup = "MYSQL"
    End Sub

    Private Sub rdo_MS_login_CheckedChanged(sender As Object, e As EventArgs) Handles rdo_MS_login.CheckedChanged
        signin = "MSSQL"
    End Sub

    Private Sub rdo_MY_login_CheckedChanged(sender As Object, e As EventArgs) Handles rdo_MY_login.CheckedChanged
        signin = "MYSQL"
    End Sub

    'Private Sub TextBoxes_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUsername.KeyDown, txtPassword.KeyDown, rdo_MY_login.KeyDown
    '    If e.KeyCode = Keys.Enter Then
    '        If open = False Then
    '            btnSignin.PerformClick()
    '            open = True
    '        End If
    '    End If
    'End Sub
    'Private Sub TextBoxes_KeyDown1(sender As Object, e As KeyEventArgs) Handles txtCreateUser.KeyDown, txtCreatePass.KeyDown, txtCreateLname.KeyDown, rdo_MY_signup.KeyDown
    '    If e.KeyCode = Keys.Enter Then
    '        If open = False Then
    '            btnSignup.PerformClick()
    '            open = True
    '        End If
    '    End If
    'End Sub

    Private Sub Form2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        If rdo_MS_login.Checked Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If open = False Then
                    btnSignin.PerformClick()
                    open = True
                End If
            End If
        ElseIf rdo_MY_login.Checked Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If open = False Then
                    btnSignin.PerformClick()
                    open = True
                End If
            End If
        End If
    End Sub

    Private Sub Form2_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If rdo_MY_login.Checked Then
            If e.KeyCode = Keys.Enter Then
                If open = False Then
                    btnSignin.PerformClick()
                    open = True
                End If
            ElseIf e.KeyCode = Keys.Left Then
                rdo_MY_signup.Checked = True
            ElseIf e.KeyCode = Keys.Right Then
                rdo_MS_login.Checked = True
            End If
        ElseIf rdo_MS_login.Checked Then
            If e.KeyCode = Keys.Enter Then
                If open = False Then
                    btnSignin.PerformClick()
                    open = True
                End If
            ElseIf e.KeyCode = Keys.Left Then
                rdo_MY_signup.Checked = True
            ElseIf e.KeyCode = Keys.Right Then
                rdo_MS_signup.Checked = True
            End If
        End If
    End Sub






    'Private Sub Form2(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
    '    Form1.Close()
    'End Sub
End Class