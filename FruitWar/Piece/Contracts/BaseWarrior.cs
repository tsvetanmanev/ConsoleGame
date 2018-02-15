using FruitWar.Common;
using FruitWar.Piece.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitWar.Piece.Contracts
{
    public abstract class BaseWarrior : IPiece, IWarrior
    {
        private const int CellsPerMove = 1;

        public int Power { get; protected set; }

        public int Speed { get; protected set; }

        public char VisualSymbol { get; set; }

        public Position Position { get; protected set; }

        public void Move(Direction direction)
        {
            int updatedRowPosition = this.Position.Row;
            int updatedColPosition = this.Position.Col;

            switch (direction)
            {
                case Direction.Up:
                    updatedRowPosition = updatedRowPosition - CellsPerMove;
                    break;
                case Direction.Right:
                    updatedColPosition = updatedColPosition + CellsPerMove;
                    break;
                case Direction.Down:
                    updatedRowPosition = updatedRowPosition + CellsPerMove;
                    break;
                case Direction.Left:
                    updatedColPosition = updatedColPosition - CellsPerMove;
                    break;
                default:
                    break;
            }

            Position updatedPosition = new Position(updatedRowPosition, updatedColPosition);

            Position.CheckIfValid(updatedPosition);

            this.Position = updatedPosition;
        }

        public void Eat(IFruit fruit)
        {
            if (fruit.StatAttribute == StatAttribute.Speed)
            {
                this.Speed += fruit.AttributePoints;
            }
            else if (fruit.StatAttribute == StatAttribute.Power)
            {
                this.Speed += fruit.AttributePoints;
            }
        }
    }
}
