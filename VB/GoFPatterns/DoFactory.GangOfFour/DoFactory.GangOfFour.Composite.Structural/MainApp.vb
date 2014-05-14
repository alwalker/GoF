Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Composite.Structural
	''' <summary>
	''' MainApp startup class for Structural 
	''' Composite Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Create a tree structure
            Dim root As New Composite("root")
            root.Add(New Leaf("Leaf A"))
            root.Add(New Leaf("Leaf B"))

            Dim comp As New Composite("Composite X")
            comp.Add(New Leaf("Leaf XA"))
            comp.Add(New Leaf("Leaf XB"))

            root.Add(comp)
            root.Add(New Leaf("Leaf C"))

            ' Add and remove a leaf
            Dim leaf As New Leaf("Leaf D")
            root.Add(leaf)
            root.Remove(leaf)

            ' Recursively display tree
            root.Display(1)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Component' abstract class
	''' </summary>
	Friend MustInherit Class Component
		Protected name As String

		' Constructor
		Public Sub New(ByVal name As String)
			Me.name = name
		End Sub

		Public MustOverride Sub Add(ByVal c As Component)
		Public MustOverride Sub Remove(ByVal c As Component)
		Public MustOverride Sub Display(ByVal depth As Integer)
	End Class

	''' <summary>
	''' The 'Composite' class
	''' </summary>
	Friend Class Composite
		Inherits Component
		Private _children As List(Of Component) = New List(Of Component)()

		' Constructor
		Public Sub New(ByVal name As String)
			MyBase.New(name)
		End Sub

		Public Overrides Sub Add(ByVal component As Component)
			_children.Add(component)
		End Sub

		Public Overrides Sub Remove(ByVal component As Component)
			_children.Remove(component)
		End Sub

		Public Overrides Sub Display(ByVal depth As Integer)
			Console.WriteLine(New String("-"c, depth) & name)

			' Recursively display child nodes
			For Each component As Component In _children
				component.Display(depth + 2)
			Next component
		End Sub
	End Class

	''' <summary>
	''' The 'Leaf' class
	''' </summary>
	Friend Class Leaf
		Inherits Component
		' Constructor
		Public Sub New(ByVal name As String)
			MyBase.New(name)
		End Sub

		Public Overrides Sub Add(ByVal c As Component)
			Console.WriteLine("Cannot add to a leaf")
		End Sub

		Public Overrides Sub Remove(ByVal c As Component)
			Console.WriteLine("Cannot remove from a leaf")
		End Sub

		Public Overrides Sub Display(ByVal depth As Integer)
			Console.WriteLine(New String("-"c, depth) & name)
		End Sub
	End Class
End Namespace
