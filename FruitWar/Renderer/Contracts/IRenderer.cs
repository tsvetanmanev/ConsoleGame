namespace FruitWar.Renderer.Contracts
{
    using FruitWar.Board.Contracts;
    using FruitWar.Piece.Contracts;
    using System.Collections.Generic;

    public interface IRenderer
    {
        void RenderBoard(IBoard board);

        void RenderWarriorsInfo(IList<IWarrior> warriors);
    }
}
