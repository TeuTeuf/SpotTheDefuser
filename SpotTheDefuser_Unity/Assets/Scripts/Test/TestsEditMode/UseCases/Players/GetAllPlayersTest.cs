using System.Collections.Generic;
using Main.Domain.Players;
using Main.UseCases.Players;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.UseCases.Players
{
    [TestFixture]
    public class GetAllPlayersTest
    {
        [Test]
        public void Get_ShouldReturnAllPlayersFromRepository()
        {
            // Given
            var allPlayers = Substitute.For<AllPlayers>();
            var getAllPlayers = new GetAllPlayers(allPlayers);

            var playersInRepository = new List<Player>();
            var player1 = new Player("Test1");
            playersInRepository.Add(player1);
            var player2 = new Player("Test2");
			playersInRepository.Add(player2);

            allPlayers.GetAll()
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
