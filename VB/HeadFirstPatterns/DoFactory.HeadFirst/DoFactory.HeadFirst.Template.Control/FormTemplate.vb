Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data

Namespace DoFactory.HeadFirst.Template.Control
	''' <summary>
	''' Summary description for Form1.
	''' </summary>
	Public Class Form1
		Inherits System.Windows.Forms.Form
		Private WithEvents buttonTemplate As System.Windows.Forms.Button
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing

		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()

			'
			' TODO: Add any constructor code after InitializeComponent call
			'
		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.buttonTemplate = New System.Windows.Forms.Button()
			Me.SuspendLayout()
			' 
			' buttonTemplate
			' 
			Me.buttonTemplate.Location = New System.Drawing.Point(45, 30)
			Me.buttonTemplate.Name = "buttonTemplate"
			Me.buttonTemplate.Size = New System.Drawing.Size(126, 32)
			Me.buttonTemplate.TabIndex = 0
			Me.buttonTemplate.Text = "Template"
'			Me.buttonTemplate.Click += New System.EventHandler(Me.buttonTemplate_Click);
'			Me.buttonTemplate.MouseEnter += New System.EventHandler(Me.buttonTemplate_MouseEnter);
'			Me.buttonTemplate.MouseLeave += New System.EventHandler(Me.buttonTemplate_MouseLeave);
			' 
			' Form1
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(206, 103)
			Me.Controls.Add(Me.buttonTemplate)
			Me.Name = "Form1"
			Me.Text = "Template Method"
			Me.ResumeLayout(False)

		End Sub
		#End Region

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread> _
		Shared Sub Main()
			Application.Run(New Form1())
		End Sub

		Private Sub buttonTemplate_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonTemplate.MouseLeave
			Me.buttonTemplate.BackColor = System.Drawing.Color.Gold
		End Sub

		Private Sub buttonTemplate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonTemplate.Click
			Console.Beep(2000,15)
		End Sub

		Private Sub buttonTemplate_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonTemplate.MouseEnter
			Me.buttonTemplate.BackColor = System.Drawing.Color.Red
		End Sub
	End Class
End Namespace
