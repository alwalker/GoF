Imports Microsoft.VisualBasic
Imports System
Imports System.Text

Namespace DoFactory.HeadFirst.State.Gumball
	Friend Class GumballMachineTestDrive
        Shared Sub Main(ByVal args() As String)

            Dim gumballMachine As New GumballMachine(5)

            Console.WriteLine(gumballMachine)

            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()

            Console.WriteLine(gumballMachine)

            gumballMachine.InsertQuarter()
            gumballMachine.EjectQuarter()
            gumballMachine.TurnCrank()

            Console.WriteLine(gumballMachine)

            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()
            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()
            gumballMachine.EjectQuarter()

            Console.WriteLine(gumballMachine)

            gumballMachine.InsertQuarter()
            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()
            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()
            gumballMachine.InsertQuarter()
            gumballMachine.TurnCrank()

            Console.WriteLine(gumballMachine)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	#Region "GumballMachine"

	Public Class GumballMachine
		Private _state As GumballMachineState = GumballMachineState.SoldOut
		Private _count As Integer = 0

		Public Sub New(ByVal count As Integer)
			Me._count = count
			If _count > 0 Then
				_state = GumballMachineState.NoQuarter
			End If
		End Sub

		Public Sub InsertQuarter()
			If _state = GumballMachineState.HasQuarter Then
				Console.WriteLine("You can't insert another quarter")
			ElseIf _state = GumballMachineState.NoQuarter Then
				_state = GumballMachineState.HasQuarter
				Console.WriteLine("You inserted a quarter")
			ElseIf _state = GumballMachineState.SoldOut Then
				Console.WriteLine("You can't insert a quarter, the machine is sold out")
			ElseIf _state = GumballMachineState.Sold Then
				Console.WriteLine("Please wait, we're already giving you a gumball")
			End If
		End Sub

		Public Sub EjectQuarter()
			If _state = GumballMachineState.HasQuarter Then
				Console.WriteLine("Quarter returned")
				_state = GumballMachineState.NoQuarter
			ElseIf _state = GumballMachineState.NoQuarter Then
				Console.WriteLine("You haven't inserted a quarter")
			ElseIf _state = GumballMachineState.Sold Then
				Console.WriteLine("Sorry, you already turned the crank")
			ElseIf _state = GumballMachineState.SoldOut Then
				Console.WriteLine("You can't eject, you haven't inserted a quarter yet")
			End If
		End Sub

		Public Sub TurnCrank()
			If _state = GumballMachineState.Sold Then
				Console.WriteLine("Turning twice doesn't get you another gumball!")
			ElseIf _state = GumballMachineState.NoQuarter Then
				Console.WriteLine("You turned but there's no quarter")
			ElseIf _state = GumballMachineState.SoldOut Then
				Console.WriteLine("You turned, but there are no gumballs")
			ElseIf _state = GumballMachineState.HasQuarter Then
				Console.WriteLine("You turned...")
				_state = GumballMachineState.Sold
				Dispense()
			End If
		End Sub

		Public Sub Dispense()
			If _state = GumballMachineState.Sold Then
				Console.WriteLine("A gumball comes rolling out the slot")
				_count = _count - 1
				If _count = 0 Then
					Console.WriteLine("Oops, out of gumballs!")
					_state = GumballMachineState.SoldOut
				Else
					_state = GumballMachineState.NoQuarter
				End If
			ElseIf _state = GumballMachineState.NoQuarter Then
				Console.WriteLine("You need to pay first")
			ElseIf _state = GumballMachineState.SoldOut Then
				Console.WriteLine("No gumball dispensed")
			ElseIf _state = GumballMachineState.HasQuarter Then
				Console.WriteLine("No gumball dispensed")
			End If
		End Sub

		Public Sub Refill(ByVal numGumBalls As Integer)
			_count = numGumBalls
			_state = GumballMachineState.NoQuarter
		End Sub

		Public Overrides Function ToString() As String
			Dim result As New StringBuilder()
			result.Append(Constants.vbLf & "Mighty Gumball, Inc.")
			result.Append(Constants.vbLf & "Java-enabled Standing Gumball Model #2004" & Constants.vbLf)
			result.Append("Inventory: " & _count & " gumball")

			If _count <> 1 Then
				result.Append("s")
			End If
			result.Append(Constants.vbLf & "Machine is ")
			If _state = GumballMachineState.SoldOut Then
				result.Append("sold out")
			ElseIf _state = GumballMachineState.NoQuarter Then
				result.Append("waiting for quarter")
			ElseIf _state = GumballMachineState.HasQuarter Then
				result.Append("waiting for turn of crank")
			ElseIf _state = GumballMachineState.Sold Then
				result.Append("delivering a gumball")
			End If
			result.Append(Constants.vbLf)
			Return result.ToString()
		End Function
	End Class
	#End Region

	#Region "State"

	' State enumercation

	Public Enum GumballMachineState
		SoldOut
		NoQuarter
		HasQuarter
		Sold
	End Enum
	#End Region
End Namespace


