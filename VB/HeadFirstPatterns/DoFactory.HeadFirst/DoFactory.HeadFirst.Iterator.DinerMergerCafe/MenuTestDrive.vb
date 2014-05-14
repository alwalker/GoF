Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.HeadFirst.Iterator.DinerMergerCafe
	Friend Class MenuTestDrive
        Shared Sub Main(ByVal args() As String)

            Dim pancakeHouseMenu As New PancakeHouseMenu()
            Dim dinerMenu As New DinerMenu()
            Dim cafeMenu As New CafeMenu()

            Dim waitress As New Waitress(pancakeHouseMenu, dinerMenu, cafeMenu)

            waitress.PrintMenu()
            waitress.PrintVegetarianMenu()

            Console.WriteLine(Constants.vbLf & "Customer asks, is the Hotdog vegetarian?")
            Console.Write("Waitress says: ")
            If waitress.IsItemVegetarian("Hotdog") Then
                Console.WriteLine("Yes")
            Else
                Console.WriteLine("No")
            End If

            Console.WriteLine(Constants.vbLf & "Customer asks, are the Waffles vegetarian?")
            Console.Write("Waitress says: ")
            If waitress.IsItemVegetarian("Waffles") Then
                Console.WriteLine("Yes")
            Else
                Console.WriteLine("No")
            End If

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	#Region "Waitress"

	Public Class Waitress
		Private _pancakeHouseMenu As IMenu
		Private _dinerMenu As IMenu
		Private _cafeMenu As IMenu

		Public Sub New(ByVal pancakeHouseMenu As IMenu, ByVal dinerMenu As IMenu, ByVal cafeMenu As IMenu)
			Me._pancakeHouseMenu = pancakeHouseMenu
			Me._dinerMenu = dinerMenu
			Me._cafeMenu = cafeMenu
		End Sub

		Public Sub PrintMenu()
			Dim pancakeIterator As IEnumerator(Of MenuItem) = _pancakeHouseMenu.CreateIterator()
			Dim dinerIterator As IEnumerator(Of MenuItem) = _dinerMenu.CreateIterator()
			Dim cafeIterator As IEnumerator(Of MenuItem) = _cafeMenu.CreateIterator()

			Console.WriteLine("MENU" & Constants.vbLf & "----" & Constants.vbLf & "BREAKFAST")
			PrintMenu(pancakeIterator)

			Console.WriteLine(Constants.vbLf & "LUNCH")
			PrintMenu(dinerIterator)

			Console.WriteLine(Constants.vbLf & "DINNER")
			PrintMenu(cafeIterator)
		End Sub

		Private Sub PrintMenu(ByVal iterator As IEnumerator(Of MenuItem))
			Do While iterator.MoveNext()
				Dim menuItem As MenuItem = CType(iterator.Current, MenuItem)

				Console.Write(menuItem.Name & ", ")
				Console.Write(menuItem.Price & " -- ")
				Console.WriteLine(menuItem.Description)
			Loop
		End Sub

		Public Sub PrintVegetarianMenu()
			Console.WriteLine(Constants.vbLf & "VEGETARIAN MENU" & Constants.vbLf & "---------------")
			PrintVegetarianMenu(_pancakeHouseMenu.CreateIterator())
			PrintVegetarianMenu(_dinerMenu.CreateIterator())
			PrintVegetarianMenu(_cafeMenu.CreateIterator())
		End Sub

		Public Function IsItemVegetarian(ByVal name As String) As Boolean
			Dim pancakeIterator As IEnumerator(Of MenuItem) = _pancakeHouseMenu.CreateIterator()
			If IsVegetarian(name, pancakeIterator) Then
				Return True
			End If
			Dim dinerIterator As IEnumerator(Of MenuItem) = _dinerMenu.CreateIterator()
			If IsVegetarian(name, dinerIterator) Then
				Return True
			End If
			Dim cafeIterator As IEnumerator(Of MenuItem) = _cafeMenu.CreateIterator()
			If IsVegetarian(name, cafeIterator) Then
				Return True
			End If
			Return False
		End Function

		Private Sub PrintVegetarianMenu(ByVal iterator As IEnumerator(Of MenuItem))
			Do While iterator.MoveNext()
				Dim menuItem As MenuItem = iterator.Current
				If menuItem.Vegetarian Then
					Console.Write(menuItem.Name & ", ")
					Console.Write(menuItem.Price & " -- ")
					Console.WriteLine(menuItem.Description)
				End If
			Loop
		End Sub

		Private Function IsVegetarian(ByVal name As String, ByVal iterator As IEnumerator(Of MenuItem)) As Boolean
			Do While iterator.MoveNext()
				Dim menuItem As MenuItem = iterator.Current
				If menuItem.Name = name Then
					If menuItem.Vegetarian Then
						Return True
					End If
				End If
			Loop
			Return False
		End Function
	End Class

	#End Region

	#Region "Menu and MenuItems"

	Public Interface IMenu
		Function CreateIterator() As IEnumerator(Of MenuItem)
	End Interface

	Public Class MenuItem
		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Private Set(ByVal value As String)
				privateName = value
			End Set
		End Property
		Private privateDescription As String
		Public Property Description() As String
			Get
				Return privateDescription
			End Get
			Private Set(ByVal value As String)
				privateDescription = value
			End Set
		End Property
		Private privateVegetarian As Boolean
		Public Property Vegetarian() As Boolean
			Get
				Return privateVegetarian
			End Get
			Private Set(ByVal value As Boolean)
				privateVegetarian = value
			End Set
		End Property
		Private privatePrice As Double
		Public Property Price() As Double
			Get
				Return privatePrice
			End Get
			Private Set(ByVal value As Double)
				privatePrice = value
			End Set
		End Property

		Public Sub New(ByVal name As String, ByVal description As String, ByVal vegetarian As Boolean, ByVal price As Double)
			Me.Name = name
			Me.Description = description
			Me.Vegetarian = vegetarian
			Me.Price = price
		End Sub
	End Class

	Public Class PancakeHouseMenu
		Implements IMenu
		Private menuItems As List(Of MenuItem) = New List(Of MenuItem)()

		Public Sub New()
			'menuItems = new ArrayList();

			AddItem("K&B's Pancake Breakfast", "Pancakes with scrambled eggs, and toast", True, 2.99)

			AddItem("Regular Pancake Breakfast", "Pancakes with fried eggs, sausage", False, 2.99)

			AddItem("Blueberry Pancakes", "Pancakes made with fresh blueberries, and blueberry syrup", True, 3.49)

			AddItem("Waffles", "Waffles, with your choice of blueberries or strawberries", True, 3.59)
		End Sub

		Public Sub AddItem(ByVal name As String, ByVal description As String, ByVal vegetarian As Boolean, ByVal price As Double)
			Dim menuItem As New MenuItem(name, description, vegetarian, price)
			menuItems.Add(menuItem)
		End Sub

		Public Function CreateIterator() As IEnumerator(Of MenuItem) Implements IMenu.CreateIterator
			Return menuItems.GetEnumerator()
		End Function

		' other menu methods here
	End Class

	Public Class CafeMenu
		Implements IMenu
		Private _menuItems As Dictionary(Of String, MenuItem) = New Dictionary(Of String, MenuItem)()

		Public Sub New()
			AddItem("Veggie Burger and Air Fries", "Veggie burger on a whole wheat bun, lettuce, tomato, and fries", True, 3.99)
			AddItem("Soup of the day", "A cup of the soup of the day, with a side salad", False, 3.69)
			AddItem("Burrito", "A large burrito, with whole pinto beans, salsa, guacamole", True, 4.29)
		End Sub

		Public Sub AddItem(ByVal name As String, ByVal description As String, ByVal vegetarian As Boolean, ByVal price As Double)
			Dim menuItem As New MenuItem(name, description, vegetarian, price)
			_menuItems.Add(menuItem.Name, menuItem)
		End Sub

		Public Function CreateIterator() As IEnumerator(Of MenuItem) Implements IMenu.CreateIterator
			Return _menuItems.Values.GetEnumerator()
		End Function
	End Class

	Public Class DinerMenu
		Implements IMenu
		Private _menuItems As List(Of MenuItem) = New List(Of MenuItem)()

		Public Sub New()
			AddItem("Vegetarian BLT", "(Fakin') Bacon with lettuce & tomato on whole wheat", True, 2.99)
			AddItem("BLT", "Bacon with lettuce & tomato on whole wheat", False, 2.99)
			AddItem("Soup of the day", "Soup of the day, with a side of potato salad", False, 3.29)
			AddItem("Hotdog", "A hot dog, with saurkraut, relish, onions, topped with cheese", False, 3.05)
			AddItem("Steamed Veggies and Brown Rice", "A medly of steamed vegetables over brown rice", True, 3.99)
			AddItem("Pasta", "Spaghetti with Marinara Sauce, and a slice of sourdough bread", True, 3.89)
		End Sub

		Public Sub AddItem(ByVal name As String, ByVal description As String, ByVal vegetarian As Boolean, ByVal price As Double)
			Dim menuItem As New MenuItem(name, description, vegetarian, price)
			_menuItems.Add(menuItem)
		End Sub

		Public Function CreateIterator() As IEnumerator(Of MenuItem) Implements IMenu.CreateIterator
			Return _menuItems.GetEnumerator()
		End Function

		' other menu methods here
	End Class

	#End Region
End Namespace
