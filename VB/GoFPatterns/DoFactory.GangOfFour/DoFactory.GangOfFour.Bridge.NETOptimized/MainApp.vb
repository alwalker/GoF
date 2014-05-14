Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Bridge.NETOptimized
	''' <summary>
	''' MainApp startup class for .NET optimized 
	''' Bridge Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Create RefinedAbstraction
            Dim customers As Customers = New Customers With {.Group = "Chicago"}

            ' Set ConcreteImplementor
            customers.DataObject = New CustomersData()

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
		' Gets or sets customer group
        Private _group As String
        Public Property Group() As String
            Get
                Return _group
            End Get
            Set(ByVal value As String)
                _group = value
            End Set
        End Property

		' Gets or sets data object
        Private _dataObject As IDataObject
        Public Property DataObject() As IDataObject
            Get
                Return _dataObject
            End Get
            Set(ByVal value As IDataObject)
                _dataObject = value
            End Set
        End Property

		Public Overridable Sub [Next]()
			DataObject.NextRecord()
		End Sub

		Public Overridable Sub Prior()
			DataObject.PriorRecord()
		End Sub

		Public Overridable Sub Add(ByVal name As String)
			DataObject.AddRecord(name)
		End Sub

		Public Overridable Sub Delete(ByVal name As String)
			DataObject.DeleteRecord(name)
		End Sub

		Public Overridable Sub Show()
			DataObject.ShowRecord()
		End Sub

		Public Overridable Sub ShowAll()
			Console.WriteLine("Customer Group: " & Group)
			DataObject.ShowAllRecords()
		End Sub
	End Class

	''' <summary>
	''' The 'RefinedAbstraction' class
	''' </summary>
	Friend Class Customers
		Inherits CustomersBase
		Public Overrides Sub ShowAll()
			' Add separator lines
			Console.WriteLine()
			Console.WriteLine("------------------------")
			MyBase.ShowAll()
			Console.WriteLine("------------------------")
		End Sub
	End Class

	''' <summary>
	''' The 'Implementor' interface
	''' </summary>
	Friend Interface IDataObject
		Sub NextRecord()
		Sub PriorRecord()
		Sub AddRecord(ByVal name As String)
		Sub DeleteRecord(ByVal name As String)
		Sub ShowRecord()
		Sub ShowAllRecords()
	End Interface

	''' <summary>
	''' The 'ConcreteImplementor' class
	''' </summary>
	Friend Class CustomersData
		Implements IDataObject
		Private _customers As List(Of String)
		Private _current As Integer = 0

		' Constructor
		Public Sub New()
			' Simulate loading from database
			_customers = New List(Of String) (New String() {"Jim Jones", "Samual Jackson", "Allan Good", "Ann Stills", "Lisa Giolani"})
		End Sub

		Public Sub NextRecord() Implements IDataObject.NextRecord
			If _current <= _customers.Count - 1 Then
				_current += 1
			End If
		End Sub

		Public Sub PriorRecord() Implements IDataObject.PriorRecord
			If _current > 0 Then
				_current -= 1
			End If
		End Sub

		Public Sub AddRecord(ByVal customer As String) Implements IDataObject.AddRecord
			_customers.Add(customer)
		End Sub

		Public Sub DeleteRecord(ByVal customer As String) Implements IDataObject.DeleteRecord
			_customers.Remove(customer)
		End Sub

		Public Sub ShowRecord() Implements IDataObject.ShowRecord
			Console.WriteLine(_customers(_current))
		End Sub

        Public Sub ShowAllRecords() Implements IDataObject.ShowAllRecords
            For Each customer As String In _customers
                Console.WriteLine(" " & customer)
            Next customer
        End Sub
	End Class
End Namespace
