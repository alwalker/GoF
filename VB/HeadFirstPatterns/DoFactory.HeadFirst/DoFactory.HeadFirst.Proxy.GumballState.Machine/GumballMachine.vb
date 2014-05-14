Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.ServiceModel

Namespace DoFactory.HeadFirst.Proxy.GumballState.Machine
	<ServiceContract(Name := "GumballMachine", Namespace := "http://www.mycompany.com/headfirst/2008/07", SessionMode := SessionMode.Required)> _
	Public Interface IGumballMachine
		<OperationContract> _
		Sub StartWithQuarters(ByVal count As Integer)

		<OperationContract> _
		Sub InsertQuarter()

		<OperationContract> _
		Sub TurnCrank()

		<OperationContract> _
		Sub EjectQuarter()

		<OperationContract> _
		Function GetReport() As String
	End Interface

	Public Class GumballMachine
		Implements IGumballMachine
		Private _state As GumballMachineState = GumballMachineState.SoldOut
		Private _count As Integer = 0

		Private _log As New StringBuilder()

		Public Sub StartWithQuarters(ByVal count As Integer) Implements IGumballMachine.StartWithQuarters
			Me._count = count
			If _count > 0 Then
				_state = GumballMachineState.NoQuarter
			End If
		End Sub

		Public Sub InsertQuarter() Implements IGumballMachine.InsertQuarter
			If _state = GumballMachineState.HasQuarter Then
				_log.Append("You can't insert another quarter" & Constants.vbLf)
			ElseIf _state = GumballMachineState.NoQuarter Then
				_state = GumballMachineState.HasQuarter
				_log.Append("You inserted a quarter" & Constants.vbLf)
			ElseIf _state = GumballMachineState.SoldOut Then
				_log.Append("You can't insert a quarter, the machine is sold out" & Constants.vbLf)
			ElseIf _state = GumballMachineState.Sold Then
				_log.Append("Please wait, we're already giving you a gumball" & Constants.vbLf)
			End If
		End Sub

		Public Sub TurnCrank() Implements IGumballMachine.TurnCrank
			If _state = GumballMachineState.Sold Then
				_log.Append("Turning twice doesn't get you another gumball!" & Constants.vbLf)
			ElseIf _state = GumballMachineState.NoQuarter Then
				_log.Append("You turned but there's no quarter" & Constants.vbLf)
			ElseIf _state = GumballMachineState.SoldOut Then
				_log.Append("You turned, but there are no gumballs" & Constants.vbLf)
			ElseIf _state = GumballMachineState.HasQuarter Then
				_log.Append("You turned..." & Constants.vbLf)
				_state = GumballMachineState.Sold
				Dispense()
			End If
		End Sub

		Public Sub EjectQuarter() Implements IGumballMachine.EjectQuarter
			If _state = GumballMachineState.HasQuarter Then
				_log.Append("Quarter returned" & Constants.vbLf)
				_state = GumballMachineState.NoQuarter
			ElseIf _state = GumballMachineState.NoQuarter Then
				_log.Append("You haven't inserted a quarter" & Constants.vbLf)
			ElseIf _state = GumballMachineState.Sold Then
				_log.Append("Sorry, you already turned the crank" & Constants.vbLf)
			ElseIf _state = GumballMachineState.SoldOut Then
				_log.Append("You can't eject, you haven't inserted a quarter yet" & Constants.vbLf)
			End If
		End Sub


		Public Sub Dispense()
			If _state = GumballMachineState.Sold Then
				_log.Append("A gumball comes rolling out the slot" & Constants.vbLf)
				_count = _count - 1
				If _count = 0 Then
					_log.Append("Oops, out of gumballs!" & Constants.vbLf)
					_state = GumballMachineState.SoldOut
				Else
					_state = GumballMachineState.NoQuarter
				End If
			ElseIf _state = GumballMachineState.NoQuarter Then
				_log.Append("You need to pay first" & Constants.vbLf)
			ElseIf _state = GumballMachineState.SoldOut Then
				_log.Append("No gumball dispensed" & Constants.vbLf)
			ElseIf _state = GumballMachineState.HasQuarter Then
				_log.Append("No gumball dispensed" & Constants.vbLf)
			End If
		End Sub

		Public Sub Refill(ByVal numGumBalls As Integer)
			_count = numGumBalls
			_state = GumballMachineState.NoQuarter
		End Sub

		Public Function GetReport() As String Implements IGumballMachine.GetReport
			Dim result As New StringBuilder()
			result.Append(Constants.vbLf & "Mighty Gumball, Inc.")
			result.Append(Constants.vbLf & ".NET3.5-enabled Standing Gumball Model #2104" & Constants.vbLf)
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

			Dim ret As String = _log.ToString() & Constants.vbLf + result.ToString()
			_log = New StringBuilder()

			Return ret.ToString()
		End Function
	End Class

    ' State enumeration

	Public Enum GumballMachineState
		SoldOut
		NoQuarter
		HasQuarter
		Sold
	End Enum

End Namespace
