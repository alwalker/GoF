Imports Microsoft.VisualBasic
Imports System
Imports System.Text

Namespace DoFactory.HeadFirst.State.GumballState
	Friend Class GumballMachineTestDrive
        Shared Sub Main(ByVal args() As String)

            Dim gumballMachine As New GumballMachine(5)

            Console.WriteLine(gumballMachine)

            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()
            Console.WriteLine(gumballMachine)

            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()
            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()

            Console.WriteLine(gumballMachine)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	#Region "Gumball Machine"

	Public Class GumballMachine
		Private _soldOutState As IState
		Private _noQuarterState As IState
		Private _hasQuarterState As IState
		Private _soldState As IState

		Private privateState As IState
		Public Property State() As IState
			Get
				Return privateState
			End Get
			Set(ByVal value As IState)
				privateState = value
			End Set
		End Property
		Private privateCount As Integer
		Public Property Count() As Integer
			Get
				Return privateCount
			End Get
			Private Set(ByVal value As Integer)
				privateCount = value
			End Set
		End Property

		' Constructor
		Public Sub New(ByVal count As Integer)
			Count = count

			_soldOutState = New SoldOutState(Me)
			_noQuarterState = New NoQuarterState(Me)
			_hasQuarterState = New HasQuarterState(Me)
			_soldState = New SoldState(Me)

			If Count > 0 Then
				State = _noQuarterState
			Else
				State = _soldOutState
			End If
		End Sub

		Public Sub InsertQuarter()
			State.InsertQuarter()
		End Sub

		Public Sub EjectQuarter()
			State.EjectQuarter()
		End Sub

		Public Sub TurnCrank()
			State.TurnCrank()
			State.Dispense()
		End Sub

		Public Sub ReleaseBall()
			Console.WriteLine("A gumball comes rolling out the slot...")
			If Count <> 0 Then
				Count -= 1
			End If
		End Sub

		Public Sub Refill(ByVal count As Integer)
			Count = count
			State = _noQuarterState
		End Sub

		Public ReadOnly Property SoldOutState() As IState
			Get
				Return _soldOutState
			End Get
		End Property

		Public ReadOnly Property NoQuarterState() As IState
			Get
				Return _noQuarterState
			End Get
		End Property

		Public ReadOnly Property HasQuarterState() As IState
			Get
				Return _hasQuarterState
			End Get
		End Property

		Public ReadOnly Property SoldState() As IState
			Get
				Return _soldState
			End Get
		End Property

		Public Overrides Function ToString() As String
			Dim result As New StringBuilder()
			result.Append(Constants.vbLf & "Mighty Gumball, Inc.")
			result.Append(Constants.vbLf & ".NET-enabled Standing Gumball Model #2004")
			result.Append(Constants.vbLf & "Inventory: " & Count & " gumball")
			If Count <> 1 Then
				result.Append("s")
			End If
			result.Append(Constants.vbLf)
            result.Append("Machine is " & State.ToString() & Constants.vbLf)
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

	Public Class HasQuarterState
		Implements IState
		Private _gumballMachine As GumballMachine

		' Constructor
		Public Sub New(ByVal gumballMachine As GumballMachine)
			Me._gumballMachine = gumballMachine
		End Sub

		Public Sub InsertQuarter() Implements IState.InsertQuarter
			Console.WriteLine("You can't insert another quarter")
		End Sub

		Public Sub EjectQuarter() Implements IState.EjectQuarter
			Console.WriteLine("Quarter returned")
			_gumballMachine.State = _gumballMachine.NoQuarterState
		End Sub

		Public Sub TurnCrank() Implements IState.TurnCrank
			Console.WriteLine("You turned...")
			_gumballMachine.State = _gumballMachine.SoldState
		End Sub

		Public Sub Dispense() Implements IState.Dispense
			Console.WriteLine("No gumball dispensed")
		End Sub

		Public Overrides Function ToString() As String
			Return "waiting for turn of crank"
		End Function
	End Class

	Public Class NoQuarterState
		Implements IState
		Private _gumballMachine As GumballMachine

		' Constructor
		Public Sub New(ByVal gumballMachine As GumballMachine)
			Me._gumballMachine = gumballMachine
		End Sub

		Public Sub InsertQuarter() Implements IState.InsertQuarter
			Console.WriteLine("You inserted a quarter")
			_gumballMachine.State = _gumballMachine.HasQuarterState
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

	Public Class SoldOutState
		Implements IState
		Private _gumballMachine As GumballMachine

		' Constructor
		Public Sub New(ByVal gumballMachine As GumballMachine)
			Me._gumballMachine = gumballMachine
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

	Public Class SoldState
		Implements IState
		Private _gumballMachine As GumballMachine

		' Constructor
		Public Sub New(ByVal gumballMachine As GumballMachine)
			Me._gumballMachine = gumballMachine
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
			_gumballMachine.ReleaseBall()
			If _gumballMachine.Count > 0 Then
				_gumballMachine.State = _gumballMachine.NoQuarterState
			Else
				Console.WriteLine("Oops, out of gumballs!")
				_gumballMachine.State = _gumballMachine.SoldOutState
			End If
		End Sub

		Public Overrides Function ToString() As String
			Return "dispensing a gumball"
		End Function
	End Class
	#End Region
End Namespace
