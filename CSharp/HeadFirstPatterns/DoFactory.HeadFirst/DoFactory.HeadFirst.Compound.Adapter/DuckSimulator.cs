using System;

namespace DoFactory.HeadFirst.Combining.Adapter
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
            IQuackable gooseDuck   = new GooseAdapter(new Goose());
 
            Console.WriteLine("Duck Simulator: With Goose Adapter");
 
            Simulate(mallardDuck);
            Simulate(redheadDuck);
            Simulate(duckCall);
            Simulate(rubberDuck);
            Simulate(gooseDuck);
        }
 
        void Simulate(IQuackable duck) 
        {
            duck.Quack();
        }
    }

    #region Ducks

    public interface IQuackable 
    {
        void Quack();
    }

    public class DecoyDuck : IQuackable 
    {
        public void Quack() 
        {
            Console.WriteLine("<< Silence >>");
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

    public class RubberDuck : IQuackable 
    {
        public void Quack() 
        {
            Console.WriteLine("Squeak");
        }
    }

    public class DuckCall : IQuackable 
    {
        public void Quack() 
        {
            Console.WriteLine("Kwak");
        }
    }

    #endregion

    #region Goose

    public class GooseAdapter : IQuackable 
    {
        private Goose _goose;
 
        // Constructor
        public GooseAdapter(Goose goose) 
        {
            this._goose = goose;
        }
 
        public void Quack() 
        {
            _goose.Honk();
        }

        public override string ToString() 
        {
            return "Goose pretending to be a Duck";
        }
    }

    public class Goose 
    {
        public void Honk() 
        {
            Console.WriteLine("Honk");
        }
    }

    #endregion
}
