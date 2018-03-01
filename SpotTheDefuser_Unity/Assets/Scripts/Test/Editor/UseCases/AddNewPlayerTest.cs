using NSubstitute;
using NUnit.Framework;
using SpotTheDefuser_Unity.Assets.Scripts.Main.Domain;
using SpotTheDefuser_Unity.Assets.Scripts.Main.UseCases;

namespace SpotTheDefuser_Unity.Editor.Assets.Scripts.Test.Editor.UseCases
{
    public class AddNewPlayerTest
    {
        [Test]
        public void ShouldAddPlayerToPlayerRepository()
        {
            // Given
            IPlayerRepository playerRepository = Substitute.For<IPlayerRepository>();
            AddNewPlayer addNewPlayer = new AddNewPlayer(playerRepository);
            Player player = new Player("Test");

            // When
            addNewPlayer.Execute(player);

            // Then
            playerRepository
                .Received()
                .Add(player);
        }
    }
}
