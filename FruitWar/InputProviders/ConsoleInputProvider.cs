namespace FruitWar.InputProviders
{
    using System;
    using System.Collections.Generic;
    using FruitWar.Common;
    using FruitWar.InputProviders.Contracts;
    using FruitWar.Piece.Contracts;
    using FruitWar.Piece.Warriors;

    public class ConsoleInputProvider : IInputProvider
    {
        private const string ChooseWarriorText = "Player{0}, please choose a warrior.\nInsert 1 for turtle / 2 for monkey / 3 for pigeon";
        private const string AskForRematchText = "Do you want to start a rematch? (y/n)";

        public Direction GetNextMove(IWarrior warrior)
        {
            Console.WriteLine($"Player{warrior.VisualSymbol}, make a move please!");

            var direction = new Direction();

            ConsoleKeyInfo keyInfo;
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        direction = Direction.Up;
                        return direction;

                    case ConsoleKey.RightArrow:
                        direction = Direction.Right;
                        return direction;

                    case ConsoleKey.DownArrow:
                        direction = Direction.Down;
                        return direction;

                    case ConsoleKey.LeftArrow:
                        direction = Direction.Left;
                        return direction;
                }
            }

            return direction;
        }

        public bool GetRematchVote()
        {
            Console.WriteLine(AskForRematchText);

            string inputChoice = Console.ReadLine();
            if (inputChoice == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<IWarrior> GetWarriors(int numberOfPlayers)
        {
            var warriors = new List<IWarrior>();

            for (int playerNumber = 1; playerNumber <= numberOfPlayers; playerNumber++)
            {
                Console.Clear();
                Console.WriteLine(string.Format(ChooseWarriorText, playerNumber));
                string inputChoice = Console.ReadLine();

                IWarrior warrior;
                char playerNumberSymbol = playerNumber.ToString()[0];

                switch (inputChoice)
                {
                    case "1":
                        warrior = new Turtle(playerNumberSymbol);
                        warriors.Add(warrior);
                        break;
                    case "2":
                        warrior = new Monkey(playerNumberSymbol);
                        warriors.Add(warrior);
                        break;
                    case "3":
                        warrior = new Pigeon(playerNumberSymbol);
                        warriors.Add(warrior);
                        break;
                    default:
                        throw new InvalidOperationException("Invalid choice!");
                }
            }

            Console.Clear();

            return warriors;
        }
    }
}
