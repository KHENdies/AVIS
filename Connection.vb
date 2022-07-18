Imports System.Data.SqlClient
Imports ThoughtWorks.QRCode.Codec
Module Connection
    Public con As New SqlConnection
    Public cmd As New SqlCommand
    Public dr As SqlDataReader
    Public ds As DataSet
    Public da As New SqlDataAdapter

    Sub opencon()
        con.ConnectionString = "Data Source=DESKTOP-VPHHFIM;Initial Catalog=avis;Integrated Security=True"
    End Sub

    Function is_empty(text As Object) As Boolean
        If text = String.Empty Then
            MsgBox("Required missing field!", vbExclamation)
            Return True
        Else
            Return False
        End If
    End Function
    Function QRCode(pic As PictureBox, id As TextBox) As Image
        Dim qrCodeEncoder As New QRCodeEncoder

        Dim type As String = "byte"
        If type = "byte" Then
            qrCodeEncoder.QRCodeEncodeMode = qrCodeEncoder.ENCODE_MODE.BYTE
        End If

        qrCodeEncoder.QRCodeVersion = 7
        qrCodeEncoder.QRCodeScale = 4
        qrCodeEncoder.QRCodeForegroundColor = Color.Black

        Dim errorCorrect As String = "M"
        If errorCorrect = "M" Then
            qrCodeEncoder.QRCodeErrorCorrect = qrCodeEncoder.ERROR_CORRECTION.M
        End If

        Dim qrImage As Image = qrCodeEncoder.Encode(id.Text)
        pic.Image = qrImage
        Return pic.Image
    End Function
    Sub autoComplete(id As TextBox, sql As String, table As String)
        Try
            con.Open()
            cmd = New SqlCommand(sql, con)
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds, table)
            con.Close()
            Dim col As New AutoCompleteStringCollection
            For i = 0 To ds.Tables(table).Rows.Count - 1
                col.Add(ds.Tables(table).Rows(i)(0).ToString)
            Next
            id.AutoCompleteSource = AutoCompleteSource.CustomSource
            id.AutoCompleteCustomSource = col
            id.AutoCompleteMode = AutoCompleteMode.Suggest
        Catch ex As Exception
            con.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub
    Sub updateBirds(sql As String)
        Try
            con.Open()
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            con.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub
End Module
