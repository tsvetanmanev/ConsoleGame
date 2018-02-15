using FruitWar.Board.Contracts;
using FruitWar.Common;
using FruitWar.Piece.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitWar.Board
{
    public class Board : IBoard
    {
        private readonly IPiece[,] board;

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
            // TODO: Check if ObjectIsNull

            Position.CheckIfValid(position);

            this.board[position.Row, position.Col] = piece;
        }

        public IPiece GetPieceAtPosition(Position position)
        {
            Position.CheckIfValid(position);

            return this.board[position.Row, position.Col];
        }

        public void MoveWarrior(IWarrior warrior, Direction direction)
        {
            warrior.Move(direction);
        }

        public void RemovePiece(Position position)
        {
            Position.CheckIfValid(position);

            this.board[position.Row, position.Col] = null;
        }
    }
}
