Imports System.Data.SqlClient
Public Class frmBirdtoSell
    Dim acc_num As String

#Region "Methods"
    Sub loadBird()
        Try
            DataGridView1.Rows.Clear()
            Dim i As Integer
            con.Open()
            cmd = New SqlCommand("", con)
            dr = cmd.ExecuteReader
            While dr.Read
                i += 1
                DataGridView1.Rows.Add(i)
                DataGridView1.ClearSelection()
                lblRecords.Text = DataGridView1.Rows.Count
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
        acc_num = id
        con.Close()
    End Sub
    Sub clear()
        txtBped.Clear()
        txtBtype.Clear()

    End Sub

#End Region
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Dispose()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            Dim colname As String = DataGridView1.Columns(e.ColumnIndex).Name
            If colname = "colSelect" Then
                txtBtype.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString
                txtBped.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            loadBird()
        End If
    End Sub

    Private Sub txtTitle_TextChanged(sender As Object, e As EventArgs) Handles txtBtype.TextChanged

    End Sub
End Class