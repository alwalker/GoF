using System;
using System.Collections;

namespace DoFactory.HeadFirst.Iterator.DinerMergerI
{
    class MenuTestDrive
    {
        static void Main(string[] args)
        {
            PancakeHouseMenu pancakeHouseMenu = new PancakeHouseMenu();
            DinerMenu dinerMenu = new DinerMenu();

            Waitress waitress = new Waitress(pancakeHouseMenu, dinerMenu);
            waitress.PrintMenu();
            waitress.PrintVegetarianMenu();

            Console.WriteLine("\nCustomer asks, is the Hotdog vegetarian?");
            Console.Write("Waitress says: ");
            if (waitress.IsItemVegetarian("Hotdog"))
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }

            Console.WriteLine("\nCustomer asks, are the Waffles vegetarian?");
            Console.Write("Waitress says: ");
            if (waitress.IsItemVegetarian("Waffles"))
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }

            // Wait for user
            Console.ReadKey();
        }
    }

    #region Waitress

    public class Waitress
    {
        private Menu _pancakeHouseMenu;
        private Menu _dinerMenu;

        public Waitress(Menu pancakeHouseMenu, Menu dinerMenu)
        {
            this._pancakeHouseMenu = pancakeHouseMenu;
            this._dinerMenu = dinerMenu;
        }

        public void PrintMenu()
        {
            IEnumerator pancakeIterator = _pancakeHouseMenu.CreateIterator();
            IEnumerator dinerIterator = _dinerMenu.CreateIterator();

            Console.WriteLine("MENU\n----\nBREAKFAST");
            PrintMenu(pancakeIterator);

            Console.WriteLine("\nLUNCH");
            PrintMenu(dinerIterator);
        }

        private void PrintMenu(IEnumerator iterator)
        {
            while (iterator.MoveNext())
            {
                MenuItem menuItem = (MenuItem)iterator.Current;
                Console.Write(menuItem.Name + ", ");
                Console.Write(menuItem.Price + " -- ");
                Console.WriteLine(menuItem.Description);
            }
        }

        public void PrintVegetarianMenu()
        {
            Console.WriteLine("\nVEGETARIAN MENU\n----\nBREAKFAST");
            PrintVegetarianMenu(_pancakeHouseMenu.CreateIterator());
            Console.WriteLine("\nLUNCH");
            PrintVegetarianMenu(_dinerMenu.CreateIterator());
        }

        public bool IsItemVegetarian(String name)
        {
            IEnumerator pancakeIterator = _pancakeHouseMenu.CreateIterator();
            if (IsVegetarian(name, pancakeIterator))
            {
                return true;
            }

            IEnumerator dinerIterator = _dinerMenu.CreateIterator();
            if (IsVegetarian(name, dinerIterator))
            {
                return true;
            }
            return false;
        }

        private void PrintVegetarianMenu(IEnumerator iterator)
        {
            while (iterator.MoveNext())
            {
                MenuItem menuItem = (MenuItem)iterator.Current;
                if (menuItem.Vegetarian)
                {
                    Console.Write(menuItem.Name + ", ");
                    Console.Write(menuItem.Price + " -- ");
                    Console.WriteLine(menuItem.Description);
                }
            }
        }

        private bool IsVegetarian(string name, IEnumerator iterator)
        {
            while (iterator.MoveNext())
            {
                MenuItem menuItem = (MenuItem)iterator.Current;
                if (menuItem.Name.Equals(name))
                {
                    if (menuItem.Vegetarian)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    #endregion

    #region Iterators (Enumerators)

    public class AlternatingDinerMenuIterator : IEnumerator
    {
        private MenuItem[] _items;
        private int _position;

        public AlternatingDinerMenuIterator(MenuItem[] items)
        {
            this._items = items;
            _position = int.Parse(DateTime.Now.DayOfWeek.ToString()) % 2;
        }

        public object Current
        {
            get
            {
                MenuItem menuItem = _items[_position];
                _position = _position + 2;
                return menuItem;
            }
        }

        public bool MoveNext()
        {
            if (_position >= _items.Length || _items[_position] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Reset()
        {
            Console.WriteLine("Alternating Diner Menu Iterator does not support reset()");
        }
    }

    public class DinerMenuIterator : IEnumerator
    {
        private MenuItem[] _list;
        private int _position = 0;

        public DinerMenuIterator(MenuItem[] list)
        {
            this._list = list;
        }

        public object Current
        {
            get
            {

                MenuItem menuItem = _list[_position];
                _position = _position + 1;
                return menuItem;
            }
        }

        public bool MoveNext()
        {
            if (_position >= _list.Length || _list[_position] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Reset()
        {
            if (_position <= 0)
            {
                throw new ApplicationException
                    ("You can't remove an item until you've done at least one Next()");
            }
            if (_list[_position - 1] != null)
            {
                for (int i = _position - 1; i < (_list.Length - 1); i++)
                {
                    _list[i] = _list[i + 1];
                }
                _list[_list.Length - 1] = null;
            }
        }
    }

    #endregion

    #region Menu and MenuItems

    public interface Menu
    {
        IEnumerator CreateIterator();
    }

    public class MenuItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Vegetarian { get; set; }
        public double Price { get; set; }

        // Constructor
        public MenuItem(string name, string description, bool vegetarian, double price)
        {
            this.Name = name;
            this.Description = description;
            this.Vegetarian = vegetarian;
            this.Price = price;
        }
    }

    public class DinerMenu : Menu
    {
        private static readonly int MAX_ITEMS = 6;
        private int _numberOfItems = 0;
        private MenuItem[] _menuItems = new MenuItem[MAX_ITEMS];

        public DinerMenu()
        {
            AddItem("Vegetarian BLT",
                "(Fakin') Bacon with lettuce & tomato on whole wheat", true, 2.99);
            AddItem("BLT",
                "Bacon with lettuce & tomato on whole wheat", false, 2.99);
            AddItem("Soup of the day",
                "Soup of the day, with a side of potato salad", false, 3.29);
            AddItem("Hotdog",
                "A hot dog, with saurkraut, relish, onions, topped with cheese",
                false, 3.05);
            AddItem("Steamed Veggies and Brown Rice",
                "Steamed vegetables over brown rice", true, 3.99);
            AddItem("Pasta",
                "Spaghetti with Marinara Sauce, and a slice of sourdough bread",
                true, 3.89);
        }

        public void AddItem(string name, string description, bool vegetarian, double price)
        {
            MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
            if (_numberOfItems >= MAX_ITEMS)
            {
                Console.WriteLine("Sorry, menu is full!  Can't add item to menu");
            }
            else
            {
                _menuItems[_numberOfItems] = menuItem;
                _numberOfItems = _numberOfItems + 1;
            }
        }

        public MenuItem[] getMenuItems()
        {
            return _menuItems;
        }

        public IEnumerator CreateIterator()
        {
            return new DinerMenuIterator(_menuItems);
        }

        // other menu methods here
    }


    public class PancakeHouseMenu : Menu
    {
        private ArrayList _menuItems;

        public PancakeHouseMenu()
        {
            _menuItems = new ArrayList();

            AddItem("K&B's Pancake Breakfast",
                "Pancakes with scrambled eggs, and toast",
                true,
                2.99);

            AddItem("Regular Pancake Breakfast",
                "Pancakes with fried eggs, sausage",
                false,
                2.99);

            AddItem("Blueberry Pancakes",
                "Pancakes made with fresh blueberries, and blueberry syrup",
                true,
                3.49);

            AddItem("Waffles",
                "Waffles, with your choice of blueberries or strawberries",
                true,
                3.59);
        }

        public void AddItem(String name, string description, bool vegetarian, double price)
        {
            MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
            _menuItems.Add(menuItem);
        }

        public ArrayList getMenuItems()
        {
            return _menuItems;
        }

        public IEnumerator CreateIterator()
        {
            return _menuItems.GetEnumerator();
        }

        // other menu methods here
    }

    #endregion
}
