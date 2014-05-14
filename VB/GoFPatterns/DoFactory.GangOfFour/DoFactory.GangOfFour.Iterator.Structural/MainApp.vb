Imports Microsoft.VisualBasic
Imports System
Imports System.Collections

Namespace DoFactory.GangOfFour.Iterator.Structural
	''' <summary>
	''' MainApp startup class for Structural 
	''' Iterator Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            Dim a As New ConcreteAggregate()
            a(0) = "Item A"
            a(1) = "Item B"
            a(2) = "Item C"
            a(3) = "Item D"

            ' Create Iterator and provide aggregate
            Dim i As New ConcreteIterator(a)

            Console.WriteLine("Iterating over collection:")

            Dim item As Object = i.First()
            Do While item IsNot Nothing
                Console.WriteLine(item)
                item = i.Next()
            Loop

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Aggregate' abstract class
	''' </summary>
	Friend MustInherit Class Aggregate
		Public MustOverride Function CreateIterator() As Iterator
	End Class

	''' <summary>
	''' The 'ConcreteAggregate' class
	''' </summary>
	Friend Class ConcreteAggregate
		Inherits Aggregate
		Private _items As New ArrayList()

		Public Overrides Function CreateIterator() As Iterator
			Return New ConcreteIterator(Me)
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
				_items.Insert(index, value)
			End Set
		End Property
	End Class

	''' <summary>
	''' The 'Iterator' abstract class
	''' </summary>
	Friend MustInherit Class Iterator
		Public MustOverride Function First() As Object
		Public MustOverride Function [Next]() As Object
		Public MustOverride Function IsDone() As Boolean
		Public MustOverride Function CurrentItem() As Object
	End Class

	''' <summary>
	''' The 'ConcreteIterator' class
	''' </summary>
	Friend Class ConcreteIterator
		Inherits Iterator
		Private _aggregate As ConcreteAggregate
		Private _current As Integer = 0

		' Constructor
		Public Sub New(ByVal aggregate As ConcreteAggregate)
			Me._aggregate = aggregate
		End Sub

		' Gets first iteration item
		Public Overrides Function First() As Object
			Return _aggregate(0)
		End Function

		' Gets next iteration item
		Public Overrides Function [Next]() As Object
			Dim ret As Object = Nothing
			If _current < _aggregate.Count - 1 Then
				_current += 1
				ret = _aggregate(_current)
			End If

			Return ret
		End Function

		' Gets current iteration item
		Public Overrides Function CurrentItem() As Object
			Return _aggregate(_current)
		End Function

		' Gets whether iterations are complete
		Public Overrides Function IsDone() As Boolean
			Return _current >= _aggregate.Count
		End Function
	End Class
End Namespace
