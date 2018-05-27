using Main.Domain.Players;
using Main.Infrastructure.Players;
using NUnit.Framework;

namespace Test.Editor.Infrastructure.Players
{
    public class LocalPlayerRepositoryTest
    {
        [Test]
        public void GetAll_ShouldReturnEmptyListWhenNoPlayerAdded()
        {
            // Given
			var localPlayerRepository = new LocalPlayerRepository();

            // When 
            var players = localPlayerRepository.GetAll();

            // Then
            Assert.That(players, Is.Empty);
        }

        [Test]
        public void Add_ShouldAddPlayersToPlayersList()
        {
            // Given
            var localPlayerRepository = new LocalPlayerRepository();
            var player1 = new Player("Player1");
            var player2 = new Player("Player2");

            // When 
            localPlayerRepository.Add(player1);
            localPlayerRepository.Add(player2);

            // Then
            var players = localPlayerRepository.GetAll();
            Assert.That(players, Has.Exactly(1).EqualTo(player1));
            Assert.That(players, Has.Exactly(1).EqualTo(player2));
			Assert.That(players.Count, Is.EqualTo(2));
        }

        [Test]
        public void Remove_ShouldRemovePlayerFromPlayersList()
        {
            // Given
            var localPlayerRepository = new LocalPlayerRepository();

            var player1 = new Player("Player1");
			localPlayerRepository.Add(player1);

            var player2 = new Player("Player2");
			localPlayerRepository.Add(player2);

            // When 
            localPlayerRepository.Remove(player1);

            // Then
            var players = localPlayerRepository.GetAll();
            Assert.That(players, Has.Exactly(1).EqualTo(player2));
            Assert.That(players.Count, Is.EqualTo(1));
        }
    }
}
