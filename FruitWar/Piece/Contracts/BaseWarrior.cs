using FruitWar.Common;
using FruitWar.Piece.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitWar.Piece.Contracts
{
    public abstract class BaseWarrior : IPiece, IWarrior
    {
        public int Power { get; private set; }

        public int Speed { get; private set; }

        public char Symbol { get; set; }

        public abstract void Move(Direction direction);

        public abstract void Eat(IFruit fruit);
    }
}
