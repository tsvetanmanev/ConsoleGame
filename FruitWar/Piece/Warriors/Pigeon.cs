namespace FruitWar.Piece.Warriors
{
    using FruitWar.Piece.Contracts;

    public class Pigeon : BaseWarrior, IWarrior, IPiece
    {
        private const int SpeedConst = 3;
        private const int PowerConst = 1;

        public Pigeon(char symbol)
        {
            this.VisualSymbol = symbol;
            this.Speed = SpeedConst;
            this.Power = PowerConst;
        }
    }
}
