using Main.Domain.Players;
using Main.UseCases.Players;
using NSubstitute;
using NUnit.Framework;

namespace Test.Editor.UseCases.Players
{
    [TestFixture]
    public class AddNewPlayerTest
    {
        [Test]
        public void Execute_ShouldAddPlayerToAllPlayers()
        {
            // Given
            var AllPlayers = Substitute.For<AllPlayers>();
            var addNewPlayer = new AddNewPlayer(AllPlayers);
            var player = new Player("Test");

            // When
            addNewPlayer.Execute(player);

            // Then
            AllPlayers
                .Received()
                .Add(player);
        }
    }
}
