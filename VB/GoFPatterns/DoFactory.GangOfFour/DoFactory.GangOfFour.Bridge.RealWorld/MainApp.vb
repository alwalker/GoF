Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Bridge.RealWorld
	''' <summary>
	''' MainApp startup class for Real-World 
	''' Bridge Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Create RefinedAbstraction
            Dim customers As New Customers("Chicago")

            ' Set ConcreteImplementor
            customers.Data = New CustomersData()

            ' Exercise the bridge
            customers.Show()
            customers.Next()
            customers.Show()
            customers.Next()
            customers.Show()
            customers.Add("Henry Velasquez")

            customers.ShowAll()

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Abstraction' class
	''' </summary>
	Friend Class CustomersBase
		Private _dataObject As DataObject
		Protected group As String

		Public Sub New(ByVal group As String)
			Me.group = group
		End Sub

		' Property
		Public Property Data() As DataObject
			Set(ByVal value As DataObject)
				_dataObject = value
			End Set
			Get
				Return _dataObject
			End Get
		End Property

		Public Overridable Sub [Next]()
			_dataObject.NextRecord()
		End Sub

		Public Overridable Sub Prior()
			_dataObject.PriorRecord()
		End Sub

		Public Overridable Sub Add(ByVal customer As String)
			_dataObject.AddRecord(customer)
		End Sub

		Public Overridable Sub Delete(ByVal customer As String)
			_dataObject.DeleteRecord(customer)
		End Sub

		Public Overridable Sub Show()
			_dataObject.ShowRecord()
		End Sub

		Public Overridable Sub ShowAll()
			Console.WriteLine("Customer Group: " & group)
			_dataObject.ShowAllRecords()
		End Sub
	End Class

	''' <summary>
	''' The 'RefinedAbstraction' class
	''' </summary>
	Friend Class Customers
		Inherits CustomersBase
		' Constructor
		Public Sub New(ByVal group As String)
			MyBase.New(group)
		End Sub

		Public Overrides Sub ShowAll()
			' Add separator lines
			Console.WriteLine()
			Console.WriteLine("------------------------")
			MyBase.ShowAll()
			Console.WriteLine("------------------------")
		End Sub
	End Class

	''' <summary>
	''' The 'Implementor' abstract class
	''' </summary>
	Friend MustInherit Class DataObject
		Public MustOverride Sub NextRecord()
		Public MustOverride Sub PriorRecord()
		Public MustOverride Sub AddRecord(ByVal name As String)
		Public MustOverride Sub DeleteRecord(ByVal name As String)
		Public MustOverride Sub ShowRecord()
		Public MustOverride Sub ShowAllRecords()
	End Class

	''' <summary>
	''' The 'ConcreteImplementor' class
	''' </summary>
	Friend Class CustomersData
		Inherits DataObject
		Private _customers As List(Of String) = New List(Of String)()
		Private _current As Integer = 0

		Public Sub New()
			' Loaded from a database 
			_customers.Add("Jim Jones")
			_customers.Add("Samual Jackson")
			_customers.Add("Allen Good")
			_customers.Add("Ann Stills")
			_customers.Add("Lisa Giolani")
		End Sub

		Public Overrides Sub NextRecord()
			If _current <= _customers.Count - 1 Then
				_current += 1
			End If
		End Sub

		Public Overrides Sub PriorRecord()
			If _current > 0 Then
				_current -= 1
			End If
		End Sub

		Public Overrides Sub AddRecord(ByVal customer As String)
			_customers.Add(customer)
		End Sub

		Public Overrides Sub DeleteRecord(ByVal customer As String)
			_customers.Remove(customer)
		End Sub

		Public Overrides Sub ShowRecord()
			Console.WriteLine(_customers(_current))
		End Sub

		Public Overrides Sub ShowAllRecords()
			For Each customer As String In _customers
				Console.WriteLine(" " & customer)
			Next customer
		End Sub
	End Class
End Namespace
