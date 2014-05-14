Imports Microsoft.VisualBasic
Imports System

Namespace DoFactory.GangOfFour.Chain.NETOptimized
    ''' <summary>
    ''' MainApp startup class for .NET optimized 
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

            larry.Successor = sam
            sam.Successor = tammy

            ' Generate and process purchase requests
            Dim thePurchase As Purchase = New Purchase With {.Number = 2034, .Amount = 350.0, .Purpose = "Supplies"}
            larry.ProcessRequest(thePurchase)

            thePurchase = New Purchase With {.Number = 2035, .Amount = 32590.1, .Purpose = "Project X"}
            larry.ProcessRequest(thePurchase)

            thePurchase = New Purchase With {.Number = 2036, .Amount = 122100.0, .Purpose = "Project Y"}
            larry.ProcessRequest(thePurchase)

            ' Wait for user
            Console.ReadKey()
        End Sub
    End Class

    ' Purchase event argument holds purchase info
    Public Class PurchaseEventArgs
        Inherits EventArgs
        Private _thePurchase As Purchase
        Friend Property ThePurchase() As Purchase
            Get
                Return _thePurchase
            End Get
            Set(ByVal value As Purchase)
                _thePurchase = value
            End Set
        End Property
    End Class

    ''' <summary>
    ''' The 'Handler' abstract class
    ''' </summary>
    Friend MustInherit Class Approver
        ' Purchase event 
        Public Event PurchaseEvent As EventHandler(Of PurchaseEventArgs)

        ' Purchase event handler
        Public MustOverride Sub PurchaseHandler(ByVal sender As Object, ByVal e As PurchaseEventArgs)

        ' Constructor
        Public Sub New()
            AddHandler PurchaseEvent, AddressOf PurchaseHandler
        End Sub

        Public Sub ProcessRequest(ByVal purchase As Purchase)
            Dim e As PurchaseEventArgs = New PurchaseEventArgs With {.ThePurchase = purchase}
            OnPurchase(e)
        End Sub

        ' Invoke the Purchase event
        Public Overridable Sub OnPurchase(ByVal e As PurchaseEventArgs)
            RaiseEvent PurchaseEvent(Me, e)
        End Sub

        ' Sets or gets the next approver
        Private _successor As Approver
        Public Property Successor() As Approver
            Get
                Return _successor
            End Get
            Set(ByVal value As Approver)
                _successor = value
            End Set
        End Property
    End Class

    ''' <summary>
    ''' The 'ConcreteHandler' class
    ''' </summary>
    Friend Class Director
        Inherits Approver
        Public Overrides Sub PurchaseHandler(ByVal sender As Object, ByVal e As PurchaseEventArgs)
            If e.ThePurchase.Amount < 10000.0 Then
                Console.WriteLine("{0} approved request# {1}", Me.GetType().Name, e.ThePurchase.Number)
            ElseIf Successor IsNot Nothing Then
                Successor.PurchaseHandler(Me, e)
            End If
        End Sub
    End Class

    ''' <summary>
    ''' The 'ConcreteHandler' class
    ''' </summary>
    Friend Class VicePresident
        Inherits Approver
        Public Overrides Sub PurchaseHandler(ByVal sender As Object, ByVal e As PurchaseEventArgs)
            If e.ThePurchase.Amount < 25000.0 Then
                Console.WriteLine("{0} approved request# {1}", Me.GetType().Name, e.ThePurchase.Number)
            ElseIf Successor IsNot Nothing Then
                Successor.PurchaseHandler(Me, e)
            End If
        End Sub
    End Class

    ''' <summary>
    ''' The 'ConcreteHandler' clas
    ''' </summary>
    Friend Class President
        Inherits Approver
        Public Overrides Sub PurchaseHandler(ByVal sender As Object, ByVal e As PurchaseEventArgs)
            If e.ThePurchase.Amount < 100000.0 Then
                Console.WriteLine("{0} approved request# {1}", sender.GetType().Name, e.ThePurchase.Number)
            ElseIf Successor IsNot Nothing Then
                Successor.PurchaseHandler(Me, e)
            Else
                Console.WriteLine("Request# {0} requires an executive meeting!", e.ThePurchase.Number)
            End If
        End Sub
    End Class

    ''' <summary>
    ''' Class that holds request details
    ''' </summary>
    Friend Class Purchase
        ' Gets or sets purchase amount
        Private _amount As Double
        Public Property Amount() As Double
            Get
                Return _amount
            End Get
            Set(ByVal value As Double)
                _amount = value
            End Set
        End Property

        ' Gets or sets purchase purpose
        Private _purpose As String
        Public Property Purpose() As String
            Get
                Return _purpose
            End Get
            Set(ByVal value As String)
                _purpose = value
            End Set
        End Property

        ' Gets or sets purchase number
        Private _number As Integer
        Public Property Number() As Integer
            Get
                Return _number
            End Get
            Set(ByVal value As Integer)
                _number = value
            End Set
        End Property
    End Class
End Namespace
