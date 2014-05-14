using System;
using System.Text;

namespace DoFactory.HeadFirst.Factory.Abstract.Pizza
{
    // PizzaTestDrive test application
    class PizzaTestDrive
    {
        static void Main(string[] args)
        {
            PizzaStore nyStore = new NewYorkPizzaStore();
            PizzaStore chStore = new ChicagoPizzaStore();

            Pizza pizza = nyStore.OrderPizza("cheese");
            Console.WriteLine("Ethan ordered a " + pizza.Name + "\n");

            pizza = chStore.OrderPizza("cheese");
            Console.WriteLine("Joel ordered a " + pizza.Name + "\n");

            pizza = nyStore.OrderPizza("clam");
            Console.WriteLine("Ethan ordered a " + pizza.Name + "\n");

            pizza = chStore.OrderPizza("clam");
            Console.WriteLine("Joel ordered a " + pizza.Name + "\n");

            pizza = nyStore.OrderPizza("pepperoni");
            Console.WriteLine("Ethan ordered a " + pizza.Name + "\n");

            pizza = chStore.OrderPizza("pepperoni");
            Console.WriteLine("Joel ordered a " + pizza.Name + "\n");

            pizza = nyStore.OrderPizza("veggie");
            Console.WriteLine("Ethan ordered a " + pizza.Name + "\n");

            pizza = chStore.OrderPizza("veggie");
            Console.WriteLine("Joel ordered a " + pizza.Name + "\n");

            // Wait for user
            Console.ReadKey();
        }
    }

    #region Ingredient Abstract Factory

    public interface IPizzaIngredientFactory
    {
        IDough CreateDough();
        ISauce CreateSauce();
        ICheese CreateCheese();
        IVeggies[] CreateVeggies();
        IPepperoni CreatePepperoni();
        IClams CreateClam();
    }

    public class NewYorkPizzaIngredientFactory : IPizzaIngredientFactory
    {
        public IDough CreateDough()
        {
            return new ThinCrustDough();
        }

        public ISauce CreateSauce()
        {
            return new MarinaraSauce();
        }

        public ICheese CreateCheese()
        {
            return new ReggianoCheese();
        }

        public IVeggies[] CreateVeggies()
        {
            IVeggies[] veggies = { new Garlic(), 
                                   new Onion(), 
                                   new Mushroom(), 
                                   new RedPepper() };
            return veggies;
        }

        public IPepperoni CreatePepperoni()
        {
            return new SlicedPepperoni();
        }

        public IClams CreateClam()
        {
            return new FreshClams();
        }
    }

    public class ChicagoPizzaIngredientFactory : IPizzaIngredientFactory
    {

        public IDough CreateDough()
        {
            return new ThickCrustDough();
        }

        public ISauce CreateSauce()
        {
            return new PlumTomatoSauce();
        }

        public ICheese CreateCheese()
        {
            return new MozzarellaCheese();
        }

        public IVeggies[] CreateVeggies()
        {
            IVeggies[] veggies = { new BlackOlives(), 
                                   new Spinach(), 
                                   new Eggplant() };
            return veggies;
        }

        public IPepperoni CreatePepperoni()
        {
            return new SlicedPepperoni();
        }

        public IClams CreateClam()
        {
            return new FrozenClams();
        }
    }

    #endregion

    #region Pizza Stores

    public abstract class PizzaStore
    {
        public Pizza OrderPizza(string type)
        {
            Pizza pizza = CreatePizza(type);
            Console.WriteLine("--- Making a " + pizza.Name + " ---");

            pizza.Prepare();
            pizza.Bake();
            pizza.Cut();
            pizza.Box();

            return pizza;
        }

        public abstract Pizza CreatePizza(string item);
    }

