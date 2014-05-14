Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Observer.NETOptimized
	''' <summary>
	''' MainApp startup class for .NET optimized 
	''' Observer Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Create IBM stock and attach investors
            Dim ibm As New IBM(120.0)

            ' Attach 'listeners', i.e. Investors
            ibm.Attach(New Investor With {.Name = "Sorros"})
            ibm.Attach(New Investor With {.Name = "Berkshire"})

            ' Fluctuating prices will notify listening investors
            ibm.Price = 120.1
            ibm.Price = 121.0
            ibm.Price = 120.5
            ibm.Price = 120.75

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	' Custom event arguments
	Public Class ChangeEventArgs
        Inherits EventArgs

		' Gets or sets symbol
        Private _symbol As String
        Public Property Symbol() As String
            Get
                Return _symbol
            End Get
            Set(ByVal value As String)
                _symbol = value
            End Set
        End Property

		' Gets or sets price
        Private _price As Double
        Public Property Price() As Double
            Get
                Return _price
            End Get
            Set(ByVal value As Double)
                _price = value
            End Set
        End Property
	End Class

	''' <summary>
	''' The 'Subject' abstract class
	''' </summary>
	Friend MustInherit Class Stock
		Protected _symbol As String
		Protected _price As Double

		' Constructor
		Public Sub New(ByVal symbol As String, ByVal price As Double)
			Me._symbol = symbol
			Me._price = price
		End Sub

		' Event
		Public Event Change As EventHandler(Of ChangeEventArgs)

		' Invoke the Change event
		Public Overridable Sub OnChange(ByVal e As ChangeEventArgs)
			RaiseEvent Change(Me, e)
		End Sub

		Public Sub Attach(ByVal investor As IInvestor)
            AddHandler Change, AddressOf investor.Update
		End Sub

		Public Sub Detach(ByVal investor As IInvestor)
            RemoveHandler Change, AddressOf investor.Update
		End Sub

		' Gets or sets the price
		Public Property Price() As Double
			Get
				Return _price
			End Get
			Set(ByVal value As Double)
				If _price <> value Then
					_price = value
					OnChange(New ChangeEventArgs With {.Symbol = _symbol, .Price = _price})
					Console.WriteLine("")
				End If
			End Set
		End Property
	End Class

	''' <summary>
	''' The 'ConcreteSubject' class
	''' </summary>
	Friend Class IBM
		Inherits Stock
		' Constructor - symbol for IBM is always same
		Public Sub New(ByVal price As Double)
			MyBase.New("IBM", price)
		End Sub
	End Class

	''' <summary>
	''' The 'Observer' interface
	''' </summary>
	Friend Interface IInvestor
		Sub Update(ByVal sender As Object, ByVal e As ChangeEventArgs)
	End Interface

	''' <summary>
	''' The 'ConcreteObserver' class
	''' </summary>
	Friend Class Investor
		Implements IInvestor
		' Gets or sets the investor name
        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

		' Gets or sets the stock
        Private _stock As Stock
        Public Property Stock() As Stock
            Get
                Return _stock
            End Get
            Set(ByVal value As Stock)
                _stock = value
            End Set
        End Property

		Public Sub Update(ByVal sender As Object, ByVal e As ChangeEventArgs) Implements IInvestor.Update
			Console.WriteLine("Notified {0} of {1}'s " & "change to {2:C}", Name, e.Symbol, e.Price)
		End Sub
	End Class
End Namespace
