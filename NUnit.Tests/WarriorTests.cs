namespace FruitWar.Tests
{
    using FruitWar.Piece.Fruits;
    using FruitWar.Piece.Warriors;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        [Test]
        public void TurtleGainsPowerWhenItEatsApple()
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
        public void TurtleEatingNullFruitThrowsArgumentNullException()
        {
            // Arrange
            var turtle = new Turtle('1');
            Apple apple = null;


            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                turtle.Eat(apple));
        }
    }
}
