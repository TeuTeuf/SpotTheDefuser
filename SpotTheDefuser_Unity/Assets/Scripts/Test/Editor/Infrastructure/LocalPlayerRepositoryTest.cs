using System.Collections.Generic;
using NUnit.Framework;
using SpotTheDefuser_Unity.Assets.Scripts.Main.Domain;
using SpotTheDefuser_Unity.Assets.Scripts.Main.Infrastructure;

namespace SpotTheDefuser_Unity.Editor.Assets.Scripts.Test.Editor.Infrastructure
{
    public class LocalPlayerRepositoryTest
    {
        [Test]
        public void Get_ShouldReturnEmptyListWhenNoPlayerAdded()
        {
            // Given
			LocalPlayerRepository localPlayerRepository = new LocalPlayerRepository();

            // When 
            IReadOnlyCollection<Player> players = localPlayerRepository.GetAll();

            // Then
            Assert.That(players, Is.Empty);
        }

        [Test]
        public void Add_ShouldAddPlayersToPlayersList()
        {
            // Given
            LocalPlayerRepository localPlayerRepository = new LocalPlayerRepository();
            Player player1 = new Player("Player1");
            Player player2 = new Player("Player2");

            // When 
            localPlayerRepository.Add(player1);
            localPlayerRepository.Add(player2);

            // Then
            IReadOnlyCollection<Player> players = localPlayerRepository.GetAll();
            Assert.That(players, Has.Exactly(1).EqualTo(player1));
            Assert.That(players, Has.Exactly(1).EqualTo(player2));
			Assert.That(players.Count, Is.EqualTo(2));
        }
    }
}
