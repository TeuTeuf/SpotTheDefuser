using Main.Domain.Players;
using Main.UseCases.Players;
using NSubstitute;
using NUnit.Framework;

namespace Test.Editor.UseCases.Players
{
    public class AddNewPlayerTest
    {
        [Test]
        public void ShouldAddPlayerToPlayerRepository()
        {
            // Given
            var playerRepository = Substitute.For<IPlayerRepository>();
            var addNewPlayer = new AddNewPlayer(playerRepository);
            var player = new Player("Test");

            // When
            addNewPlayer.Execute(player);

            // Then
            playerRepository
                .Received()
                .Add(player);
        }
    }
}
