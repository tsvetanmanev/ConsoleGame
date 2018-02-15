namespace FruitWar
{
    using FruitWar.Engine;
    using FruitWar.Renderer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main(string[] args)
        {
            var renderer = new ConsoleRenderer();

            var fruitWarEngine = new StandardFruitWarEngine(renderer);

            fruitWarEngine.Initialize();

            Console.ReadLine();
        }
    }
}
