using NSubstitute;
using NUnit.Framework;
using SpotTheDefuser_Unity.Assets.Scripts.Main.Domain;
using SpotTheDefuser_Unity.Assets.Scripts.Main.UseCases;

namespace SpotTheDefuser_Unity.Editor.Assets.Scripts.Test.Editor.UseCases
{
    public class RemovePlayerTest
    {
        [Test]
        public void ShouldRemovePlayerFromPlayerRepository()
        {
            // Given
            IPlayersRepository playerRepository = Substitute.For<IPlayersRepository>();
            RemovePlayer removePlayer = new RemovePlayer(playerRepository);
            Player player = new Player("Test");

            // When
            removePlayer.execute(player);

            // Then
            playerRepository
                .Received()
                .Remove(player);
        }
    }
}
