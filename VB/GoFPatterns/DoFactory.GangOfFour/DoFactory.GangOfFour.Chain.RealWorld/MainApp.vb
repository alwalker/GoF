Imports Microsoft.VisualBasic
Imports System

Namespace DoFactory.GangOfFour.Chain.RealWorld
	''' <summary>
	''' MainApp startup class for Real-World 
	''' Chain of Responsibility Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Setup Chain of Responsibility
            Dim larry As Approver = New Director()
            Dim sam As Approver = New VicePresident()
            Dim tammy As Approver = New President()

            larry.SetSuccessor(sam)
            sam.SetSuccessor(tammy)

            ' Generate and process purchase requests
            Dim p As New Purchase(2034, 350.0, "Supplies")
            larry.ProcessRequest(p)

            p = New Purchase(2035, 32590.1, "Project X")
            larry.ProcessRequest(p)

            p = New Purchase(2036, 122100.0, "Project Y")
            larry.ProcessRequest(p)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Handler' abstract class
	''' </summary>
	Friend MustInherit Class Approver
		Protected successor As Approver

		Public Sub SetSuccessor(ByVal successor As Approver)
			Me.successor = successor
		End Sub

		Public MustOverride Sub ProcessRequest(ByVal purchase As Purchase)
	End Class

	''' <summary>
	''' The 'ConcreteHandler' class
	''' </summary>
	Friend Class Director
		Inherits Approver
		Public Overrides Sub ProcessRequest(ByVal purchase As Purchase)
			If purchase.Amount < 10000.0 Then
				Console.WriteLine("{0} approved request# {1}", Me.GetType().Name, purchase.Number)
			ElseIf successor IsNot Nothing Then
				successor.ProcessRequest(purchase)
			End If
		End Sub
	End Class

	''' <summary>
	''' The 'ConcreteHandler' class
	''' </summary>
	Friend Class VicePresident
		Inherits Approver
		Public Overrides Sub ProcessRequest(ByVal purchase As Purchase)
			If purchase.Amount < 25000.0 Then
				Console.WriteLine("{0} approved request# {1}", Me.GetType().Name, purchase.Number)
			ElseIf successor IsNot Nothing Then
				successor.ProcessRequest(purchase)
			End If
		End Sub
	End Class

	''' <summary>
	''' The 'ConcreteHandler' class
	''' </summary>
	Friend Class President
		Inherits Approver
		Public Overrides Sub ProcessRequest(ByVal purchase As Purchase)
			If purchase.Amount < 100000.0 Then
				Console.WriteLine("{0} approved request# {1}", Me.GetType().Name, purchase.Number)
			Else
				Console.WriteLine("Request# {0} requires an executive meeting!", purchase.Number)
			End If
		End Sub
	End Class

	''' <summary>
	''' Class holding request details
	''' </summary>
	Friend Class Purchase
		Private _number As Integer
		Private _amount As Double
		Private _purpose As String

		' Constructor
		Public Sub New(ByVal number As Integer, ByVal amount As Double, ByVal purpose As String)
			Me._number = number
			Me._amount = amount
			Me._purpose = purpose
		End Sub

		' Gets or sets purchase number
		Public Property Number() As Integer
			Get
				Return _number
			End Get
			Set(ByVal value As Integer)
				_number = value
			End Set
		End Property

		' Gets or sets purchase amount
		Public Property Amount() As Double
			Get
				Return _amount
			End Get
			Set(ByVal value As Double)
				_amount = value
			End Set
		End Property

		' Gets or sets purchase purpose
		Public Property Purpose() As String
			Get
				Return _purpose
			End Get
			Set(ByVal value As String)
				_purpose = value
			End Set
		End Property
	End Class
End Namespace
