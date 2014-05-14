Imports Microsoft.VisualBasic
Imports System
Imports System.Collections

Namespace DoFactory.HeadFirst.Iterator.DinerMergerI
	Friend Class MenuTestDrive
        Shared Sub Main(ByVal args() As String)

            Dim pancakeHouseMenu As New PancakeHouseMenu()
            Dim dinerMenu As New DinerMenu()

            Dim waitress As New Waitress(pancakeHouseMenu, dinerMenu)
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
		Private _pancakeHouseMenu As Menu
		Private _dinerMenu As Menu

		Public Sub New(ByVal pancakeHouseMenu As Menu, ByVal dinerMenu As Menu)
			Me._pancakeHouseMenu = pancakeHouseMenu
			Me._dinerMenu = dinerMenu
		End Sub

		Public Sub PrintMenu()
			Dim pancakeIterator As IEnumerator = _pancakeHouseMenu.CreateIterator()
			Dim dinerIterator As IEnumerator = _dinerMenu.CreateIterator()

			Console.WriteLine("MENU" & Constants.vbLf & "----" & Constants.vbLf & "BREAKFAST")
			PrintMenu(pancakeIterator)

			Console.WriteLine(Constants.vbLf & "LUNCH")
			PrintMenu(dinerIterator)
		End Sub

		Private Sub PrintMenu(ByVal iterator As IEnumerator)
			Do While iterator.MoveNext()
				Dim menuItem As MenuItem = CType(iterator.Current, MenuItem)
				Console.Write(menuItem.Name & ", ")
				Console.Write(menuItem.Price & " -- ")
				Console.WriteLine(menuItem.Description)
			Loop
		End Sub

		Public Sub PrintVegetarianMenu()
			Console.WriteLine(Constants.vbLf & "VEGETARIAN MENU" & Constants.vbLf & "----" & Constants.vbLf & "BREAKFAST")
			PrintVegetarianMenu(_pancakeHouseMenu.CreateIterator())
			Console.WriteLine(Constants.vbLf & "LUNCH")
			PrintVegetarianMenu(_dinerMenu.CreateIterator())
		End Sub

		Public Function IsItemVegetarian(ByVal name As String) As Boolean
			Dim pancakeIterator As IEnumerator = _pancakeHouseMenu.CreateIterator()
			If IsVegetarian(name, pancakeIterator) Then
				Return True
			End If

			Dim dinerIterator As IEnumerator = _dinerMenu.CreateIterator()
			If IsVegetarian(name, dinerIterator) Then
				Return True
			End If
			Return False
		End Function

		Private Sub PrintVegetarianMenu(ByVal iterator As IEnumerator)
			Do While iterator.MoveNext()
				Dim menuItem As MenuItem = CType(iterator.Current, MenuItem)
				If menuItem.Vegetarian Then
					Console.Write(menuItem.Name & ", ")
					Console.Write(menuItem.Price & " -- ")
					Console.WriteLine(menuItem.Description)
				End If
			Loop
		End Sub

		Private Function IsVegetarian(ByVal name As String, ByVal iterator As IEnumerator) As Boolean
			Do While iterator.MoveNext()
				Dim menuItem As MenuItem = CType(iterator.Current, MenuItem)
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

	#Region "Iterators (Enumerators)"

	Public Class AlternatingDinerMenuIterator
		Implements IEnumerator
		Private _items() As MenuItem
		Private _position As Integer

		Public Sub New(ByVal items() As MenuItem)
			Me._items = items
			_position = Integer.Parse(DateTime.Now.DayOfWeek.ToString()) Mod 2
		End Sub

		Public ReadOnly Property Current() As Object Implements IEnumerator.Current
			Get
				Dim menuItem As MenuItem = _items(_position)
				_position = _position + 2
				Return menuItem
			End Get
		End Property

		Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
			If _position >= _items.Length OrElse _items(_position) Is Nothing Then
				Return False
			Else
				Return True
			End If
		End Function

		Public Sub Reset() Implements IEnumerator.Reset
			Console.WriteLine("Alternating Diner Menu Iterator does not support reset()")
		End Sub
	End Class

	Public Class DinerMenuIterator
		Implements IEnumerator
		Private _list() As MenuItem
		Private _position As Integer = 0

		Public Sub New(ByVal list() As MenuItem)
			Me._list = list
		End Sub

		Public ReadOnly Property Current() As Object Implements IEnumerator.Current
			Get

				Dim menuItem As MenuItem = _list(_position)
				_position = _position + 1
				Return menuItem
			End Get
		End Property

		Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
			If _position >= _list.Length OrElse _list(_position) Is Nothing Then
				Return False
			Else
				Return True
			End If
		End Function

		Public Sub Reset() Implements IEnumerator.Reset
			If _position <= 0 Then
				Throw New ApplicationException ("You can't remove an item until you've done at least one Next()")
			End If
			If _list(_position - 1) IsNot Nothing Then
				For i As Integer = _position - 1 To (_list.Length - 1) - 1
					_list(i) = _list(i + 1)
				Next i
				_list(_list.Length - 1) = Nothing
			End If
		End Sub
	End Class

	#End Region

	#Region "Menu and MenuItems"

	Public Interface Menu
		Function CreateIterator() As IEnumerator
	End Interface

	Public Class MenuItem
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
		Private privateVegetarian As Boolean
		Public Property Vegetarian() As Boolean
			Get
				Return privateVegetarian
			End Get
			Set(ByVal value As Boolean)
				privateVegetarian = value
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

		' Constructor
		Public Sub New(ByVal name As String, ByVal description As String, ByVal vegetarian As Boolean, ByVal price As Double)
			Me.Name = name
			Me.Description = description
			Me.Vegetarian = vegetarian
			Me.Price = price
		End Sub
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

		Public Function getMenuItems() As MenuItem()
			Return _menuItems
		End Function

		Public Function CreateIterator() As IEnumerator Implements Menu.CreateIterator
			Return New DinerMenuIterator(_menuItems)
		End Function

		' other menu methods here
	End Class


	Public Class PancakeHouseMenu
		Implements Menu
		Private _menuItems As ArrayList

		Public Sub New()
			_menuItems = New ArrayList()

			AddItem("K&B's Pancake Breakfast", "Pancakes with scrambled eggs, and toast", True, 2.99)

			AddItem("Regular Pancake Breakfast", "Pancakes with fried eggs, sausage", False, 2.99)

			AddItem("Blueberry Pancakes", "Pancakes made with fresh blueberries, and blueberry syrup", True, 3.49)

			AddItem("Waffles", "Waffles, with your choice of blueberries or strawberries", True, 3.59)
		End Sub

		Public Sub AddItem(ByVal name As String, ByVal description As String, ByVal vegetarian As Boolean, ByVal price As Double)
			Dim menuItem As New MenuItem(name, description, vegetarian, price)
			_menuItems.Add(menuItem)
		End Sub

		Public Function getMenuItems() As ArrayList
			Return _menuItems
		End Function

		Public Function CreateIterator() As IEnumerator Implements Menu.CreateIterator
			Return _menuItems.GetEnumerator()
		End Function

		' other menu methods here
	End Class

	#End Region
End Namespace
