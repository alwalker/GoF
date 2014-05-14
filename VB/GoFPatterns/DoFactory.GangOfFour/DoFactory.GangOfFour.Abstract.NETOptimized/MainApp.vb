Imports Microsoft.VisualBasic
Imports System

Namespace DoFactory.GangOfFour.Abstract.NETOptimized
	''' <summary>
	''' MainApp startup class for .NET optimized 
	''' Abstract Factory Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Public Shared Sub Main()

            ' Create and run the African animal world
            Dim world As New AnimalWorld(Continent.Africa)
            world.RunFoodChain()

            ' Create and run the American animal world
            world = New AnimalWorld(Continent.America)
            world.RunFoodChain()

            ' Wait for user input
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'AbstractFactory' interface. 
	''' </summary>
	Friend Interface IContinentFactory
		Function CreateHerbivore() As IHerbivore
		Function CreateCarnivore() As ICarnivore
	End Interface

	''' <summary>
	''' The 'ConcreteFactory1' class.
	''' </summary>
	Friend Class AfricaFactory
		Implements IContinentFactory
		Public Function CreateHerbivore() As IHerbivore Implements IContinentFactory.CreateHerbivore
			Return New Wildebeest()
		End Function

		Public Function CreateCarnivore() As ICarnivore Implements IContinentFactory.CreateCarnivore
			Return New Lion()
		End Function
	End Class

	''' <summary>
	''' The 'ConcreteFactory2' class.
	''' </summary>
	Friend Class AmericaFactory
		Implements IContinentFactory
		Public Function CreateHerbivore() As IHerbivore Implements IContinentFactory.CreateHerbivore
			Return New Bison()
		End Function

		Public Function CreateCarnivore() As ICarnivore Implements IContinentFactory.CreateCarnivore
			Return New Wolf()
		End Function
	End Class

	''' <summary>
	''' The 'AbstractProductA' interface
	''' </summary>
	Friend Interface IHerbivore
	End Interface

	''' <summary>
	''' The 'AbstractProductB' interface
	''' </summary>
	Friend Interface ICarnivore
		Sub Eat(ByVal h As IHerbivore)
	End Interface

	''' <summary>
	''' The 'ProductA1' class
	''' </summary>
	Friend Class Wildebeest
		Implements IHerbivore
	End Class

	''' <summary>
	''' The 'ProductB1' class
	''' </summary>
	Friend Class Lion
		Implements ICarnivore
		Public Sub Eat(ByVal h As IHerbivore) Implements ICarnivore.Eat
			' Eat Wildebeest
			Console.WriteLine(Me.GetType().Name & " eats " & CType(h, Object).GetType().Name)
		End Sub
	End Class

	''' <summary>
	''' The 'ProductA2' class
	''' </summary>
	Friend Class Bison
		Implements IHerbivore
	End Class

	''' <summary>
	''' The 'ProductB2' class
	''' </summary>
	Friend Class Wolf
		Implements ICarnivore
		Public Sub Eat(ByVal h As IHerbivore) Implements ICarnivore.Eat
			' Eat Bison
			Console.WriteLine(Me.GetType().Name & " eats " & CType(h, Object).GetType().Name)
		End Sub
	End Class

	''' <summary>
	''' The 'Client' class
	''' </summary>
	Friend Class AnimalWorld
		Private _herbivore As IHerbivore
		Private _carnivore As ICarnivore

		''' <summary>
		''' Contructor of Animalworld
		''' </summary>
		''' <param name="continent">Continent of the animal world that is created.</param>
		Public Sub New(ByVal continent As Continent)
			' Get fully qualified factory name
			Dim name As String = Me.GetType().Namespace + "." & continent.ToString() & "Factory"

			' Dynamic factory creation
			Dim factory As IContinentFactory = CType(System.Activator.CreateInstance (Type.GetType(name)), IContinentFactory)

			' Factory creates carnivores and herbivores
			_carnivore = factory.CreateCarnivore()
			_herbivore = factory.CreateHerbivore()
		End Sub

		''' <summary>
		''' Runs the foodchain, that is, carnivores are eating herbivores.
		''' </summary>
		Public Sub RunFoodChain()
			_carnivore.Eat(_herbivore)
		End Sub
	End Class

	''' <summary>
	''' Enumeration of continents.
	''' </summary>
	Friend Enum Continent
		''' <summary>
		''' Represents continent of Africa.
		''' </summary>
		Africa

		''' <summary>
		''' Represents continent of America.
		''' </summary>
		America

		''' <summary>
		''' Represents continent of Asia.
		''' </summary>
		Asia

		''' <summary>
		''' Represents continent of Europe.
		''' </summary>
		Europe

		''' <summary>
		''' Represents continent of Australia.
		''' </summary>
		Australia
	End Enum
End Namespace
