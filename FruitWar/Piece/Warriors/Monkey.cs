namespace FruitWar.Piece.Warriors
{
    using FruitWar.Piece.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Monkey: BaseWarrior, IWarrior, IPiece
    {
        const int SpeedConst = 2;
        const int PowerConst = 2;

        public Monkey(char symbol)
        {
            this.VisualSymbol = symbol;
            this.Speed = SpeedConst;
            this.Power = PowerConst;
        }
    }
}
