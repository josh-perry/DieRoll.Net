using DieRoll.Net;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DieRollerTests
    {
        [Test]
        public void Roll_1d6_1to6()
        {
            // Arrange
            var roller = new DieRoller();

            // Act
            var result = roller.Roll("1d6");

            // Assert
            Assert.IsTrue(result >= 1);
            Assert.IsTrue(result <= 6);
        }

        [Test]
        public void Roll_2d6_2to12()
        {
            // Arrange
            var roller = new DieRoller();

            // Act
            var result = roller.Roll("2d6");

            // Assert
            Assert.IsTrue(result >= 2);
            Assert.IsTrue(result <= 12);
        }

        [Test]
        public void Roll_1d6plus1_2to7()
        {
            // Arrange
            var roller = new DieRoller();

            // Act
            var result = roller.Roll("1d6+1");

            // Assert
            Assert.IsTrue(result >= 2);
            Assert.IsTrue(result <= 7);
        }

        [Test]
        public void Roll_1d1plus10_11()
        {
            // Arrange
            var roller = new DieRoller();

            // Act
            var result = roller.Roll("1d1+10");

            // Assert
            Assert.IsTrue(result == 11);
        }
    }
}