using FruitWar.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitWar.Piece.Contracts
{
    public interface IPiece
    {
        Position Position { get; }

        char VisualSymbol { get; }

    }
}
