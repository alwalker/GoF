Imports Microsoft.VisualBasic
Imports System
Imports System.Text
Imports System.Collections.Generic

Namespace DoFactory.HeadFirst.Factory.PizzaShop
	Friend Class PizzaTestDrive
        Shared Sub Main(ByVal args() As String)

            Dim factory As New SimplePizzaFactory()
            Dim store As New PizzaStore(factory)

            Dim pizza As Pizza = store.OrderPizza("cheese")
            Console.WriteLine("We ordered a " & pizza.Name + Constants.vbLf)

            pizza = store.OrderPizza("veggie")
            Console.WriteLine("We ordered a " & pizza.Name + Constants.vbLf)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class
	#Region "PizzaStore"

	Public Class PizzaStore
		Private _factory As SimplePizzaFactory

		Public Sub New(ByVal factory As SimplePizzaFactory)
			Me._factory = factory
		End Sub

		Public Function OrderPizza(ByVal type As String) As Pizza
			Dim pizza As Pizza = _factory.CreatePizza(type)

			pizza.Prepare()
			pizza.Bake()
			pizza.Cut()
			pizza.Box()

			Return pizza
		End Function
	End Class

	#End Region

	#Region "SimplePizzaFactory"

	Public Class SimplePizzaFactory
		Public Function CreatePizza(ByVal type As String) As Pizza
			Dim pizza As Pizza = Nothing
			Select Case type
				Case "cheese"
					pizza = New CheesePizza()
				Case "pepperoni"
					pizza = New PepperoniPizza()
				Case "clam"
					pizza = New ClamPizza()
				Case "veggie"
					pizza = New VeggiePizza()
			End Select
			Console.WriteLine(pizza)
			Return pizza
		End Function
	End Class

	#End Region

	#Region "Pizza"

	Public MustInherit Class Pizza
		Private _name As String
		Private _dough As String
		Private _sauce As String
		Private toppings_Renamed As List(Of String) = New List(Of String)()

		Public Sub New(ByVal name As String, ByVal dough As String, ByVal sauce As String)
			Me._name = name
			Me._dough = dough
			Me._sauce = sauce
		End Sub

		Public Property Name() As String
			Get
				Return _name
			End Get
			Set(ByVal value As String)
				_name = value
			End Set
		End Property

		Public ReadOnly Property Toppings() As List(Of String)
			Get
				Return toppings_Renamed
			End Get
		End Property

		Public Sub Prepare()
			Console.WriteLine("Preparing " & _name)
		End Sub

		Public Sub Bake()
			Console.WriteLine("Baking " & _name)
		End Sub

		Public Sub Cut()
			Console.WriteLine("Cutting " & _name)
		End Sub

		Public Sub Box()
			Console.WriteLine("Boxing " & _name)
		End Sub

		' code to display pizza name and ingredients
		Public Overrides Function ToString() As String
			Dim display As New StringBuilder()
			display.Append("---- " & _name & " ----" & Constants.vbLf)
			display.Append(_dough & Constants.vbLf)
			display.Append(_sauce & Constants.vbLf)
			For Each topping As String In toppings_Renamed
				display.Append(topping & Constants.vbLf)
			Next topping

			Return display.ToString()
		End Function
	End Class

	Public Class CheesePizza
		Inherits Pizza
		Public Sub New()
			MyBase.New("Cheese Pizza", "Regular Crust", "Marinara Pizza Sauce")
			Toppings.Add("Fresh Mozzarella")
			Toppings.Add("Parmesan")
		End Sub
	End Class

	Public Class VeggiePizza
		Inherits Pizza
		Public Sub New()
			MyBase.New("Veggie Pizza", "Crust", "Marinara sauce")
			Toppings.Add("Shredded mozzarella")
			Toppings.Add("Grated parmesan")
			Toppings.Add("Diced onion")
			Toppings.Add("Sliced mushrooms")
			Toppings.Add("Sliced red pepper")
			Toppings.Add("Sliced black olives")
		End Sub
	End Class

	Public Class PepperoniPizza
		Inherits Pizza
		Public Sub New()
			MyBase.New("Pepperoni Pizza", "Crust", "Marinara sauce")
			Toppings.Add("Sliced Pepperoni")
			Toppings.Add("Sliced Onion")
			Toppings.Add("Grated parmesan cheese")
		End Sub
	End Class

	Public Class ClamPizza
		Inherits Pizza
		Public Sub New()
			MyBase.New("Clam Pizza", "Thin crust", "White garlic sauce")
			Toppings.Add("Clams")
			Toppings.Add("Grated parmesan cheese")
		End Sub
	End Class
	#End Region
End Namespace
