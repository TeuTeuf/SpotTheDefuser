using System.Collections.Generic;
using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using NUnit.Framework;

namespace Test.Editor.Domain.DefuseAttempts
{
    [TestFixture]
    public class DefuseAttemptTest
    {
        private readonly Player _player1 = new Player("player1");
        private readonly Player _player2 = new Player("player2");
        private readonly Player _player3 = new Player("player3");

        [Test]
        public void IsDefuser_ShouldReturnTrueWithDefuserPlayer()
        {
            // Given
            var defuserPlayers = new List<Player> {_player1, _player2};
            var defuseAttempt = new DefuseAttempt(defuserPlayers);
            
            // When
            var isDefuser = defuseAttempt.IsDefuser(_player1);
            
            // Then
            Assert.IsTrue(isDefuser);
        }

        [Test]
        public void IsDefuser_ShouldReturnFalseWithNotDefuserPlayer()
        {
            // Given
            var defuserPlayers = new List<Player> {_player1, _player2};
            var defuseAttempt = new DefuseAttempt(defuserPlayers);
            
            // When
            var isDefuser = defuseAttempt.IsDefuser(_player3);
            
            // Then
            Assert.IsFalse(isDefuser);
        }
    }
}
