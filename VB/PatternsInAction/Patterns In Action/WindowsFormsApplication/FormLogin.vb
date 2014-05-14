Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Imports WindowsFormsPresenter
Imports WindowsFormsView

Namespace WindowsFormsApplication
	''' <summary>
	''' This is where users enter login credentials.
	''' </summary>
	''' <remarks>
	''' Valid demo values are: 
	'''   userName: debbie
	'''   password: secret123
	''' </remarks>
	Partial Public Class FormLogin
		Inherits Form
		Implements ILoginView
		' The Presenter
		Private _loginPresenter As LoginPresenter
		Private _cancelClose As Boolean

		''' <summary>
		''' Default contructor of FormLogin.
		''' </summary>
		Public Sub New()
			InitializeComponent()
			AddHandler Me.Closing, AddressOf FormLogin_Closing

			_loginPresenter = New LoginPresenter(Me)
		End Sub

		''' <summary>
		''' Gets the username.
		''' </summary>
		Public ReadOnly Property UserName() As String Implements ILoginView.UserName
			Get
				Return textBoxUserName.Text.Trim()
			End Get
		End Property

		''' <summary>
		''' Gets the password.
		''' </summary>
		Public ReadOnly Property Password() As String Implements ILoginView.Password
			Get
				Return textBoxPassword.Text.Trim()
			End Get
		End Property


		''' <summary>
		''' Performs login and upson success closes dialog.
		''' </summary>
		Private Sub buttonOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles buttonOK.Click
			Try
				_loginPresenter.Login()
				Me.Close()
			Catch ex As ApplicationException
				MessageBox.Show(ex.Message, "Login failed")
				_cancelClose = True
			End Try
		End Sub

		''' <summary>
		''' Cancel was requested. Now closes dialog.
		''' </summary>
		Private Sub buttonCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles buttonCancel.Click
			Me.Close()
		End Sub

		''' <summary>
		''' Displays valid demo credentials
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub linkLabelValid_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles linkLabelValid.LinkClicked
			MessageBox.Show("You can use the following credentials: " & Constants.vbCrLf & Constants.vbCrLf & "    UserName:    debbie " & Constants.vbCrLf & "    PassWord:    secret123", "Login Credentials")
		End Sub

		''' <summary>
		''' Provides opportunity to cancel the dialog close.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub FormLogin_Closing(ByVal sender As Object, ByVal e As CancelEventArgs)
			e.Cancel = _cancelClose
			_cancelClose = False
		End Sub
	End Class
End Namespace
