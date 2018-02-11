using FruitWar.Common;
using FruitWar.Piece.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitWar.Piece.Fruits
{
    public class Pear : IFruit
    {
        public Pear()
        {
            this.StatAttribute = StatAttribute.Speed;
            this.AttributePoints = GlobalConstants.PearSymbol;
            this.Symbol = GlobalConstants.PearSymbol;
        }

        public StatAttribute StatAttribute { get; private set; }

        public int AttributePoints { get; private set; }

        public char Symbol { get; private set; }
    }
}
