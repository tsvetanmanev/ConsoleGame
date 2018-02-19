namespace FruitWar.Piece.Fruits
{
    using FruitWar.Common;
    using FruitWar.Piece.Contracts;

    public class Pear : IFruit, IPiece
    {
        public Pear()
        {
            this.StatAttribute = StatAttribute.Speed;
            this.AttributePoints = GlobalConstants.PearAttributePoints;
            this.VisualSymbol = GlobalConstants.PearSymbol;
        }

        public StatAttribute StatAttribute { get; private set; }

        public int AttributePoints { get; private set; }

        public char VisualSymbol { get; private set; }

        public Position Position { get; set; }
    }
}
