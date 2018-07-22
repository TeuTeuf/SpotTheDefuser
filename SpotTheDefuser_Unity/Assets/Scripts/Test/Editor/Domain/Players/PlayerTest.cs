using Main.Domain.Players;
using NUnit.Framework;

namespace Test.Editor.Domain.Players {
    [TestFixture]
    public class PlayerTest
    {
        [Test]
        public void NewPlayer_ShouldHaveAName()
        {
            // Given
            const string playerName = "Test";
            
            // When
            var player = new Player(playerName);

            // Then
            Assert.That(player.Name, Is.EqualTo(playerName));
        }

        [Test]
        public void NewPlayer_ShouldADefaultConstructorForNetworkSerialization()
        {
            // When
            var player = new Player();
            
            // Then
            Assert.IsNotNull(player);
        }
    }    
}
