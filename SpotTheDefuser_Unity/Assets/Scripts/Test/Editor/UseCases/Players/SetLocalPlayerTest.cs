using Main.Domain.Players;
using Main.UseCases.Players;
using NSubstitute;
using NUnit.Framework;

namespace Test.Editor.UseCases.Players
{
    [TestFixture]
    public class SetLocalPlayerTest
    {
        [Test]
        public void Set_ShouldSetPlayerAsLocalPlayerInPlayerRepository()
        {
            // Given
            var player = new Player("Test");
            var playerRepository = Substitute.For<PlayerRepository>();
            var setLocalPlayer = new SetLocalPlayer(playerRepository);

            // When
            setLocalPlayer.Set(player);

            // Then
            playerRepository.Received()
                .SetLocalPlayer(player);
        }
    }
}