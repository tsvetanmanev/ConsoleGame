namespace FruitWar.Board.Contracts
{
    using FruitWar.Common;
    using FruitWar.Piece.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBoard
    {
        int TotalRows { get; }

        int TotalCols { get; }

        void AddPiece(IPiece piece, Position position);

        void RemovePiece(Position position);

        IPiece GetPieceAtPosition(Position position);

        void MoveWarrior(IWarrior warrior, Direction direction);
    }
}
