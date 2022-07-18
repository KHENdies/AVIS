Imports System.Data.SqlClient
Public Class frmAddUser
#Region "Methods"
    Sub Clear()
        txtID.Clear()
        txtLname.Clear()
        txtFname.Clear()
        txtMname.Clear()
        dtpDOB.ResetText()
        cboSex.SelectedIndex = -1
        txtContact.Clear()
        txtAddress.Clear()
        cboProgram.SelectedIndex = -1
        btnSave.Enabled = True
        btnUpdate.Enabled = False
    End Sub
    Sub LoadProgram()
        Try
            cboProgram.Items.Clear()
            con.Open()
            cmd = New SqlCommand("select program from tblProgram", con)
            dr = cmd.ExecuteReader
            While dr.Read
                cboProgram.Items.Add(dr.Item("program").ToString)
            End While
            dr.Close()
            con.Close()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub
#End Region
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Dispose()
    End Sub

    Private Sub cboCategory_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboSex.KeyPress, cboProgram.KeyPress
        e.Handled = True
    End Sub

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtContact.KeyPress, txtID.KeyPress
        Select Case Asc(e.KeyChar)
            Case 8, 47 To 58
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If is_empty(txtID.Text) = True Then Return
            If is_empty(txtLname.Text) = True Then Return
            If is_empty(txtFname.Text) = True Then Return
            'If is_empty(txtMname.Text) = True Then Return
            If is_empty(cboSex.Text) = True Then Return
            If is_empty(txtContact.Text) = True Then Return
            If is_empty(txtAddress.Text) = True Then Return
            If is_empty(cboProgram.Text) = True Then Return
            If MsgBox("do you want to add this Borrower?", vbQuestion + vbYesNo) = vbYes Then
                con.Open()
                cmd = New SqlCommand("insert into tblBorrowers values (@bid,@lname,@fname,@mname,@dob,@sex,@contact,@address,@pid)", con)
                With cmd
                    .Parameters.AddWithValue("@bid", txtID.Text)
                    .Parameters.AddWithValue("@lname", Trim(txtLname.Text))
                    .Parameters.AddWithValue("@fname", Trim(txtFname.Text))
                    .Parameters.AddWithValue("@mname", Trim(txtMname.Text))
                    .Parameters.AddWithValue("@dob", dtpDOB.Value.ToString("yyyy-MM-dd"))
                    .Parameters.AddWithValue("@sex", cboSex.Text)
                    .Parameters.AddWithValue("@contact", txtContact.Text)
                    .Parameters.AddWithValue("@address", Trim(txtAddress.Text))
                    .Parameters.AddWithValue("@pid", cboProgram.Text)
                    .ExecuteNonQuery()
                End With
                con.Close()
                MsgBox("New Borrowers has been added!", vbInformation)
                frmBorrower.loadBorrowers()
                Me.Dispose()
            End If

        Catch ex As Exception
            con.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clear()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If is_empty(txtID.Text) = True Then Return
            If is_empty(txtLname.Text) = True Then Return
            If is_empty(txtFname.Text) = True Then Return
            'If is_empty(txtMname.Text) = True Then Return
            If is_empty(cboSex.Text) = True Then Return
            If is_empty(txtContact.Text) = True Then Return
            If is_empty(txtAddress.Text) = True Then Return
            If is_empty(cboProgram.Text) = True Then Return
            If MsgBox("Do you want to update this Borrower?", vbQuestion + vbYesNo) = vbYes Then
                con.Open()
                cmd = New SqlCommand("update tblBorrowers set lname=@lname,fname=@fname,mname=@mname,dob=@dob,sex=@sex,contact=@contact,address=@address,pid=@pid where bid like '" & txtID.Text & "'", con)
                With cmd
                    .Parameters.AddWithValue("@lname", Trim(txtLname.Text))
                    .Parameters.AddWithValue("@fname", Trim(txtFname.Text))
                    .Parameters.AddWithValue("@mname", Trim(txtMname.Text))
                    .Parameters.AddWithValue("@dob", dtpDOB.Value.ToString("yyyy-MM-dd"))
                    .Parameters.AddWithValue("@sex", cboSex.Text)
                    .Parameters.AddWithValue("@contact", txtContact.Text)
                    .Parameters.AddWithValue("@address", Trim(txtAddress.Text))
                    .Parameters.AddWithValue("@pid", cboProgram.Text)
                    .ExecuteNonQuery()
                End With
                con.Close()
                MsgBox("Borrowers has been updated!", vbInformation)
                frmBorrower.loadBorrowers()
                Me.Dispose()
            End If

        Catch ex As Exception
            con.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub frmAddUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class