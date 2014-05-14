using System;
using System.Collections;

namespace DoFactory.HeadFirst.Iterator.DinerMerger
{
    class MenuTestDrive
    {
        static void Main(string[] args)
        {
            PancakeHouseMenu pancakeHouseMenu = new PancakeHouseMenu();
            DinerMenu dinerMenu = new DinerMenu();

            Waitress waitress = new Waitress(pancakeHouseMenu, dinerMenu);

            waitress.PrintMenu();

            // Wait for user
            Console.ReadKey();
        }
    }

    #region Waitress

    public class Waitress
    {
        private PancakeHouseMenu _pancakeHouseMenu;
        private DinerMenu _dinerMenu;

        public Waitress(PancakeHouseMenu pancakeHouseMenu, DinerMenu dinerMenu)
        {
            this._pancakeHouseMenu = pancakeHouseMenu;
            this._dinerMenu = dinerMenu;
        }

        public void PrintMenu()
        {
            Iterator pancakeIterator = _pancakeHouseMenu.CreateIterator();
            Iterator dinerIterator = _dinerMenu.CreateIterator();

            Console.WriteLine("MENU\n----\nBREAKFAST");
            PrintMenu(pancakeIterator);

            Console.WriteLine("\nLUNCH");
            PrintMenu(dinerIterator);
        }

        private void PrintMenu(Iterator iterator)
        {
            while (iterator.HasNext())
            {
                MenuItem menuItem = (MenuItem)iterator.Next();
                Console.Write(menuItem.Name + ", ");
                Console.Write(menuItem.Price + " -- ");
                Console.WriteLine(menuItem.Description);
            }
        }

        public void PrintVegetarianMenu()
        {
            PrintVegetarianMenu(_pancakeHouseMenu.CreateIterator());
            PrintVegetarianMenu(_dinerMenu.CreateIterator());
        }

        public bool IsItemVegetarian(String name)
        {
            Iterator breakfastIterator = _pancakeHouseMenu.CreateIterator();
            if (IsVegetarian(name, breakfastIterator))
            {
                return true;
            }

            Iterator dinnerIterator = _dinerMenu.CreateIterator();
            if (IsVegetarian(name, dinnerIterator))
            {
                return true;
            }
            return false;
        }

        private void PrintVegetarianMenu(Iterator iterator)
        {
            while (iterator.HasNext())
            {
                MenuItem menuItem = (MenuItem)iterator.Next();
                if (menuItem.Vegetarian)
                {
                    Console.WriteLine(menuItem.Name);
                    Console.WriteLine("\t\t" + menuItem.Price);
                    Console.WriteLine("\t" + menuItem.Description);
                }
            }
        }

        private bool IsVegetarian(string name, Iterator iterator)
        {
            while (iterator.HasNext())
            {
                MenuItem menuItem = (MenuItem)iterator.Next();
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

    #region Iterators

    public interface Iterator
    {
        bool HasNext();
        object Next();
    }

    public class AlternatingDinerMenuIterator : Iterator
    {
        private MenuItem[] _list;
        private int _position;

        public AlternatingDinerMenuIterator(MenuItem[] list)
        {
            this._list = list;
            _position = int.Parse(DateTime.Now.DayOfWeek.ToString()) % 2;
        }

        public object Next()
        {
            MenuItem menuItem = _list[_position];
            _position = _position + 2;
            return menuItem;
        }

        public bool HasNext()
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

        public override string ToString()
        {
            return "Alternating Diner Menu Iterator";
        }
    }

    public class ArrayIterator : Iterator
    {
        private MenuItem[] _items;
        private int _position = 0;

        // Constructore
        public ArrayIterator(MenuItem[] items)
        {
            this._items = items;
        }

        public object Next()
        {
            MenuItem menuItem = _items[_position];
            _position = _position + 1;
            return menuItem;
        }

        public bool HasNext()
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
    }

    public class ArrayListIterator : Iterator
    {
        private ArrayList _items;
        private int _position = 0;

        // Constructor
        public ArrayListIterator(ArrayList items)
        {
            this._items = items;
        }

        public object Next()
        {
            object o = _items[_position];
            _position = _position + 1;
            return o;
        }

        public bool HasNext()
        {
            if (_position >= _items.Count)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class DinerMenuIterator : Iterator
    {
        private MenuItem[] _items;
        private int _position = 0;

        public DinerMenuIterator(MenuItem[] items)
        {
            this._items = items;
        }

        public Object Next()
        {
            MenuItem menuItem = _items[_position];
            _position = _position + 1;
            return menuItem;
        }

        public bool HasNext()
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
    }
    #endregion

    #region Menu and MenuItems

    public interface Menu
    {
        Iterator CreateIterator();
    }

    public class MenuItem
    {
        // Constructor
        public MenuItem(string name, string description, bool vegetarian, double price)
        {
            this.Name = name;
            this.Description = description;
            this.Vegetarian = vegetarian;
            this.Price = price;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Vegetarian { get; set; }

        public override string ToString()
        {
            return (Name + ", $" + Price + "\n   " + Description);
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

        public void AddItem(String name, String description,
            bool vegetarian, double price)
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

        public MenuItem[] GetMenuItems()
        {
            return _menuItems;
        }

        public Iterator CreateIterator()
        {
            return new DinerMenuIterator(_menuItems);
        }

        // other menu methods here
    }

    public class PancakeHouseMenu : Menu
    {
        private ArrayList _menuItems = new ArrayList();

        public PancakeHouseMenu()
        {
            AddItem("K&B's Pancake Breakfast",
                "Pancakes with scrambled eggs, and toast",
                true,
                2.99);

            AddItem("Regular Pancake Breakfast",
                "Pancakes with fried eggs, sausage",
                false,
                2.99);

            AddItem("Blueberry Pancakes",
                "Pancakes made with fresh blueberries",
                true,
                3.49);

            AddItem("Waffles",
                "Waffles, with your choice of blueberries or strawberries",
                true,
                3.59);
        }

        public void AddItem(String name, String description,
            bool vegetarian, double price)
        {
            _menuItems.Add(new MenuItem(name, description, vegetarian, price));
        }

        public ArrayList GetMenuItems()
        {
            return _menuItems;
        }

        public Iterator CreateIterator()
        {
            return new PancakeHouseMenuIterator(_menuItems);
        }

        public override string ToString()
        {
            return "Objectville Pancake House Menu";
        }

        // other menu methods here
    }

    public class PancakeHouseMenuIterator : Iterator
    {
        private ArrayList _items;
        private int _position = 0;

        public PancakeHouseMenuIterator(ArrayList items)
        {
            this._items = items;
        }

        public object Next()
        {
            object o = _items[_position];
            _position = _position + 1;
            return o;
        }

        public bool HasNext()
        {
            if (_position >= _items.Count)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    #endregion
}
