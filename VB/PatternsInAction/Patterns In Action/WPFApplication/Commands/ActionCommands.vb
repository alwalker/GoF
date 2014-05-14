Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Imports System.Windows.Input

Namespace WPFApplication.Commands
	''' <summary>
	''' Class that holds static commands  
	''' </summary>
	Public Class ActionCommands
		' Static routed commands
        Private Shared _loginCommand As RoutedUICommand
        Public Shared Property LoginCommand() As RoutedUICommand
            Get
                Return _loginCommand
            End Get
            Private Set(ByVal value As RoutedUICommand)
                _loginCommand = value
            End Set
        End Property
        Private Shared _logoutCommand As RoutedUICommand
        Public Shared Property LogoutCommand() As RoutedUICommand
            Get
                Return _logoutCommand
            End Get
            Private Set(ByVal value As RoutedUICommand)
                _logoutCommand = value
            End Set
        End Property
        Private Shared _exitCommand As RoutedUICommand
        Public Shared Property ExitCommand() As RoutedUICommand
            Get
                Return _exitCommand
            End Get
            Private Set(ByVal value As RoutedUICommand)
                _exitCommand = value
            End Set
        End Property

        Private Shared _addCommand As RoutedUICommand
        Public Shared Property AddCommand() As RoutedUICommand
            Get
                Return _addCommand
            End Get
            Private Set(ByVal value As RoutedUICommand)
                _addCommand = value
            End Set
        End Property
        Private Shared _editCommand As RoutedUICommand
        Public Shared Property EditCommand() As RoutedUICommand
            Get
                Return _editCommand
            End Get
            Private Set(ByVal value As RoutedUICommand)
                _editCommand = value
            End Set
        End Property
        Private Shared _deleteCommand As RoutedUICommand
		Public Shared Property DeleteCommand() As RoutedUICommand
			Get
                Return _deleteCommand
			End Get
			Private Set(ByVal value As RoutedUICommand)
                _deleteCommand = value
			End Set
		End Property
        Private Shared _viewOrdersCommand As RoutedUICommand
        Public Shared Property ViewOrdersCommand() As RoutedUICommand
            Get
                Return _viewOrdersCommand
            End Get
            Private Set(ByVal value As RoutedUICommand)
                _viewOrdersCommand = value
            End Set
        End Property

        Private Shared _howDoICommand As RoutedUICommand
        Public Shared Property HowDoICommand() As RoutedUICommand
            Get
                Return _howDoICommand
            End Get
            Private Set(ByVal value As RoutedUICommand)
                _howDoICommand = value
            End Set
        End Property
        Private Shared _indexCommand As RoutedUICommand
        Public Shared Property IndexCommand() As RoutedUICommand
            Get
                Return _indexCommand
            End Get
            Private Set(ByVal value As RoutedUICommand)
                _indexCommand = value
            End Set
        End Property
        Private Shared _aboutCommand As RoutedUICommand
        Public Shared Property AboutCommand() As RoutedUICommand
            Get
                Return _aboutCommand
            End Get
            Private Set(ByVal value As RoutedUICommand)
                _aboutCommand = value
            End Set
        End Property

		''' <summary>
		''' Static constructor. 
		''' Creates several Routed UI commands with and without shortcut keys.
		''' </summary>
		Shared Sub New()
			' Initialize static commands
			LoginCommand = MakeRoutedUICommand("Login", Key.I, "Ctrl+I")
			LogoutCommand = MakeRoutedUICommand("Logout", Key.O, "Ctrl+O")
			ExitCommand = MakeRoutedUICommand("Exit")

			AddCommand = MakeRoutedUICommand("Add", Key.A, "Ctrl+A")
			EditCommand = MakeRoutedUICommand("Edit", Key.E, "Ctrl+E")
			DeleteCommand = MakeRoutedUICommand("Delete", Key.Delete, "Del")

			ViewOrdersCommand = MakeRoutedUICommand("View Orders")

			HowDoICommand = MakeRoutedUICommand("How Do I", Key.H, "Ctrl+D")
			IndexCommand = MakeRoutedUICommand("Index", Key.N, "Ctrl+N")
			AboutCommand = MakeRoutedUICommand("About")
		End Sub

		''' <summary>
		''' Creates a routed command instance without shortcut key.
		''' </summary>
		''' <param name="name">Given name.</param>
		''' <returns>The routed UI command.</returns>
		Private Shared Function MakeRoutedUICommand(ByVal name As String) As RoutedUICommand
			Return New RoutedUICommand(name, name, GetType(ActionCommands))
		End Function

		''' <summary>
		''' Creates a routed command instance with a shortcut key.
		''' </summary>
		''' <param name="name">Given name.</param>
		''' <param name="key">Shortcut key.</param>
		''' <param name="displayString">Display string.</param>
		''' <returns>The Routed UI command.</returns>
		Private Shared Function MakeRoutedUICommand(ByVal name As String, ByVal key As Key, ByVal displayString As String) As RoutedUICommand
			Dim gestures As New InputGestureCollection()
			gestures.Add(New KeyGesture(key, ModifierKeys.Control, displayString))

			Return New RoutedUICommand(name, name, GetType(ActionCommands), gestures)
		End Function
	End Class
End Namespace
