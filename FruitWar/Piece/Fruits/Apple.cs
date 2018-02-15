using System;
using FruitWar.Piece.Contracts;
using FruitWar.Common;

namespace FruitWar.Piece.Fruits
{
    public class Apple : IFruit
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

        public Position Position { get; private set; }
    }
}
