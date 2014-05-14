Imports Microsoft.VisualBasic
Imports System
Imports System.Text

Namespace DoFactory.HeadFirst.Proxy.GumballMonitor
	Friend Class GumballMachineTestDrive
        Shared Sub Main(ByVal args() As String)

            Dim location As String = "Seattle"
            Dim count As Integer = 122
            Dim gumballMachine As New GumballMachine(location, count)
            Dim monitor As New GumballMonitor(gumballMachine)

            Console.WriteLine(gumballMachine)

            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()
            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()

            Console.WriteLine(gumballMachine)

            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()
            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()

            Console.WriteLine(gumballMachine)

            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()
            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()

            Console.WriteLine(gumballMachine)

            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()
            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()

            Console.WriteLine(gumballMachine)

            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()
            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()

            Console.WriteLine(gumballMachine)

            monitor.Report()

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	#Region "Gumball Monitor"

	Public Class GumballMonitor
		Private _machine As GumballMachine

		Public Sub New(ByVal machine As GumballMachine)
			Me._machine = machine
		End Sub

		Public Sub Report()
			Console.WriteLine("Gumball Machine: " & _machine.Location)
			Console.WriteLine("Current inventory: " & _machine.Count & " gumballs")
            Console.WriteLine("Current state: " & _machine.State.ToString())
		End Sub
	End Class

	#End Region

	#Region "Gumball Machine"

	Public Class GumballMachine
		Private _soldOutState As IState
		Private _noQuarterState As IState
		Private _hasQuarterState As IState
		Private _soldState As IState
		Private _winnerState As IState

		Private _state As IState

		Private privateCount As Integer
		Public Property Count() As Integer
			Get
				Return privateCount
			End Get
			Private Set(ByVal value As Integer)
				privateCount = value
			End Set
		End Property
		Private privateLocation As String
		Public Property Location() As String
			Get
				Return privateLocation
			End Get
			Private Set(ByVal value As String)
				privateLocation = value
			End Set
		End Property

		Public Sub New(ByVal location As String, ByVal count As Integer)
			_soldOutState = New SoldOutState(Me)
			_noQuarterState = New NoQuarterState(Me)
			_hasQuarterState = New HasQuarterState(Me)
			_soldState = New SoldState(Me)
			_winnerState = New WinnerState(Me)

			Location = location
			Count = count

			If Count > 0 Then
				_state = _noQuarterState
			Else
				_state = _soldOutState
			End If
		End Sub

		Public Sub InsertQuarter()
			_state.InsertQuarter()
		End Sub

		Public Sub EjectQuarter()
			_state.EjectQuarter()
		End Sub

		Public Sub TurnCrank()
			_state.TurnCrank()
			_state.Dispense()
		End Sub

		Public Property State() As IState
			Set(ByVal value As IState)
				_state = value
			End Set
			Get
				Return _state
			End Get
		End Property

		Public Sub ReleaseBall()
			If Count > 0 Then
				Console.WriteLine("A gumball comes rolling out the slot...")
				Count -= 1
			End If
		End Sub

		Private Sub Refill(ByVal count As Integer)
			Count = count
			_state = _noQuarterState
		End Sub


		Public Function GetSoldOutState() As IState
			Return _soldOutState
		End Function

		Public Function GetNoQuarterState() As IState
			Return _noQuarterState
		End Function

		Public Function GetHasQuarterState() As IState
			Return _hasQuarterState
		End Function

		Public Function GetSoldState() As IState
			Return _soldState
		End Function

		Public Function GetWinnerState() As IState
			Return _winnerState
		End Function

		Public Overrides Function ToString() As String
			Dim result As New StringBuilder()
			result.Append(Constants.vbLf & "Mighty Gumball, Inc.")
			result.Append(Constants.vbLf & ".NET-enabled Standing Gumball Model #2004")
			result.Append(Constants.vbLf & "Inventory: " & Count & " gumball")
			If Count <> 1 Then
				result.Append("s")
			End If
			result.Append(Constants.vbLf)
            result.Append("Machine is " & _state.ToString() & Constants.vbLf)
			Return result.ToString()
		End Function
	End Class

	#End Region

	#Region "State"

	Public Interface IState
		Sub InsertQuarter()
		Sub EjectQuarter()
		Sub TurnCrank()
		Sub Dispense()
	End Interface

	Public Class SoldState
		Implements IState
		Private _machine As GumballMachine

		Public Sub New(ByVal machine As GumballMachine)
			Me._machine = machine
		End Sub

		Public Sub InsertQuarter() Implements IState.InsertQuarter
			Console.WriteLine("Please wait, we're already giving you a gumball")
		End Sub

		Public Sub EjectQuarter() Implements IState.EjectQuarter
			Console.WriteLine("Sorry, you already turned the crank")
		End Sub

		Public Sub TurnCrank() Implements IState.TurnCrank
			Console.WriteLine("Turning twice doesn't get you another gumball!")
		End Sub

		Public Sub Dispense() Implements IState.Dispense
			_machine.ReleaseBall()
			If _machine.Count > 0 Then
				_machine.State = _machine.GetNoQuarterState()
			Else
				Console.WriteLine("Oops, out of gumballs!")
				_machine.State = _machine.GetSoldOutState()
			End If
		End Sub

		Public Overrides Function ToString() As String
			Return "dispensing a gumball"
		End Function
	End Class

	Public Class SoldOutState
		Implements IState
		Private _machine As GumballMachine

		Public Sub New(ByVal machine As GumballMachine)
			Me._machine = machine
		End Sub

		Public Sub InsertQuarter() Implements IState.InsertQuarter
			Console.WriteLine("You can't insert a quarter, the machine is sold out")
		End Sub

		Public Sub EjectQuarter() Implements IState.EjectQuarter
			Console.WriteLine("You can't eject, you haven't inserted a quarter yet")
		End Sub

		Public Sub TurnCrank() Implements IState.TurnCrank
			Console.WriteLine("You turned, but there are no gumballs")
		End Sub

		Public Sub Dispense() Implements IState.Dispense
			Console.WriteLine("No gumball dispensed")
		End Sub

		Public Overrides Function ToString() As String
			Return "sold out"
		End Function
	End Class

	Public Class NoQuarterState
		Implements IState
		Private _machine As GumballMachine

		Public Sub New(ByVal machine As GumballMachine)
			Me._machine = machine
		End Sub

		Public Sub InsertQuarter() Implements IState.InsertQuarter
			Console.WriteLine("You inserted a quarter")
			_machine.State = _machine.GetHasQuarterState()
		End Sub

		Public Sub EjectQuarter() Implements IState.EjectQuarter
			Console.WriteLine("You haven't inserted a quarter")
		End Sub

		Public Sub TurnCrank() Implements IState.TurnCrank
			Console.WriteLine("You turned, but there's no quarter")
		End Sub

		Public Sub Dispense() Implements IState.Dispense
			Console.WriteLine("You need to pay first")
		End Sub

		Public Overrides Function ToString() As String
			Return "waiting for quarter"
		End Function
	End Class

	Public Class HasQuarterState
		Implements IState
		Private _machine As GumballMachine

		Public Sub New(ByVal machine As GumballMachine)
			Me._machine = machine
		End Sub

		Public Sub InsertQuarter() Implements IState.InsertQuarter
			Console.WriteLine("You can't insert another quarter")
		End Sub

		Public Sub EjectQuarter() Implements IState.EjectQuarter
			Console.WriteLine("Quarter returned")
			_machine.State = _machine.GetNoQuarterState()
		End Sub

		Public Sub TurnCrank() Implements IState.TurnCrank
			Console.WriteLine("You turned...")
			Dim random As New Random()

			Dim winner As Integer = random.Next(11)
			If (winner = 0) AndAlso (_machine.Count > 1) Then
				_machine.State = _machine.GetWinnerState()
			Else
				_machine.State = _machine.GetSoldState()
			End If
		End Sub

		Public Sub Dispense() Implements IState.Dispense
			Console.WriteLine("No gumball dispensed")
		End Sub

		Public Overrides Function ToString() As String
			Return "waiting for turn of crank"
		End Function
	End Class

	Public Class WinnerState
		Implements IState
		Private _machine As GumballMachine

		Public Sub New(ByVal machine As GumballMachine)
			Me._machine = machine
		End Sub

		Public Sub InsertQuarter() Implements IState.InsertQuarter
			Console.WriteLine("Please wait, we're already giving you a Gumball")
		End Sub

		Public Sub EjectQuarter() Implements IState.EjectQuarter
			Console.WriteLine("Please wait, we're already giving you a Gumball")
		End Sub

		Public Sub TurnCrank() Implements IState.TurnCrank
			Console.WriteLine("Turning again doesn't get you another gumball!")
		End Sub

		Public Sub Dispense() Implements IState.Dispense
			Console.WriteLine("YOU'RE A WINNER! You get two gumballs for your quarter")
			_machine.ReleaseBall()
			If _machine.Count = 0 Then
				_machine.State = _machine.GetSoldOutState()
			Else
				_machine.ReleaseBall()
				If _machine.Count > 0 Then
					_machine.State = _machine.GetNoQuarterState()
				Else
					Console.WriteLine("Oops, out of gumballs!")
					_machine.State = _machine.GetSoldOutState()
				End If
			End If
		End Sub

		Public Overrides Function ToString() As String
			Return "despensing two gumballs for your quarter, because YOU'RE A WINNER!"
		End Function
	End Class
	#End Region
End Namespace
