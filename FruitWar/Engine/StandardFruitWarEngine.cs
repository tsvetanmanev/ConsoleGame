namespace FruitWar.Engine
{
    using FruitWar.Board.Contracts;
    using FruitWar.Board;
    using FruitWar.Engine.Contracts;
    using FruitWar.Renderer.Contracts;
    using FruitWar.Piece.Warriors;
    using FruitWar.Common;
    using FruitWar.InputProviders.Contracts;
    using FruitWar.Piece.Contracts;
    using System.Collections.Generic;
    using System;
    using FruitWar.Piece.Fruits;

    public class StandardFruitWarEngine : IFruitWarEngine
    {
        private readonly IRenderer renderer;
        private readonly IBoard board;
        private readonly IInputProvider input;

        private IList<IWarrior> warriors;

        private const int CountOfFruit = 7;

        public StandardFruitWarEngine(IRenderer renderer, IInputProvider inputProvider)
        {
            this.renderer = renderer;
            this.input = inputProvider;
            this.board = new Board();
        }

        public void Initialize()
        {
            // Let players choose their warrior
            //warriors = this.input.GetWarriors(2);
            warriors = new List<IWarrior> { { new Monkey('1') }, { new Pigeon('2') } };

            this.AddWarriorsToBoard();

            this.AddFruitsToBoard();
        }

        private void AddFruitsToBoard()
        {
            var fruitPositions = GetPiecePositions(CountOfFruit, 1);

            Random random = new Random();

            for (int i = 0; i < CountOfFruit; i++)
            {
                IFruit fruit;

                int randomNumber = random.Next(0, 200);

                if (randomNumber >= 100)
                {
                    fruit = new Apple();
                }
                else
                {
                    fruit = new Pear();
                }

                this.board.AddPiece(fruit, fruitPositions[i]);
            }
        }

        private void AddWarriorsToBoard()
        {
            var warriorPositions = GetPiecePositions(warriors.Count, 2);

            for (int i = 0; i < warriors.Count; i++)
            {
                var warrior = warriors[i];
                var position = warriorPositions[i];

                this.board.AddPiece(warriors[i], warriorPositions[i]);
            }
        }

        private IList<Position> GetPiecePositions(int piecesCount, int disabledCellsAround)
        {
            List<Position> positions = new List<Position>();

            bool[,] gridMask = new bool[this.board.TotalRows, this.board.TotalCols];

            Random random = new Random();

            for (int i = 0; i < piecesCount; i++)
            {
                int rowIndex;
                int colIndex;

                Position positionCheck = new Position();

                while (true)
                {
                    rowIndex = random.Next(0, (this.board.TotalRows - 1));
                    colIndex = random.Next(0, (this.board.TotalCols - 1));

                    positionCheck.Row = rowIndex;
                    positionCheck.Col = colIndex;

                    if (gridMask[rowIndex, colIndex] == false && this.board.GetPieceAtPosition(positionCheck) == null)
                    {
                        break;
                    }
                }

                positions.Add(positionCheck);

                MarkDisabledCellsInGrid(rowIndex, colIndex, disabledCellsAround, gridMask);
            }

            return positions;

        }

        private void MarkDisabledCellsInGrid(int firstRowIndex, int firstColIndex, int cellsAround, bool[,] boardMask)
        {
            for (int rowsToMark = 0; rowsToMark <= cellsAround; rowsToMark++)
            {
                int cellsToMark = (rowsToMark * cellsAround) + 1;
                int currentCol = firstColIndex - rowsToMark;
                int currentRow = (firstRowIndex - cellsAround) + rowsToMark;

                MarkDisabledCellsInRow(cellsToMark, currentCol, currentRow, boardMask);
            }

            for (int rowsToMark = (cellsAround - 1), currentRow = (firstRowIndex + 1); rowsToMark >= 0; rowsToMark--)
            {
                int cellsToMark = (rowsToMark * cellsAround) + 1;
                int currentCol = firstColIndex - rowsToMark;

                MarkDisabledCellsInRow(cellsToMark, currentCol, currentRow, boardMask);

                currentRow++;
            }

            // For debugging purposes
            // this.RenderBoardMask(boardMask);
        }

        private void MarkDisabledCellsInRow(int cellsToMark, int currentCol, int currentRow, bool[,] boardMask)
        {
            for (int markedCells = 0; markedCells < cellsToMark; markedCells++, currentCol++)
            {
                if (currentRow < 0 || currentRow >= this.board.TotalRows || currentCol < 0 || currentCol >= this.board.TotalCols)
                {
                    continue;
                }

                boardMask[currentRow, currentCol] = true;
            }
        }

        private void RenderBoardMask(bool[,] boardMask)
        {
            for (int rows = 0; rows < 8; rows++)
            {
                for (int cols = 0; cols < 8; cols++)
                {
                    if (boardMask[rows, cols] == false)
                    {
                        System.Console.Write('-');
                    }
                    else
                    {
                        System.Console.Write('x');
                    }
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine();
        }

        public void Start()
        {
            while (true)
            {
                this.renderer.RenderBoard(this.board);

                this.renderer.RenderWarriorsInfo(warriors);

                foreach (var warrior in warriors)
                {
                    var direction = this.input.GetNextMove(warrior);
                }

                Console.ReadLine();
            }
        }

        public void CheckIfWon()
        {
            throw new System.NotImplementedException();
        }

    }
}
