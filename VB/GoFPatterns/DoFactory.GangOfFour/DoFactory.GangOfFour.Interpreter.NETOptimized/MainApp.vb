Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Interpreter.NETOptimized
	''' <summary>
	''' MainApp startup class for .NET optimized 
	''' Interpreter Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            Dim roman As String = "MCMXXVIII"
            Dim context As Context = New Context With {.Input = roman}

            ' Build the 'parse tree'
            Dim tree As List(Of Expression) = New List(Of Expression)()

            tree.Add(New ThousandExpression())
            tree.Add(New HundredExpression())
            tree.Add(New TenExpression())
            tree.Add(New OneExpression())

            ' Interpret
            For Each expression As Expression In tree
                expression.Interpret(context)
            Next expression

            Console.WriteLine("{0} = {1}", roman, context.Output)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Context' class
	''' </summary>
	Friend Class Context
		' Gets or sets input
        Private _input As String
        Public Property Input() As String
            Get
                Return _input
            End Get
            Set(ByVal value As String)
                _input = value
            End Set
        End Property

		' Gets or sets output
        Private _output As Integer
        Public Property Output() As Integer
            Get
                Return _output
            End Get
            Set(ByVal value As Integer)
                _output = value
            End Set
        End Property
	End Class

	''' <summary>
	''' The 'AbstractExpression' abstract class
	''' </summary>
	Friend MustInherit Class Expression
		Public Sub Interpret(ByVal context As Context)
			If context.Input.Length = 0 Then
				Return
			End If

			If context.Input.StartsWith(Nine()) Then
				context.Output += (9 * Multiplier())
				context.Input = context.Input.Substring(2)
			ElseIf context.Input.StartsWith(Four()) Then
				context.Output += (4 * Multiplier())
				context.Input = context.Input.Substring(2)
			ElseIf context.Input.StartsWith(Five()) Then
				context.Output += (5 * Multiplier())
				context.Input = context.Input.Substring(1)
			End If

			Do While context.Input.StartsWith(One())
				context.Output += (1 * Multiplier())
				context.Input = context.Input.Substring(1)
			Loop
		End Sub

		Public MustOverride Function One() As String
		Public MustOverride Function Four() As String
		Public MustOverride Function Five() As String
		Public MustOverride Function Nine() As String
		Public MustOverride Function Multiplier() As Integer
	End Class

	''' <summary>
	''' A 'TerminalExpression' class
	''' <remarks>
	''' Thousand checks for the Roman Numeral M
	''' </remarks>
	''' </summary>
	Friend Class ThousandExpression
		Inherits Expression
		Public Overrides Function One() As String
			Return "M"
		End Function
		Public Overrides Function Four() As String
			Return " "
		End Function
		Public Overrides Function Five() As String
			Return " "
		End Function
		Public Overrides Function Nine() As String
			Return " "
		End Function
		Public Overrides Function Multiplier() As Integer
			Return 1000
		End Function
	End Class

	''' <summary>
	''' A 'TerminalExpression' class
	''' <remarks>
	''' Hundred checks C, CD, D or CM
	''' </remarks>
	''' </summary>
	Friend Class HundredExpression
		Inherits Expression
		Public Overrides Function One() As String
			Return "C"
		End Function
		Public Overrides Function Four() As String
			Return "CD"
		End Function
		Public Overrides Function Five() As String
			Return "D"
		End Function
		Public Overrides Function Nine() As String
			Return "CM"
		End Function
		Public Overrides Function Multiplier() As Integer
			Return 100
		End Function
	End Class

	''' <summary>
	''' A 'TerminalExpression' class
	''' <remarks>
	''' Ten checks for X, XL, L and XC
	''' </remarks>
	''' </summary>
	Friend Class TenExpression
		Inherits Expression
		Public Overrides Function One() As String
			Return "X"
		End Function
		Public Overrides Function Four() As String
			Return "XL"
		End Function
		Public Overrides Function Five() As String
			Return "L"
		End Function
		Public Overrides Function Nine() As String
			Return "XC"
		End Function
		Public Overrides Function Multiplier() As Integer
			Return 10
		End Function
	End Class

	''' <summary>
	''' A 'TerminalExpression' class
	''' <remarks>
	''' One checks for I, II, III, IV, V, VI, VI, VII, VIII, IX
	''' </remarks>
	''' </summary>
	Friend Class OneExpression
		Inherits Expression
		Public Overrides Function One() As String
			Return "I"
		End Function
		Public Overrides Function Four() As String
			Return "IV"
		End Function
		Public Overrides Function Five() As String
			Return "V"
		End Function
		Public Overrides Function Nine() As String
			Return "IX"
		End Function
		Public Overrides Function Multiplier() As Integer
			Return 1
		End Function
	End Class
End Namespace
