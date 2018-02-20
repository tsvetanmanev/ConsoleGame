namespace FruitWar.Engine
{
    using System;
    using System.Collections.Generic;
    using FruitWar.GameBoard;
    using FruitWar.GameBoard.Contracts;
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
        private IWarrior winner;
        private int currentWarriorIndex;
        private int currentTurnsLeft;
        private bool gameIsFinished;
        private bool gameIsDraw;

        public StandardFruitWarEngine(IRenderer renderer, IInputProvider inputProvider, IBoard board)
        {
            this.renderer = renderer;
            this.input = inputProvider;
            this.board = board;
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

        /// <summary>
        /// Gets the next warrior to play in the game.
        /// Moves the index forward for next iteration.
        /// </summary>
        private IWarrior GetNextWarrior()
        {
            //// Check If currentWarriorIndex is negative or bigger than the warrior count.
            //// If so restart the index to zero.
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
            var fruitPositions = this.GetPiecePositions(CountOfFruit, GlobalConstants.DisabledCellsAroundFruit);

            Random random = new Random();

            
            for (int i = 0; i < CountOfFruit; i++)
            {
                IFruit fruit;

                //// Pick the type of next fruit to add on the board.
                //// There is 50% chance of Apple and 50% chance of Pear.
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
            var warriorPositions = this.GetPiecePositions(this.warriors.Count, GlobalConstants.DisabledCellsAroundWarrior);

            for (int i = 0; i < this.warriors.Count; i++)
            {
                var warrior = this.warriors[i];
                var position = warriorPositions[i];

                this.board.AddPiece(this.warriors[i], warriorPositions[i]);
            }
        }

        /// <summary>
        /// Gets a list of randomly distributed positinions that can be used for the given game piece.
        /// </summary>
        /// <param name="piecesCount">A number of pieces. Can only be positive number.</param>
        /// <param name="amountOfDisabledCells">Number of cells around the piece that should unavailable for population.</param>
        /// <returns></returns>
        private IList<Position> GetPiecePositions(int piecesCount, int amountOfDisabledCells)
        {
            IList<Position> positions = new List<Position>();

            //// Creates a bool mask of the gameboard. 
            //// We use this to mark the spaces around the created piece that cannot be used for new pieces
            bool[,] gridMask = new bool[this.board.TotalRows, this.board.TotalCols];

            Random random = new Random();

            for (int i = 0; i < piecesCount; i++)
            {
                int rowIndex;
                int colIndex;

                Position position = new Position();

                //// Create new random Position and check if this position can be placed on the board
                while (true)
                {
                    rowIndex = random.Next(0, (this.board.TotalRows - 1));
                    colIndex = random.Next(0, (this.board.TotalCols - 1));

                    position.Row = rowIndex;
                    position.Col = colIndex;

                    //// If the new position is available on the mask and on the board - stop the loop and add it to the resuls list
                    //// Otherwise continue to generate new positions until it finds an available one
                    if (gridMask[rowIndex, colIndex] == false && this.board.GetPieceAtPosition(position) == null)
                    {
                        break;
                    }
                }

                positions.Add(position);

                this.MarkDisabledCellsInGrid(position, amountOfDisabledCells, gridMask);
            }

            return positions;
        }

        /// <summary>
        /// Marks cells around the position as unavailable.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="cellsAround">A radius of cells around the position that should be made unavailable.</param>
        /// <param name="boardMask"></param>
        private void MarkDisabledCellsInGrid(Position position, int cellsAround, bool[,] boardMask)
        {
            //// First loop marks the upper part and the middle row of the rhombus of unavailable cells
            for (int rowsToMark = 0; rowsToMark <= cellsAround; rowsToMark++)
            {
                int cellsToMark = (rowsToMark * 2) + 1;
                int currentCol = position.Col - rowsToMark;
                int currentRow = (position.Row - cellsAround) + rowsToMark;

                this.MarkDisabledCellsInRow(cellsToMark, currentCol, currentRow, boardMask);
            }

            //// Second loop marks the lower part of the rhombus of unavailable cells
            for (int rowsToMark = (cellsAround - 1), currentRow = (position.Row + 1); rowsToMark >= 0; rowsToMark--)
            {
                int cellsToMark = (rowsToMark * cellsAround) + 1;
                int currentCol = position.Col - rowsToMark;

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
