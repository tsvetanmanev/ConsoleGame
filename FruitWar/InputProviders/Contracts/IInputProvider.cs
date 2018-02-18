namespace FruitWar.InputProviders.Contracts
{
    using FruitWar.Common;
    using FruitWar.Piece.Contracts;
    using System.Collections.Generic;

    public interface IInputProvider
    {
        IList<IWarrior> GetWarriors(int numberOfPlayers);

        Position GetNextMove(IWarrior warrior);
    }
}
