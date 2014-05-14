Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Visitor.Structural
	''' <summary>
	''' MainApp startup class for Structural 
	''' Visitor Design Pattern.
	''' </summary>
	Friend Class MainApp
        Shared Sub Main()

            ' Setup structure
            Dim o As New ObjectStructure()
            o.Attach(New ConcreteElementA())
            o.Attach(New ConcreteElementB())

            ' Create visitor objects
            Dim v1 As New ConcreteVisitor1()
            Dim v2 As New ConcreteVisitor2()

            ' Structure accepting visitors
            o.Accept(v1)
            o.Accept(v2)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Visitor' abstract class
	''' </summary>
	Friend MustInherit Class Visitor
		Public MustOverride Sub VisitConcreteElementA(ByVal concreteElementA As ConcreteElementA)
		Public MustOverride Sub VisitConcreteElementB(ByVal concreteElementB As ConcreteElementB)
	End Class

	''' <summary>
	''' A 'ConcreteVisitor' class
	''' </summary>
	Friend Class ConcreteVisitor1
		Inherits Visitor
		Public Overrides Sub VisitConcreteElementA(ByVal concreteElementA As ConcreteElementA)
			Console.WriteLine("{0} visited by {1}", concreteElementA.GetType().Name, Me.GetType().Name)
		End Sub

		Public Overrides Sub VisitConcreteElementB(ByVal concreteElementB As ConcreteElementB)
			Console.WriteLine("{0} visited by {1}", concreteElementB.GetType().Name, Me.GetType().Name)
		End Sub
	End Class

	''' <summary>
	''' A 'ConcreteVisitor' class
	''' </summary>
	Friend Class ConcreteVisitor2
		Inherits Visitor
		Public Overrides Sub VisitConcreteElementA(ByVal concreteElementA As ConcreteElementA)
			Console.WriteLine("{0} visited by {1}", concreteElementA.GetType().Name, Me.GetType().Name)
		End Sub

		Public Overrides Sub VisitConcreteElementB(ByVal concreteElementB As ConcreteElementB)
			Console.WriteLine("{0} visited by {1}", concreteElementB.GetType().Name, Me.GetType().Name)
		End Sub
	End Class

	''' <summary>
	''' The 'Element' abstract class
	''' </summary>
	Friend MustInherit Class Element
		Public MustOverride Sub Accept(ByVal visitor As Visitor)
	End Class

	''' <summary>
	''' A 'ConcreteElement' class
	''' </summary>
	Friend Class ConcreteElementA
		Inherits Element
		Public Overrides Sub Accept(ByVal visitor As Visitor)
			visitor.VisitConcreteElementA(Me)
		End Sub

		Public Sub OperationA()
		End Sub
	End Class

	''' <summary>
	''' A 'ConcreteElement' class
	''' </summary>
	Friend Class ConcreteElementB
		Inherits Element
		Public Overrides Sub Accept(ByVal visitor As Visitor)
			visitor.VisitConcreteElementB(Me)
		End Sub

		Public Sub OperationB()
		End Sub
	End Class

	''' <summary>
	''' The 'ObjectStructure' class
	''' </summary>
	Friend Class ObjectStructure
		Private _elements As List(Of Element) = New List(Of Element)()

		Public Sub Attach(ByVal element As Element)
			_elements.Add(element)
		End Sub

		Public Sub Detach(ByVal element As Element)
			_elements.Remove(element)
		End Sub

		Public Sub Accept(ByVal visitor As Visitor)
			For Each element As Element In _elements
				element.Accept(visitor)
			Next element
		End Sub
	End Class
End Namespace
