Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Reflection

Namespace DoFactory.GangOfFour.Visitor.NETOptimized
	''' <summary>
	''' MainApp startup class for .NET optimized 
	''' Visitor Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Setup employee collection
            Dim e As New Employees()
            e.Attach(New Clerk())
            e.Attach(New Director())
            e.Attach(New President())

            ' Employees are 'visited'
            e.Accept(New IncomeVisitor())
            e.Accept(New VacationVisitor())

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Visitor' abstract class
	''' </summary>
	Public MustInherit Class Visitor
		' Use reflection to see if the Visitor has a method 
		' named Visit with the appropriate parameter type 
		' (i.e. a specific Employee). If so, invoke it.
		Public Sub ReflectiveVisit(ByVal element As IElement)
			Dim types() As Type = { CType(element, Object).GetType() }
			Dim mi As MethodInfo = Me.GetType().GetMethod("Visit", types)

			If mi IsNot Nothing Then
				mi.Invoke(Me, New Object() { element })
			End If
		End Sub
	End Class

	''' <summary>
	''' A 'ConcreteVisitor' class
	''' </summary>
	Friend Class IncomeVisitor
		Inherits Visitor
		' Visit clerk
		Public Sub Visit(ByVal clerk As Clerk)
			DoVisit(clerk)
		End Sub

		' Visit director
		Public Sub Visit(ByVal director As Director)
			DoVisit(director)
		End Sub

		Private Sub DoVisit(ByVal element As IElement)
			Dim employee As Employee = TryCast(element, Employee)

			' Provide 10% pay raise
			employee.Income *= 1.10
			Console.WriteLine("{0} {1}'s new income: {2:C}", employee.GetType().Name, employee.Name, employee.Income)
		End Sub
	End Class

	''' <summary>
	''' A 'ConcreteVisitor' class
	''' </summary>
	Friend Class VacationVisitor
		Inherits Visitor
		' Visit director
		Public Sub Visit(ByVal director As Director)
			DoVisit(director)
		End Sub

		Private Sub DoVisit(ByVal element As IElement)
			Dim employee As Employee = TryCast(element, Employee)

			' Provide 3 extra vacation days
			employee.VacationDays += 3
			Console.WriteLine("{0} {1}'s new vacation days: {2}", employee.GetType().Name, employee.Name, employee.VacationDays)
		End Sub
	End Class

	''' <summary>
	''' The 'Element' interface
	''' </summary>
	Public Interface IElement
		Sub Accept(ByVal visitor As Visitor)
	End Interface

	''' <summary>
	''' The 'ConcreteElement' class
	''' </summary>
	Friend Class Employee
		Implements IElement
		' Constructor
		Public Sub New(ByVal name As String, ByVal income As Double, ByVal vacationDays As Integer)
			Me.Name = name
			Me.Income = income
			Me.VacationDays = vacationDays
		End Sub

		' Gets or sets name
        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

		' Gets or set income
        Private _income As Double
        Public Property Income() As Double
            Get
                Return _income
            End Get
            Set(ByVal value As Double)
                _income = value
            End Set
        End Property

		' Gets or sets vacation days
        Private _vacationDays As Integer
        Public Property VacationDays() As Integer
            Get
                Return _vacationDays
            End Get
            Set(ByVal value As Integer)
                _vacationDays = value
            End Set
        End Property

		Public Overridable Sub Accept(ByVal visitor As Visitor) Implements IElement.Accept
			visitor.ReflectiveVisit(Me)
		End Sub
	End Class

	''' <summary>
	''' The 'ObjectStructure' class
	''' </summary>
	Friend Class Employees
		Inherits List(Of Employee)
		Public Sub Attach(ByVal employee As Employee)
			Add(employee)
		End Sub

		Public Sub Detach(ByVal employee As Employee)
			Remove(employee)
		End Sub

		Public Sub Accept(ByVal visitor As Visitor)
            ' Iterate over all employees and accept visitor
            For Each employee As Employee In Me
                employee.Accept(visitor)
            Next employee

            Console.WriteLine()
        End Sub
	End Class

	' Three employee types

	Friend Class Clerk
		Inherits Employee
		' Constructor
		Public Sub New()
			MyBase.New("Hank", 25000.0, 14)
		End Sub
	End Class

	Friend Class Director
		Inherits Employee
		' Constructor
		Public Sub New()
			MyBase.New("Elly", 35000.0, 16)
		End Sub
	End Class

	Friend Class President
		Inherits Employee
		' Constructor
		Public Sub New()
			MyBase.New("Dick", 45000.0, 21)
		End Sub
	End Class
End Namespace
