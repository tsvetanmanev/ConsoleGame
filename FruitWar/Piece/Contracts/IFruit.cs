namespace FruitWar.Piece.Contracts
{
    using FruitWar.Common;

    public interface IFruit : IPiece
    {
        StatAttribute StatAttribute { get; }

        int AttributePoints { get; }
    }
}
