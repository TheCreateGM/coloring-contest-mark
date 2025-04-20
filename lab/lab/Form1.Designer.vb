<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.nudCreativity = New System.Windows.Forms.NumericUpDown()
        Me.nudAccuracy = New System.Windows.Forms.NumericUpDown()
        Me.nudAttractiveness = New System.Windows.Forms.NumericUpDown()
        Me.nudColorScheme = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPicturePath = New System.Windows.Forms.TextBox()
        Me.txtParticipantName = New System.Windows.Forms.TextBox()
        Me.txtJuryName = New System.Windows.Forms.TextBox()
        Me.txtTotalMark = New System.Windows.Forms.TextBox()
        Me.picSubmission = New System.Windows.Forms.PictureBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnLast = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.btnFirst = New System.Windows.Forms.Button()
        Me.btnBrowsePicture = New System.Windows.Forms.Button()
        Me.btnCalculateTotal = New System.Windows.Forms.Button()
        Me.ofdPicture = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nudCreativity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudAccuracy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudAttractiveness, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudColorScheme, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picSubmission, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Participant Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Jury Name:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(323, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(99, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Submission Picture:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(323, 255)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Picture Path:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 255)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(97, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Total Mark (0-100):"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.GroupBox1.Controls.Add(Me.nudCreativity)
        Me.GroupBox1.Controls.Add(Me.nudAccuracy)
        Me.GroupBox1.Controls.Add(Me.nudAttractiveness)
        Me.GroupBox1.Controls.Add(Me.nudColorScheme)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 64)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(238, 179)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Marking Criteria"
        '
        'nudCreativity
        '
        Me.nudCreativity.Location = New System.Drawing.Point(119, 127)
        Me.nudCreativity.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.nudCreativity.Name = "nudCreativity"
        Me.nudCreativity.Size = New System.Drawing.Size(62, 20)
        Me.nudCreativity.TabIndex = 16
        '
        'nudAccuracy
        '
        Me.nudAccuracy.Location = New System.Drawing.Point(119, 94)
        Me.nudAccuracy.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.nudAccuracy.Name = "nudAccuracy"
        Me.nudAccuracy.Size = New System.Drawing.Size(62, 20)
        Me.nudAccuracy.TabIndex = 15
        '
        'nudAttractiveness
        '
        Me.nudAttractiveness.Location = New System.Drawing.Point(119, 64)
        Me.nudAttractiveness.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.nudAttractiveness.Name = "nudAttractiveness"
        Me.nudAttractiveness.Size = New System.Drawing.Size(62, 20)
        Me.nudAttractiveness.TabIndex = 14
        '
        'nudColorScheme
        '
        Me.nudColorScheme.Location = New System.Drawing.Point(118, 34)
        Me.nudColorScheme.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.nudColorScheme.Name = "nudColorScheme"
        Me.nudColorScheme.Size = New System.Drawing.Size(62, 20)
        Me.nudColorScheme.TabIndex = 13
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 96)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(85, 13)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Accuracy (0-30):"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 129)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Creativity (0-20):"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 66)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Attractiveness (0-30):"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Color Scheme (0-20):"
        '
        'txtPicturePath
        '
        Me.txtPicturePath.Location = New System.Drawing.Point(326, 271)
        Me.txtPicturePath.Name = "txtPicturePath"
        Me.txtPicturePath.ReadOnly = True
        Me.txtPicturePath.Size = New System.Drawing.Size(100, 20)
        Me.txtPicturePath.TabIndex = 10
        '
        'txtParticipantName
        '
        Me.txtParticipantName.Location = New System.Drawing.Point(109, 6)
        Me.txtParticipantName.Name = "txtParticipantName"
        Me.txtParticipantName.Size = New System.Drawing.Size(141, 20)
        Me.txtParticipantName.TabIndex = 11
        '
        'txtJuryName
        '
        Me.txtJuryName.Location = New System.Drawing.Point(109, 34)
        Me.txtJuryName.Name = "txtJuryName"
        Me.txtJuryName.Size = New System.Drawing.Size(141, 20)
        Me.txtJuryName.TabIndex = 12
        '
        'txtTotalMark
        '
        Me.txtTotalMark.BackColor = System.Drawing.SystemColors.Info
        Me.txtTotalMark.Location = New System.Drawing.Point(115, 252)
        Me.txtTotalMark.Name = "txtTotalMark"
        Me.txtTotalMark.ReadOnly = True
        Me.txtTotalMark.Size = New System.Drawing.Size(100, 20)
        Me.txtTotalMark.TabIndex = 13
        '
        'picSubmission
        '
        Me.picSubmission.BackColor = System.Drawing.SystemColors.ControlLight
        Me.picSubmission.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picSubmission.Location = New System.Drawing.Point(326, 34)
        Me.picSubmission.Name = "picSubmission"
        Me.picSubmission.Size = New System.Drawing.Size(282, 209)
        Me.picSubmission.TabIndex = 14
        Me.picSubmission.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.GroupBox2.Controls.Add(Me.btnDelete)
        Me.GroupBox2.Controls.Add(Me.btnNew)
        Me.GroupBox2.Controls.Add(Me.btnSave)
        Me.GroupBox2.Location = New System.Drawing.Point(14, 332)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(111, 124)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Actions:"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(16, 82)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(16, 53)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 23)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = "New"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(16, 24)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.ControlLight
        Me.GroupBox3.Controls.Add(Me.btnLast)
        Me.GroupBox3.Controls.Add(Me.btnNext)
        Me.GroupBox3.Controls.Add(Me.btnPrevious)
        Me.GroupBox3.Controls.Add(Me.btnFirst)
        Me.GroupBox3.Location = New System.Drawing.Point(131, 332)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(477, 124)
        Me.GroupBox3.TabIndex = 16
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Navigation:"
        '
        'btnLast
        '
        Me.btnLast.Location = New System.Drawing.Point(260, 24)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(75, 23)
        Me.btnLast.TabIndex = 21
        Me.btnLast.Text = ">>"
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(179, 24)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNext.TabIndex = 20
        Me.btnNext.Text = ">"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnPrevious
        '
        Me.btnPrevious.Location = New System.Drawing.Point(98, 24)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(75, 23)
        Me.btnPrevious.TabIndex = 19
        Me.btnPrevious.Text = "<"
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'btnFirst
        '
        Me.btnFirst.Location = New System.Drawing.Point(17, 24)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(75, 23)
        Me.btnFirst.TabIndex = 18
        Me.btnFirst.Text = "<<"
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'btnBrowsePicture
        '
        Me.btnBrowsePicture.Location = New System.Drawing.Point(432, 271)
        Me.btnBrowsePicture.Name = "btnBrowsePicture"
        Me.btnBrowsePicture.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowsePicture.TabIndex = 17
        Me.btnBrowsePicture.Text = " Browse..."
        Me.btnBrowsePicture.UseVisualStyleBackColor = True
        '
        'btnCalculateTotal
        '
        Me.btnCalculateTotal.Location = New System.Drawing.Point(115, 278)
        Me.btnCalculateTotal.Name = "btnCalculateTotal"
        Me.btnCalculateTotal.Size = New System.Drawing.Size(100, 23)
        Me.btnCalculateTotal.TabIndex = 18
        Me.btnCalculateTotal.Text = "Calculate Total"
        Me.btnCalculateTotal.UseVisualStyleBackColor = True
        '
        'ofdPicture
        '
        Me.ofdPicture.FileName = "OpenFileDialog1"
        Me.ofdPicture.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All files (*.*)|*.*"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(641, 480)
        Me.Controls.Add(Me.btnCalculateTotal)
        Me.Controls.Add(Me.btnBrowsePicture)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.picSubmission)
        Me.Controls.Add(Me.txtTotalMark)
        Me.Controls.Add(Me.txtJuryName)
        Me.Controls.Add(Me.txtParticipantName)
        Me.Controls.Add(Me.txtPicturePath)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Jury Coloring Competition Marking"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nudCreativity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudAccuracy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudAttractiveness, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudColorScheme, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picSubmission, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPicturePath As System.Windows.Forms.TextBox
    Friend WithEvents txtParticipantName As System.Windows.Forms.TextBox
    Friend WithEvents txtJuryName As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalMark As System.Windows.Forms.TextBox
    Friend WithEvents picSubmission As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnBrowsePicture As System.Windows.Forms.Button
    Friend WithEvents btnCalculateTotal As System.Windows.Forms.Button
    Friend WithEvents ofdPicture As System.Windows.Forms.OpenFileDialog
    Friend WithEvents nudCreativity As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudAccuracy As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudAttractiveness As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudColorScheme As System.Windows.Forms.NumericUpDown

End Class
