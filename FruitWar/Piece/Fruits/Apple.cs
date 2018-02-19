namespace FruitWar.Piece.Fruits
{
    using FruitWar.Common;
    using FruitWar.Piece.Contracts;

    public class Apple : IFruit, IPiece
    {
        public Apple()
        {
            this.StatAttribute = StatAttribute.Power;
            this.AttributePoints = GlobalConstants.AppleAttributePoints;
            this.VisualSymbol = GlobalConstants.AppleSymbol;
        }

        public StatAttribute StatAttribute { get; private set; }

        public int AttributePoints { get; private set; }

        public char VisualSymbol { get; private set; }

        public Position Position { get; set; }
    }
}
