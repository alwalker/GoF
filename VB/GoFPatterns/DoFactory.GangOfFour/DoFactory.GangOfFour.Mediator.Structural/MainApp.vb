Imports Microsoft.VisualBasic
Imports System

Namespace DoFactory.GangOfFour.Mediator.Structural
	''' <summary>
	''' MainApp startup class for Structural 
	''' Mediator Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            Dim m As New ConcreteMediator()

            Dim c1 As New ConcreteColleague1(m)
            Dim c2 As New ConcreteColleague2(m)

            m.Colleague1 = c1
            m.Colleague2 = c2

            c1.Send("How are you?")
            c2.Send("Fine, thanks")

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Mediator' abstract class
	''' </summary>
	Friend MustInherit Class Mediator
		Public MustOverride Sub Send(ByVal message As String, ByVal colleague As Colleague)
	End Class

	''' <summary>
	''' The 'ConcreteMediator' class
	''' </summary>
	Friend Class ConcreteMediator
		Inherits Mediator
		Private _colleague1 As ConcreteColleague1
		Private _colleague2 As ConcreteColleague2

		Public WriteOnly Property Colleague1() As ConcreteColleague1
			Set(ByVal value As ConcreteColleague1)
				_colleague1 = value
			End Set
		End Property

		Public WriteOnly Property Colleague2() As ConcreteColleague2
			Set(ByVal value As ConcreteColleague2)
				_colleague2 = value
			End Set
		End Property

		Public Overrides Sub Send(ByVal message As String, ByVal colleague As Colleague)
			If colleague Is _colleague1 Then
				_colleague2.Notify(message)
			Else
				_colleague1.Notify(message)
			End If
		End Sub
	End Class

	''' <summary>
	''' The 'Colleague' abstract class
	''' </summary>
	Friend MustInherit Class Colleague
		Protected mediator As Mediator

		' Constructor
		Public Sub New(ByVal mediator As Mediator)
			Me.mediator = mediator
		End Sub
	End Class

	''' <summary>
	''' A 'ConcreteColleague' class
	''' </summary>
	Friend Class ConcreteColleague1
		Inherits Colleague
		' Constructor
		Public Sub New(ByVal mediator As Mediator)
			MyBase.New(mediator)
		End Sub

		Public Sub Send(ByVal message As String)
			mediator.Send(message, Me)
		End Sub

		Public Sub Notify(ByVal message As String)
			Console.WriteLine("Colleague1 gets message: " & message)
		End Sub
	End Class

	''' <summary>
	''' A 'ConcreteColleague' class
	''' </summary>
	Friend Class ConcreteColleague2
		Inherits Colleague
		' Constructor
		Public Sub New(ByVal mediator As Mediator)
			MyBase.New(mediator)
		End Sub

		Public Sub Send(ByVal message As String)
			mediator.Send(message, Me)
		End Sub

		Public Sub Notify(ByVal message As String)
			Console.WriteLine("Colleague2 gets message: " & message)
		End Sub
	End Class
End Namespace
