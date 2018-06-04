using Main.Domain.Players;
using NUnit.Framework;

namespace Test.Editor.Domain.Players {
    [TestFixture]
    public class PlayerTest
    {
        [Test]
        public void ShouldHaveAPseudo()
        {
            // Given
            // When
            var player = new Player("Test");

            // Then
            Assert.That(player.Pseudo, Is.EqualTo("Test"));
        }
    }    
}
