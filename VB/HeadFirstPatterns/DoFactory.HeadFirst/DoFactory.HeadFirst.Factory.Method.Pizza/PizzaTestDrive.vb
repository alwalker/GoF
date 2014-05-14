Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace DoFactory.HeadFirst.Factory.Method.Pizza
	Friend Class PizzaTestDrive
        Shared Sub Main(ByVal args() As String)

            Dim nyStore As PizzaStore = New NYPizzaStore()
            Dim chicagoStore As PizzaStore = New ChicagoPizzaStore()

            Dim pizza As Pizza = nyStore.OrderPizza("cheese")
            Console.WriteLine("Ethan ordered a " & pizza.Name + Constants.vbLf)

            pizza = chicagoStore.OrderPizza("cheese")
            Console.WriteLine("Joel ordered a " & pizza.Name + Constants.vbLf)

            pizza = nyStore.OrderPizza("clam")
            Console.WriteLine("Ethan ordered a " & pizza.Name + Constants.vbLf)

            pizza = chicagoStore.OrderPizza("clam")
            Console.WriteLine("Joel ordered a " & pizza.Name + Constants.vbLf)

            pizza = nyStore.OrderPizza("pepperoni")
            Console.WriteLine("Ethan ordered a " & pizza.Name + Constants.vbLf)

            pizza = chicagoStore.OrderPizza("pepperoni")
            Console.WriteLine("Joel ordered a " & pizza.Name + Constants.vbLf)

            pizza = nyStore.OrderPizza("veggie")
            Console.WriteLine("Ethan ordered a " & pizza.Name + Constants.vbLf)

            pizza = chicagoStore.OrderPizza("veggie")
            Console.WriteLine("Joel ordered a " & pizza.Name + Constants.vbLf)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	#Region "Pizza Stores"

	Public MustInherit Class PizzaStore
		Public MustOverride Function CreatePizza(ByVal item As String) As Pizza

		Public Function OrderPizza(ByVal type As String) As Pizza
			Dim pizza As Pizza = CreatePizza(type)
			Console.WriteLine("--- Making a " & pizza.Name & " ---")
			pizza.Prepare()
			pizza.Bake()
			pizza.Cut()
			pizza.Box()
			Return pizza
		End Function
	End Class

	Public Class NYPizzaStore
		Inherits PizzaStore
		Public Overrides Function CreatePizza(ByVal item As String) As Pizza
			Dim pizza As Pizza = Nothing

			Select Case item
				Case "cheese"
					pizza = New NYStyleCheesePizza()
				Case "veggie"
					pizza = New NYStyleVeggiePizza()
				Case "clam"
					pizza = New NYStyleClamPizza()
				Case "pepperoni"
					pizza = New NYStylePepperoniPizza()
			End Select

			Return pizza
		End Function
	End Class

	Public Class ChicagoPizzaStore
		Inherits PizzaStore
		Public Overrides Function CreatePizza(ByVal item As String) As Pizza
			Dim pizza As Pizza = Nothing

			Select Case item
				Case "cheese"
					pizza = New ChicagoStyleCheesePizza()
				Case "veggie"
					pizza = New ChicagoStyleVeggiePizza()
				Case "clam"
					pizza = New ChicagoStyleClamPizza()
				Case "pepperoni"
					pizza = New ChicagoStylePepperoniPizza()
			End Select

			Return pizza
		End Function
	End Class

	' Alternatively use this store

	Public Class DependentPizzaStore
		Public Function CreatePizza(ByVal style As String, ByVal type As String) As Pizza
			Dim pizza As Pizza = Nothing

			If style = "NY" Then
				Select Case type
					Case "cheese"
						pizza = New NYStyleCheesePizza()
					Case "veggie"
						pizza = New NYStyleVeggiePizza()
					Case "clam"
						pizza = New NYStyleClamPizza()
					Case "pepperoni"
						pizza = New NYStylePepperoniPizza()
				End Select
			ElseIf style = "Chicago" Then
				Select Case type
					Case "cheese"
						pizza = New ChicagoStyleCheesePizza()
					Case "veggie"
						pizza = New ChicagoStyleVeggiePizza()
					Case "clam"
						pizza = New ChicagoStyleClamPizza()
					Case "pepperoni"
						pizza = New ChicagoStylePepperoniPizza()
				End Select
			Else
				Console.WriteLine("Error: invalid store")
				Return Nothing
			End If
			pizza.Prepare()
			pizza.Bake()
			pizza.Cut()
			pizza.Box()

			Return pizza
		End Function
	End Class

	#End Region

	#Region "Pizzas"

	Public MustInherit Class Pizza
		Public Sub New()
			Toppings = New List(Of String)()
		End Sub

		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property
		Private privateDough As String
		Public Property Dough() As String
			Get
				Return privateDough
			End Get
			Set(ByVal value As String)
				privateDough = value
			End Set
		End Property
		Private privateSauce As String
		Public Property Sauce() As String
			Get
				Return privateSauce
			End Get
			Set(ByVal value As String)
				privateSauce = value
			End Set
		End Property
		Private privateToppings As List(Of String)
		Public Property Toppings() As List(Of String)
			Get
				Return privateToppings
			End Get
			Set(ByVal value As List(Of String))
				privateToppings = value
			End Set
		End Property

		Public Sub Prepare()
			Console.WriteLine("Preparing " & Name)
			Console.WriteLine("Tossing dough...")
			Console.WriteLine("Adding sauce...")
			Console.WriteLine("Adding toppings: ")
			For Each topping As String In Toppings
				Console.WriteLine("   " & topping)
			Next topping
		End Sub

		Public Sub Bake()
			Console.WriteLine("Bake for 25 minutes at 350")
		End Sub

		Public Overridable Sub Cut()
			Console.WriteLine("Cutting the pizza into diagonal slices")
		End Sub

		Public Sub Box()
			Console.WriteLine("Place pizza in official PizzaStore box")
		End Sub


		Public Overrides Function ToString() As String
			Dim display As New StringBuilder()
			display.Append("---- " & Name & " ----" & Constants.vbLf)
			display.Append(Dough + Constants.vbLf)
			display.Append(Sauce + Constants.vbLf)
			For Each topping As String In Toppings
				display.Append(topping.ToString() & Constants.vbLf)
			Next topping

			Return display.ToString()
		End Function
	End Class

	Public Class ChicagoStyleVeggiePizza
		Inherits Pizza
		Public Sub New()
			Name = "Chicago Deep Dish Veggie Pizza"
			Dough = "Extra Thick Crust Dough"
			Sauce = "Plum Tomato Sauce"

			Toppings.Add("Shredded Mozzarella Cheese")
			Toppings.Add("Black Olives")
			Toppings.Add("Spinach")
			Toppings.Add("Eggplant")
		End Sub

		Public Overrides Sub Cut()
			Console.WriteLine("Cutting the pizza into square slices")
		End Sub
	End Class

	Public Class ChicagoStylePepperoniPizza
		Inherits Pizza
		Public Sub New()
			Name = "Chicago Style Pepperoni Pizza"
			Dough = "Extra Thick Crust Dough"
			Sauce = "Plum Tomato Sauce"

			Toppings.Add("Shredded Mozzarella Cheese")
			Toppings.Add("Black Olives")
			Toppings.Add("Spinach")
			Toppings.Add("Eggplant")
			Toppings.Add("Sliced Pepperoni")
		End Sub

		Public Overrides Sub Cut()
			Console.WriteLine("Cutting the pizza into square slices")
		End Sub
	End Class

	Public Class ChicagoStyleClamPizza
		Inherits Pizza
		Public Sub New()
			Name = "Chicago Style Clam Pizza"
			Dough = "Extra Thick Crust Dough"
			Sauce = "Plum Tomato Sauce"

			Toppings.Add("Shredded Mozzarella Cheese")
			Toppings.Add("Frozen Clams from Chesapeake Bay")
		End Sub

		Public Overrides Sub Cut()
			Console.WriteLine("Cutting the pizza into square slices")
		End Sub
	End Class

	Public Class ChicagoStyleCheesePizza
		Inherits Pizza
		Public Sub New()
			Name = "Chicago Style Deep Dish Cheese Pizza"
			Dough = "Extra Thick Crust Dough"
			Sauce = "Plum Tomato Sauce"

			Toppings.Add("Shredded Mozzarella Cheese")
		End Sub

		Public Overrides Sub Cut()
			Console.WriteLine("Cutting the pizza into square slices")
		End Sub
	End Class

	Public Class NYStyleVeggiePizza
		Inherits Pizza

		Public Sub New()
			Name = "NY Style Veggie Pizza"
			Dough = "Thin Crust Dough"
			Sauce = "Marinara Sauce"

			Toppings.Add("Grated Reggiano Cheese")
			Toppings.Add("Garlic")
			Toppings.Add("Onion")
			Toppings.Add("Mushrooms")
			Toppings.Add("Red Pepper")
		End Sub
	End Class

	Public Class NYStylePepperoniPizza
		Inherits Pizza

		Public Sub New()
			Name = "NY Style Pepperoni Pizza"
			Dough = "Thin Crust Dough"
			Sauce = "Marinara Sauce"

			Toppings.Add("Grated Reggiano Cheese")
			Toppings.Add("Sliced Pepperoni")
			Toppings.Add("Garlic")
			Toppings.Add("Onion")
			Toppings.Add("Mushrooms")
			Toppings.Add("Red Pepper")
		End Sub
	End Class

	Public Class NYStyleClamPizza
		Inherits Pizza

		Public Sub New()
			Name = "NY Style Clam Pizza"
			Dough = "Thin Crust Dough"
			Sauce = "Marinara Sauce"

			Toppings.Add("Grated Reggiano Cheese")
			Toppings.Add("Fresh Clams from Long Island Sound")
		End Sub
	End Class

	Public Class NYStyleCheesePizza
		Inherits Pizza
		Public Sub New()
			Name = "NY Style Sauce and Cheese Pizza"
			Dough = "Thin Crust Dough"
			Sauce = "Marinara Sauce"

			Toppings.Add("Grated Reggiano Cheese")
		End Sub
	End Class
	#End Region
End Namespace
