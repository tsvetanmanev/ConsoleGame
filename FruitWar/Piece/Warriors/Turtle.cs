﻿namespace FruitWar.Piece.Warriors
{
    using FruitWar.Piece.Contracts;

    public class Turtle : BaseWarrior, IWarrior, IPiece
    {
        private const int SpeedConst = 1;
        private const int PowerConst = 3;

        public Turtle(char symbol)
        {
            this.VisualSymbol = symbol;
            this.Speed = SpeedConst;
            this.Power = PowerConst;
        }
    }
}
