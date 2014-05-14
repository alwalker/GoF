Imports Microsoft.VisualBasic
Imports System

Namespace DoFactory.HeadFirst.Strategy
	Public Class MiniDuckSimulator
        Shared Sub Main(ByVal args() As String)

            Dim mallard As Duck = New MallardDuck()
            mallard.Display()
            mallard.PerformQuack()
            mallard.PerformFly()

            Console.WriteLine("")

            Dim model As Duck = New ModelDuck()
            model.Display()
            model.PerformFly()

            model.FlyBehavior = New FlyRocketPowered()
            model.PerformFly()

            ' Wait for user input
            Console.ReadKey()
        End Sub
	End Class

	#Region "Duck"

	Public MustInherit Class Duck
		Private privateFlyBehavior As IFlyBehavior
		Public Property FlyBehavior() As IFlyBehavior
			Get
				Return privateFlyBehavior
			End Get
			Set(ByVal value As IFlyBehavior)
				privateFlyBehavior = value
			End Set
		End Property
		Private privateQuackBehavior As IQuackBehavior
		Public Property QuackBehavior() As IQuackBehavior
			Get
				Return privateQuackBehavior
			End Get
			Set(ByVal value As IQuackBehavior)
				privateQuackBehavior = value
			End Set
		End Property

		Public MustOverride Sub Display()

		Public Sub PerformFly()
			FlyBehavior.Fly()
		End Sub

		Public Sub PerformQuack()
			QuackBehavior.Quack()
		End Sub

		Public Sub Swim()
			Console.WriteLine("All ducks float, even decoys!")
		End Sub

	End Class

	Public Class MallardDuck
		Inherits Duck
		Public Sub New()
			QuackBehavior = New LoudQuack()
			FlyBehavior = New FlyWithWings()
		End Sub

		Public Overrides Sub Display()
			Console.WriteLine("I'm a real Mallard duck")
		End Sub
	End Class

	Public Class ModelDuck
		Inherits Duck
		Public Sub New()
			QuackBehavior = New LoudQuack()
			FlyBehavior = New FlyNoWay()
		End Sub

		Public Overrides Sub Display()
			Console.WriteLine("I'm a model duck")
		End Sub
	End Class

	#End Region

	#Region "FlyBehavior"

	Public Interface IFlyBehavior
		Sub Fly()
	End Interface

	Public Class FlyWithWings
		Implements IFlyBehavior
		Public Sub Fly() Implements IFlyBehavior.Fly
			Console.WriteLine("I'm flying!!")
		End Sub
	End Class
	Public Class FlyNoWay
		Implements IFlyBehavior
		Public Sub Fly() Implements IFlyBehavior.Fly
			Console.WriteLine("I can't fly")
		End Sub
	End Class
	Public Class FlyRocketPowered
		Implements IFlyBehavior
		Public Sub Fly() Implements IFlyBehavior.Fly
			Console.WriteLine("I'm flying with a rocket!")
		End Sub
	End Class
	#End Region

	#Region "QuackBehavior"

	Public Interface IQuackBehavior
		Sub Quack()
	End Interface

	' Name it LoadQuack to avoid conflict with method name
	Public Class LoudQuack
		Implements IQuackBehavior
		Public Sub Quack() Implements IQuackBehavior.Quack
			Console.WriteLine("LoudQuack")
		End Sub
	End Class

	Public Class MuteQuack
		Implements IQuackBehavior
		Public Sub Quack() Implements IQuackBehavior.Quack
			Console.WriteLine("<< Silence >>")
		End Sub
	End Class

	Public Class Squeak
		Implements IQuackBehavior
		Public Sub Quack() Implements IQuackBehavior.Quack
			Console.WriteLine("Squeak")
		End Sub
	End Class
	#End Region
End Namespace
