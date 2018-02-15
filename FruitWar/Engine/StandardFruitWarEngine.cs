namespace FruitWar.Engine
{
    using FruitWar.Board.Contracts;
    using FruitWar.Board;
    using FruitWar.Engine.Contracts;
    using FruitWar.Renderer.Contracts;

    public class StandardFruitWarEngine : IFruitWarEngine
    {
        private readonly IRenderer renderer;
        private readonly IBoard board;

        public StandardFruitWarEngine(IRenderer renderer)
        {
            this.renderer = renderer;
            this.board = new Board();


        }

        // Choose warriors


        public void Initialize()
        {
            this.renderer.RenderBoard(this.board);
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void CheckIfWon()
        {
            throw new System.NotImplementedException();
        }

    }
}
