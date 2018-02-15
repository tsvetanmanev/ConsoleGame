﻿using FruitWar.Common;
using FruitWar.Piece.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitWar.Piece.Warriors
{
    public class Turtle : BaseWarrior, IPiece, IWarrior
    {
        const int SpeedConst = 1;
        const int PowerConst = 3;

        public Turtle()
        {
            this.Speed = SpeedConst;
            this.Power = PowerConst;
        }
    }
}
