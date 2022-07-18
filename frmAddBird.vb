Imports System.Data.SqlClient
Public Class frmAddBird
#Region "Methods"
    Sub Auto_ID()
        Dim id As String
        Dim int As Integer
        con.Open()
        cmd = New SqlCommand("", con) 'ID0000001'
        id = cmd.ExecuteScalar.ToString
        If String.IsNullOrEmpty(id) Then
            id = "ID0000001"
        Else
            id.Substring(3)
            Int32.TryParse(id, int)
            int = id + 1
            id = "ID" + int.ToString("D7")
        End If

        txtID.Text = id
        con.Close()
    End Sub

    Sub loadcategory()
        Try
            cboCategory.Items.Clear()
            con.Open()
            cmd = New SqlCommand("", con)
            dr = cmd.ExecuteReader
            While dr.Read
                cboCategory.Items.Add(dr.Item("").ToString)
            End While
            dr.Close()
            con.Close()

        Catch ex As Exception
            con.Close()
        End Try
    End Sub

    Sub Auto_acc_num()
        Dim id As String
        Dim int As Integer
        con.Open()
        cmd = New SqlCommand("", con) 'AN0000001'
        id = cmd.ExecuteScalar.ToString
        If String.IsNullOrEmpty(id) Then
            id = "AN0000001"
        Else
            id.Substring(3)
            Int32.TryParse(id, int)
            int = id + 1
            id = "AN" + int.ToString("D7")
        End If
        txtAccNum.Text = id
        con.Close()
    End Sub

    Sub clear()
        Auto_ID()
        Auto_acc_num()
        cboCategory.SelectedIndex = -1
        txtTitle.Clear()
        txtAuthor.Clear()
        txtPrice.Clear()
        btnSave.Enabled = True
        btnUpdate.Enabled = False
    End Sub
#End Region
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Dispose()
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboCategory.KeyPress
        e.Handled = True
    End Sub

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress
        Select Case Asc(e.KeyChar)
            Case 8, 47 To 58
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub btnCatSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If is_empty(cboCategory.Text) = True Then Return
            If is_empty(txtTitle.Text) = True Then Return
            If is_empty(txtAuthor.Text) = True Then Return
            If is_empty(txtPrice.Text) = True Then Return

            If MsgBox("Do you want to add this bird?", vbQuestion + vbYesNo) = vbYes Then
                con.Open()
                cmd = New SqlCommand("", con)
                With cmd
                    .Parameters.AddWithValue("", txtID.Text)
                    .Parameters.AddWithValue("", txtAccNum.Text)
                    .Parameters.AddWithValue("", cboCategory.Text)
                    .Parameters.AddWithValue("", Trim(txtTitle.Text))
                    .Parameters.AddWithValue("", Trim(txtAuthor.Text))
                    .Parameters.AddWithValue("", CInt(txtPrice.Text))
                    .ExecuteNonQuery()
                End With
                con.Close()
                MsgBox("New book is added", vbInformation)
                clear()
                frmBirdtoSell.loadBird()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            con.Close()
        End Try
    End Sub

    'Private Sub txtyrpublished_keypress(sender As Object, e As KeyPressEventArgs) Handles txtyrpublished.keypress
    '    Select Case Asc(e.KeyChar)
    '        Case 8, 47 To 58
    '        Case Else
    '            e.Handled = True
    '    End Select
    'End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clear()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If is_empty(cboCategory.Text) = True Then Return
            If is_empty(txtTitle.Text) = True Then Return
            If is_empty(txtAuthor.Text) = True Then Return
            If is_empty(txtPrice.Text) = True Then Return

            If MsgBox("Do you want to update this bird?", vbQuestion + vbYesNo) = vbYes Then
                con.Open()
                cmd = New SqlCommand("", con)
                With cmd
                    .Parameters.AddWithValue("", cboCategory.Text)
                    .Parameters.AddWithValue("", Trim(txtTitle.Text))
                    .Parameters.AddWithValue("", Trim(txtAuthor.Text))
                    .ExecuteNonQuery()
                End With
                con.Close()
                MsgBox(txtID.Text & Environment.NewLine &
                    "Has been updated", vbInformation)
                Me.Dispose()
                frmBirdtoSell.loadBird()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            con.Close()
        End Try
    End Sub

    Private Sub txtID_TextChanged(sender As Object, e As EventArgs) Handles txtID.TextChanged
        QRCode(PictureBox2, txtID)
    End Sub
End Class