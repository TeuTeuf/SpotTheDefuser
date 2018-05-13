using System.Collections.Generic;
using Main.Domain;
using Main.Domain.Players;
using NSubstitute;
using NUnit.Framework;

namespace Test.Editor.Domain
{
    public class DefuseAttemptTest
    {
        private readonly Player _player1 = new Player("player1");
        private readonly Player _player2 = new Player("player2");
        private readonly Player _player3 = new Player("player3");

        private IList<Player> _listPlayer;
        private IRandom _random;

        [SetUp]
        public void Setup()
        {
            _listPlayer = new List<Player> {_player1, _player2, _player3};
            _random = Substitute.For<IRandom>();
        }

        [Test]
        public void IsDefuser_ShouldReturnTrueIfRandomValueIsEqualToPlayerIndex()
        {
            _random.Range(0, _listPlayer.Count)
                  .Returns(0);

            var defuseAttempt = new DefuseAttempt(_random, _listPlayer);

            // When
            var isPlayer1Defuser = defuseAttempt.IsDefuser(_player1);

            // Then
            Assert.That(isPlayer1Defuser, Is.True);
        }

        [Test]
        public void IsDefuser_ShouldReturnFalseIfRandomValueIsNotEqualToPlayerIndex()
        {
            _random.Range(0, _listPlayer.Count)
                  .Returns(1);

            var defuseAttempt = new DefuseAttempt(_random, _listPlayer);

            // When
            var isPlayer1Defuser = defuseAttempt.IsDefuser(_player1);

            // Then
            Assert.That(isPlayer1Defuser, Is.False);
        }
    }
}
