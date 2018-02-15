namespace FruitWar.Renderer.Contracts
{
    using FruitWar.Board.Contracts;

    public interface IRenderer
    {
        void RenderBoard(IBoard board);
    }
}
