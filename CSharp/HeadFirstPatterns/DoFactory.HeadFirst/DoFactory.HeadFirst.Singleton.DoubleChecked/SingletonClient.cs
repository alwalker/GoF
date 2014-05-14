using System;

namespace DoFactory.HeadFirst.Singleton.DoubleChecked
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

    public class Singleton 
    {
        private volatile static Singleton _uniqueInstance;
        private static readonly object _syncLock = new object();
 
        private Singleton() {}
 
        public static Singleton getInstance() 
        {
            if (_uniqueInstance == null) 
            {
                // Lock area where instance is created
                lock(_syncLock)
                {
                    if (_uniqueInstance == null) 
                    {
                        _uniqueInstance = new Singleton();
                    }
                }
            }
            return _uniqueInstance;
        }

        public void SaySomething()
        {
            Console.WriteLine("I am double checked, therefore I am");
        }
    }
    #endregion
}
