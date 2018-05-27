using System.Collections.Generic;
using Main.Domain.Players;
using Main.UseCases.Players;
using NSubstitute;
using NUnit.Framework;

namespace Test.Editor.UseCases.Players
{
    public class GetAllPlayersTest
    {
        [Test]
        public void ShouldRemovePlayerFromPlayerRepository()
        {
            // Given
            var playerRepository = Substitute.For<IPlayerRepository>();
            var getAllPlayers = new GetAllPlayers(playerRepository);

            var playersInRepository = new List<Player>();
            var player1 = new Player("Test1");
            playersInRepository.Add(player1);
            var player2 = new Player("Test2");
			playersInRepository.Add(player2);

            playerRepository.GetAll()
                            .Returns(playersInRepository.AsReadOnly());


            // When
            var players = getAllPlayers.Get();

            // Then
            Assert.That(players, Has.Exactly(1).EqualTo(player1));
            Assert.That(players, Has.Exactly(1).EqualTo(player2));
            Assert.That(players.Count, Is.EqualTo(2));
        }
    }
}
