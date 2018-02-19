namespace FruitWar.Piece.Contracts
{
    using FruitWar.Common;

    public interface IPiece
    {
        Position Position { get; set; }

        char VisualSymbol { get; }

    }
}
