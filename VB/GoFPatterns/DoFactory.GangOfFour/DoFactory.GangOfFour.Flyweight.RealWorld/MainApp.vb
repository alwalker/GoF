Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Flyweight.RealWorld
	''' <summary>
	''' MainApp startup class for Real-World 
	''' Flyweight Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Build a document with text
            Dim document As String = "AAZZBBZB"
            Dim chars() As Char = document.ToCharArray()

            Dim factory As New CharacterFactory()

            ' extrinsic state
            Dim pointSize As Integer = 10

            ' For each character use a flyweight object
            For Each c As Char In chars
                pointSize += 1
                Dim character As Character = factory.GetCharacter(c)
                character.Display(pointSize)
            Next c

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'FlyweightFactory' class
	''' </summary>
	Friend Class CharacterFactory
		Private _characters As Dictionary(Of Char, Character) = New Dictionary(Of Char, Character)()

		Public Function GetCharacter(ByVal key As Char) As Character

            ' Uses "lazy initialization"
			Dim character As Character = Nothing
			If _characters.ContainsKey(key) Then
				character = _characters(key)
			Else
				Select Case key
					Case "A"c
						character = New CharacterA()
					Case "B"c
						character = New CharacterB()
					'...
					Case "Z"c
						character = New CharacterZ()
				End Select
				_characters.Add(key, character)
			End If
			Return character
		End Function
	End Class

	''' <summary>
	''' The 'Flyweight' abstract class
	''' </summary>
	Friend MustInherit Class Character
		Protected symbol As Char
		Protected width As Integer
		Protected height As Integer
		Protected ascent As Integer
		Protected descent As Integer
		Protected pointSize As Integer

		Public MustOverride Sub Display(ByVal pointSize As Integer)
	End Class

	''' <summary>
	''' A 'ConcreteFlyweight' class
	''' </summary>
	Friend Class CharacterA
		Inherits Character
		' Constructor
		Public Sub New()
			Me.symbol = "A"c
			Me.height = 100
			Me.width = 120
			Me.ascent = 70
			Me.descent = 0
		End Sub

		Public Overrides Sub Display(ByVal pointSize As Integer)
			Me.pointSize = pointSize
			Console.WriteLine(Me.symbol & " (pointsize " & Me.pointSize & ")")
		End Sub
	End Class

	''' <summary>
	''' A 'ConcreteFlyweight' class
	''' </summary>
	Friend Class CharacterB
		Inherits Character
		' Constructor
		Public Sub New()
			Me.symbol = "B"c
			Me.height = 100
			Me.width = 140
			Me.ascent = 72
			Me.descent = 0
		End Sub

		Public Overrides Sub Display(ByVal pointSize As Integer)
			Me.pointSize = pointSize
			Console.WriteLine(Me.symbol & " (pointsize " & Me.pointSize & ")")
		End Sub

	End Class

	' ... C, D, E, etc.

	''' <summary>
	''' A 'ConcreteFlyweight' class
	''' </summary>
	Friend Class CharacterZ
		Inherits Character
		' Constructor
		Public Sub New()
			Me.symbol = "Z"c
			Me.height = 100
			Me.width = 100
			Me.ascent = 68
			Me.descent = 0
		End Sub

		Public Overrides Sub Display(ByVal pointSize As Integer)
			Me.pointSize = pointSize
			Console.WriteLine(Me.symbol & " (pointsize " & Me.pointSize & ")")
		End Sub
	End Class
End Namespace
