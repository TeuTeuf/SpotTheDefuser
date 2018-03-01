using NUnit.Framework;
using SpotTheDefuser_Unity.Assets.Scripts.Main.Domain;

namespace SpotTheDefuser_Unity.Editor.Assets.Scripts.Test.Editor.Domain {
    public class PlayerTest
    {

        [Test]
        public void ShouldHaveAPseudo()
        {
            // Given
            // When
            Player player = new Player("Test");

            // Then
            Assert.That(player.pseudo, Is.EqualTo("Test"));
        }
    }    
}
