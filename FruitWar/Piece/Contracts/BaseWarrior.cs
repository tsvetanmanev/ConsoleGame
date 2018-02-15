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
        public int Power { get; protected set; }

        public int Speed { get; protected set; }

        public char VisualSymbol { get; set; }

        public Position Position { get; protected set; }

        public void Move(Direction direction)
        {
            int updatedRowPosition = this.Position.Row;
            int updatedColPosition = this.Position.Col;
            int cellsToMove = this.Speed;

            switch (direction)
            {
                case Direction.Up:
                    updatedRowPosition = updatedRowPosition - cellsToMove;
                    break;
                case Direction.Right:
                    updatedColPosition = updatedColPosition + cellsToMove;
                    break;
                case Direction.Down:
                    updatedRowPosition = updatedRowPosition + cellsToMove;
                    break;
                case Direction.Left:
                    updatedColPosition = updatedColPosition - cellsToMove;
                    break;
                default:
                    break;
            }

            this.Position = new Position(updatedRowPosition, updatedColPosition);

            //TODO: Make sure game checks if new Position is possible.
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
