Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Visitor.RealWorld
	''' <summary>
	''' MainApp startup class for Real-World 
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
	''' The 'Visitor' interface
	''' </summary>
	Friend Interface IVisitor
		Sub Visit(ByVal element As Element)
	End Interface

	''' <summary>
	''' A 'ConcreteVisitor' class
	''' </summary>
	Friend Class IncomeVisitor
		Implements IVisitor
		Public Sub Visit(ByVal element As Element) Implements IVisitor.Visit
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
		Implements IVisitor
		Public Sub Visit(ByVal element As Element) Implements IVisitor.Visit
			Dim employee As Employee = TryCast(element, Employee)

			' Provide 3 extra vacation days
			Console.WriteLine("{0} {1}'s new vacation days: {2}", employee.GetType().Name, employee.Name, employee.VacationDays)
		End Sub
	End Class

	''' <summary>
	''' The 'Element' abstract class
	''' </summary>
	Friend MustInherit Class Element
		Public MustOverride Sub Accept(ByVal visitor As IVisitor)
	End Class

	''' <summary>
	''' The 'ConcreteElement' class
	''' </summary>
	Friend Class Employee
		Inherits Element
		Private _name As String
		Private _income As Double
		Private _vacationDays As Integer

		' Constructor
		Public Sub New(ByVal name As String, ByVal income As Double, ByVal vacationDays As Integer)
			Me._name = name
			Me._income = income
			Me._vacationDays = vacationDays
		End Sub

		' Gets or sets the name
		Public Property Name() As String
			Get
				Return _name
			End Get
			Set(ByVal value As String)
				_name = value
			End Set
		End Property

		' Gets or sets income
		Public Property Income() As Double
			Get
				Return _income
			End Get
			Set(ByVal value As Double)
				_income = value
			End Set
		End Property

		' Gets or sets number of vacation days
		Public Property VacationDays() As Integer
			Get
				Return _vacationDays
			End Get
			Set(ByVal value As Integer)
				_vacationDays = value
			End Set
		End Property

		Public Overrides Sub Accept(ByVal visitor As IVisitor)
			visitor.Visit(Me)
		End Sub
	End Class

	''' <summary>
	''' The 'ObjectStructure' class
	''' </summary>
	Friend Class Employees
		Private _employees As List(Of Employee) = New List(Of Employee)()

		Public Sub Attach(ByVal employee As Employee)
			_employees.Add(employee)
		End Sub

		Public Sub Detach(ByVal employee As Employee)
			_employees.Remove(employee)
		End Sub

		Public Sub Accept(ByVal visitor As IVisitor)
			For Each e As Employee In _employees
				e.Accept(visitor)
			Next e
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
