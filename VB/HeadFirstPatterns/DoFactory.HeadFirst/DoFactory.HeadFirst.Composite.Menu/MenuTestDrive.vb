Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.HeadFirst.Composite.Menu
	Friend Class MenuTestDrive
        Shared Sub Main(ByVal args() As String)

            Dim pancakeHouseMenu As MenuComponent = New Menu With {.Name = "PANCAKE HOUSE MENU", .Description = "Breakfast"}
            Dim dinerMenu As MenuComponent = New Menu With {.Name = "DINER MENU", .Description = "Lunch"}
            Dim cafeMenu As MenuComponent = New Menu With {.Name = "CAFE MENU", .Description = "Dinner"}
            Dim dessertMenu As MenuComponent = New Menu With {.Name = "DESSERT MENU", .Description = "Dessert of course!"}
            Dim coffeeMenu As MenuComponent = New Menu With {.Name = "COFFEE MENU", .Description = "Stuff to go with your afternoon coffee"}

            Dim allMenus As MenuComponent = New Menu With {.Name = "ALL MENUS", .Description = "All menus combined"}

            allMenus.Add(pancakeHouseMenu)
            allMenus.Add(dinerMenu)
            allMenus.Add(cafeMenu)

            pancakeHouseMenu.Add(New MenuItem With {.Name = "K&B's Pancake Breakfast", .Description = "Pancakes with scrambled eggs, and toast", .Vegetarian = True, .Price = 2.99})
            pancakeHouseMenu.Add(New MenuItem With {.Name = "Regular Pancake Breakfast", .Description = "Pancakes with fried eggs, sausage", .Vegetarian = False, .Price = 2.99})
            pancakeHouseMenu.Add(New MenuItem With {.Name = "Blueberry Pancakes", .Description = "Pancakes made with fresh blueberries, and blueberry syrup", .Vegetarian = True, .Price = 3.49})
            pancakeHouseMenu.Add(New MenuItem With {.Name = "Waffles", .Description = "Waffles, with your choice of blueberries or strawberries", .Vegetarian = True, .Price = 3.59})

            dinerMenu.Add(New MenuItem With {.Name = "Vegetarian BLT", .Description = "(Fakin') Bacon with lettuce & tomato on whole wheat", .Vegetarian = True, .Price = 2.99})

            dinerMenu.Add(New MenuItem With {.Name = "BLT", .Description = "Bacon with lettuce & tomato on whole wheat", .Vegetarian = False, .Price = 2.99})
            dinerMenu.Add(New MenuItem With {.Name = "Soup of the day", .Description = "A bowl of the soup of the day, with a side of potato salad", .Vegetarian = False, .Price = 3.29})
            dinerMenu.Add(New MenuItem With {.Name = "Hotdog", .Description = "A hot dog, with saurkraut, relish, onions, topped with cheese", .Vegetarian = False, .Price = 3.05})
            dinerMenu.Add(New MenuItem With {.Name = "Steamed Veggies and Brown Rice", .Description = "Steamed vegetables over brown rice", .Vegetarian = True, .Price = 3.99})
            dinerMenu.Add(New MenuItem With {.Name = "Pasta", .Description = "Spaghetti with Marinara Sauce, and a slice of sourdough bread", .Vegetarian = True, .Price = 3.89})

            dinerMenu.Add(dessertMenu)

            dessertMenu.Add(New MenuItem With {.Name = "Apple Pie", .Description = "Apple pie with a flakey crust, topped with vanilla icecream", .Vegetarian = True, .Price = 1.59})
            dessertMenu.Add(New MenuItem With {.Name = "Cheesecake", .Description = "Creamy New York cheesecake, with a chocolate graham crust", .Vegetarian = True, .Price = 1.99})
            dessertMenu.Add(New MenuItem With {.Name = "Sorbet", .Description = "A scoop of raspberry and a scoop of lime", .Vegetarian = True, .Price = 1.89})
            cafeMenu.Add(New MenuItem With {.Name = "Veggie Burger and Air Fries", .Description = "Veggie burger on a whole wheat bun, lettuce, tomato, and fries", .Vegetarian = True, .Price = 3.99})
            cafeMenu.Add(New MenuItem With {.Name = "Soup of the day", .Description = "A cup of the soup of the day, with a side salad", .Vegetarian = False, .Price = 3.69})
            cafeMenu.Add(New MenuItem With {.Name = "Burrito", .Description = "A large burrito, with whole pinto beans, salsa, guacamole", .Vegetarian = True, .Price = 4.29})

            cafeMenu.Add(coffeeMenu)

            coffeeMenu.Add(New MenuItem With {.Name = "Coffee Cake", .Description = "Crumbly cake topped with cinnamon and walnuts", .Vegetarian = True, .Price = 1.59})
            coffeeMenu.Add(New MenuItem With {.Name = "Bagel", .Description = "Flavors include sesame, poppyseed, cinnamon raisin, pumpkin", .Vegetarian = False, .Price = 0.69})
            coffeeMenu.Add(New MenuItem With {.Name = "Biscotti", .Description = "Three almond or hazelnut biscotti cookies", .Vegetarian = True, .Price = 0.89})

            Dim waitress As New Waitress(allMenus)
            waitress.PrintMenu()

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	#Region "Waitress"

	Public Class Waitress
		Private _allMenus As MenuComponent

		Public Sub New(ByVal allMenus As MenuComponent)
			Me._allMenus = allMenus
		End Sub

		Public Sub PrintMenu()
			_allMenus.Print()
		End Sub
	End Class

	#End Region

	#Region "Menu"

	Public MustInherit Class MenuComponent
		Public Overridable Sub Add(ByVal menuComponent As MenuComponent)
			Throw New NotSupportedException()
		End Sub

		Public Overridable Sub Remove(ByVal menuComponent As MenuComponent)
			Throw New NotSupportedException()
		End Sub

		Public Overridable Function GetChild(ByVal i As Integer) As MenuComponent
			Throw New NotSupportedException()
		End Function

		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property

		Private privateDescription As String
		Public Property Description() As String
			Get
				Return privateDescription
			End Get
			Set(ByVal value As String)
				privateDescription = value
			End Set
		End Property

		Private privatePrice As Double
		Public Property Price() As Double
			Get
				Return privatePrice
			End Get
			Set(ByVal value As Double)
				privatePrice = value
			End Set
		End Property

		Private privateVegetarian As Boolean
		Public Property Vegetarian() As Boolean
			Get
				Return privateVegetarian
			End Get
			Set(ByVal value As Boolean)
				privateVegetarian = value
			End Set
		End Property

		Public Overridable Sub Print()
			Throw New NotSupportedException()
		End Sub
	End Class

	Public Class MenuItem
		Inherits MenuComponent
		Public Overrides Sub Print()
			Console.Write(Name)
			If Vegetarian Then
				Console.Write("(v)")
			End If
			Console.Write(", " & Price)
			Console.WriteLine(Constants.vbLf & " -- " & Description)
		End Sub
	End Class

	Public Class Menu
		Inherits MenuComponent
		Private _menuComponents As List(Of MenuComponent) = New List(Of MenuComponent)()

		Public Overrides Sub Add(ByVal menuComponent As MenuComponent)
			_menuComponents.Add(menuComponent)
		End Sub

		Public Overrides Sub Remove(ByVal menuComponent As MenuComponent)
			_menuComponents.Remove(menuComponent)
		End Sub

		Public Overrides Function GetChild(ByVal i As Integer) As MenuComponent
			Return _menuComponents(i)
		End Function

		Public Overrides Sub Print()
			Console.Write(Constants.vbLf + Name)
			Console.WriteLine(", " & Description)
			Console.WriteLine("---------------------")

			For Each menu As MenuComponent In _menuComponents
				menu.Print()
			Next menu
		End Sub
	End Class
	#End Region
End Namespace
