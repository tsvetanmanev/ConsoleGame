namespace FruitWar.Renderer
{
    using FruitWar.Board.Contracts;
    using FruitWar.Common;
    using FruitWar.Renderer.Contracts;

    public class ConsoleRenderer : IRenderer
    {
        public void RenderBoard(IBoard board)
        {
            // TODO: Optimize board with StreamWriter

            for (int rows = 0; rows < board.TotalRows; rows++)
            {
                for (int cols = 0; cols < board.TotalCols; cols++)
                {
                    var position = new Position(rows, cols);

                    var piece = board.GetPieceAtPosition(position);

                    if (piece == null)
                    {
                        System.Console.Write(' ');
                    }
                    else
                    {
                        System.Console.Write(piece.VisualSymbol);
                    }

                }

                System.Console.WriteLine();
            }
        }
    }
}
