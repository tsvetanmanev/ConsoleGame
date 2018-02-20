namespace FruitWar.Renderer.Contracts
{
    using System.Collections.Generic;
    using FruitWar.GameBoard.Contracts;
    using FruitWar.Piece.Contracts;

    public interface IRenderer
    {
        void RenderBoard(IBoard board);

        void RenderWarriorsInfo(IList<IWarrior> warriors);

        void RenderErrorMessage(string errorMessage);

        void RenderWinner(IWarrior warrior);

        void RenderDraw();

        void Clear();
    }
}
