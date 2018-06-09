using Main.Domain.Players;
using NUnit.Framework;

namespace Test.Editor.Domain.Players
{
    public class PlayerRepositoryTest
    {
        [Test]
        public void GetAll_ShouldReturnEmptyListWhenNoPlayerAdded()
        {
            // Given
			var localPlayerRepository = new PlayerRepository();

            // When 
            var players = localPlayerRepository.GetAll();

            // Then
            Assert.That(players, Is.Empty);
        }

        [Test]
        public void Add_ShouldAddPlayersToPlayersList()
        {
            // Given
            var localPlayerRepository = new PlayerRepository();
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
            var localPlayerRepository = new PlayerRepository();

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
