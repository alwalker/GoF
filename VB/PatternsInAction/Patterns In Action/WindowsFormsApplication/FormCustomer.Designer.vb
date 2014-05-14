Imports Microsoft.VisualBasic
Imports System
Namespace WindowsFormsApplication
	Partial Public Class FormCustomer
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
			Me.label3 = New System.Windows.Forms.Label()
			Me.label2 = New System.Windows.Forms.Label()
			Me.label1 = New System.Windows.Forms.Label()
			Me.textBoxCountry = New System.Windows.Forms.TextBox()
			Me.textBoxCity = New System.Windows.Forms.TextBox()
			Me.textBoxCompany = New System.Windows.Forms.TextBox()
			Me.buttonCancel = New System.Windows.Forms.Button()
			Me.buttonSave = New System.Windows.Forms.Button()
			Me.SuspendLayout()
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(40, 82)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(46, 13)
			Me.label3.TabIndex = 15
			Me.label3.Text = "Country:"
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(40, 53)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(27, 13)
			Me.label2.TabIndex = 14
			Me.label2.Text = "City:"
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(40, 28)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(54, 13)
			Me.label1.TabIndex = 13
			Me.label1.Text = "Company:"
			' 
			' textBoxCountry
			' 
			Me.textBoxCountry.Location = New System.Drawing.Point(100, 79)
			Me.textBoxCountry.Name = "textBoxCountry"
			Me.textBoxCountry.Size = New System.Drawing.Size(228, 20)
			Me.textBoxCountry.TabIndex = 10
			' 
			' textBoxCity
			' 
			Me.textBoxCity.Location = New System.Drawing.Point(100, 50)
			Me.textBoxCity.Name = "textBoxCity"
			Me.textBoxCity.Size = New System.Drawing.Size(228, 20)
			Me.textBoxCity.TabIndex = 9
			' 
			' textBoxCompany
			' 
			Me.textBoxCompany.Location = New System.Drawing.Point(100, 22)
			Me.textBoxCompany.Name = "textBoxCompany"
			Me.textBoxCompany.Size = New System.Drawing.Size(228, 20)
			Me.textBoxCompany.TabIndex = 8
			' 
			' buttonCancel
			' 
			Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
			Me.buttonCancel.Location = New System.Drawing.Point(209, 121)
			Me.buttonCancel.Name = "buttonCancel"
			Me.buttonCancel.Size = New System.Drawing.Size(75, 23)
			Me.buttonCancel.TabIndex = 12
			Me.buttonCancel.Text = "Cancel"
			Me.buttonCancel.UseVisualStyleBackColor = True
			' 
			' buttonSave
			' 
			Me.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK
			Me.buttonSave.Location = New System.Drawing.Point(118, 121)
			Me.buttonSave.Name = "buttonSave"
			Me.buttonSave.Size = New System.Drawing.Size(75, 23)
			Me.buttonSave.TabIndex = 11
			Me.buttonSave.Text = "Save"
			Me.buttonSave.UseVisualStyleBackColor = True
'			Me.buttonSave.Click += New System.EventHandler(Me.buttonSave_Click);
			' 
			' FormCustomer
			' 
			Me.AcceptButton = Me.buttonSave
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.CancelButton = Me.buttonCancel
			Me.ClientSize = New System.Drawing.Size(368, 167)
			Me.Controls.Add(Me.label3)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.textBoxCountry)
			Me.Controls.Add(Me.textBoxCity)
			Me.Controls.Add(Me.textBoxCompany)
			Me.Controls.Add(Me.buttonCancel)
			Me.Controls.Add(Me.buttonSave)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
			Me.Name = "FormCustomer"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
			Me.Text = "Customer"
'			Me.Load += New System.EventHandler(Me.FormCustomer_Load);
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private label3 As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private label1 As System.Windows.Forms.Label
		Private textBoxCountry As System.Windows.Forms.TextBox
		Private textBoxCity As System.Windows.Forms.TextBox
		Private textBoxCompany As System.Windows.Forms.TextBox
		Private buttonCancel As System.Windows.Forms.Button
		Private WithEvents buttonSave As System.Windows.Forms.Button
	End Class
End Namespace