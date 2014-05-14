Imports Microsoft.VisualBasic
Imports System

Namespace DoFactory.GangOfFour.Abstract.RealWorld
	''' <summary>
	''' MainApp startup class for Real-World
	''' Abstract Factory Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Public Shared Sub Main()

            ' Create and run the African animal world
            Dim africa As ContinentFactory = New AfricaFactory()
            Dim world As New AnimalWorld(africa)
            world.RunFoodChain()

            ' Create and run the American animal world
            Dim america As ContinentFactory = New AmericaFactory()
            world = New AnimalWorld(america)
            world.RunFoodChain()

            ' Wait for user input
            Console.ReadKey()
        End Sub
	End Class


	''' <summary>
	''' The 'AbstractFactory' abstract class
	''' </summary>
	Friend MustInherit Class ContinentFactory
		Public MustOverride Function CreateHerbivore() As Herbivore
		Public MustOverride Function CreateCarnivore() As Carnivore
	End Class

	''' <summary>
	''' The 'ConcreteFactory1' class
	''' </summary>
	Friend Class AfricaFactory
		Inherits ContinentFactory
		Public Overrides Function CreateHerbivore() As Herbivore
			Return New Wildebeest()
		End Function
		Public Overrides Function CreateCarnivore() As Carnivore
			Return New Lion()
		End Function
	End Class

	''' <summary>
	''' The 'ConcreteFactory2' class
	''' </summary>
	Friend Class AmericaFactory
		Inherits ContinentFactory
		Public Overrides Function CreateHerbivore() As Herbivore
			Return New Bison()
		End Function
		Public Overrides Function CreateCarnivore() As Carnivore
			Return New Wolf()
		End Function
	End Class

	''' <summary>
	''' The 'AbstractProductA' abstract class
	''' </summary>
	Friend MustInherit Class Herbivore
	End Class

	''' <summary>
	''' The 'AbstractProductB' abstract class
	''' </summary>
	Friend MustInherit Class Carnivore
		Public MustOverride Sub Eat(ByVal h As Herbivore)
	End Class

	''' <summary>
	''' The 'ProductA1' class
	''' </summary>
	Friend Class Wildebeest
		Inherits Herbivore
	End Class

	''' <summary>
	''' The ProductB1' class
	''' </summary>
	Friend Class Lion
		Inherits Carnivore
		Public Overrides Sub Eat(ByVal h As Herbivore)
			' Eat Wildebeest
			Console.WriteLine(Me.GetType().Name & " eats " & h.GetType().Name)
		End Sub
	End Class

	''' <summary>
	''' The 'ProductA2' class
	''' </summary>
	Friend Class Bison
		Inherits Herbivore
	End Class

	''' <summary>
	''' The 'ProductB2' class
	''' </summary>
	Friend Class Wolf
		Inherits Carnivore
		Public Overrides Sub Eat(ByVal h As Herbivore)
			' Eat Bison
			Console.WriteLine(Me.GetType().Name & " eats " & h.GetType().Name)
		End Sub
	End Class

	''' <summary>
	''' The 'Client' class 
	''' </summary>
	Friend Class AnimalWorld
		Private _herbivore As Herbivore
		Private _carnivore As Carnivore

		' Constructor
		Public Sub New(ByVal factory As ContinentFactory)
			_carnivore = factory.CreateCarnivore()
			_herbivore = factory.CreateHerbivore()
		End Sub

		Public Sub RunFoodChain()
			_carnivore.Eat(_herbivore)
		End Sub
	End Class
End Namespace