Imports System.Data.SqlClient
Public Class frmAddUser
#Region "Methods"
    Sub Clear()
        txtID.Clear()
        txtLname.Clear()
        txtFname.Clear()
        txtMname.Clear()
        cboSex.SelectedIndex = -1
        txtContact.Clear()
        txtAddress.Clear()
        btnSave.Enabled = True
        btnUpdate.Enabled = False
    End Sub
#End Region
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Dispose()
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
            If MsgBox("do you want to add this User?", vbQuestion + vbYesNo) = vbYes Then
                con.Open()
                cmd = New SqlCommand("insert into tbluser values (@bid,@lname,@fname,@mname,@dob,@sex,@contact,@address,@pid)", con)
                With cmd
                    .Parameters.AddWithValue("@uid", txtID.Text)
                    .Parameters.AddWithValue("@lname", Trim(txtLname.Text))
                    .Parameters.AddWithValue("@fname", Trim(txtFname.Text))
                    .Parameters.AddWithValue("@mname", Trim(txtMname.Text))
                    .Parameters.AddWithValue("@sex", cboSex.Text)
                    .Parameters.AddWithValue("@contact", txtContact.Text)
                    .Parameters.AddWithValue("@address", Trim(txtAddress.Text))
                    .ExecuteNonQuery()
                End With
                con.Close()
                MsgBox("New user has been added!", vbInformation)
                frmUser.loadUser()
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
            If MsgBox("Do you want to update this User?", vbQuestion + vbYesNo) = vbYes Then
                con.Open()
                cmd = New SqlCommand("", con)
                With cmd
                    .Parameters.AddWithValue("", Trim(txtLname.Text))
                    .Parameters.AddWithValue("", Trim(txtFname.Text))
                    .Parameters.AddWithValue("", Trim(txtMname.Text))
                    .Parameters.AddWithValue("", cboSex.Text)
                    .Parameters.AddWithValue("", txtContact.Text)
                    .Parameters.AddWithValue("", Trim(txtAddress.Text))
                    .ExecuteNonQuery()
                End With
                con.Close()
                MsgBox("Borrowers has been updated!", vbInformation)
                frmUser.loadUser()
                Me.Dispose()
            End If

        Catch ex As Exception
            con.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub
End Class