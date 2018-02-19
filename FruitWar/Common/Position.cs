namespace FruitWar.Common
{
    using System;

    public struct Position
    {
        public Position(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row { get; set; }

        public int Col { get; set; }

        public static void CheckIfValid(Position position)
        {
            if (position.Row < 0
                || position.Row >= GlobalConstants.StandardGameTotalBoardRows)
            {
                throw new IndexOutOfRangeException("Selected row position on the board is not valid!");
            }

            if (position.Col < 0
                || position.Col >= GlobalConstants.StandardGameTotalBoardCols)
            {
                throw new IndexOutOfRangeException("Selected column position on the board is not valid!");
            }
        }
    }
}
