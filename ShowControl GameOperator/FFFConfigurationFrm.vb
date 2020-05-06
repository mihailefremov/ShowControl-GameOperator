Public Class FFFConfigurationFrm
    Private Sub SetFFFMomentStatus_Button_Click(sender As Object, e As EventArgs) Handles SetFFFMomentStatus_Button.Click
        FFFOperator.SetMomentStatus(FFFMomentStatus_ComboBox.SelectedItem)
        Me.Close()
    End Sub

    Private Sub LoadContestants_Button_Click(sender As Object, e As EventArgs) Handles LoadContestants_Button.Click
        Dim openFileDialog1 As System.Windows.Forms.OpenFileDialog
        openFileDialog1 = New System.Windows.Forms.OpenFileDialog()

        openFileDialog1.ShowDialog()

        FFFContestantFile_TextBox.Text = openFileDialog1.FileName
        FFFOperator.ParseContestantsToUI(FFFContestantFile_TextBox.Text)

        Me.Close()

    End Sub
End Class