namespace FruitWar.Piece.Warriors
{
    using FruitWar.Piece.Contracts;

    public class Monkey : BaseWarrior, IWarrior, IPiece
    {
        private const int SpeedConst = 2;
        private const int PowerConst = 2;

        public Monkey(char symbol)
        {
            this.VisualSymbol = symbol;
            this.Speed = SpeedConst;
            this.Power = PowerConst;
        }
    }
}
