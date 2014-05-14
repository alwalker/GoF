Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Flyweight.NETOptimized
	''' <summary>
	''' MainApp startup class for .NET optimized 
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
                Dim character As Character = factory(c)
                pointSize += 1
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

		' Character indexer
		Default Public ReadOnly Property Item(ByVal key As Char) As Character
			Get
				' Uses "lazy initialization" -- i.e. only create when needed.
				Dim character As Character = Nothing
				If _characters.ContainsKey(key) Then
					character = _characters(key)
				Else
					' Instead of a case statement with 26 cases (characters).
					' First, get qualified class name, then dynamically create instance 
					Dim name As String = Me.GetType().Namespace + "." & "Character" & key.ToString()
					character = CType(Activator.CreateInstance (Type.GetType(name)), Character)
				End If

				Return character
			End Get
		End Property
	End Class

	''' <summary>
	''' The 'Flyweight' class
	''' </summary>
	Friend Class Character
		Protected symbol As Char
		Protected width As Integer
		Protected height As Integer
		Protected ascent As Integer
		Protected descent As Integer

		Public Sub Display(ByVal pointSize As Integer)
			Console.WriteLine(Me.symbol & " (pointsize " & pointSize & ")")
		End Sub

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
	End Class
End Namespace
