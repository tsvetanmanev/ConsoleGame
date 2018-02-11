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
        int TotalRows { get; set; }

        int TotalCols { get; set; }

        void AddPiece(IPiece piece, Position position);

        void RemovePiece(Position position);

        IPiece GetPieceAtPosition(Position position);

        void MoveWarrior(IWarrior warrior, Position to);
    }
}
