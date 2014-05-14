using System;

namespace DoFactory.HeadFirst.Combining.Ducks
{
    class DuckSimulator
    {
        static void Main(string[] args)
        {
            DuckSimulator simulator = new DuckSimulator();
            simulator.Simulate();

            // Wait for user
            Console.ReadKey();
        }
  
        void Simulate() 
        {
            IQuackable mallardDuck = new MallardDuck();
            IQuackable redheadDuck = new RedheadDuck();
            IQuackable duckCall    = new DuckCall();
            IQuackable rubberDuck  = new RubberDuck();
 
            Console.WriteLine("Duck Simulator");
 
            Simulate(mallardDuck);
            Simulate(redheadDuck);
            Simulate(duckCall);
            Simulate(rubberDuck);
        }
   
        void Simulate(IQuackable duck) 
        {
            duck.Quack();
        }
    }

    #region Duck

    public interface IQuackable 
    {
        void Quack();
    }

    public class RubberDuck : IQuackable 
    {
        public void Quack() 
        {
            Console.WriteLine("Squeak");
        }
    }

    public class RedheadDuck : IQuackable 
    {
        public void Quack() 
        {
            Console.WriteLine("Quack");
        }
    }

    public class MallardDuck : IQuackable 
    {
        public void Quack() 
        {
            Console.WriteLine("Quack");
        }
    }

    public class DuckCall : IQuackable 
    {
        public void Quack() 
        {
            Console.WriteLine("Kwak");
        }
    }

    public class DecoyDuck : IQuackable 
    {
        public void Quack() 
        {
            Console.WriteLine("<< Silence >>");
        }
    }

    #endregion
}