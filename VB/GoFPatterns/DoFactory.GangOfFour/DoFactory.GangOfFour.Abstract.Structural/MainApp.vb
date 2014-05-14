Imports Microsoft.VisualBasic
Imports System

Namespace DoFactory.GangOfFour.Abstract.Structural
	''' <summary>
	''' MainApp startup class for Structural
	''' Abstract Factory Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Public Shared Sub Main()

            ' Abstract factory #1
            Dim factory1 As AbstractFactory = New ConcreteFactory1()
            Dim client1 As New Client(factory1)
            client1.Run()

            ' Abstract factory #2
            Dim factory2 As AbstractFactory = New ConcreteFactory2()
            Dim client2 As New Client(factory2)
            client2.Run()

            ' Wait for user input
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'AbstractFactory' abstract class
	''' </summary>
	Friend MustInherit Class AbstractFactory
		Public MustOverride Function CreateProductA() As AbstractProductA
		Public MustOverride Function CreateProductB() As AbstractProductB
	End Class


	''' <summary>
	''' The 'ConcreteFactory1' class
	''' </summary>
	Friend Class ConcreteFactory1
		Inherits AbstractFactory
		Public Overrides Function CreateProductA() As AbstractProductA
			Return New ProductA1()
		End Function
		Public Overrides Function CreateProductB() As AbstractProductB
			Return New ProductB1()
		End Function
	End Class

	''' <summary>
	''' The 'ConcreteFactory2' class
	''' </summary>
	Friend Class ConcreteFactory2
		Inherits AbstractFactory
		Public Overrides Function CreateProductA() As AbstractProductA
			Return New ProductA2()
		End Function
		Public Overrides Function CreateProductB() As AbstractProductB
			Return New ProductB2()
		End Function
	End Class

	''' <summary>
	''' The 'AbstractProductA' abstract class
	''' </summary>
	Friend MustInherit Class AbstractProductA
	End Class

	''' <summary>
	''' The 'AbstractProductB' abstract class
	''' </summary>
	Friend MustInherit Class AbstractProductB
		Public MustOverride Sub Interact(ByVal a As AbstractProductA)
	End Class


	''' <summary>
	''' The 'ProductA1' class
	''' </summary>
	Friend Class ProductA1
		Inherits AbstractProductA
	End Class

	''' <summary>
	''' The 'ProductB1' class
	''' </summary>
	Friend Class ProductB1
		Inherits AbstractProductB
		Public Overrides Sub Interact(ByVal a As AbstractProductA)
			Console.WriteLine(Me.GetType().Name & " interacts with " & a.GetType().Name)
		End Sub
	End Class

	''' <summary>
	''' The 'ProductA2' class
	''' </summary>
	Friend Class ProductA2
		Inherits AbstractProductA
	End Class

	''' <summary>
	''' The 'ProductB2' class
	''' </summary>
	Friend Class ProductB2
		Inherits AbstractProductB
		Public Overrides Sub Interact(ByVal a As AbstractProductA)
			Console.WriteLine(Me.GetType().Name & " interacts with " & a.GetType().Name)
		End Sub
	End Class

	''' <summary>
	''' The 'Client' class. Interaction environment for the products.
	''' </summary>
	Friend Class Client
		Private _abstractProductA As AbstractProductA
		Private _abstractProductB As AbstractProductB

		' Constructor
		Public Sub New(ByVal factory As AbstractFactory)
			_abstractProductB = factory.CreateProductB()
			_abstractProductA = factory.CreateProductA()
		End Sub

		Public Sub Run()
			_abstractProductB.Interact(_abstractProductA)
		End Sub
	End Class
End Namespace

