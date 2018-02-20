namespace FruitWar.Tests
{
    using FruitWar.Common;
    using FruitWar.GameBoard;
    using FruitWar.Piece.Contracts;
    using FruitWar.Piece.Fruits;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void AddPieceWithNullPieceShouldThrowArgumentNullException()
        {
            // Arrange
            var board = new Board();
            IPiece piece = null;
            var position = new Position(2, 2);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                board.AddPiece(piece, position));
        }

        [Test]
        public void AddPieceWithInvalidPositionShouldThrowIndexOutOfRangeException()
        {
            // Arrange
            var board = new Board();
            IPiece piece = new Apple();
            var position = new Position(10, 16);

            // Act and Assert
            Assert.Throws<IndexOutOfRangeException>(() =>
                board.AddPiece(piece, position));
        }

        [Test]
        public void AddPieceWithValidParametersShouldUpdateThePositionOfThePiece()
        {
            // Arrange
            var board = new Board();
            IPiece piece = new Pear();
            var position = new Position(4, 4);

            // Act
            board.AddPiece(piece, position);

            // Assert
            Assert.AreEqual(position, piece.Position);
        }

        [Test]
        public void AddPieceWithValidParametersShouldAddThePieceToTheBoard()
        {
            // Arrange
            var board = new Board();
            IPiece piece = new Pear();
            var position = new Position(4, 4);

            // Act
            board.AddPiece(piece, position);
            var extractedPiece = board.GetPieceAtPosition(position);

            // Assert
            Assert.AreSame(piece, extractedPiece);

        }

        [Test]
        public void RemovePieceShouldNullThisPositionOnTheBoard()
        {
            // Arrange
            var board = new Board();
            IPiece piece = new Pear();
            var position = new Position(4, 4);
            board.AddPiece(piece, position);

            // Act
            board.RemovePiece(position);
            var extractedPiece = board.GetPieceAtPosition(position);

            // Assert
            Assert.AreEqual(null, extractedPiece);
        }
    }
}
