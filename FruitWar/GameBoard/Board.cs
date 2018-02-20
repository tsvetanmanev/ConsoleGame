namespace FruitWar.GameBoard
{
    using FruitWar.GameBoard.Contracts;
    using FruitWar.Common;
    using FruitWar.Piece.Contracts;
    using System;

    public class Board : IBoard
    {
        private IPiece[,] board;

        public Board(int rows = GlobalConstants.StandardGameTotalBoardRows, int cols = GlobalConstants.StandardGameTotalBoardCols)
        {
            this.TotalRows = rows;
            this.TotalCols = cols;

            this.board = new IPiece[rows, cols];
        }

        public int TotalRows { get; private set; }

        public int TotalCols { get; private set; }

        public void AddPiece(IPiece piece, Position position)
        {
            if (piece == null)
            {
                throw new ArgumentNullException("piece");
            }

            Position.CheckIfValid(position);
            this.board[position.Row, position.Col] = piece;
            piece.Position = position;
        }

        public IPiece GetPieceAtPosition(Position position)
        {
            Position.CheckIfValid(position);

            return this.board[position.Row, position.Col];
        }

        public void RemovePiece(Position position)
        {
            Position.CheckIfValid(position);

            this.board[position.Row, position.Col] = null;
        }

        public void Clear()
        {
            this.board = new IPiece[this.TotalRows, this.TotalCols];
        }
    }
}
