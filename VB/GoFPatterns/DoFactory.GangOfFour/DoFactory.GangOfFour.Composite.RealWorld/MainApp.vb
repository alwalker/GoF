Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Composite.RealWorld
	''' <summary>
	''' MainApp startup class for Real-World 
	''' Composite Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Create a tree structure 
            Dim root As New CompositeElement("Picture")
            root.Add(New PrimitiveElement("Red Line"))
            root.Add(New PrimitiveElement("Blue Circle"))
            root.Add(New PrimitiveElement("Green Box"))

            ' Create a branch
            Dim comp As New CompositeElement("Two Circles")
            comp.Add(New PrimitiveElement("Black Circle"))
            comp.Add(New PrimitiveElement("White Circle"))
            root.Add(comp)

            ' Add and remove a PrimitiveElement
            Dim pe As New PrimitiveElement("Yellow Line")
            root.Add(pe)
            root.Remove(pe)

            ' Recursively display nodes
            root.Display(1)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Component' Treenode
	''' </summary>
	Friend MustInherit Class DrawingElement
		Protected _name As String

		' Constructor
		Public Sub New(ByVal name As String)
			Me._name = name
		End Sub

		Public MustOverride Sub Add(ByVal d As DrawingElement)
		Public MustOverride Sub Remove(ByVal d As DrawingElement)
		Public MustOverride Sub Display(ByVal indent As Integer)
	End Class

	''' <summary>
	''' The 'Leaf' class
	''' </summary>
	Friend Class PrimitiveElement
		Inherits DrawingElement
		' Constructor
		Public Sub New(ByVal name As String)
			MyBase.New(name)
		End Sub

		Public Overrides Sub Add(ByVal c As DrawingElement)
			Console.WriteLine("Cannot add to a PrimitiveElement")
		End Sub

		Public Overrides Sub Remove(ByVal c As DrawingElement)
			Console.WriteLine("Cannot remove from a PrimitiveElement")
		End Sub

		Public Overrides Sub Display(ByVal indent As Integer)
			Console.WriteLine(New String("-"c, indent) & " " & _name)
		End Sub
	End Class

	''' <summary>
	''' The 'Composite' class
	''' </summary>
	Friend Class CompositeElement
		Inherits DrawingElement
		Private elements As List(Of DrawingElement) = New List(Of DrawingElement)()

		' Constructor
		Public Sub New(ByVal name As String)
			MyBase.New(name)
		End Sub

		Public Overrides Sub Add(ByVal d As DrawingElement)
			elements.Add(d)
		End Sub

		Public Overrides Sub Remove(ByVal d As DrawingElement)
			elements.Remove(d)
		End Sub

		Public Overrides Sub Display(ByVal indent As Integer)
			Console.WriteLine(New String("-"c, indent) & "+ " & _name)

			' Display each child element on this node
			For Each d As DrawingElement In elements
				d.Display(indent + 2)
			Next d
		End Sub
	End Class
End Namespace
