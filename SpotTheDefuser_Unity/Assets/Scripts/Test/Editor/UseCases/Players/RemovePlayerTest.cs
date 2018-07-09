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
        public void Execute_ShouldRemovePlayerFromPlayerRepository()
        {
            // Given
            var playerRepository = Substitute.For<PlayerRepository>();
            var removePlayer = new RemovePlayer(playerRepository);
            var player = new Player("Test");

            // When
            removePlayer.Execute(player);

            // Then
            playerRepository
                .Received()
                .Remove(player);
        }
    }
}
