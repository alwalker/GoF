using System;

namespace DoFactory.HeadFirst.Singleton.Classic
{
    class SingletonClient
    {
        static void Main(string[] args)
        {
            Singleton singleton = Singleton.getInstance();
            singleton.SaySomething();

            // Wait for user
            Console.ReadKey();
        }
    }

    #region Singleton

    // NOTE: This is not thread safe

    public class Singleton
    {
        private static Singleton _uniqueInstance;

        // other useful instance variables here

        // * Private* Constructor
        private Singleton() { }

        public static Singleton getInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new Singleton();
            }
            return _uniqueInstance;
        }

        public void SaySomething()
        {
            Console.WriteLine("Hi, here I am");
        }

        // other useful methods here
    }
    #endregion
}
