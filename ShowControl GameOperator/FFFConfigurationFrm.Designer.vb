<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FFFConfigurationFrm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.FFFMomentStatus_ComboBox = New System.Windows.Forms.ComboBox()
        Me.SetFFFMomentStatus_Button = New System.Windows.Forms.Button()
        Me.FFFContestantFile_TextBox = New System.Windows.Forms.TextBox()
        Me.LoadContestants_Button = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'FFFMomentStatus_ComboBox
        '
        Me.FFFMomentStatus_ComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!)
        Me.FFFMomentStatus_ComboBox.FormattingEnabled = True
        Me.FFFMomentStatus_ComboBox.Items.AddRange(New Object() {"", "LoadedQuestion_Fired", "QuestionFFF_Fired", "AnswersABCDFFF_Fired", "RightOrderReady", "NextRevealRightOrder", "WhoAnsweredCorrectlyReady", "WhoAnsweredCorrectlyGreen", "WhoAnsweredCorrectlyBlink", "QuestionAnswers_Dissolved", "WinnerClockName", "RightOrderFlyIN"})
        Me.FFFMomentStatus_ComboBox.Location = New System.Drawing.Point(25, 21)
        Me.FFFMomentStatus_ComboBox.Name = "FFFMomentStatus_ComboBox"
        Me.FFFMomentStatus_ComboBox.Size = New System.Drawing.Size(287, 32)
        Me.FFFMomentStatus_ComboBox.TabIndex = 0
        '
        'SetFFFMomentStatus_Button
        '
        Me.SetFFFMomentStatus_Button.Location = New System.Drawing.Point(328, 21)
        Me.SetFFFMomentStatus_Button.Name = "SetFFFMomentStatus_Button"
        Me.SetFFFMomentStatus_Button.Size = New System.Drawing.Size(175, 34)
        Me.SetFFFMomentStatus_Button.TabIndex = 1
        Me.SetFFFMomentStatus_Button.Text = "SET MOMENT STATUS"
        Me.SetFFFMomentStatus_Button.UseVisualStyleBackColor = True
        '
        'FFFContestantFile_TextBox
        '
        Me.FFFContestantFile_TextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!)
        Me.FFFContestantFile_TextBox.Location = New System.Drawing.Point(25, 74)
        Me.FFFContestantFile_TextBox.Name = "FFFContestantFile_TextBox"
        Me.FFFContestantFile_TextBox.Size = New System.Drawing.Size(404, 29)
        Me.FFFContestantFile_TextBox.TabIndex = 2
        '
        'LoadContestants_Button
        '
        Me.LoadContestants_Button.Location = New System.Drawing.Point(435, 74)
        Me.LoadContestants_Button.Name = "LoadContestants_Button"
        Me.LoadContestants_Button.Size = New System.Drawing.Size(68, 29)
        Me.LoadContestants_Button.TabIndex = 3
        Me.LoadContestants_Button.Text = "LOAD"
        Me.LoadContestants_Button.UseVisualStyleBackColor = True
        '
        'FFFConfigurationFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 121)
        Me.Controls.Add(Me.LoadContestants_Button)
        Me.Controls.Add(Me.FFFContestantFile_TextBox)
        Me.Controls.Add(Me.SetFFFMomentStatus_Button)
        Me.Controls.Add(Me.FFFMomentStatus_ComboBox)
        Me.Name = "FFFConfigurationFrm"
        Me.Text = "FFFConfigurationFrm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents FFFMomentStatus_ComboBox As ComboBox
    Friend WithEvents SetFFFMomentStatus_Button As Button
    Friend WithEvents FFFContestantFile_TextBox As TextBox
    Friend WithEvents LoadContestants_Button As Button
End Class
