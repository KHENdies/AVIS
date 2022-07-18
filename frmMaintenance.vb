Imports System.Data.SqlClient
Public Class frmMaintenance
    Dim id As String
    'connecctions'
#Region "Load Data"
    Sub loadcategory()
        Try
            DataGridView1.Rows.Clear()
            Dim i As Integer
            con.Open()
            cmd = New SqlCommand("select * from ", con)
            dr = cmd.ExecuteReader
            While dr.Read
                i += 1
                DataGridView1.Rows.Add(i)
                DataGridView1.ClearSelection()
            End While
            dr.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
        End Try
    End Sub
#End Region
    'end connections'


    'Start of Category option'
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnCatSave.Click
        Try
            If is_empty(txtCategory.Text) = True Then Return
            If MsgBox("Do you want to save this category?", vbQuestion + vbYesNo) = vbYes Then
                con.Open()
                cmd = New SqlCommand("insert into tblCategory values (@category)", con)
                cmd.Parameters.AddWithValue("@category", Trim(txtCategory.Text))
                cmd.ExecuteNonQuery()
                con.Close()
                MsgBox("Category has been saved!", vbInformation)
                txtCategory.Clear()
                loadcategory()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            con.Close()
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            Dim colname As String = DataGridView1.Columns(e.ColumnIndex).Name
            If colname = "colEdit" Then
                btnCatSave.Enabled = False
                btnCatUpdate.Enabled = True
                txtCategory.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString
                id = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString
            ElseIf colname = "colDel" Then
                If MsgBox("Delete this record?", vbQuestion + vbYesNo) = vbYes Then
                    con.Open()
                    cmd = New SqlCommand("delete from tblCategory where category like '" & DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString & "'", con)
                    cmd.ExecuteNonQuery()
                    con.Close()
                    MsgBox("Record has been deleted", vbInformation)
                    Button2.PerformClick()
                    loadcategory()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            con.Close()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        txtCategory.Clear()
        btnCatSave.Enabled = True
        btnCatUpdate.Enabled = False

    End Sub

    Private Sub btnCatUpdate_Click(sender As Object, e As EventArgs) Handles btnCatUpdate.Click
        Try
            If is_empty(txtCategory.Text) = True Then Return
            If MsgBox("Do you want to update this category?", vbQuestion + vbYesNo) = vbYes Then
                con.Open()
                cmd = New SqlCommand("update tblCategory set category=@category where category like '" & id & "' ", con)
                    cmd.Parameters.AddWithValue("@category", Trim(txtCategory.Text))
                cmd.ExecuteNonQuery()
                con.Close()
                MsgBox("Category has been updated!", vbInformation)
                Button2.PerformClick()
                loadcategory()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            con.Close()
        End Try
    End Sub

    'End of Category'

    Private Sub txtBooksAllowed_KeyPress(sender As Object, e As KeyPressEventArgs)
        Select Case Asc(e.KeyChar)
            Case 8, 46, 48 To 57
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Dispose()
    End Sub
End Class
