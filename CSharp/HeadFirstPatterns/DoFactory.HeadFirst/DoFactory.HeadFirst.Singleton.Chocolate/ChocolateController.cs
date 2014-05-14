using System;

namespace DoFactory.HeadFirst.Singleton.Chocolate
{
    class ChocolateController
    {
        static void Main(string[] args)
        {
            ChocolateBoiler boiler = ChocolateBoiler.GetInstance();
            boiler.Fill();
            boiler.Boil();
            boiler.Drain();

            // will return the existing instance
            ChocolateBoiler boiler2 = ChocolateBoiler.GetInstance();

            // Are they the same?
            if (boiler == boiler2)
            {
                Console.WriteLine("Same instances");
            }

            Singleton s1 = Singleton.GetInstance();
            Singleton s2 = Singleton.GetInstance();
            Singleton s3 = Singleton.GetInstance();

            if (s1 == s2 && s2 == s3)
            {
                Console.WriteLine("Same instances");
            }

            // Wait for user
            Console.ReadKey();
        }
    }

    #region ChocolateBoiler

    public class ChocolateBoiler
    {
        private static ChocolateBoiler _uniqueInstance;

        private bool _empty;
        private bool _boiled;

        // *Private* constructor
        private ChocolateBoiler()
        {
            _empty = true;
            _boiled = false;
        }

        public static ChocolateBoiler GetInstance()
        {
            if (_uniqueInstance == null)
            {
                Console.WriteLine("Creating unique instance of Chocolate Boiler");
                _uniqueInstance = new ChocolateBoiler();
            }

            Console.WriteLine("Returning instance of Chocolate Boiler");
            return _uniqueInstance;
        }

        public void Fill()
        {
            if (Empty)
            {
                _empty = false;
                _boiled = false;
                // fill the boiler with a milk/chocolate mixture
            }
        }

        public void Drain()
        {
            if (Empty && Boiled)
            {
                // drain the boiled milk and chocolate
                _empty = true;
            }
        }

        public void Boil()
        {
            if (!Empty && !Boiled)
            {
                // bring the contents to a boil
                _boiled = true;
            }
        }

        // Properties 
        public bool Empty
        {
            get { return _empty; }
        }

        public bool Boiled
        {
            get { return _boiled; }
        }
    }
    #endregion

    #region Singleton

    public class Singleton
    {
        private static Singleton _uniqueInstance;

        // other useful instance variables here

        // Constructor
        private Singleton() { }

        private static readonly object _syncLock = new object();

        public static Singleton GetInstance()
        {
            // Double checked locking
            if (_uniqueInstance == null)
            {
                lock (_syncLock)
                {
                    if (_uniqueInstance == null)
                    {
                        _uniqueInstance = new Singleton();
                    }
                }
            }
            return _uniqueInstance;
        }

        // other useful methods here
    }
    #endregion
}
