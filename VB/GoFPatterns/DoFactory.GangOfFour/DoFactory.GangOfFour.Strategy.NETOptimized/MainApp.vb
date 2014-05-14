Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Strategy.NETOptimized
	''' <summary>
	''' MainApp startup class for .NET optimized 
	''' Strategy Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Two contexts following different strategies
            Dim studentRecords As New SortedList()
            studentRecords.Add(New Student With {.Name = "Samual", .Ssn = "154-33-2009"})
            studentRecords.Add(New Student With {.Name = "Jimmy", .Ssn = "487-43-1665"})
            studentRecords.Add(New Student With {.Name = "Sandra", .Ssn = "655-00-2944"})
            studentRecords.Add(New Student With {.Name = "Vivek", .Ssn = "133-98-8399"})
            studentRecords.Add(New Student With {.Name = "Anna", .Ssn = "760-94-9844"})

            studentRecords.SortStrategy = New QuickSort()
            studentRecords.SortStudents()

            studentRecords.SortStrategy = New ShellSort()
            studentRecords.SortStudents()

            studentRecords.SortStrategy = New MergeSort()
            studentRecords.SortStudents()

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Strategy' interface
	''' </summary>
	Friend Interface ISortStrategy
		Sub Sort(ByVal list As List(Of Student))
	End Interface

	''' <summary>
	''' A 'ConcreteStrategy' class
	''' </summary>
	Friend Class QuickSort
		Implements ISortStrategy
		Public Sub Sort(ByVal list As List(Of Student)) Implements ISortStrategy.Sort
			' Call overloaded Sort
			Sort(list, 0, list.Count - 1)
			Console.WriteLine("QuickSorted list ")
		End Sub

		' Recursively sort
		Private Sub Sort(ByVal list As List(Of Student), ByVal left As Integer, ByVal right As Integer)
			Dim lhold As Integer = left
			Dim rhold As Integer = right

			' Use a random pivot
			Dim random As New Random()
			Dim pivot As Integer = random.Next(left, right)
			Swap(list, pivot, left)
			pivot = left
			left += 1

			Do While right >= left
				Dim compareleft As Integer = list(left).Name.CompareTo(list(pivot).Name)
				Dim compareright As Integer = list(right).Name.CompareTo(list(pivot).Name)

				If (compareleft >= 0) AndAlso (compareright < 0) Then
					Swap(list, left, right)
				Else
					If compareleft >= 0 Then
						right -= 1
					Else
						If compareright < 0 Then
							left += 1
						Else
							right -= 1
							left += 1
						End If
					End If
				End If
			Loop
			Swap(list, pivot, right)
			pivot = right

			If pivot > lhold Then
				Sort(list, lhold, pivot)
			End If
			If rhold > pivot + 1 Then
				Sort(list, pivot + 1, rhold)
			End If
		End Sub

		' Swap helper function
		Private Sub Swap(ByVal list As List(Of Student), ByVal left As Integer, ByVal right As Integer)
			Dim temp As Student = list(right)
			list(right) = list(left)
			list(left) = temp
		End Sub
	End Class

	''' <summary>
	''' A 'ConcreteStrategy' class
	''' </summary>
	Friend Class ShellSort
		Implements ISortStrategy
		Public Sub Sort(ByVal list As List(Of Student)) Implements ISortStrategy.Sort
			' ShellSort();  not-implemented
			Console.WriteLine("ShellSorted list ")
		End Sub
	End Class

	''' <summary>
	''' A 'ConcreteStrategy' class
	''' </summary>
	Friend Class MergeSort
		Implements ISortStrategy
		Public Sub Sort(ByVal list As List(Of Student)) Implements ISortStrategy.Sort
			' MergeSort(); not-implemented
			Console.WriteLine("MergeSorted list ")
		End Sub
	End Class

	''' <summary>
	''' The 'Context' class
	''' </summary>
	Friend Class SortedList
		Inherits List(Of Student)
		' Sets sort strategy
        Private _sortStrategy As ISortStrategy
        Public Property SortStrategy() As ISortStrategy
            Get
                Return _sortStrategy
            End Get
            Set(ByVal value As ISortStrategy)
                _sortStrategy = value
            End Set
        End Property

		' Perform sort
		Public Sub SortStudents()
			SortStrategy.Sort(Me)

			' Display sort results
			For Each student As Student In Me
				Console.WriteLine(" " & student.Name)
			Next student
			Console.WriteLine()
		End Sub
	End Class

	''' <summary>
	''' Represents a student
	''' </summary>
	Friend Class Student
		' Gets or sets student name
        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

		' Gets or sets student social security number
        Private _ssn As String
        Public Property Ssn() As String
            Get
                Return _ssn
            End Get
            Set(ByVal value As String)
                _ssn = value
            End Set
        End Property
	End Class
End Namespace
