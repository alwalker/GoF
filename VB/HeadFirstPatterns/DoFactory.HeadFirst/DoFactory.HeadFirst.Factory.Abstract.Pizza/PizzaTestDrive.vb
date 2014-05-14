Imports Microsoft.VisualBasic
Imports System
Imports System.Text

Namespace DoFactory.HeadFirst.Factory.Abstract.Pizza
	' PizzaTestDrive test application
	Friend Class PizzaTestDrive
        Shared Sub Main(ByVal args() As String)

            Dim nyStore As PizzaStore = New NewYorkPizzaStore()
            Dim chStore As PizzaStore = New ChicagoPizzaStore()

            Dim pizza As Pizza = nyStore.OrderPizza("cheese")
            Console.WriteLine("Ethan ordered a " & pizza.Name + Constants.vbLf)

            pizza = chStore.OrderPizza("cheese")
            Console.WriteLine("Joel ordered a " & pizza.Name + Constants.vbLf)

            pizza = nyStore.OrderPizza("clam")
            Console.WriteLine("Ethan ordered a " & pizza.Name + Constants.vbLf)

            pizza = chStore.OrderPizza("clam")
            Console.WriteLine("Joel ordered a " & pizza.Name + Constants.vbLf)

            pizza = nyStore.OrderPizza("pepperoni")
            Console.WriteLine("Ethan ordered a " & pizza.Name + Constants.vbLf)

            pizza = chStore.OrderPizza("pepperoni")
            Console.WriteLine("Joel ordered a " & pizza.Name + Constants.vbLf)

            pizza = nyStore.OrderPizza("veggie")
            Console.WriteLine("Ethan ordered a " & pizza.Name + Constants.vbLf)

            pizza = chStore.OrderPizza("veggie")
            Console.WriteLine("Joel ordered a " & pizza.Name + Constants.vbLf)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	#Region "Ingredient Abstract Factory"

	Public Interface IPizzaIngredientFactory
		Function CreateDough() As IDough
		Function CreateSauce() As ISauce
		Function CreateCheese() As ICheese
		Function CreateVeggies() As IVeggies()
		Function CreatePepperoni() As IPepperoni
		Function CreateClam() As IClams
	End Interface

	Public Class NewYorkPizzaIngredientFactory
		Implements IPizzaIngredientFactory
		Public Function CreateDough() As IDough Implements IPizzaIngredientFactory.CreateDough
			Return New ThinCrustDough()
		End Function

		Public Function CreateSauce() As ISauce Implements IPizzaIngredientFactory.CreateSauce
			Return New MarinaraSauce()
		End Function

		Public Function CreateCheese() As ICheese Implements IPizzaIngredientFactory.CreateCheese
			Return New ReggianoCheese()
		End Function

		Public Function CreateVeggies() As IVeggies() Implements IPizzaIngredientFactory.CreateVeggies
			Dim veggies() As IVeggies = { New Garlic(), New Onion(), New Mushroom(), New RedPepper() }
			Return veggies
		End Function

		Public Function CreatePepperoni() As IPepperoni Implements IPizzaIngredientFactory.CreatePepperoni
			Return New SlicedPepperoni()
		End Function

		Public Function CreateClam() As IClams Implements IPizzaIngredientFactory.CreateClam
			Return New FreshClams()
		End Function
	End Class

	Public Class ChicagoPizzaIngredientFactory
		Implements IPizzaIngredientFactory

		Public Function CreateDough() As IDough Implements IPizzaIngredientFactory.CreateDough
			Return New ThickCrustDough()
		End Function

		Public Function CreateSauce() As ISauce Implements IPizzaIngredientFactory.CreateSauce
			Return New PlumTomatoSauce()
		End Function

		Public Function CreateCheese() As ICheese Implements IPizzaIngredientFactory.CreateCheese
			Return New MozzarellaCheese()
		End Function

		Public Function CreateVeggies() As IVeggies() Implements IPizzaIngredientFactory.CreateVeggies
			Dim veggies() As IVeggies = { New BlackOlives(), New Spinach(), New Eggplant() }
			Return veggies
		End Function

		Public Function CreatePepperoni() As IPepperoni Implements IPizzaIngredientFactory.CreatePepperoni
			Return New SlicedPepperoni()
		End Function

		Public Function CreateClam() As IClams Implements IPizzaIngredientFactory.CreateClam
			Return New FrozenClams()
		End Function
	End Class

	#End Region

	#Region "Pizza Stores"

	Public MustInherit Class PizzaStore
		Public Function OrderPizza(ByVal type As String) As Pizza
			Dim pizza As Pizza = CreatePizza(type)
			Console.WriteLine("--- Making a " & pizza.Name & " ---")

			pizza.Prepare()
			pizza.Bake()
			pizza.Cut()
			pizza.Box()

			Return pizza
		End Function

		Public MustOverride Function CreatePizza(ByVal item As String) As Pizza
	End Class

	Public Class NewYorkPizzaStore
		Inherits PizzaStore
		Public Overrides Function CreatePizza(ByVal item As String) As Pizza
			Dim pizza As Pizza = Nothing
			Dim ingredientFactory As IPizzaIngredientFactory = New NewYorkPizzaIngredientFactory()

			Select Case item
				Case "cheese"
					pizza = New CheesePizza(ingredientFactory)
					pizza.Name = "New York Style Cheese Pizza"
				Case "veggie"
					pizza = New VeggiePizza(ingredientFactory)
					pizza.Name = "New York Style Veggie Pizza"
				Case "clam"
					pizza = New ClamPizza(ingredientFactory)
					pizza.Name = "New York Style Clam Pizza"
				Case "pepperoni"
					pizza = New PepperoniPizza(ingredientFactory)
					pizza.Name = "New York Style Pepperoni Pizza"
			End Select
			Return pizza
		End Function
	End Class

	Public Class ChicagoPizzaStore
		Inherits PizzaStore
		' Factory method implementation
		Public Overrides Function CreatePizza(ByVal item As String) As Pizza
			Dim pizza As Pizza = Nothing
			Dim ingredientFactory As IPizzaIngredientFactory = New ChicagoPizzaIngredientFactory()

			Select Case item
				Case "cheese"
					pizza = New CheesePizza(ingredientFactory)
					pizza.Name = "Chicago Style Cheese Pizza"
				Case "veggie"
					pizza = New VeggiePizza(ingredientFactory)
					pizza.Name = "Chicago Style Veggie Pizza"
				Case "clam"
					pizza = New ClamPizza(ingredientFactory)
					pizza.Name = "Chicago Style Clam Pizza"
				Case "pepperoni"
					pizza = New PepperoniPizza(ingredientFactory)
					pizza.Name = "Chicago Style Pepperoni Pizza"
			End Select
			Return pizza
		End Function
	End Class

	#End Region

	#Region "Pizzas"

	Public MustInherit Class Pizza
		Protected dough As IDough
		Protected sauce As ISauce
		Protected veggies() As IVeggies
		Protected cheese As ICheese
		Protected pepperoni As IPepperoni
		Protected clam As IClams

		Private name_Renamed As String

		Public MustOverride Sub Prepare()

		Public Sub Bake()
			Console.WriteLine("Bake for 25 minutes at 350")
		End Sub

		Public Overridable Sub Cut()
			Console.WriteLine("Cutting the pizza into diagonal slices")
		End Sub

		Public Sub Box()
			Console.WriteLine("Place pizza in official Pizzastore box")
		End Sub

		Public Property Name() As String
			Get
				Return name_Renamed
			End Get
			Set(ByVal value As String)
				name_Renamed = value
			End Set
		End Property

		Public Overrides Function ToString() As String
			Dim result As New StringBuilder()
			result.Append("---- " & Me.Name & " ----" & Constants.vbLf)
			If dough IsNot Nothing Then
				result.Append(dough)
				result.Append(Constants.vbLf)
			End If
			If sauce IsNot Nothing Then
				result.Append(sauce)
				result.Append(Constants.vbLf)
			End If
			If cheese IsNot Nothing Then
				result.Append(cheese)
				result.Append(Constants.vbLf)
			End If
			If veggies IsNot Nothing Then
				For i As Integer = 0 To veggies.Length - 1
					result.Append(veggies(i))
					If i < veggies.Length - 1 Then
						result.Append(", ")
					End If
				Next i
				result.Append(Constants.vbLf)
			End If
			If clam IsNot Nothing Then
				result.Append(clam)
				result.Append(Constants.vbLf)
			End If
			If pepperoni IsNot Nothing Then
				result.Append(pepperoni)
				result.Append(Constants.vbLf)
			End If
			Return result.ToString()
		End Function
	End Class

	Public Class ClamPizza
		Inherits Pizza
		Private _ingredientFactory As IPizzaIngredientFactory

		Public Sub New(ByVal ingredientFactory As IPizzaIngredientFactory)
			Me._ingredientFactory = ingredientFactory
		End Sub

		Public Overrides Sub Prepare()
			Console.WriteLine("Preparing " & Me.Name)
			dough = _ingredientFactory.CreateDough()
			sauce = _ingredientFactory.CreateSauce()
			cheese = _ingredientFactory.CreateCheese()
			clam = _ingredientFactory.CreateClam()
		End Sub
	End Class

	Public Class CheesePizza
		Inherits Pizza
		Private _ingredientFactory As IPizzaIngredientFactory

		Public Sub New(ByVal ingredientFactory As IPizzaIngredientFactory)
			Me._ingredientFactory = ingredientFactory
		End Sub

		Public Overrides Sub Prepare()
			Console.WriteLine("Preparing " & Me.Name)
			dough = _ingredientFactory.CreateDough()
			sauce = _ingredientFactory.CreateSauce()
			cheese = _ingredientFactory.CreateCheese()
		End Sub
	End Class

	Public Class PepperoniPizza
		Inherits Pizza
		Private _ingredientFactory As IPizzaIngredientFactory

		Public Sub New(ByVal ingredientFactory As IPizzaIngredientFactory)
			Me._ingredientFactory = ingredientFactory
		End Sub

		Public Overrides Sub Prepare()
			Console.WriteLine("Preparing " & Me.Name)
			dough = _ingredientFactory.CreateDough()
			sauce = _ingredientFactory.CreateSauce()
			cheese = _ingredientFactory.CreateCheese()
			veggies = _ingredientFactory.CreateVeggies()
			pepperoni = _ingredientFactory.CreatePepperoni()
		End Sub
	End Class

	Public Class VeggiePizza
		Inherits Pizza
		Private _ingredientFactory As IPizzaIngredientFactory

		Public Sub New(ByVal ingredientFactory As IPizzaIngredientFactory)
			Me._ingredientFactory = ingredientFactory
		End Sub

		Public Overrides Sub Prepare()
			Console.WriteLine("Preparing " & Me.Name)
			dough = _ingredientFactory.CreateDough()
			sauce = _ingredientFactory.CreateSauce()
			cheese = _ingredientFactory.CreateCheese()
			veggies = _ingredientFactory.CreateVeggies()
		End Sub
	End Class

	#End Region

	#Region "Ingredients"

	Public Class ThinCrustDough
		Implements IDough
		Public Overrides Function ToString() As String Implements IDough.ToString
			Return "Thin Crust Dough"
		End Function
	End Class

	Public Class ThickCrustDough
		Implements IDough
		Public Overrides Function ToString() As String Implements IDough.ToString
			Return "ThickCrust style extra thick crust dough"
		End Function
	End Class

	Public Class Spinach
		Implements IVeggies
		Public Overrides Function ToString() As String Implements IVeggies.ToString
			Return "Spinach"
		End Function
	End Class

	Public Class SlicedPepperoni
		Implements IPepperoni
		Public Overrides Function ToString() As String Implements IPepperoni.ToString
			Return "Sliced Pepperoni"
		End Function
	End Class

	Public Interface ISauce
		Overloads Function ToString() As String
	End Interface
	Public Interface IDough
		Overloads Function ToString() As String
	End Interface
	Public Interface IClams
		Overloads Function ToString() As String
	End Interface
	Public Interface IVeggies
		Overloads Function ToString() As String
	End Interface
	Public Interface ICheese
		Overloads Function ToString() As String
	End Interface

	Public Interface IPepperoni
		Overloads Function ToString() As String
	End Interface

	Public Class Garlic
		Implements IVeggies
		Public Overrides Function ToString() As String Implements IVeggies.ToString
			Return "Garlic"
		End Function
	End Class

	Public Class Onion
		Implements IVeggies
		Public Overrides Function ToString() As String Implements IVeggies.ToString
			Return "Onion"
		End Function
	End Class

	Public Class Mushroom
		Implements IVeggies

		Public Overrides Function ToString() As String Implements IVeggies.ToString
			Return "Mushrooms"
		End Function
	End Class

	Public Class Eggplant
		Implements IVeggies
		Public Overrides Function ToString() As String Implements IVeggies.ToString
			Return "Eggplant"
		End Function
	End Class

	Public Class BlackOlives
		Implements IVeggies
		Public Overrides Function ToString() As String Implements IVeggies.ToString
			Return "Black Olives"
		End Function
	End Class

	Public Class RedPepper
		Implements IVeggies
		Public Overrides Function ToString() As String Implements IVeggies.ToString
			Return "Red Pepper"
		End Function
	End Class

	Public Class PlumTomatoSauce
		Implements ISauce
		Public Overrides Function ToString() As String Implements ISauce.ToString
			Return "Tomato sauce with plum tomatoes"
		End Function
	End Class
	Public Class MarinaraSauce
		Implements ISauce
		Public Overrides Function ToString() As String Implements ISauce.ToString
			Return "Marinara Sauce"
		End Function
	End Class

	Public Class FreshClams
		Implements IClams

		Public Overrides Function ToString() As String Implements IClams.ToString
			Return "Fresh Clams from Long Island Sound"
		End Function
	End Class
	Public Class FrozenClams
		Implements IClams
		Public Overrides Function ToString() As String Implements IClams.ToString
			Return "Frozen Clams from Chesapeake Bay"
		End Function
	End Class

	Public Class ParmesanCheese
		Implements ICheese
		Public Overrides Function ToString() As String Implements ICheese.ToString
			Return "Shredded Parmesan"
		End Function
	End Class

	Public Class MozzarellaCheese
		Implements ICheese
		Public Overrides Function ToString() As String Implements ICheese.ToString
			Return "Shredded Mozzarella"
		End Function
	End Class

	Public Class ReggianoCheese
		Implements ICheese
		Public Overrides Function ToString() As String Implements ICheese.ToString
			Return "Reggiano Cheese"
		End Function
	End Class
	#End Region
End Namespace
