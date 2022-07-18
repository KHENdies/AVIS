Imports System.Data.SqlClient
Public Class frmUser
#Region "Methods"
    Sub loadUser()
        Try
            DataGridView1.Rows.Clear()
            Dim i As Integer
            con.Open()
            cmd = New SqlCommand("select *,concat(lname,', ',fname,' ',mname) as fullname from user where concat(lname,', ',fname,' ',mname) like '" & txtSearch.Text & "%' order by fullname", con)
            dr = cmd.ExecuteReader
            While dr.Read
                i += 1
                DataGridView1.Rows.Add(i, dr.Item("bid").ToString, dr.Item("fullname").ToString, Format(dr.Item("dob"), "yyyy-MM-dd"), dr.Item("sex").ToString, dr.Item("contact").ToString, dr.Item("address").ToString, dr.Item("fname").ToString, dr.Item("mname").ToString, dr.Item("lname").ToString, dr.Item("pid").ToString)
                DataGridView1.ClearSelection()
                lblRecords.Text = DataGridView1.Rows.Count
            End While
            dr.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
        End Try
    End Sub
#End Region
    Private Sub btnCatSave_Click(sender As Object, e As EventArgs) Handles btnCatSave.Click
        With frmAddUser
            .ShowDialog()
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Dispose()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            Dim colname As String = DataGridView1.Columns(e.ColumnIndex).Name
            If colname = "colEdit" Then
                With frmAddUser
                    .Text = "Edit User"
                    .txtID.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString
                    .txtFname.Text = DataGridView1.Rows(e.RowIndex).Cells(7).Value.ToString
                    .txtMname.Text = DataGridView1.Rows(e.RowIndex).Cells(8).Value.ToString
                    .txtLname.Text = DataGridView1.Rows(e.RowIndex).Cells(9).Value.ToString
                    .cboSex.SelectedItem = DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString
                    .txtContact.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value.ToString
                    .txtAddress.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value.ToString
                    .btnSave.Enabled = False
                    .btnUpdate.Enabled = True
                    .txtID.Enabled = False
                    .Label1.Select()
                    .ShowDialog()
                End With
            ElseIf colname = "colDel" Then
                If MsgBox("Do you want to remove this User?", vbQuestion + vbYesNo) = vbYes Then
                    con.Open()
                    cmd = New SqlCommand("delete from user where bid like '" & DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString & "'", con)
                    cmd.ExecuteNonQuery()
                    con.Close()
                    MsgBox("User has been removed", vbInformation)
                    loadUser()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            loadUser()
        End If
    End Sub
End Class