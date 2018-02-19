namespace FruitWar.InputProviders.Contracts
{
    using System.Collections.Generic;
    using FruitWar.Common;
    using FruitWar.Piece.Contracts;

    public interface IInputProvider
    {
        IList<IWarrior> GetWarriors(int numberOfPlayers);

        Direction GetNextMove(IWarrior warrior);

        bool GetRematchVote();
    }
}
