Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Command.NETOptimized
	''' <summary>
	''' MainApp startup class for .NET optimized 
	''' Command Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Create user and let her compute
            Dim user As New User()

            ' Issue several compute commands
            user.Compute("+"c, 100)
            user.Compute("-"c, 50)
            user.Compute("*"c, 10)
            user.Compute("/"c, 2)

            ' Undo 4 commands
            user.Undo(4)

            ' Redo 3 commands
            user.Redo(3)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Command' interface
	''' </summary>
	Friend Interface ICommand
		Sub Execute()
		Sub UnExecute()
	End Interface

	''' <summary>
	''' The 'ConcreteCommand' class
	''' </summary>
	Friend Class CalculatorCommand
		Implements ICommand
		Private _operator As Char
		Private _operand As Integer
		Private _calculator As Calculator

		' Constructor
		Public Sub New(ByVal calculator As Calculator, ByVal [operator] As Char, ByVal operand As Integer)
			Me._calculator = calculator
			Me._operator = [operator]
			Me._operand = operand
		End Sub

		' Sets operator
		Public WriteOnly Property [Operator]() As Char
			Set(ByVal value As Char)
				_operator = value
			End Set
		End Property

		' Sets operand
		Public WriteOnly Property Operand() As Integer
			Set(ByVal value As Integer)
				_operand = value
			End Set
		End Property

		' Execute command
		Public Sub Execute() Implements ICommand.Execute
			_calculator.Operation(_operator, _operand)
		End Sub

		' Unexecute command
		Public Sub UnExecute() Implements ICommand.UnExecute
			_calculator.Operation(Undo(_operator), _operand)
		End Sub

		' Return opposite operator for given operator
		Private Function Undo(ByVal [operator] As Char) As Char
			Select Case [operator]
				Case "+"c
					Return "-"c
				Case "-"c
					Return "+"c
				Case "*"c
					Return "/"c
				Case "/"c
					Return "*"c
				Case Else
					Throw New ArgumentException("@operator")
			End Select
		End Function
	End Class

	''' <summary>
	''' The 'Receiver' class
	''' </summary>
	Friend Class Calculator
		Private _current As Integer = 0

		' Perform operation for given operator and operand
		Public Sub Operation(ByVal [operator] As Char, ByVal operand As Integer)
			Select Case [operator]
				Case "+"c
					_current += operand
				Case "-"c
					_current -= operand
				Case "*"c
					_current *= operand
				Case "/"c
                    _current /= operand
			End Select
			Console.WriteLine("Current value = {0,3} (following {1} {2})", _current, [operator], operand)
		End Sub
	End Class

	''' <summary>
	''' The 'Invoker' class
	''' </summary>
	Friend Class User
		Private _calculator As New Calculator()
		Private _commands As List(Of ICommand) = New List(Of ICommand)()
		Private _current As Integer = 0

		' Redo original commands
		Public Sub Redo(ByVal levels As Integer)
			Console.WriteLine(Constants.vbLf & "---- Redo {0} levels ", levels)

			' Perform redo operations
			For i As Integer = 0 To levels - 1
				If _current < _commands.Count - 1 Then
					_commands(_current).Execute()
					_current += 1
				End If
			Next i
		End Sub

		' Undo prior commands
		Public Sub Undo(ByVal levels As Integer)
			Console.WriteLine(Constants.vbLf & "---- Undo {0} levels ", levels)

			' Perform undo operations
			For i As Integer = 0 To levels - 1
				If _current > 0 Then
					_current -= 1
					_commands(_current).UnExecute()
				End If
			Next i
		End Sub

		' Compute new value given operator and operand
		Public Sub Compute(ByVal [operator] As Char, ByVal operand As Integer)
			' Create command operation and execute it
			Dim command As ICommand = New CalculatorCommand(_calculator, [operator], operand)
			command.Execute()

			' Add command to undo list
			_commands.Add(command)
			_current += 1
		End Sub
	End Class
End Namespace
