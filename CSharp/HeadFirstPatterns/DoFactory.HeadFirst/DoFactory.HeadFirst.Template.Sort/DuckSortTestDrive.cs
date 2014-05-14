using System;

namespace DoFactory.HeadFirst.Template.Sort
{
    class DuckSortTestDrive
    {
        static void Main(string[] args)
        {
            Duck[] ducks = { 
                    new Duck("Daffy", 8), 
                    new Duck("Dewey", 2),
                    new Duck("Howard", 7),
                    new Duck("Louie", 2),
                    new Duck("Donald", 10), 
                    new Duck("Huey", 2)};

            Console.WriteLine("Before sorting:");
            Display(ducks);

            Array.Sort(ducks);

            Console.WriteLine("\nAfter sorting:");
            Display(ducks);

            // Wait for user
            Console.ReadKey();
        }

        public static void Display(Duck[] ducks)
        {
            foreach (Duck duck in ducks)
            {
                Console.WriteLine(duck);
            }
        }
    }

    #region Duck

    public class Duck : IComparable
    {
        private string _name;
        private int _weight;

        public Duck(string name, int weight)
        {
            this._name = name;
            this._weight = weight;
        }

        public override string ToString()
        {
            return _name + " weighs " + _weight;
        }

        public int CompareTo(Object o)
        {
            Duck otherDuck = (Duck)o;

            if (this._weight < otherDuck._weight)
            {
                return -1;
            }
            else if (this._weight == otherDuck._weight)
            {
                return 0;
            }
            else // this.weight > otherDuck.weight
            {
                return 1;
            }
        }
    }
    #endregion
}
