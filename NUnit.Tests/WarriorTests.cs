namespace FruitWar.Tests
{
    using FruitWar.Common;
    using FruitWar.Piece.Fruits;
    using FruitWar.Piece.Warriors;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        [Test]
        public void WarriorGainsPowerWhenItEatsApple()
        {
            // Arrange
            var turtle = new Turtle('1');
            var apple = new Apple();
            var oldPower = turtle.Power;

            // Act
            turtle.Eat(apple);

            // Assert
            var newPower = turtle.Power;
            Assert.Greater(newPower, oldPower);
        }

        [Test]
        public void WarriorGainsSpeedWhenItEatsPear()
        {
            // Arrange
            var monkey = new Monkey('1');
            var pear = new Pear();
            var oldSpeed = monkey.Speed;

            // Act
            monkey.Eat(pear);

            // Assert
            var newSpeed = monkey.Speed;
            Assert.Greater(newSpeed, oldSpeed);
        }

        [Test]
        public void WarriorEatingNullFruitThrowsArgumentNullException()
        {
            // Arrange
            var turtle = new Turtle('1');
            Apple apple = null;


            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                turtle.Eat(apple));
        }

        [Test]
        public void WarriorMovingOutsideTheGameFieldThrowsIndexOutOfRangeException()
        {
            // Arrange
            var pigeon = new Pigeon('1')
            {
                Position = new Position(0, 0)
            };

            // Act and Assert
            Assert.Throws<IndexOutOfRangeException>(() =>
            pigeon.Move(Direction.Up));
        }

        [Test]
        public void WarriorMovingToValidPositionShouldChangePosition()
        {
            // Arrange
            var pigeon = new Pigeon('1')
            {
                Position = new Position(0, 0)
            };

            var expectedPosition = new Position(1, 0);

            // Act
            pigeon.Move(Direction.Down);

            // Assert
            Assert.AreEqual(expectedPosition, pigeon.Position);
        }
    }
}
