using System.Collections.Generic;
using Main.Domain;
using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.Domain.DefuseAttempts
{
    [TestFixture]
    public class DefuseAttemptTest
    {
        private readonly Player _player1 = new Player("Player");
        private readonly Player _player2 = new Player("Player");
        private readonly Player _player3 = new Player("Player");
        private readonly Player _player4 = new Player("Player");
        private readonly Player _player5 = new Player("Player");
        private readonly Player _player6 = new Player("Player");

        private IRandom _random;
        private DefuserCounter _defuserCounter;
        
        [SetUp]
        public void Setup()
        {
            _random = Substitute.For<IRandom>();
            _defuserCounter = new DefuserCounter();
        }

        [Test]
        public void
            Execute_ShouldReturnDefuseAttemptReturningIsDefuserTrueForFirstPlayerAndFalseForOthers_WhenRandomReturnZero_WithThreePlayers()
        {
            // Given
            var players = new List<Player> {_player1, _player2, _player3}.AsReadOnly();

            _random.Range(Arg.Any<int>(), Arg.Any<int>())
                .Returns(0);

            // When
            var defuseAttempt = new DefuseAttempt(_random, _defuserCounter, players);

            // Then
            Assert.IsTrue(defuseAttempt.IsDefuser(_player1));
            Assert.IsFalse(defuseAttempt.IsDefuser(_player2));
            Assert.IsFalse(defuseAttempt.IsDefuser(_player3));
        }

        [Test]
        public void
        Execute_ShouldReturnDefuseAttemptReturningIsDefuserTrueForLastPlayerAndFalseForOthers_WhenRandomReturnMaxValue_WithThreePlayers()
        {
            // Given
            var players = new List<Player> {_player1, _player2, _player3}.AsReadOnly();

            _random.Range(Arg.Any<int>(), Arg.Any<int>())
                .Returns(players.Count - 1);

            // When
            var defuseAttempt = new DefuseAttempt(_random, _defuserCounter, players);

            // Then
            Assert.IsFalse(defuseAttempt.IsDefuser(_player1));
            Assert.IsFalse(defuseAttempt.IsDefuser(_player2));
            Assert.IsTrue(defuseAttempt.IsDefuser(_player3));
        }


        [Test]
        public void
            Execute_ShouldReturnDefuseAttemptReturningIsDefuserTrueForTwoFirstPlayersAndFalseForOthers_WhenRandomReturnAlwaysZero_WithFivePlayers()
        {
            // Given
            var players = new List<Player> {_player1, _player2, _player3, _player4, _player5}.AsReadOnly();

            _random.Range(Arg.Any<int>(), Arg.Any<int>())
                .Returns(0);

            // When
            var defuseAttempt = new DefuseAttempt(_random, _defuserCounter, players);

            // Then
            Assert.IsTrue(defuseAttempt.IsDefuser(_player1));
            Assert.IsTrue(defuseAttempt.IsDefuser(_player2));
            Assert.IsFalse(defuseAttempt.IsDefuser(_player3));
            Assert.IsFalse(defuseAttempt.IsDefuser(_player4));
            Assert.IsFalse(defuseAttempt.IsDefuser(_player5));
        }


        [Test]
        public void
            Execute_ShouldReturnDefuseAttemptReturningIsDefuserTrueForTwoLastPlayersAndFalseForOthers_WhenRandomReturnMaxValue_WithSixPlayers()
        {
            // Given
            var players = new List<Player> {_player1, _player2, _player3, _player4, _player5, _player6}.AsReadOnly();

            _random.Range(0, 6).Returns(5);
            _random.Range(0, 5).Returns(4);

            // When
            var defuseAttempt = new DefuseAttempt(_random, _defuserCounter, players);

            // Then
            Assert.IsFalse(defuseAttempt.IsDefuser(_player1));
            Assert.IsFalse(defuseAttempt.IsDefuser(_player2));
            Assert.IsFalse(defuseAttempt.IsDefuser(_player3));
            Assert.IsFalse(defuseAttempt.IsDefuser(_player4));
            Assert.IsTrue(defuseAttempt.IsDefuser(_player5));
            Assert.IsTrue(defuseAttempt.IsDefuser(_player6));
        }
    }
}