    public class NewYorkPizzaStore : PizzaStore
    {
        public override Pizza CreatePizza(string item)
        {
            Pizza pizza = null;
            IPizzaIngredientFactory ingredientFactory =
                new NewYorkPizzaIngredientFactory();

            switch (item)
            {
                case "cheese":
                    pizza = new CheesePizza(ingredientFactory);
                    pizza.Name = "New York Style Cheese Pizza";
                    break;
                case "veggie":
                    pizza = new VeggiePizza(ingredientFactory);
                    pizza.Name = "New York Style Veggie Pizza";
                    break;
                case "clam":
                    pizza = new ClamPizza(ingredientFactory);
                    pizza.Name = "New York Style Clam Pizza";
                    break;
                case "pepperoni":
                    pizza = new PepperoniPizza(ingredientFactory);
                    pizza.Name = "New York Style Pepperoni Pizza";
                    break;
            }
            return pizza;
        }
    }

    public class ChicagoPizzaStore : PizzaStore
    {
        // Factory method implementation
        public override Pizza CreatePizza(string item)
        {
            Pizza pizza = null;
            IPizzaIngredientFactory ingredientFactory =
                new ChicagoPizzaIngredientFactory();

            switch (item)
            {
                case "cheese":
                    pizza = new CheesePizza(ingredientFactory);
                    pizza.Name = "Chicago Style Cheese Pizza";
                    break;
                case "veggie":
                    pizza = new VeggiePizza(ingredientFactory);
                    pizza.Name = "Chicago Style Veggie Pizza";
                    break;
                case "clam":
                    pizza = new ClamPizza(ingredientFactory);
                    pizza.Name = "Chicago Style Clam Pizza";
                    break;
                case "pepperoni":
                    pizza = new PepperoniPizza(ingredientFactory);
                    pizza.Name = "Chicago Style Pepperoni Pizza";
                    break;
            }
            return pizza;
        }
    }

    #endregion

    #region Pizzas

    public abstract class Pizza
    {
        protected IDough dough;
        protected ISauce sauce;
        protected IVeggies[] veggies;
        protected ICheese cheese;
        protected IPepperoni pepperoni;
        protected IClams clam;

        private string name;

        public abstract void Prepare();

        public void Bake()
        {
            Console.WriteLine("Bake for 25 minutes at 350");
        }

        public virtual void Cut()
        {
            Console.WriteLine("Cutting the pizza into diagonal slices");
        }

        public void Box()
        {
            Console.WriteLine("Place pizza in official Pizzastore box");
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append("---- " + this.Name + " ----\n");
            if (dough != null)
            {
                result.Append(dough);
                result.Append("\n");
            }
            if (sauce != null)
            {
                result.Append(sauce);
                result.Append("\n");
            }
            if (cheese != null)
            {
                result.Append(cheese);
                result.Append("\n");
            }
            if (veggies != null)
            {
                for (int i = 0; i < veggies.Length; i++)
                {
                    result.Append(veggies[i]);
                    if (i < veggies.Length - 1)
                    {
                        result.Append(", ");
                    }
                }
                result.Append("\n");
            }
            if (clam != null)
            {
                result.Append(clam);
                result.Append("\n");
            }
            if (pepperoni != null)
            {
                result.Append(pepperoni);
                result.Append("\n");
            }
            return result.ToString();
        }
    }

    public class ClamPizza : Pizza
    {
        private IPizzaIngredientFactory _ingredientFactory;

        public ClamPizza(IPizzaIngredientFactory ingredientFactory)
        {
            this._ingredientFactory = ingredientFactory;
        }

        public override void Prepare()
        {
            Console.WriteLine("Preparing " + this.Name);
            dough = _ingredientFactory.CreateDough();
            sauce = _ingredientFactory.CreateSauce();
            cheese = _ingredientFactory.CreateCheese();
            clam = _ingredientFactory.CreateClam();
        }
    }

    public class CheesePizza : Pizza
    {
        private IPizzaIngredientFactory _ingredientFactory;

        public CheesePizza(IPizzaIngredientFactory ingredientFactory)
        {
            this._ingredientFactory = ingredientFactory;
        }

