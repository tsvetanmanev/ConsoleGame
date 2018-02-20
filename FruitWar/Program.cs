namespace FruitWar
{
    using System;
    using FruitWar.Engine;
    using FruitWar.InputProviders;
    using FruitWar.Renderer;

    public class Program
    {
        public static void Main(string[] args)
        {
            var renderer = new ConsoleRenderer();
            var inputProvider = new ConsoleInputProvider();
            var board = new FruitWar.Board.Board();

            var fruitWarEngine = new StandardFruitWarEngine(renderer, inputProvider, board);

            fruitWarEngine.Initialize();

            fruitWarEngine.Start();

            Console.ReadLine();
        }
    }
}
