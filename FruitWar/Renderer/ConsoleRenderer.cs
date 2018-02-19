namespace FruitWar.Renderer
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using FruitWar.Board.Contracts;
    using FruitWar.Common;
    using FruitWar.Piece.Contracts;
    using FruitWar.Renderer.Contracts;

    public class ConsoleRenderer : IRenderer
    {
        public void RenderBoard(IBoard board)
        {
            //// TODO: Optimize board with StreamWriter
            for (int rows = 0; rows < board.TotalRows; rows++)
            {
                for (int cols = 0; cols < board.TotalCols; cols++)
                {
                    var position = new Position(rows, cols);

                    var piece = board.GetPieceAtPosition(position);

                    if (piece == null)
                    {
                        Console.Write('-');
                    }
                    else
                    {
                        Console.Write(piece.VisualSymbol);
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public void RenderWarriorsInfo(IList<IWarrior> warriors)
        {
            foreach (var warrior in warriors)
            {
                Console.WriteLine($"Player{warrior.VisualSymbol}: {warrior.Power} Power; {warrior.Speed} Speed");
            }

            Console.WriteLine();
        }

        public void RenderErrorMessage(string errorMessage)
        {
            Console.WriteLine(errorMessage);
            Thread.Sleep(2000);
        }

        public void RenderWinner(IWarrior warrior)
        {
            Console.WriteLine($"Player {warrior.VisualSymbol} wins the game.");
            Console.WriteLine($"Player{warrior.VisualSymbol}: {warrior.Power} Power; {warrior.Speed} Speed");
        }

        public void RenderDraw()
        {
            Console.WriteLine("Draw game.");
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
