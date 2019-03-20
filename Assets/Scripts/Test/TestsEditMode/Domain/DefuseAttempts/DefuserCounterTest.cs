using Main.Domain.DefuseAttempts;
using NUnit.Framework;

namespace Test.TestsEditMode.Domain.DefuseAttempts
{
    [TestFixture]
    public class DefuserCounterTest
    {
        [Test]
        public void GetNumberOfBombPlayers_ShouldReturn3BombsFor5Players()
        {
            // Given
            var defuserCounter = new DefuserCounter();

            // When
            var numberOfBombs = defuserCounter.GetNumberOfExplosivePlayers(5);

            // Then
            Assert.That(numberOfBombs, Is.EqualTo(3));
        }
    }
}