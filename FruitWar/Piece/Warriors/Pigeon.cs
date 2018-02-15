using FruitWar.Piece.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitWar.Piece.Warriors
{
    public class Pigeon : BaseWarrior, IWarrior, IPiece
    {
        const int SpeedConst = 3;
        const int PowerConst = 1;

        public Pigeon()
        {
            this.Speed = SpeedConst;
            this.Power = PowerConst;
        }
    }
}
