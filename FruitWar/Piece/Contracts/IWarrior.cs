namespace FruitWar.Piece.Contracts
{
    using FruitWar.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IWarrior : IPiece
    {
        int Power { get; }

        int Speed { get; }

        void Move(Direction direction);

        void Eat(IFruit fruit);
    }
}
