using System;

namespace DoFactory.HeadFirst.Combining.Factory
{
    class DuckSimulator
    {
        static void Main(string[] args)
        {
            DuckSimulator simulator = new DuckSimulator();
            AbstractDuckFactory factory = new CountingDuckFactory();
 
            simulator.Simulate(factory);

            // Wait for user
            Console.ReadKey();
        }
 
        void Simulate(AbstractDuckFactory factory) 
        {
            IQuackable mallardDuck = factory.CreateMallardDuck();
            IQuackable redheadDuck = factory.CreateRedheadDuck();
            IQuackable duckCall    = factory.CreateDuckCall();
            IQuackable rubberDuck  = factory.CreateRubberDuck();
            IQuackable gooseDuck = new GooseAdapter(new Goose());
 
            Console.WriteLine("Duck Simulator: With Abstract Factory");
 
            Simulate(mallardDuck);
            Simulate(redheadDuck);
            Simulate(duckCall);
            Simulate(rubberDuck);
            Simulate(gooseDuck);
 
            Console.WriteLine("The ducks quacked " + 
                QuackCounter.Quacks + " times");
        }
 
        void Simulate(IQuackable duck) 
        {
            duck.Quack();
        }
    }

    #region Factory

    public abstract class AbstractDuckFactory 
    {
        public abstract IQuackable CreateMallardDuck();
        public abstract IQuackable CreateRedheadDuck();
        public abstract IQuackable CreateDuckCall();
        public abstract IQuackable CreateRubberDuck();
    }

    public class DuckFactory : AbstractDuckFactory 
    {
         public override IQuackable CreateMallardDuck() 
        {
            return new MallardDuck();
        }
  
        public override IQuackable CreateRedheadDuck() 
        {
            return new RedheadDuck();
        }
  
        public override IQuackable CreateDuckCall() 
        {
            return new DuckCall();
        }
   
        public override IQuackable CreateRubberDuck() 
        {
            return new RubberDuck();
        }
    }

    public class CountingDuckFactory : AbstractDuckFactory 
    {
         public override IQuackable CreateMallardDuck() 
        {
            return new QuackCounter(new MallardDuck());
        }
  
        public override IQuackable CreateRedheadDuck() 
        {
            return new QuackCounter(new RedheadDuck());
        }
  
        public override IQuackable CreateDuckCall() 
        {
            return new QuackCounter(new DuckCall());
        }
   
        public override IQuackable CreateRubberDuck() 
        {
            return new QuackCounter(new RubberDuck());
        }
    }
    #endregion

    #region Quack Counter

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
 
        public static int Quacks { get; private set; }
   
        public override string ToString() 
        {
            return _duck.ToString();
        }
    }

    #endregion

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
