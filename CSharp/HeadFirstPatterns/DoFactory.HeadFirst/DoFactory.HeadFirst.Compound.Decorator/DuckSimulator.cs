using System;

namespace DoFactory.HeadFirst.Combining.Decorator
{
    class DuckSimulator
    {
        static void Main(string[] args)
        {
            DuckSimulator simulator = new DuckSimulator();
            simulator.Simulate();
        }

        void Simulate() 
        {
            IQuackable mallardDuck = new QuackCounter(new MallardDuck());
            IQuackable redheadDuck = new QuackCounter(new RedheadDuck());
            IQuackable duckCall    = new QuackCounter(new DuckCall());
            IQuackable rubberDuck  = new QuackCounter(new RubberDuck());
            IQuackable gooseDuck   = new GooseAdapter(new Goose());

            Console.WriteLine("Duck Simulator: With Decorator");

            Simulate(mallardDuck);
            Simulate(redheadDuck);
            Simulate(duckCall);
            Simulate(rubberDuck);
            Simulate(gooseDuck);

            Console.WriteLine("The ducks quacked " + 
                QuackCounter.Quacks + " times");

            // Wait for user
            Console.ReadKey();
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

    public class RubberDuck : IQuackable 
    {
        public void Quack() 
        {
            Console.WriteLine("Squeak");
        }
  
        public override string ToString() 
        {
            return "Rubber Duck";
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
 
        public override string ToString() 
        {
            return "Mallard Duck";
        }
    }

    public class DuckCall : IQuackable 
    {
         public void Quack() 
        {
            Console.WriteLine("Kwak");
        }
 
        public override string ToString() 
        {
            return "Duck Call";
        }
    }

    public class DecoyDuck : IQuackable 
    {
         public void Quack() 
        {
            Console.WriteLine("<< Silence >>");
        }
 
        public override string ToString() 
        {
            return "Decoy Duck";
        }
    }

    #endregion

    #region QuackCounter

    public class QuackCounter : IQuackable 
    {
        private IQuackable _duck;
  
        public QuackCounter(IQuackable duck) 
        {
            this._duck = duck;
        }
  
        public void Quack() 
        {
            _duck.Quack();
            Quacks++;
        }
 
        public static int Quacks{ get; private set; }

        public override string ToString() 
        {
            return _duck.ToString();
        }
    }

    #endregion

    #region Goose 

    public class GooseAdapter : IQuackable 
    {
        private Goose _goose;
 
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

        public override string ToString() 
        {
            return "Goose";
        }
    }
    #endregion
}
