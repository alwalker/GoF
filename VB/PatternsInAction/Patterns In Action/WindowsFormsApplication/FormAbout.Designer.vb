Imports Microsoft.VisualBasic
Imports System
Namespace WindowsFormsApplication
	Partial Public Class FormAbout
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
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(FormAbout))
			Me.label4 = New System.Windows.Forms.Label()
			Me.label3 = New System.Windows.Forms.Label()
			Me.label2 = New System.Windows.Forms.Label()
			Me.label1 = New System.Windows.Forms.Label()
			Me.labelCopyright = New System.Windows.Forms.Label()
			Me.buttonClose = New System.Windows.Forms.Button()
			Me.pictureBox1 = New System.Windows.Forms.PictureBox()
			CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.label4.Location = New System.Drawing.Point(147, 24)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(130, 13)
			Me.label4.TabIndex = 13
			Me.label4.Text = "Patterns in Action 3.5"
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(147, 84)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(77, 13)
			Me.label3.TabIndex = 12
			Me.label3.Text = "Service client. "
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(147, 71)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(176, 13)
			Me.label2.TabIndex = 11
			Me.label2.Text = "the capabilities of a Windows Forms"
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(147, 58)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(168, 13)
			Me.label1.TabIndex = 10
			Me.label1.Text = "Sample application demonstrating "
			' 
			' labelCopyright
			' 
			Me.labelCopyright.AutoSize = True
			Me.labelCopyright.Location = New System.Drawing.Point(34, 136)
			Me.labelCopyright.Name = "labelCopyright"
			Me.labelCopyright.Size = New System.Drawing.Size(320, 13)
			Me.labelCopyright.TabIndex = 9
			Me.labelCopyright.Text = "Copyright (c) 2008. Data && Object Factory, LLC. All rights reserved."
			' 
			' buttonClose
			' 
			Me.buttonClose.Location = New System.Drawing.Point(150, 162)
			Me.buttonClose.Name = "buttonClose"
			Me.buttonClose.Size = New System.Drawing.Size(75, 23)
			Me.buttonClose.TabIndex = 8
			Me.buttonClose.Text = "Close"
			Me.buttonClose.UseVisualStyleBackColor = True
'			Me.buttonClose.Click += New System.EventHandler(Me.buttonClose_Click);
			' 
			' pictureBox1
			' 
			Me.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
			Me.pictureBox1.Image = (CType(resources.GetObject("pictureBox1.Image"), System.Drawing.Image))
			Me.pictureBox1.Location = New System.Drawing.Point(27, 24)
			Me.pictureBox1.Name = "pictureBox1"
			Me.pictureBox1.Size = New System.Drawing.Size(88, 87)
			Me.pictureBox1.TabIndex = 7
			Me.pictureBox1.TabStop = False
			' 
			' FormAbout
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(380, 209)
			Me.Controls.Add(Me.label4)
			Me.Controls.Add(Me.label3)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.labelCopyright)
			Me.Controls.Add(Me.buttonClose)
			Me.Controls.Add(Me.pictureBox1)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
			Me.Name = "FormAbout"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
			Me.Text = "About Patterns In Action 3.5 Windows Forms"
			CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private label4 As System.Windows.Forms.Label
		Private label3 As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private label1 As System.Windows.Forms.Label
		Private labelCopyright As System.Windows.Forms.Label
		Private WithEvents buttonClose As System.Windows.Forms.Button
		Private pictureBox1 As System.Windows.Forms.PictureBox
	End Class
End Namespace