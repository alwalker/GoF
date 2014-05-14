Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.HeadFirst.Combining.Composite
	Friend Class DuckSimulator
        Shared Sub Main(ByVal args() As String)

            Dim factory As AbstractDuckFactory = New CountingDuckFactory()

            Dim simulator As New DuckSimulator()
            simulator.Simulate(factory)

            ' Wait for user
            Console.ReadKey()
        End Sub

		Private Sub Simulate(ByVal factory As AbstractDuckFactory)
			Dim redheadDuck As IQuackable = factory.CreateRedheadDuck()
			Dim duckCall As IQuackable = factory.CreateDuckCall()
			Dim rubberDuck As IQuackable = factory.CreateRubberDuck()
			Dim gooseDuck As IQuackable = New GooseAdapter(New Goose())

			Console.WriteLine("Duck Simulator: With Composite - Flocks")

			Dim flockOfDucks As New Flock()

			flockOfDucks.Add(redheadDuck)
			flockOfDucks.Add(duckCall)
			flockOfDucks.Add(rubberDuck)
			flockOfDucks.Add(gooseDuck)

			Dim flockOfMallards As New Flock()

			Dim mallardOne As IQuackable = factory.CreateMallardDuck()
			Dim mallardTwo As IQuackable = factory.CreateMallardDuck()
			Dim mallardThree As IQuackable = factory.CreateMallardDuck()
			Dim mallardFour As IQuackable = factory.CreateMallardDuck()

			flockOfMallards.Add(mallardOne)
			flockOfMallards.Add(mallardTwo)
			flockOfMallards.Add(mallardThree)
			flockOfMallards.Add(mallardFour)

			flockOfDucks.Add(flockOfMallards)

			Console.WriteLine(Constants.vbLf & "Duck Simulator: Whole Flock Simulation")
			Simulate(flockOfDucks)

			Console.WriteLine(Constants.vbLf & "Duck Simulator: Mallard Flock Simulation")
			Simulate(flockOfMallards)

			Console.WriteLine(Constants.vbLf & "The ducks quacked " & QuackCounter.Quacks & " times")
		End Sub

		Private Sub Simulate(ByVal duck As IQuackable)
			duck.Quack()
		End Sub
	End Class

	#Region "Factory"

	Public MustInherit Class AbstractDuckFactory
		Public MustOverride Function CreateMallardDuck() As IQuackable
		Public MustOverride Function CreateRedheadDuck() As IQuackable
		Public MustOverride Function CreateDuckCall() As IQuackable
		Public MustOverride Function CreateRubberDuck() As IQuackable
	End Class

	Public Class DuckFactory
		Inherits AbstractDuckFactory
		Public Overrides Function CreateMallardDuck() As IQuackable
			Return New MallardDuck()
		End Function

		Public Overrides Function CreateRedheadDuck() As IQuackable
			Return New RedheadDuck()
		End Function

		Public Overrides Function CreateDuckCall() As IQuackable
			Return New DuckCall()
		End Function

		Public Overrides Function CreateRubberDuck() As IQuackable
			Return New RubberDuck()
		End Function
	End Class

	Public Class CountingDuckFactory
		Inherits AbstractDuckFactory

		Public Overrides Function CreateMallardDuck() As IQuackable
			Return New QuackCounter(New MallardDuck())
		End Function

		Public Overrides Function CreateRedheadDuck() As IQuackable
			Return New QuackCounter(New RedheadDuck())
		End Function

		Public Overrides Function CreateDuckCall() As IQuackable
			Return New QuackCounter(New DuckCall())
		End Function

		Public Overrides Function CreateRubberDuck() As IQuackable
			Return New QuackCounter(New RubberDuck())
		End Function
	End Class

	#End Region

	#Region "Quack Counter"

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

		Public Overrides Function ToString() As String
			Return "Redhead Duck"
		End Function
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

	#Region "Flock"

	Public Class Flock
		Implements IQuackable
		Private _quackers As List(Of IQuackable) = New List(Of IQuackable)()

		Public Sub Add(ByVal quacker As IQuackable)
			_quackers.Add(quacker)
		End Sub

		Public Sub Quack() Implements IQuackable.Quack
			For Each quacker As IQuackable In _quackers
				quacker.Quack()
			Next quacker
		End Sub

		Public Overrides Function ToString() As String
			Return "Flock of Quackers"
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
