namespace FruitWar.Engine
{
    using System;
    using System.Collections.Generic;
    using FruitWar.Board;
    using FruitWar.Board.Contracts;
    using FruitWar.Common;
    using FruitWar.Engine.Contracts;
    using FruitWar.InputProviders.Contracts;
    using FruitWar.Piece.Contracts;
    using FruitWar.Piece.Fruits;
    using FruitWar.Renderer.Contracts;

    public class StandardFruitWarEngine : IFruitWarEngine
    {
        private const int CountOfFruit = 7;

        private readonly IRenderer renderer;
        private readonly IBoard board;
        private readonly IInputProvider input;

        private IList<IWarrior> warriors;
        private int currentWarriorIndex;
        private int currentTurnsLeft;
        private bool gameIsFinished;
        private bool gameIsDraw;
        private IWarrior winner;

        public StandardFruitWarEngine(IRenderer renderer, IInputProvider inputProvider)
        {
            this.renderer = renderer;
            this.input = inputProvider;
            this.board = new Board();
        }

        public void Initialize()
        {
            this.warriors = this.input.GetWarriors(2);

            this.AddWarriorsToBoard();

            this.AddFruitsToBoard();

            this.currentWarriorIndex = 0;
            this.currentTurnsLeft = 0;
            this.gameIsFinished = false;
            this.gameIsDraw = false;
            this.winner = null;
        }

        public void Start()
        {
            while (true)
            {
                var warrior = this.GetNextWarrior();

                this.currentTurnsLeft = warrior.Speed;

                while (this.currentTurnsLeft > 0)
                {
                    try
                    {
                        this.renderer.RenderBoard(this.board);
                        this.renderer.RenderWarriorsInfo(this.warriors);

                        var direction = this.input.GetNextMove(warrior);

                        var oldPosition = warrior.Position;
                        warrior.Move(direction);
                        var newPosition = warrior.Position;

                        var pieceOnNewPosition = this.board.GetPieceAtPosition(newPosition);
                        if (pieceOnNewPosition != null)
                        {
                            warrior = this.GetWarriorAfterCollision(warrior, pieceOnNewPosition);
                        }

                        this.MoveWarrior(warrior, oldPosition, newPosition);

                        this.renderer.Clear();

                        this.currentTurnsLeft--;

                        if (this.gameIsFinished)
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.renderer.RenderErrorMessage(ex.Message);
                        this.renderer.Clear();
                    }
                }

                if (this.gameIsFinished)
                {
                    this.FinishGame();
                    break;
                }
            }
        }

        private void MoveWarrior(IWarrior warrior, Position oldPosition, Position newPosition)
        {
            this.board.RemovePiece(oldPosition);
            this.board.AddPiece(warrior, newPosition);
        }

        private void FinishGame()
        {
            if (this.gameIsDraw)
            {
                this.renderer.RenderDraw();
            }
            else
            {
                this.renderer.RenderBoard(this.board);
                this.renderer.RenderWinner(this.winner);
            }

            var rematchVote = this.input.GetRematchVote();

            if (rematchVote == true)
            {
                this.Restart();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void Restart()
        {
            this.board.Clear();
            this.renderer.Clear();
            this.Initialize();
            this.Start();
        }

        private IWarrior GetWarriorAfterCollision(IWarrior warrior, IPiece piece)
        {
            if (piece is IFruit)
            {
                warrior.Eat((IFruit)piece);
                return warrior;
            }
            else if (piece is IWarrior)
            {
                IWarrior defendingWarrrior = (IWarrior)piece;

                this.winner = warrior;

                if (warrior.Power > defendingWarrrior.Power)
                {
                    this.winner = warrior;
                    this.gameIsFinished = true;
                }
                else if (warrior.Power < defendingWarrrior.Power)
                {
                    this.winner = defendingWarrrior;
                    this.gameIsFinished = true;
                }
                else
                {
                    this.gameIsDraw = true;
                    this.gameIsFinished = true;
                }
            }

            return this.winner;
        }

        private IWarrior GetNextWarrior()
        {
            if (this.currentWarriorIndex < 0 || this.currentWarriorIndex >= this.warriors.Count)
            {
                this.currentWarriorIndex = 0;
            }

            int currentIndex = this.currentWarriorIndex;

            this.currentWarriorIndex++;

            return this.warriors[currentIndex];
        }

        private void AddFruitsToBoard()
        {
            var fruitPositions = this.GetPiecePositions(CountOfFruit, 1);

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
            var warriorPositions = this.GetPiecePositions(this.warriors.Count, 2);

            for (int i = 0; i < this.warriors.Count; i++)
            {
                var warrior = this.warriors[i];
                var position = warriorPositions[i];

                this.board.AddPiece(this.warriors[i], warriorPositions[i]);
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

                this.MarkDisabledCellsInGrid(rowIndex, colIndex, disabledCellsAround, gridMask);
            }

            return positions;
        }

        private void MarkDisabledCellsInGrid(int firstRowIndex, int firstColIndex, int cellsAround, bool[,] boardMask)
        {
            for (int rowsToMark = 0; rowsToMark <= cellsAround; rowsToMark++)
            {
                int cellsToMark = (rowsToMark * 2) + 1;
                int currentCol = firstColIndex - rowsToMark;
                int currentRow = (firstRowIndex - cellsAround) + rowsToMark;

                this.MarkDisabledCellsInRow(cellsToMark, currentCol, currentRow, boardMask);
            }

            for (int rowsToMark = (cellsAround - 1), currentRow = (firstRowIndex + 1); rowsToMark >= 0; rowsToMark--)
            {
                int cellsToMark = (rowsToMark * cellsAround) + 1;
                int currentCol = firstColIndex - rowsToMark;

                MarkDisabledCellsInRow(cellsToMark, currentCol, currentRow, boardMask);

                currentRow++;
            }
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
                        Console.Write('-');
                    }
                    else
                    {
                        Console.Write('x');
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
