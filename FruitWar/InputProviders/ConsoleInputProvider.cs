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

        public Direction GetNextMove(IWarrior warrior)
        {
            Console.WriteLine($"Player{warrior.VisualSymbol}, make a move please!");

            Console.ReadKey();

            return Direction.Down; 
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
                char playerNumberSymbol = (char)playerNumber;

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
