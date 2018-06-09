using System;
using Boo.Lang.Runtime;
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
			var playerRepository = new PlayerRepository();

            // When 
            var players = playerRepository.GetAll();

            // Then
            Assert.That(players, Is.Empty);
        }

        [Test]
        public void Add_ShouldAddPlayersToPlayersList()
        {
            // Given
            var playerRepository = new PlayerRepository();
            var player1 = new Player("Player1");
            var player2 = new Player("Player2");

            // When 
            playerRepository.Add(player1);
            playerRepository.Add(player2);

            // Then
            var players = playerRepository.GetAll();
            Assert.That(players, Has.Exactly(1).EqualTo(player1));
            Assert.That(players, Has.Exactly(1).EqualTo(player2));
			Assert.That(players.Count, Is.EqualTo(2));
        }

        [Test]
        public void Remove_ShouldRemovePlayerFromPlayersList()
        {
            // Given
            var playerRepository = new PlayerRepository();

            var player1 = new Player("Player1");
			playerRepository.Add(player1);

            var player2 = new Player("Player2");
			playerRepository.Add(player2);

            // When 
            playerRepository.Remove(player1);

            // Then
            var players = playerRepository.GetAll();
            Assert.That(players, Has.Exactly(1).EqualTo(player2));
            Assert.That(players.Count, Is.EqualTo(1));
        }

        [Test]
        public void SetLocalPlayer_ShouldSetLocalPlayer()
        {
            // Given
            var playerRepository = new PlayerRepository();

            var player1 = new Player("Player1");
            
            // When
            playerRepository.SetLocalPlayer(player1);

            // Then
            Assert.AreEqual(playerRepository.LocalPlayer, player1);
        }
        
        

        [Test]
        public void SetLocalPlayer_ShouldThrowExceptionIfPlayerAlreadySet()
        {
            // Given
            var playerRepository = new PlayerRepository();

            var player1 = new Player("Player1");
            var player2 = new Player("Player2");
            
            playerRepository.SetLocalPlayer(player1);
            
            // When
            var exception = Assert.Throws<LocalPlayerAlreadySetException>(() => playerRepository.SetLocalPlayer(player2));

            // Then
            var expectedException = new LocalPlayerAlreadySetException();
            Assert.AreEqual(exception.Message, expectedException.Message);
        }
    }
}
