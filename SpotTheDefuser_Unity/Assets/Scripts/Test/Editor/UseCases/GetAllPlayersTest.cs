using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NSubstitute;
using NUnit.Framework;
using SpotTheDefuser_Unity.Assets.Scripts.Main.Domain;
using SpotTheDefuser_Unity.Assets.Scripts.Main.UseCases;

namespace SpotTheDefuser_Unity.Editor.Assets.Scripts.Test.Editor.UseCases
{
    public class GetAllPlayersTest
    {
        [Test]
        public void ShouldRemovePlayerFromPlayerRepository()
        {
            // Given
            IPlayerRepository playerRepository = Substitute.For<IPlayerRepository>();
            GetAllPlayers getAllPlayers = new GetAllPlayers(playerRepository);

            List<Player> playersInRepository = new List<Player>();
            Player player1 = new Player("Test1");
            playersInRepository.Add(player1);
            Player player2 = new Player("Test2");
			playersInRepository.Add(player2);

            playerRepository.GetAll()
                            .Returns(playersInRepository.AsReadOnly());


            // When
            ReadOnlyCollection<Player> players = getAllPlayers.Get();

            // Then
            Assert.That(players, Has.Exactly(1).EqualTo(player1));
            Assert.That(players, Has.Exactly(1).EqualTo(player2));
            Assert.That(players.Count, Is.EqualTo(2));
        }
    }
}
