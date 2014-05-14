Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Mediator.RealWorld
	''' <summary>
	''' MainApp startup class for Real-World 
	''' Mediator Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Create chatroom
            Dim chatroom As New Chatroom()

            ' Create participants and register them
            Dim George As Participant = New Beatle("George")
            Dim Paul As Participant = New Beatle("Paul")
            Dim Ringo As Participant = New Beatle("Ringo")
            Dim John As Participant = New Beatle("John")
            Dim Yoko As Participant = New NonBeatle("Yoko")

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
	''' The 'Mediator' abstract class
	''' </summary>
	Friend MustInherit Class AbstractChatroom
		Public MustOverride Sub Register(ByVal participant As Participant)
		Public MustOverride Sub Send(ByVal from As String, ByVal [to] As String, ByVal message As String)
	End Class

	''' <summary>
	''' The 'ConcreteMediator' class
	''' </summary>
	Friend Class Chatroom
		Inherits AbstractChatroom
		Private _participants As Dictionary(Of String,Participant) = New Dictionary(Of String,Participant)()

		Public Overrides Sub Register(ByVal participant As Participant)
			If (Not _participants.ContainsValue(participant)) Then
				_participants(participant.Name) = participant
			End If

			participant.Chatroom = Me
		End Sub

		Public Overrides Sub Send(ByVal from As String, ByVal [to] As String, ByVal message As String)
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
		Private _chatroom As Chatroom
		Private _name As String

		' Constructor
		Public Sub New(ByVal name As String)
			Me._name = name
		End Sub

		' Gets participant name
		Public ReadOnly Property Name() As String
			Get
				Return _name
			End Get
		End Property

		' Gets chatroom
		Public Property Chatroom() As Chatroom
			Set(ByVal value As Chatroom)
				_chatroom = value
			End Set
			Get
				Return _chatroom
			End Get
		End Property

		' Sends message to given participant
		Public Sub Send(ByVal [to] As String, ByVal message As String)
			_chatroom.Send(_name, [to], message)
		End Sub

		' Receives message from given participant
		Public Overridable Sub Receive(ByVal from As String, ByVal message As String)
			Console.WriteLine("{0} to {1}: '{2}'", From, Name, message)
		End Sub
	End Class

	''' <summary>
	''' A 'ConcreteColleague' class
	''' </summary>
	Friend Class Beatle
		Inherits Participant
		' Constructor
		Public Sub New(ByVal name As String)
			MyBase.New(name)
		End Sub

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
		' Constructor
		Public Sub New(ByVal name As String)
			MyBase.New(name)
		End Sub

		Public Overrides Sub Receive(ByVal from As String, ByVal message As String)
			Console.Write("To a non-Beatle: ")
			MyBase.Receive(From, message)
		End Sub
	End Class
End Namespace
