using Main.Domain.Players;
using NUnit.Framework;

namespace Test.TestsEditMode.Domain.Players
{
    public class AllPlayersTest
    {
        
        [Test]
        public void GetAll_ShouldReturnEmptyListWhenNoPlayerAdded()
        {
            // Given
			var allPlayers = new AllPlayers();

            // When 
            var players = allPlayers.GetAll();

            // Then
            Assert.That(players, Is.Empty);
        }

        [Test]
        public void Add_ShouldAddPlayersToPlayersList()
        {
            // Given
            var allPlayers = new AllPlayers();
            var player1 = new Player("Player1");
            var player2 = new Player("Player2");

            // When 
            allPlayers.Add(player1);
            allPlayers.Add(player2);

            // Then
            var players = allPlayers.GetAll();
            Assert.That(players, Has.Exactly(1).EqualTo(player1));
            Assert.That(players, Has.Exactly(1).EqualTo(player2));
			Assert.That(players.Count, Is.EqualTo(2));
        }

        [Test]
        public void Remove_ShouldRemovePlayerFromPlayersList()
        {
            // Given
            var allPlayers = new AllPlayers();

            var player1 = new Player("Player1");
			allPlayers.Add(player1);

            var player2 = new Player("Player2");
			allPlayers.Add(player2);

            // When 
            allPlayers.Remove(player1);

            // Then
            var players = allPlayers.GetAll();
            Assert.That(players, Has.Exactly(1).EqualTo(player2));
            Assert.That(players.Count, Is.EqualTo(1));
        }
    }
}
