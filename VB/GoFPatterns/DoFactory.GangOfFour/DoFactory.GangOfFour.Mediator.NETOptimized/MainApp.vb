Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Mediator.NETOptimized
	''' <summary>
	''' MainApp startup class for .NET optimized 
	''' Mediator Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Create chatroom participants
            Dim George As Participant = New Beatle With {.Name = "George"}
            Dim Paul As Participant = New Beatle With {.Name = "Paul"}
            Dim Ringo As Participant = New Beatle With {.Name = "Ringo"}
            Dim John As Participant = New Beatle With {.Name = "John"}
            Dim Yoko As Participant = New NonBeatle With {.Name = "Yoko"}

            ' Create chatroom and register participants
            Dim chatroom As New Chatroom()
            chatroom.Register(George)
            chatroom.Register(Paul)
            chatroom.Register(Ringo)
            chatroom.Register(John)
            chatroom.Register(Yoko)

            ' Chatting participants
            Yoko.Send("John", "Hi John!")
            Paul.Send("Ringo", "All you need is love")
            Ringo.Send("George", "My sweet Lord")
            Paul.Send("John", "Can't buy me love")
            John.Send("Yoko", "My sweet love")

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Mediator' interface
	''' </summary>
	Friend Interface IChatroom
		Sub Register(ByVal participant As Participant)
		Sub Send(ByVal from As String, ByVal [to] As String, ByVal message As String)
	End Interface

	''' <summary>
	''' The 'ConcreteMediator' class
	''' </summary>
	Friend Class Chatroom
		Implements IChatroom
		Private _participants As Dictionary(Of String, Participant) = New Dictionary(Of String, Participant)()

		Public Sub Register(ByVal participant As Participant) Implements IChatroom.Register
			If (Not _participants.ContainsKey(participant.Name)) Then
				_participants.Add(participant.Name, participant)
			End If

			participant.Chatroom = Me
		End Sub

		Public Sub Send(ByVal from As String, ByVal [to] As String, ByVal message As String) Implements IChatroom.Send
			Dim participant As Participant = _participants([to])
			If participant IsNot Nothing Then
				participant.Receive(From, message)
			End If
		End Sub
	End Class

	''' <summary>
	''' The 'AbstractColleague' class
	''' </summary>
	Friend Class Participant
		' Gets or sets participant name
        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

		' Gets or sets chatroom
        Private _chatroom As Chatroom
        Public Property Chatroom() As Chatroom
            Get
                Return _chatroom
            End Get
            Set(ByVal value As Chatroom)
                _chatroom = value
            End Set
        End Property

		' Send a message to given participant
		Public Sub Send(ByVal [to] As String, ByVal message As String)
			Chatroom.Send(Name, [to], message)
		End Sub

		' Receive message from participant
		Public Overridable Sub Receive(ByVal from As String, ByVal message As String)
			Console.WriteLine("{0} to {1}: '{2}'", From, Name, message)
		End Sub
	End Class

	''' <summary>
	''' A 'ConcreteColleague' class
	''' </summary>
	Friend Class Beatle
		Inherits Participant
		Public Overrides Sub Receive(ByVal from As String, ByVal message As String)
			Console.Write("To a Beatle: ")
			MyBase.Receive(From, message)
		End Sub
	End Class

	''' <summary>
	''' A 'ConcreteColleague' class
	''' </summary>
	Friend Class NonBeatle
		Inherits Participant
		Public Overrides Sub Receive(ByVal from As String, ByVal message As String)
			Console.Write("To a non-Beatle: ")
			MyBase.Receive(From, message)
		End Sub
	End Class
End Namespace
