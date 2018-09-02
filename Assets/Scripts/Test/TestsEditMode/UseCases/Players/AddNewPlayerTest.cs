using Main.Domain.Players;
using Main.UseCases.Players;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.UseCases.Players
{
    [TestFixture]
    public class AddNewPlayerTest
    {
        [Test]
        public void Execute_ShouldAddPlayerToAllPlayers()
        {
            // Given
            var allPlayers = Substitute.For<AllPlayers>();
            var addNewPlayer = new AddNewPlayer(allPlayers);
            var player = new Player("Test");

            // When
            addNewPlayer.Execute(player);

            // Then
            allPlayers
                .Received()
                .Add(player);
        }
    }
}
