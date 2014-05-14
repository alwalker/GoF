Imports Microsoft.VisualBasic
Imports System

Namespace DoFactory.GangOfFour.State.RealWorld
	''' <summary>
	''' MainApp startup class for Real-World 
	''' State Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Open a new account
            Dim account As New Account("Jim Johnson")

            ' Apply financial transactions
            account.Deposit(500.0)
            account.Deposit(300.0)
            account.Deposit(550.0)
            account.PayInterest()
            account.Withdraw(2000.0)
            account.Withdraw(1100.0)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'State' abstract class
	''' </summary>
	Friend MustInherit Class State
        Protected _account As Account
        Protected _balance As Double

        Protected interest As Double
        Protected lowerLimit As Double
        Protected upperLimit As Double

        ' Properties
        Public Property Account() As Account
            Get
                Return _account
            End Get
            Set(ByVal value As Account)
                _account = value
            End Set
        End Property

        Public Property Balance() As Double
            Get
                Return _balance
            End Get
            Set(ByVal value As Double)
                _balance = value
            End Set
        End Property

        Public MustOverride Sub Deposit(ByVal amount As Double)
        Public MustOverride Sub Withdraw(ByVal amount As Double)
        Public MustOverride Sub PayInterest()
    End Class


    ''' <summary>
    ''' A 'ConcreteState' class
    ''' <remarks>
    ''' Red indicates that account is overdrawn 
    ''' </remarks>
    ''' </summary>
    Friend Class RedState
        Inherits State
        Private _serviceFee As Double

        ' Constructor
        Public Sub New(ByVal state As State)
            Me._balance = state.Balance
            Me._account = state.Account
            Initialize()
        End Sub

        Private Sub Initialize()
            ' Should come from a datasource
            interest = 0.0
            lowerLimit = -100.0
            upperLimit = 0.0
            _serviceFee = 15.0
        End Sub

        Public Overrides Sub Deposit(ByVal amount As Double)
            _balance += amount
            StateChangeCheck()
        End Sub

        Public Overrides Sub Withdraw(ByVal amount As Double)
            amount = amount - _serviceFee
            Console.WriteLine("No funds available for withdrawal!")
        End Sub

        Public Overrides Sub PayInterest()
            ' No interest is paid
        End Sub

        Private Sub StateChangeCheck()
            If _balance > upperLimit Then
                _account.State = New SilverState(Me)
            End If
        End Sub
    End Class

    ''' <summary>
    ''' A 'ConcreteState' class
    ''' <remarks>
    ''' Silver indicates a non-interest bearing state
    ''' </remarks>
    ''' </summary>
    Friend Class SilverState
        Inherits State
        ' Overloaded constructors

        Public Sub New(ByVal state As State)
            Me.New(state.Balance, state.Account)
        End Sub

        Public Sub New(ByVal balance As Double, ByVal account As Account)
            Me._balance = balance
            Me._account = account
            Initialize()
        End Sub

        Private Sub Initialize()
            ' Should come from a datasource
            interest = 0.0
            lowerLimit = 0.0
            upperLimit = 1000.0
        End Sub

        Public Overrides Sub Deposit(ByVal amount As Double)
            _balance += amount
            StateChangeCheck()
        End Sub

        Public Overrides Sub Withdraw(ByVal amount As Double)
            _balance -= amount
            StateChangeCheck()
        End Sub

        Public Overrides Sub PayInterest()
            _balance += interest * _balance
            StateChangeCheck()
        End Sub

        Private Sub StateChangeCheck()
            If _balance < lowerLimit Then
                _account.State = New RedState(Me)
            ElseIf _balance > upperLimit Then
                _account.State = New GoldState(Me)
            End If
        End Sub
    End Class

    ''' <summary>
    ''' A 'ConcreteState' class
    ''' <remarks>
    ''' Gold indicates an interest bearing state
    ''' </remarks>
    ''' </summary>
    Friend Class GoldState
        Inherits State
        ' Overloaded constructors
        Public Sub New(ByVal state As State)
            Me.New(state.Balance, state.Account)
        End Sub

        Public Sub New(ByVal balance As Double, ByVal account As Account)
            Me._balance = balance
            Me._account = account
            Initialize()
        End Sub

        Private Sub Initialize()
            ' Should come from a database
            interest = 0.05
            lowerLimit = 1000.0
            upperLimit = 10000000.0
        End Sub

        Public Overrides Sub Deposit(ByVal amount As Double)
            _balance += amount
            StateChangeCheck()
        End Sub

        Public Overrides Sub Withdraw(ByVal amount As Double)
            _balance -= amount
            StateChangeCheck()
        End Sub

        Public Overrides Sub PayInterest()
            _balance += interest * _balance
            StateChangeCheck()
        End Sub

        Private Sub StateChangeCheck()
            If _balance < 0.0 Then
                _account.State = New RedState(Me)
            ElseIf _balance < lowerLimit Then
                _account.State = New SilverState(Me)
			End If
		End Sub
	End Class

	''' <summary>
	''' The 'Context' class
	''' </summary>
	Friend Class Account
		Private _state As State
		Private _owner As String

		' Constructor
		Public Sub New(ByVal owner As String)
			' New accounts are 'Silver' by default
			Me._owner = owner
			Me._state = New SilverState(0.0, Me)
		End Sub

		' Properties
		Public ReadOnly Property Balance() As Double
			Get
				Return _state.Balance
			End Get
		End Property

		Public Property State() As State
			Get
				Return _state
			End Get
			Set(ByVal value As State)
				_state = value
			End Set
		End Property

		Public Sub Deposit(ByVal amount As Double)
			_state.Deposit(amount)
			Console.WriteLine("Deposited {0:C} --- ", amount)
			Console.WriteLine(" Balance = {0:C}", Me.Balance)
			Console.WriteLine(" Status  = {0}", Me.State.GetType().Name)
			Console.WriteLine("")
		End Sub

		Public Sub Withdraw(ByVal amount As Double)
			_state.Withdraw(amount)
			Console.WriteLine("Withdrew {0:C} --- ", amount)
			Console.WriteLine(" Balance = {0:C}", Me.Balance)
			Console.WriteLine(" Status  = {0}" & Constants.vbLf, Me.State.GetType().Name)
		End Sub

		Public Sub PayInterest()
			_state.PayInterest()
			Console.WriteLine("Interest Paid --- ")
			Console.WriteLine(" Balance = {0:C}", Me.Balance)
			Console.WriteLine(" Status  = {0}" & Constants.vbLf, Me.State.GetType().Name)
		End Sub
	End Class
End Namespace
