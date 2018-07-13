using Main.Domain.Players;
using Main.UseCases.Players;
using NSubstitute;
using NUnit.Framework;

namespace Test.Editor.UseCases.Players
{
    [TestFixture]
    public class RemovePlayerTest
    {
        [Test]
        public void Execute_ShouldRemovePlayerFromAllPlayers()
        {
            // Given
            var allPlayers = Substitute.For<AllPlayers>();
            var removePlayer = new RemovePlayer(allPlayers);
            var player = new Player("Test");

            // When
            removePlayer.Execute(player);

            // Then
            allPlayers
                .Received()
                .Remove(player);
        }
    }
}
