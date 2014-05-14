Imports Microsoft.VisualBasic
Imports System

' Replacement for the Java 'Swing example'
Namespace DoFactory.HeadFirst.Observer.DotNet
	Friend Class DotNetObserverExample
        Shared Sub Main()

            ' Create listeners
            Dim angel As New ActionListener("Angel")
            Dim devil As New ActionListener("Devil")

            ' Create Button and attach listeners
            Dim button As New Button("Click Me")
            button.Attach(angel)
            button.Attach(devil)

            ' Simulate clicks on button
            button.Push(1, 3)
            button.Push(5, 4)
            button.Push(8, 5)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	#Region "EventArgs"

	' Custom event arguments
	Public Class ClickEventArgs
		Inherits EventArgs
		Private privateX As Integer
		Public Property X() As Integer
			Get
				Return privateX
			End Get
			Private Set(ByVal value As Integer)
				privateX = value
			End Set
		End Property
		Private privateY As Integer
		Public Property Y() As Integer
			Get
				Return privateY
			End Get
			Private Set(ByVal value As Integer)
				privateY = value
			End Set
		End Property

		' Constructor
		Public Sub New(ByVal x As Integer, ByVal y As Integer)
			Me.X = x
			Me.Y = y
		End Sub
	End Class

	#End Region

	#Region "Controls"

	' Base class for UI controls

	Friend MustInherit Class Control
		Protected text As String

		' Constructor
		Public Sub New(ByVal text As String)
			Me.text = text
		End Sub

		' Event
		Public Event Click As EventHandler(Of ClickEventArgs)

		' Invoke the Click  event
		Public Overridable Sub OnClick(ByVal e As ClickEventArgs)
			RaiseEvent Click(Me, e)
		End Sub

		Public Sub Attach(ByVal listener As ActionListener)
            AddHandler Click, AddressOf listener.Update
		End Sub

		Public Sub Detach(ByVal listener As ActionListener)
            RemoveHandler Click, AddressOf listener.Update
		End Sub

		' Use this method to simulate push (click) events
		Public Sub Push(ByVal x As Integer, ByVal y As Integer)
			OnClick(New ClickEventArgs(x, y))
			Console.WriteLine("")
		End Sub
	End Class

	' Button control

	Friend Class Button
		Inherits Control
		' Constructor
		Public Sub New(ByVal text As String)
			MyBase.New(text)
		End Sub
	End Class

	#End Region

	#Region "ActionListener"

	Friend Interface IActionListener
		Sub Update(ByVal sender As Object, ByVal e As ClickEventArgs)
	End Interface

	Friend Class ActionListener
		Implements IActionListener
		Private _name As String

		' Constructor
		Public Sub New(ByVal name As String)
			Me._name = name
		End Sub

		Public Sub Update(ByVal sender As Object, ByVal e As ClickEventArgs) Implements IActionListener.Update
			Console.WriteLine("Notified {0} of click at ({1},{2})", _name, e.X, e.Y)
		End Sub
	End Class
	#End Region
End Namespace
