Public Class Form1
#Region "Methods"
    Sub Close_All_Forms()
        For i = My.Application.OpenForms.Count - 1 To 0 Step -1
            If My.Application.OpenForms(i) IsNot Me Then
                My.Application.OpenForms(i).Dispose()
            End If
        Next
    End Sub
#End Region

    Private Sub MainFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = Screen.PrimaryScreen.WorkingArea.Width
        Me.Height = Screen.PrimaryScreen.WorkingArea.Height
        Timer1.Enabled = True
        opencon()
    End Sub

    Private Sub btnMaintenance_Click(sender As Object, e As EventArgs) Handles btnMaintenance.Click
        With frmMaintenance
            .loadcategory()
            .ShowDialog()

        End With
    End Sub

    Private Sub btnBookEntry_Click(sender As Object, e As EventArgs) Handles btnBookEntry.Click
        For Each f As Form In My.Application.OpenForms
            If f.Name = "frmBirdHave" Then Return
        Next
        Close_All_Forms()
        With frmBirdsHave
            .TopLevel = False
            MainPanel.Controls.Add(frmBirdsHave)
            .loadBird()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub btnBooksAcquired_Click(sender As Object, e As EventArgs) Handles btnBooksAcquired.Click
        For Each f As Form In My.Application.OpenForms
            If f.Name = "frmBirdtoSell" Then Return
        Next
        Close_All_Forms()
        With frmBirdtoSell
            .TopLevel = False
            MainPanel.Controls.Add(frmBirdtoSell)
            .loadBird()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub btnUser_Click(sender As Object, e As EventArgs) Handles btnUserAccnt.Click
        For Each f As Form In My.Application.OpenForms
            If f.Name = "frmUser" Then Return
        Next
        Close_All_Forms()
        With frmUser
            .TopLevel = False
            MainPanel.Controls.Add(frmUser)
            .loadUser()
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblDate.Text = Date.Now.ToString("MM-dd-yyyy hh:mm:ss")
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Me.Dispose()
    End Sub

    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click

    End Sub
End Class
