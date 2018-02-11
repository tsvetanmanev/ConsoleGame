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
            this.Symbol = GlobalConstants.AppleSymbol;
        }

        public StatAttribute StatAttribute { get; private set; }

        public int AttributePoints { get; private set; }

        public char Symbol { get; private set; }
    }
}
