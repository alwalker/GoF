Imports Microsoft.VisualBasic
Imports System
Namespace WindowsFormsApplication
	Partial Public Class FormLogin
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.linkLabelValid = New System.Windows.Forms.LinkLabel()
			Me.groupBox1 = New System.Windows.Forms.GroupBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.textBoxPassword = New System.Windows.Forms.TextBox()
			Me.label2 = New System.Windows.Forms.Label()
			Me.textBoxUserName = New System.Windows.Forms.TextBox()
			Me.buttonCancel = New System.Windows.Forms.Button()
			Me.buttonOK = New System.Windows.Forms.Button()
			Me.groupBox1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' linkLabelValid
			' 
			Me.linkLabelValid.AutoSize = True
			Me.linkLabelValid.Location = New System.Drawing.Point(219, 14)
			Me.linkLabelValid.Name = "linkLabelValid"
			Me.linkLabelValid.Size = New System.Drawing.Size(127, 13)
			Me.linkLabelValid.TabIndex = 15
			Me.linkLabelValid.TabStop = True
			Me.linkLabelValid.Text = "What are my credentials?"
'			Me.linkLabelValid.LinkClicked += New System.Windows.Forms.LinkLabelLinkClickedEventHandler(Me.linkLabelValid_LinkClicked);
			' 
			' groupBox1
			' 
			Me.groupBox1.Controls.Add(Me.label1)
			Me.groupBox1.Controls.Add(Me.textBoxPassword)
			Me.groupBox1.Controls.Add(Me.label2)
			Me.groupBox1.Controls.Add(Me.textBoxUserName)
			Me.groupBox1.Location = New System.Drawing.Point(19, 32)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(303, 104)
			Me.groupBox1.TabIndex = 14
			Me.groupBox1.TabStop = False
			Me.groupBox1.Text = "Login"
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(24, 33)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(63, 13)
			Me.label1.TabIndex = 2
			Me.label1.Text = "User Name:"
			' 
			' textBoxPassword
			' 
			Me.textBoxPassword.Location = New System.Drawing.Point(131, 60)
			Me.textBoxPassword.Name = "textBoxPassword"
			Me.textBoxPassword.PasswordChar = "*"c
			Me.textBoxPassword.Size = New System.Drawing.Size(154, 20)
			Me.textBoxPassword.TabIndex = 1
			Me.textBoxPassword.Text = "secret123"
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(24, 63)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(56, 13)
			Me.label2.TabIndex = 6
			Me.label2.Text = "Password:"
			' 
			' textBoxUserName
			' 
			Me.textBoxUserName.Location = New System.Drawing.Point(131, 30)
			Me.textBoxUserName.Name = "textBoxUserName"
			Me.textBoxUserName.Size = New System.Drawing.Size(154, 20)
			Me.textBoxUserName.TabIndex = 0
			Me.textBoxUserName.Text = "debbie"
			' 
			' buttonCancel
			' 
			Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
			Me.buttonCancel.Location = New System.Drawing.Point(175, 147)
			Me.buttonCancel.Name = "buttonCancel"
			Me.buttonCancel.Size = New System.Drawing.Size(72, 24)
			Me.buttonCancel.TabIndex = 13
			Me.buttonCancel.Text = "Cancel"
			Me.buttonCancel.UseVisualStyleBackColor = True
'			Me.buttonCancel.Click += New System.EventHandler(Me.buttonCancel_Click);
			' 
			' buttonOK
			' 
			Me.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK
			Me.buttonOK.Location = New System.Drawing.Point(93, 147)
			Me.buttonOK.Name = "buttonOK"
			Me.buttonOK.Size = New System.Drawing.Size(71, 24)
			Me.buttonOK.TabIndex = 12
			Me.buttonOK.Text = "OK"
			Me.buttonOK.UseVisualStyleBackColor = True
'			Me.buttonOK.Click += New System.EventHandler(Me.buttonOK_Click);
			' 
			' FormLogin
			' 
			Me.AcceptButton = Me.buttonOK
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.CancelButton = Me.buttonCancel
			Me.ClientSize = New System.Drawing.Size(365, 184)
			Me.Controls.Add(Me.linkLabelValid)
			Me.Controls.Add(Me.groupBox1)
			Me.Controls.Add(Me.buttonCancel)
			Me.Controls.Add(Me.buttonOK)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
			Me.Name = "FormLogin"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
			Me.Text = "Login"
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox1.PerformLayout()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private WithEvents linkLabelValid As System.Windows.Forms.LinkLabel
		Private groupBox1 As System.Windows.Forms.GroupBox
		Private label1 As System.Windows.Forms.Label
		Private textBoxPassword As System.Windows.Forms.TextBox
		Private label2 As System.Windows.Forms.Label
		Private textBoxUserName As System.Windows.Forms.TextBox
		Private WithEvents buttonCancel As System.Windows.Forms.Button
		Private WithEvents buttonOK As System.Windows.Forms.Button
	End Class
End Namespace