Imports Microsoft.VisualBasic
Imports System

Namespace DoFactory.HeadFirst.Combining.Decorator
	Friend Class DuckSimulator
        Shared Sub Main(ByVal args() As String)

            Dim simulator As New DuckSimulator()
            simulator.Simulate()

        End Sub

        Private Sub Simulate()

            Dim mallardDuck As IQuackable = New QuackCounter(New MallardDuck())
            Dim redheadDuck As IQuackable = New QuackCounter(New RedheadDuck())
            Dim duckCall As IQuackable = New QuackCounter(New DuckCall())
            Dim rubberDuck As IQuackable = New QuackCounter(New RubberDuck())
            Dim gooseDuck As IQuackable = New GooseAdapter(New Goose())

            Console.WriteLine("Duck Simulator: With Decorator")

            Simulate(mallardDuck)
            Simulate(redheadDuck)
            Simulate(duckCall)
            Simulate(rubberDuck)
            Simulate(gooseDuck)

            Console.WriteLine("The ducks quacked " & QuackCounter.Quacks & " times")

            ' Wait for user
            Console.ReadKey()
        End Sub

		Private Sub Simulate(ByVal duck As IQuackable)
			duck.Quack()
		End Sub
	End Class

	#Region "Ducks "

	Public Interface IQuackable
		Sub Quack()
	End Interface

	Public Class RubberDuck
		Implements IQuackable
		Public Sub Quack() Implements IQuackable.Quack
			Console.WriteLine("Squeak")
		End Sub

		Public Overrides Function ToString() As String
			Return "Rubber Duck"
		End Function
	End Class

	Public Class RedheadDuck
		Implements IQuackable
		Public Sub Quack() Implements IQuackable.Quack
			Console.WriteLine("Quack")
		End Sub
	End Class

	Public Class MallardDuck
		Implements IQuackable
		 Public Sub Quack() Implements IQuackable.Quack
			Console.WriteLine("Quack")
		 End Sub

		Public Overrides Function ToString() As String
			Return "Mallard Duck"
		End Function
	End Class

	Public Class DuckCall
		Implements IQuackable
		 Public Sub Quack() Implements IQuackable.Quack
			Console.WriteLine("Kwak")
		 End Sub

		Public Overrides Function ToString() As String
			Return "Duck Call"
		End Function
	End Class

	Public Class DecoyDuck
		Implements IQuackable
		 Public Sub Quack() Implements IQuackable.Quack
			Console.WriteLine("<< Silence >>")
		 End Sub

		Public Overrides Function ToString() As String
			Return "Decoy Duck"
		End Function
	End Class

	#End Region

	#Region "QuackCounter"

	Public Class QuackCounter
		Implements IQuackable
		Private _duck As IQuackable

		Public Sub New(ByVal duck As IQuackable)
			Me._duck = duck
		End Sub

		Public Sub Quack() Implements IQuackable.Quack
			_duck.Quack()
			Quacks += 1
		End Sub

        Private Shared _quacks As Integer
		Public Shared Property Quacks() As Integer
			Get
                Return _quacks
			End Get
			Private Set(ByVal value As Integer)
                _quacks = value
			End Set
		End Property

		Public Overrides Function ToString() As String
			Return CType(_duck, Object).ToString()
		End Function
	End Class

	#End Region

	#Region "Goose "

	Public Class GooseAdapter
		Implements IQuackable
		Private _goose As Goose

		Public Sub New(ByVal goose As Goose)
			Me._goose = goose
		End Sub

		Public Sub Quack() Implements IQuackable.Quack
			_goose.Honk()
		End Sub

		Public Overrides Function ToString() As String
			Return "Goose pretending to be a Duck"
		End Function
	End Class

	Public Class Goose
		Public Sub Honk()
			Console.WriteLine("Honk")
		End Sub

		Public Overrides Function ToString() As String
			Return "Goose"
		End Function
	End Class
	#End Region
End Namespace
