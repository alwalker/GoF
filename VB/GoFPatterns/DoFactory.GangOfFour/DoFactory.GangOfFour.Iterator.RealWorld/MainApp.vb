Imports Microsoft.VisualBasic
Imports System
Imports System.Collections

Namespace DoFactory.GangOfFour.Iterator.RealWorld
	''' <summary>
	''' MainApp startup class for Real-World 
	''' Iterator Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
		Shared Sub Main()
			' Build a collection
			Dim collection As New Collection()
			collection(0) = New Item("Item 0")
			collection(1) = New Item("Item 1")
			collection(2) = New Item("Item 2")
			collection(3) = New Item("Item 3")
			collection(4) = New Item("Item 4")
			collection(5) = New Item("Item 5")
			collection(6) = New Item("Item 6")
			collection(7) = New Item("Item 7")
			collection(8) = New Item("Item 8")

			' Create iterator
			Dim iterator As New Iterator(collection)

			' Skip every other item
			iterator.Step = 2

			Console.WriteLine("Iterating over collection:")

			Dim item As Item = iterator.First()
			Do While Not iterator.IsDone
				Console.WriteLine(item.Name)
				item = iterator.Next()
			Loop

			' Wait for user
			Console.ReadKey()
		End Sub
	End Class

	''' <summary>
	''' A collection item
	''' </summary>
	Friend Class Item
		Private _name As String

		' Constructor
		Public Sub New(ByVal name As String)
			Me._name = name
		End Sub

		' Gets name
		Public ReadOnly Property Name() As String
			Get
				Return _name
			End Get
		End Property
	End Class

	''' <summary>
	''' The 'Aggregate' interface
	''' </summary>
	Friend Interface IAbstractCollection
		Function CreateIterator() As Iterator
	End Interface

	''' <summary>
	''' The 'ConcreteAggregate' class
	''' </summary>
	Friend Class Collection
		Implements IAbstractCollection
		Private _items As New ArrayList()

		Public Function CreateIterator() As Iterator Implements IAbstractCollection.CreateIterator
			Return New Iterator(Me)
		End Function

		' Gets item count
		Public ReadOnly Property Count() As Integer
			Get
				Return _items.Count
			End Get
		End Property

		' Indexer
		Default Public Property Item(ByVal index As Integer) As Object
			Get
				Return _items(index)
			End Get
			Set(ByVal value As Object)
				_items.Add(value)
			End Set
		End Property
	End Class

	''' <summary>
	''' The 'Iterator' interface
	''' </summary>
	Friend Interface IAbstractIterator
		Function First() As Item
		Function [Next]() As Item
		ReadOnly Property IsDone() As Boolean
		ReadOnly Property CurrentItem() As Item
	End Interface

	''' <summary>
	''' The 'ConcreteIterator' class
	''' </summary>
	Friend Class Iterator
		Implements IAbstractIterator
		Private _collection As Collection
		Private _current As Integer = 0
		Private _step As Integer = 1

		' Constructor
		Public Sub New(ByVal collection As Collection)
			Me._collection = collection
		End Sub

		' Gets first item
		Public Function First() As Item Implements IAbstractIterator.First
			_current = 0
			Return TryCast(_collection(_current), Item)
		End Function

		' Gets next item
		Public Function [Next]() As Item Implements IAbstractIterator.Next
			_current += _step
			If (Not IsDone) Then
				Return TryCast(_collection(_current), Item)
			Else
				Return Nothing
			End If
		End Function

		' Gets or sets stepsize
		Public Property [Step]() As Integer
			Get
				Return _step
			End Get
			Set(ByVal value As Integer)
				_step = value
			End Set
		End Property

		' Gets current iterator item
		Public ReadOnly Property CurrentItem() As Item Implements IAbstractIterator.CurrentItem
			Get
				Return TryCast(_collection(_current), Item)
			End Get
		End Property

		' Gets whether iteration is complete
		Public ReadOnly Property IsDone() As Boolean Implements IAbstractIterator.IsDone
			Get
				Return _current >= _collection.Count
			End Get
		End Property
	End Class
End Namespace