        public override void Prepare()
        {
            Console.WriteLine("Preparing " + this.Name);
            dough = _ingredientFactory.CreateDough();
            sauce = _ingredientFactory.CreateSauce();
            cheese = _ingredientFactory.CreateCheese();
        }
    }

    public class PepperoniPizza : Pizza
    {
        private IPizzaIngredientFactory _ingredientFactory;

        public PepperoniPizza(IPizzaIngredientFactory ingredientFactory)
        {
            this._ingredientFactory = ingredientFactory;
        }

        public override void Prepare()
        {
            Console.WriteLine("Preparing " + this.Name);
            dough = _ingredientFactory.CreateDough();
            sauce = _ingredientFactory.CreateSauce();
            cheese = _ingredientFactory.CreateCheese();
            veggies = _ingredientFactory.CreateVeggies();
            pepperoni = _ingredientFactory.CreatePepperoni();
        }
    }

    public class VeggiePizza : Pizza
    {
        private IPizzaIngredientFactory _ingredientFactory;

        public VeggiePizza(IPizzaIngredientFactory ingredientFactory)
        {
            this._ingredientFactory = ingredientFactory;
        }

        public override void Prepare()
        {
            Console.WriteLine("Preparing " + this.Name);
            dough = _ingredientFactory.CreateDough();
            sauce = _ingredientFactory.CreateSauce();
            cheese = _ingredientFactory.CreateCheese();
            veggies = _ingredientFactory.CreateVeggies();
        }
    }

    #endregion

    #region Ingredients

    public class ThinCrustDough : IDough
    {
        public override string ToString()
        {
            return "Thin Crust Dough";
        }
    }

    public class ThickCrustDough : IDough
    {
        public override string ToString()
        {
            return "ThickCrust style extra thick crust dough";
        }
    }

    public class Spinach : IVeggies
    {
        public override string ToString()
        {
            return "Spinach";
        }
    }

    public class SlicedPepperoni : IPepperoni
    {
        public override string ToString()
        {
            return "Sliced Pepperoni";
        }
    }

    public interface ISauce
    {
        string ToString();
    }
    public interface IDough
    {
        string ToString();
    }
    public interface IClams
    {
        string ToString();
    }
    public interface IVeggies
    {
        string ToString();
    }
    public interface ICheese
    {
        string ToString();
    }

    public interface IPepperoni
    {
        string ToString();
    }

    public class Garlic : IVeggies
    {
        public override string ToString()
        {
            return "Garlic";
        }
    }

    public class Onion : IVeggies
    {
        public override string ToString()
        {
            return "Onion";
        }
    }

    public class Mushroom : IVeggies
    {

        public override string ToString()
        {
            return "Mushrooms";
        }
    }

    public class Eggplant : IVeggies
    {
        public override string ToString()
        {
            return "Eggplant";
        }
    }

    public class BlackOlives : IVeggies
    {
        public override string ToString()
        {
            return "Black Olives";
        }
    }

    public class RedPepper : IVeggies
    {
        public override string ToString()
        {
            return "Red Pepper";
        }
    }

    public class PlumTomatoSauce : ISauce
    {
        public override string ToString()
        {
            return "Tomato sauce with plum tomatoes";
        }
    }
    public class MarinaraSauce : ISauce
    {
        public override string ToString()
        {
            return "Marinara Sauce";
        }
    }

    public class FreshClams : IClams
    {

        public override string ToString()
        {
            return "Fresh Clams from Long Island Sound";
        }
    }
    public class FrozenClams : IClams
    {
        public override string ToString()
        {
            return "Frozen Clams from Chesapeake Bay";
        }
    }

    public class ParmesanCheese : ICheese
    {
        public override string ToString()
        {
            return "Shredded Parmesan";
        }
    }

    public class MozzarellaCheese : ICheese
    {
        public override string ToString()
        {
            return "Shredded Mozzarella";
        }
    }

    public class ReggianoCheese : ICheese
    {
        public override string ToString()
        {
            return "Reggiano Cheese";
        }
    }
    #endregion
}
