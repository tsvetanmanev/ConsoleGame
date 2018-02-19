namespace FruitWar.Board.Contracts
{
    using FruitWar.Common;
    using FruitWar.Piece.Contracts;

    public interface IBoard
    {
        int TotalRows { get; }

        int TotalCols { get; }

        void AddPiece(IPiece piece, Position position);

        void RemovePiece(Position position);

        IPiece GetPieceAtPosition(Position position);

        void Clear();
    }
}
