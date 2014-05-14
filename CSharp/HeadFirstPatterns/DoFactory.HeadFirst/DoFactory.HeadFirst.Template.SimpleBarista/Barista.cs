using System;

namespace DoFactory.HeadFirst.Template.SimpleBarista
{
    class Barista
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nMaking tea...");
            Tea tea = new Tea();
            tea.PrepareRecipe();

            Console.WriteLine("\nMaking coffee...");
            Coffee coffee = new Coffee();
            coffee.PrepareRecipe();

            // Wait for user
            Console.ReadKey();
        }
    }

    #region Coffee

    public class Coffee
    {
        public void PrepareRecipe()
        {
            BoilWater();
            BrewCoffeeGrinds();
            PourInCup();
            AddSugarAndMilk();
        }

        public void BoilWater()
        {
            Console.WriteLine("Boiling water");
        }

        public void BrewCoffeeGrinds()
        {
            Console.WriteLine("Dripping Coffee through filter");
        }

        public void PourInCup()
        {
            Console.WriteLine("Pouring into cup");
        }

        public void AddSugarAndMilk()
        {
            Console.WriteLine("Adding Sugar and Milk");
        }
    }

    #endregion

    #region Tea

    public class Tea
    {

        public void PrepareRecipe()
        {
            BoilWater();
            SteepTeaBag();
            PourInCup();
            AddLemon();
        }

        public void BoilWater()
        {
            Console.WriteLine("Boiling water");
        }

        public void SteepTeaBag()
        {
            Console.WriteLine("Steeping the tea");
        }

        public void AddLemon()
        {
            Console.WriteLine("Adding Lemon");
        }

        public void PourInCup()
        {
            Console.WriteLine("Pouring into cup");
        }
    }
    #endregion
}
