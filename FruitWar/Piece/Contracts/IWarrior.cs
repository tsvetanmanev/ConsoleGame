namespace FruitWar.Piece.Contracts
{
    using FruitWar.Common;

    public interface IWarrior : IPiece
    {
        int Power { get; }

        int Speed { get; }

        void Move(Direction direction);

        void Eat(IFruit fruit);
    }
}
