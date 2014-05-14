using System;
using System.Collections.Generic;

namespace DoFactory.HeadFirst.Combining.Observer
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
            IQuackable redheadDuck = factory.CreateRedheadDuck();
            IQuackable duckCall    = factory.CreateDuckCall();
            IQuackable rubberDuck  = factory.CreateRubberDuck();
            IQuackable gooseDuck   = new GooseAdapter(new Goose());
 
            Flock flockOfDucks = new Flock();
 
            flockOfDucks.Add(redheadDuck);
            flockOfDucks.Add(duckCall);
            flockOfDucks.Add(rubberDuck);
            flockOfDucks.Add(gooseDuck);
 
            Flock flockOfMallards = new Flock();
 
            IQuackable mallardOne   = factory.CreateMallardDuck();
            IQuackable mallardTwo   = factory.CreateMallardDuck();
            IQuackable mallardThree = factory.CreateMallardDuck();
            IQuackable mallardFour  = factory.CreateMallardDuck();

            flockOfMallards.Add(mallardOne);
            flockOfMallards.Add(mallardTwo);
            flockOfMallards.Add(mallardThree);
            flockOfMallards.Add(mallardFour);

            flockOfDucks.Add(flockOfMallards);

            Console.WriteLine("Duck Simulator: With Observer");

            Quackologist quackologist = new Quackologist();
            flockOfDucks.RegisterObserver(quackologist);

            Simulate(flockOfDucks);

            Console.WriteLine("\nThe ducks quacked " + 
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
 
        public static int Quacks {get; private set; }
 
        public void RegisterObserver(IObserver observer) 
        {
            _duck.RegisterObserver(observer);
        }
 
        public void NotifyObservers() 
        {
            _duck.NotifyObservers();
        }
   
        public override string ToString() 
        {
            return _duck.ToString();
        }
    }

    #endregion

    #region Observer

    public interface IObserver 
    {
        void Update(IQuackObservable duck);
    }

    public class Quackologist : IObserver 
    {
        public void Update(IQuackObservable duck) 
        {
            Console.WriteLine("Quackologist: " + duck + " just quacked.");
        }
 
        public override string ToString() 
        {
            return "Quackologist";
        }
    }

    public class Observable : IQuackObservable 
    {
        private List<IObserver> _observers = new List<IObserver>();
        private IQuackObservable _duck;
 
        public Observable(IQuackObservable duck) 
        {
            this._duck = duck;
        }
  
        public void RegisterObserver(IObserver observer) 
        {
            _observers.Add(observer);
        }
  
        public void NotifyObservers() 
        {
            foreach(IObserver observer in _observers)
            {
                observer.Update(_duck);
            }
        }
    }

    #endregion

    #region Flock 

    public class Flock : IQuackable 
    {
        private List<IQuackable> _ducks = new List<IQuackable>();
  
        public void Add(IQuackable duck) 
        {
            _ducks.Add(duck);
        }
  
        public void Quack() 
        {
            foreach(IQuackable duck in _ducks)
            {
                duck.Quack();
            }
        }
   
        public void RegisterObserver(IObserver observer) 
        {
            foreach(IQuackable duck in _ducks)
            {
                duck.RegisterObserver(observer);
            }
        }
  
        public void NotifyObservers() { }
  
        public override string ToString() 
        {
            return "Flock of Ducks";
        }
    }

    #endregion

    #region Ducks

    public interface IQuackObservable 
    {
        void RegisterObserver(IObserver observer);
        void NotifyObservers();
    }

    public interface IQuackable : IQuackObservable 
    {
        void Quack();
    }

    public class RubberDuck : IQuackable 
    {
        private Observable _observable;

        public RubberDuck() 
        {
            _observable = new Observable(this);
        }
 
        public void Quack() 
        {
            Console.WriteLine("Squeak");
            NotifyObservers();
        }

        public void RegisterObserver(IObserver observer) 
        {
            _observable.RegisterObserver(observer);
        }

        public void NotifyObservers() 
        {
            _observable.NotifyObservers();
        }
  
        public override string ToString() 
        {
            return "Rubber Duck";
        }
    }

    public class MallardDuck : IQuackable 
    {
        private Observable _observable;
 
        public MallardDuck() 
        {
            _observable = new Observable(this);
        }
 
        public void Quack() 
        {
            Console.WriteLine("Quack");
            NotifyObservers();
        }
 
        public void RegisterObserver(IObserver observer) 
        {
            _observable.RegisterObserver(observer);
        }
 
        public void NotifyObservers() 
        {
            _observable.NotifyObservers();
        }
 
        public override string ToString() 
        {
            return "Mallard Duck";
        }
    }

    public class RedheadDuck : IQuackable 
    {
        private Observable _observable;

        public RedheadDuck() 
        {
            _observable = new Observable(this);
        }

        public void Quack() 
        {
            Console.WriteLine("Quack");
            NotifyObservers();
        }

        public void RegisterObserver(IObserver observer) 
        {
            _observable.RegisterObserver(observer);
        }

        public void NotifyObservers() 
        {
            _observable.NotifyObservers();
        }

        public override string ToString() 
        {
            return "Redhead Duck";
        }
    }

    public class DuckCall : IQuackable 
    {
        private Observable _observable;

        public DuckCall() 
        {
            _observable = new Observable(this);
        }
 
        public void Quack() 
        {
            Console.WriteLine("Kwak");
            NotifyObservers();
        }
 
        public void RegisterObserver(IObserver observer) 
        {
            _observable.RegisterObserver(observer);
        }

        public void NotifyObservers() 
        {
            _observable.NotifyObservers();
        }
 
        public override string ToString() 
        {
            return "Duck Call";
        }
    }

    public class DecoyDuck : IQuackable 
    {
        private Observable _observable;

        public DecoyDuck() 
        {
            _observable = new Observable(this);
        }
 
        public void Quack() 
        {
            Console.WriteLine("<< Silence >>");
            NotifyObservers();
        }
 
        public void RegisterObserver(IObserver observer) 
        {
            _observable.RegisterObserver(observer);
        }

        public void NotifyObservers() 
        {
            _observable.NotifyObservers();
        }
 
        public override string ToString() 
        {
            return "Decoy Duck";
        }
    }

    #endregion

    #region Goose

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

    public class GooseAdapter : IQuackable 
    {
        private Goose _goose;
        private Observable _observable;

        public GooseAdapter(Goose goose) 
        {
            this._goose = goose;
            _observable = new Observable(this);
        }
 
        public void Quack() 
        {
            _goose.Honk();
            NotifyObservers();
        }

        public void RegisterObserver(IObserver observer) 
        {
            _observable.RegisterObserver(observer);
        }

        public void NotifyObservers() 
        {
            _observable.NotifyObservers();
        }

        public override string ToString() 
        {
            return "Goose pretending to be a Duck";
        }
    }
    #endregion
}
