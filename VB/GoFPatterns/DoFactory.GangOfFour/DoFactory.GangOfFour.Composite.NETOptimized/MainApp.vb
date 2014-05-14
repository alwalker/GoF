Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Composite.NETOptimized
	''' <summary>
	''' MainApp startup class for .NET optimized 
	''' Composite Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Build a tree of shapes
            Dim root As TreeNode(Of Shape) = New TreeNode(Of Shape) With {.Node = New Shape("Picture")}

            root.Add(New Shape("Red Line"))
            root.Add(New Shape("Blue Circle"))
            root.Add(New Shape("Green Box"))

            Dim branch As TreeNode(Of Shape) = root.Add(New Shape("Two Circles"))
            branch.Add(New Shape("Black Circle"))
            branch.Add(New Shape("White Circle"))

            ' Add, remove, and add a shape
            Dim shape As New Shape("Yellow Line")
            root.Add(shape)
            root.Remove(shape)
            root.Add(shape)

            ' Display tree using static method
            TreeNode(Of Shape).Display(root, 1)

            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' Generic tree node class
	''' </summary>
	''' <typeparam name="T">Node type</typeparam>
	Friend Class TreeNode(Of T As IComparable(Of T))
		Private _children As List(Of TreeNode(Of T)) = New List(Of TreeNode(Of T))()

		' Add a child tree node
		Public Function Add(ByVal child As T) As TreeNode(Of T)
			Dim newNode As TreeNode(Of T) = New TreeNode(Of T) With {.Node = child}
			_children.Add(newNode)
			Return newNode
		End Function

		' Remove a child tree node
		Public Sub Remove(ByVal child As T)
			For Each treeNode As TreeNode(Of T) In _children
				If treeNode.Node.CompareTo(child) = 0 Then
					_children.Remove(treeNode)
					Return
				End If
			Next treeNode
		End Sub

		' Gets or sets the node
        Private _node As T
        Public Property Node() As T
            Get
                Return _node
            End Get
            Set(ByVal value As T)
                _node = value
            End Set
        End Property

		' Gets treenode children
		Public ReadOnly Property Children() As List(Of TreeNode(Of T))
			Get
				Return _children
			End Get
		End Property

		' Recursively displays node and its children 
		Public Shared Sub Display(ByVal node As TreeNode(Of T), ByVal indentation As Integer)
			Dim line As New String("-"c, indentation)
            Console.WriteLine(line & " " & node.Node.ToString())

			For Each treeNode As TreeNode(Of T) In node.Children
				Display(treeNode, indentation + 1)
			Next treeNode
		End Sub
	End Class

	''' <summary>
	''' Shape class
	''' <remarks>
	''' Implements generic IComparable interface
	''' </remarks>
	''' </summary>

	Friend Class Shape
		Implements IComparable(Of Shape)
		Private _name As String

		' Constructor
		Public Sub New(ByVal name As String)
			Me._name = name
		End Sub

		' Override ToString method
		Public Overrides Function ToString() As String
			Return _name
		End Function

		' IComparable<Shape> Member
		Public Function CompareTo(ByVal other As Shape) As Integer Implements IComparable(Of Shape).CompareTo
			Return If((Me Is other), 0, -1)
		End Function
	End Class
End Namespace
