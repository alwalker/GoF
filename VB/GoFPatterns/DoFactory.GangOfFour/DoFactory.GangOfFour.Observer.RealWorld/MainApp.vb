Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Observer.RealWorld
	''' <summary>
	''' MainApp startup class for Real-World 
	''' Observer Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Create IBM stock and attach investors
            Dim ibm As New IBM("IBM", 120.0)
            ibm.Attach(New Investor("Sorros"))
            ibm.Attach(New Investor("Berkshire"))

            ' Fluctuating prices will notify investors
            ibm.Price = 120.1
            ibm.Price = 121.0
            ibm.Price = 120.5
            ibm.Price = 120.75

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Subject' abstract class
	''' </summary>
	Friend MustInherit Class Stock
		Private _symbol As String
		Private _price As Double
		Private _investors As List(Of IInvestor) = New List(Of IInvestor)()

		' Constructor
		Public Sub New(ByVal symbol As String, ByVal price As Double)
			Me._symbol = symbol
			Me._price = price
		End Sub

		Public Sub Attach(ByVal investor As IInvestor)
			_investors.Add(investor)
		End Sub

		Public Sub Detach(ByVal investor As IInvestor)
			_investors.Remove(investor)
		End Sub

		Public Sub Notify()
			For Each investor As IInvestor In _investors
				investor.Update(Me)
			Next investor

			Console.WriteLine("")
		End Sub

		' Gets or sets the price
		Public Property Price() As Double
			Get
				Return _price
			End Get
			Set(ByVal value As Double)
				If _price <> value Then
					_price = value
					Notify()
				End If
			End Set
		End Property

		' Gets the symbol
		Public ReadOnly Property Symbol() As String
			Get
				Return _symbol
			End Get
		End Property
	End Class

	''' <summary>
	''' The 'ConcreteSubject' class
	''' </summary>
	Friend Class IBM
		Inherits Stock
		' Constructor
		Public Sub New(ByVal symbol As String, ByVal price As Double)
			MyBase.New(symbol, price)
		End Sub
	End Class

	''' <summary>
	''' The 'Observer' interface
	''' </summary>
	Friend Interface IInvestor
		Sub Update(ByVal stock As Stock)
	End Interface

	''' <summary>
	''' The 'ConcreteObserver' class
	''' </summary>
	Friend Class Investor
		Implements IInvestor
		Private _name As String
		Private _stock As Stock

		' Constructor
		Public Sub New(ByVal name As String)
			Me._name = name
		End Sub

		Public Sub Update(ByVal stock As Stock) Implements IInvestor.Update
			Console.WriteLine("Notified {0} of {1}'s " & "change to {2:C}", _name, stock.Symbol, stock.Price)
		End Sub

		' Gets or sets the stock
		Public Property Stock() As Stock
			Get
				Return _stock
			End Get
			Set(ByVal value As Stock)
				_stock = value
			End Set
		End Property
	End Class
End Namespace
