Imports Microsoft.VisualBasic
Imports System
Imports System.Collections

Namespace DoFactory.HeadFirst.Iterator.DinerMerger
	Friend Class MenuTestDrive
        Shared Sub Main(ByVal args() As String)

            Dim pancakeHouseMenu As New PancakeHouseMenu()
            Dim dinerMenu As New DinerMenu()

            Dim waitress As New Waitress(pancakeHouseMenu, dinerMenu)

            waitress.PrintMenu()

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	#Region "Waitress"

	Public Class Waitress
		Private _pancakeHouseMenu As PancakeHouseMenu
		Private _dinerMenu As DinerMenu

		Public Sub New(ByVal pancakeHouseMenu As PancakeHouseMenu, ByVal dinerMenu As DinerMenu)
			Me._pancakeHouseMenu = pancakeHouseMenu
			Me._dinerMenu = dinerMenu
		End Sub

		Public Sub PrintMenu()
			Dim pancakeIterator As Iterator = _pancakeHouseMenu.CreateIterator()
			Dim dinerIterator As Iterator = _dinerMenu.CreateIterator()

			Console.WriteLine("MENU" & Constants.vbLf & "----" & Constants.vbLf & "BREAKFAST")
			PrintMenu(pancakeIterator)

			Console.WriteLine(Constants.vbLf & "LUNCH")
			PrintMenu(dinerIterator)
		End Sub

		Private Sub PrintMenu(ByVal iterator As Iterator)
			Do While iterator.HasNext()
				Dim menuItem As MenuItem = CType(iterator.Next(), MenuItem)
				Console.Write(menuItem.Name & ", ")
				Console.Write(menuItem.Price & " -- ")
				Console.WriteLine(menuItem.Description)
			Loop
		End Sub

		Public Sub PrintVegetarianMenu()
			PrintVegetarianMenu(_pancakeHouseMenu.CreateIterator())
			PrintVegetarianMenu(_dinerMenu.CreateIterator())
		End Sub

		Public Function IsItemVegetarian(ByVal name As String) As Boolean
			Dim breakfastIterator As Iterator = _pancakeHouseMenu.CreateIterator()
			If IsVegetarian(name, breakfastIterator) Then
				Return True
			End If

			Dim dinnerIterator As Iterator = _dinerMenu.CreateIterator()
			If IsVegetarian(name, dinnerIterator) Then
				Return True
			End If
			Return False
		End Function

		Private Sub PrintVegetarianMenu(ByVal iterator As Iterator)
			Do While iterator.HasNext()
				Dim menuItem As MenuItem = CType(iterator.Next(), MenuItem)
				If menuItem.Vegetarian Then
					Console.WriteLine(menuItem.Name)
                    Console.WriteLine(Constants.vbTab & Constants.vbTab & menuItem.Price.ToString())
                    Console.WriteLine(Constants.vbTab & menuItem.Description)
				End If
			Loop
		End Sub

		Private Function IsVegetarian(ByVal name As String, ByVal iterator As Iterator) As Boolean
			Do While iterator.HasNext()
				Dim menuItem As MenuItem = CType(iterator.Next(), MenuItem)
				If menuItem.Name.Equals(name) Then
					If menuItem.Vegetarian Then
						Return True
					End If
				End If
			Loop
			Return False
		End Function
	End Class

	#End Region

	#Region "Iterators"

	Public Interface Iterator
		Function HasNext() As Boolean
		Function [Next]() As Object
	End Interface

	Public Class AlternatingDinerMenuIterator
		Implements Iterator
		Private _list() As MenuItem
		Private _position As Integer

		Public Sub New(ByVal list() As MenuItem)
			Me._list = list
			_position = Integer.Parse(DateTime.Now.DayOfWeek.ToString()) Mod 2
		End Sub

		Public Function [Next]() As Object Implements Iterator.Next
			Dim menuItem As MenuItem = _list(_position)
			_position = _position + 2
			Return menuItem
		End Function

		Public Function HasNext() As Boolean Implements Iterator.HasNext
			If _position >= _list.Length OrElse _list(_position) Is Nothing Then
				Return False
			Else
				Return True
			End If
		End Function

		Public Overrides Function ToString() As String
			Return "Alternating Diner Menu Iterator"
		End Function
	End Class

	Public Class ArrayIterator
		Implements Iterator
		Private _items() As MenuItem
		Private _position As Integer = 0

		' Constructore
		Public Sub New(ByVal items() As MenuItem)
			Me._items = items
		End Sub

		Public Function [Next]() As Object Implements Iterator.Next
			Dim menuItem As MenuItem = _items(_position)
			_position = _position + 1
			Return menuItem
		End Function

		Public Function HasNext() As Boolean Implements Iterator.HasNext
			If _position >= _items.Length OrElse _items(_position) Is Nothing Then
				Return False
			Else
				Return True
			End If
		End Function
	End Class

	Public Class ArrayListIterator
		Implements Iterator
		Private _items As ArrayList
		Private _position As Integer = 0

		' Constructor
		Public Sub New(ByVal items As ArrayList)
			Me._items = items
		End Sub

		Public Function [Next]() As Object Implements Iterator.Next
			Dim o As Object = _items(_position)
			_position = _position + 1
			Return o
		End Function

		Public Function HasNext() As Boolean Implements Iterator.HasNext
			If _position >= _items.Count Then
				Return False
			Else
				Return True
			End If
		End Function
	End Class

	Public Class DinerMenuIterator
		Implements Iterator
		Private _items() As MenuItem
		Private _position As Integer = 0

		Public Sub New(ByVal items() As MenuItem)
			Me._items = items
		End Sub

		Public Function [Next]() As Object Implements Iterator.Next
			Dim menuItem As MenuItem = _items(_position)
			_position = _position + 1
			Return menuItem
		End Function

		Public Function HasNext() As Boolean Implements Iterator.HasNext
			If _position >= _items.Length OrElse _items(_position) Is Nothing Then
				Return False
			Else
				Return True
			End If
		End Function
	End Class
	#End Region

	#Region "Menu and MenuItems"

	Public Interface Menu
		Function CreateIterator() As Iterator
	End Interface

	Public Class MenuItem
		' Constructor
		Public Sub New(ByVal name As String, ByVal description As String, ByVal vegetarian As Boolean, ByVal price As Double)
			Me.Name = name
			Me.Description = description
			Me.Vegetarian = vegetarian
			Me.Price = price
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

		Public Overrides Function ToString() As String
            Return (Name & ", $" & Price.ToString() + Constants.vbLf & "   " & Description)
		End Function
	End Class

	Public Class DinerMenu
		Implements Menu
		Private Shared ReadOnly MAX_ITEMS As Integer = 6
		Private _numberOfItems As Integer = 0
		Private _menuItems(MAX_ITEMS - 1) As MenuItem

		Public Sub New()
			AddItem("Vegetarian BLT", "(Fakin') Bacon with lettuce & tomato on whole wheat", True, 2.99)
			AddItem("BLT", "Bacon with lettuce & tomato on whole wheat", False, 2.99)
			AddItem("Soup of the day", "Soup of the day, with a side of potato salad", False, 3.29)
			AddItem("Hotdog", "A hot dog, with saurkraut, relish, onions, topped with cheese", False, 3.05)
			AddItem("Steamed Veggies and Brown Rice", "Steamed vegetables over brown rice", True, 3.99)
			AddItem("Pasta", "Spaghetti with Marinara Sauce, and a slice of sourdough bread", True, 3.89)
		End Sub

		Public Sub AddItem(ByVal name As String, ByVal description As String, ByVal vegetarian As Boolean, ByVal price As Double)
			Dim menuItem As New MenuItem(name, description, vegetarian, price)
			If _numberOfItems >= MAX_ITEMS Then
				Console.WriteLine("Sorry, menu is full!  Can't add item to menu")
			Else
				_menuItems(_numberOfItems) = menuItem
				_numberOfItems = _numberOfItems + 1
			End If
		End Sub

		Public Function GetMenuItems() As MenuItem()
			Return _menuItems
		End Function

		Public Function CreateIterator() As Iterator Implements Menu.CreateIterator
			Return New DinerMenuIterator(_menuItems)
		End Function

		' other menu methods here
	End Class

	Public Class PancakeHouseMenu
		Implements Menu
		Private _menuItems As New ArrayList()

		Public Sub New()
			AddItem("K&B's Pancake Breakfast", "Pancakes with scrambled eggs, and toast", True, 2.99)

			AddItem("Regular Pancake Breakfast", "Pancakes with fried eggs, sausage", False, 2.99)

			AddItem("Blueberry Pancakes", "Pancakes made with fresh blueberries", True, 3.49)

			AddItem("Waffles", "Waffles, with your choice of blueberries or strawberries", True, 3.59)
		End Sub

		Public Sub AddItem(ByVal name As String, ByVal description As String, ByVal vegetarian As Boolean, ByVal price As Double)
			_menuItems.Add(New MenuItem(name, description, vegetarian, price))
		End Sub

		Public Function GetMenuItems() As ArrayList
			Return _menuItems
		End Function

		Public Function CreateIterator() As Iterator Implements Menu.CreateIterator
			Return New PancakeHouseMenuIterator(_menuItems)
		End Function

		Public Overrides Function ToString() As String
			Return "Objectville Pancake House Menu"
		End Function

		' other menu methods here
	End Class

	Public Class PancakeHouseMenuIterator
		Implements Iterator
		Private _items As ArrayList
		Private _position As Integer = 0

		Public Sub New(ByVal items As ArrayList)
			Me._items = items
		End Sub

		Public Function [Next]() As Object Implements Iterator.Next
			Dim o As Object = _items(_position)
			_position = _position + 1
			Return o
		End Function

		Public Function HasNext() As Boolean Implements Iterator.HasNext
			If _position >= _items.Count Then
				Return False
			Else
				Return True
			End If
		End Function
	End Class
	#End Region
End Namespace
