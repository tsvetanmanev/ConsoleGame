namespace FruitWar.Piece.Warriors
{
    using FruitWar.Piece.Contracts;

    public class Pigeon : BaseWarrior, IWarrior, IPiece
    {
        const int SpeedConst = 3;
        const int PowerConst = 1;

        public Pigeon(char symbol)
        {
            this.VisualSymbol = symbol;
            this.Speed = SpeedConst;
            this.Power = PowerConst;
        }
    }
}
