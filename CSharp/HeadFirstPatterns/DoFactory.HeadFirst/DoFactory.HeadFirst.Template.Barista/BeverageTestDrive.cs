using System;

namespace DoFactory.HeadFirst.Template.Barista
{
    class BeverageTestDrive
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nMaking tea...");
            Tea tea = new Tea();
            tea.PrepareRecipe();

            Console.WriteLine("\nMaking coffee...");
            Coffee coffee = new Coffee();
            coffee.PrepareRecipe();

            // Hooked on Template (page 292)

            Console.WriteLine("\nMaking tea...");
            TeaWithHook teaHook = new TeaWithHook();
            teaHook.PrepareRecipe();

            Console.WriteLine("\nMaking coffee...");
            CoffeeWithHook coffeeHook = new CoffeeWithHook();
            coffeeHook.PrepareRecipe();

            // Wait for user
            Console.ReadKey();
        }
    }

    #region Coffee and Tea

    public abstract class CaffeineBeverage
    {
        public void PrepareRecipe()
        {
            BoilWater();
            Brew();
            PourInCup();
            AddCondiments();
        }

        public abstract void Brew();

        public abstract void AddCondiments();

        void BoilWater()
        {
            Console.WriteLine("Boiling water");
        }

        void PourInCup()
        {
            Console.WriteLine("Pouring into cup");
        }
    }

    public class Coffee : CaffeineBeverage
    {
        public override void Brew()
        {
            Console.WriteLine("Dripping Coffee through filter");
        }
        public override void AddCondiments()
        {
            Console.WriteLine("Adding Sugar and Milk");
        }
    }

    public class Tea : CaffeineBeverage
    {
        public override void Brew()
        {
            Console.WriteLine("Steeping the tea");
        }
        public override void AddCondiments()
        {
            Console.WriteLine("Adding Lemon");
        }
    }

    #endregion

    #region Coffee and Tea with Hook

    public abstract class CaffeineBeverageWithHook
    {
        public void PrepareRecipe()
        {
            BoilWater();
            Brew();
            PourInCup();
            if (CustomerWantsCondiments())
            {
                AddCondiments();
            }
        }

        public abstract void Brew();

        public abstract void AddCondiments();

        public void BoilWater()
        {
            Console.WriteLine("Boiling water");
        }

        public void PourInCup()
        {
            Console.WriteLine("Pouring into cup");
        }

        public virtual bool CustomerWantsCondiments()
        {
            return true;
        }
    }

    public class CoffeeWithHook : CaffeineBeverageWithHook
    {
        public override void Brew()
        {
            Console.WriteLine("Dripping Coffee through filter");
        }

        public override void AddCondiments()
        {
            Console.WriteLine("Adding Sugar and Milk");
        }

        public override bool CustomerWantsCondiments()
        {
            string answer = GetUserInput();

            if (answer.ToLower().StartsWith("y"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetUserInput()
        {
            string answer = null;
            Console.WriteLine("Would you like milk and sugar with your coffee (y/n)? ");

            try
            {
                answer = Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("IO error trying to read your answer");
            }

            if (answer == null)
            {
                return "no";
            }
            return answer;
        }
    }

    public class TeaWithHook : CaffeineBeverageWithHook
    {

        public override void Brew()
        {
            Console.WriteLine("Steeping the tea");
        }

        public override void AddCondiments()
        {
            Console.WriteLine("Adding Lemon");
        }

        public override bool CustomerWantsCondiments()
        {
            string answer = GetUserInput();

            if (answer.ToLower().StartsWith("y"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GetUserInput()
        {
            // get the user's response
            string answer = null;

            Console.WriteLine("Would you like lemon with your tea (y/n)? ");

            try
            {
                answer = Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("IO error trying to read your answer");
            }

            if (answer == null)
            {
                return "no";
            }
            return answer;
        }
    }
    #endregion
}
