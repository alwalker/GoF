Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic

' MainApp startup application

Class MainApp

    Shared Sub Main()

        ' Create and populate collection of items
        Dim collection As New List(Of Item)

        collection.Add(New Item("Item 0"))
        collection.Add(New Item("Item 1"))
        collection.Add(New Item("Item 2"))
        collection.Add(New Item("Item 3"))
        collection.Add(New Item("Item 4"))
        collection.Add(New Item("Item 5"))
        collection.Add(New Item("Item 6"))
        collection.Add(New Item("Item 7"))
        collection.Add(New Item("Item 8"))

        ' Create iterator
        Dim iterator As New Iterator(Of Item)(collection)

        ' Skip every other item
        iterator.Step = 2

        Console.WriteLine("Iterating over collection:")

        While iterator.MoveNext()
            Console.WriteLine(iterator.Current.Name)
        End While

        ' Alternatively: use .NET foreach syntax
        Console.WriteLine(ControlChars.Lf & "Using .NET foreach syntax:")

        For Each item As Item In collection
            Console.WriteLine(item.Name)
        Next item

        ' Wait for user
        Console.Read()

    End Sub
End Class

' "ConcreteIterator" 
Class Iterator(Of T)
    Implements IEnumerator(Of T)

    Private _collection As List(Of T)
    Private _curr As Integer = -1
    Private _step As Integer = 1

    Public Sub New(ByVal _collection As List(Of T))
        Me._collection = _collection
    End Sub

    Public Property [Step]() As Integer
        Get
            Return _step
        End Get
        Set(ByVal Value As Integer)
            _step = Value
        End Set
    End Property

    ' Interface implemenations
    Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
        _curr += _step
        Return _curr < _collection.Count
    End Function

    Public ReadOnly Property Current() As T Implements IEnumerator(Of T).Current
        Get
            Return _collection(_curr)
        End Get
    End Property

    Public Sub Reset() Implements IEnumerator.Reset
        _curr = -1
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

    Public ReadOnly Property CurrentNonGeneric() As Object Implements IEnumerator.Current
        Get
            Return _collection(_curr)
        End Get
    End Property
End Class

' Items that are iterated over
Class Item
    Private _name As String

    ' Constructor
    Public Sub New(ByVal _name As String)
        Me._name = _name
    End Sub

    ' Property
    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property
End Class
