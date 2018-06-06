using System.Collections.Generic;
using Main.Domain;
using Main.Domain.Players;
using Main.UseCases.DefuseAttempts;
using NSubstitute;
using NUnit.Framework;

namespace Test.Editor.UseCases.DefuseAttempts
{
    [TestFixture]
    public class CreateDefuseAttemptTest {
        
        private readonly Player _player1 = new Player("player1");
        private readonly Player _player2 = new Player("player2");
        private readonly Player _player3 = new Player("player3");
        private readonly Player _player4 = new Player("player4");
        private readonly Player _player5 = new Player("player5");
        private readonly Player _player6 = new Player("player6");

        private CreateDefuseAttempt _createDefuseAttempt;
        private IRandom _random;
        private IPlayerRepository _playerRepository;

        [SetUp]
        public void Setup()
        {
            _random = Substitute.For<IRandom>();
            _playerRepository = Substitute.For<IPlayerRepository>();
            _createDefuseAttempt = new CreateDefuseAttempt(_random, _playerRepository);
        }        

        [Test]
        public void Execute_ShouldReturnDefuseAttemptReturningIsDefuserTrueForFirstPlayerAndFalseForOthers_WhenRandomReturnZero_WithThreePlayers()
        {
            // Given
            var players = new List<Player> {_player1, _player2, _player3};

            _playerRepository.GetAll()
                .Returns(players.AsReadOnly());
            
            _random.Range(Arg.Any<int>(), Arg.Any<int>())
                .Returns(0);

            // When
            var defuseAttempt = _createDefuseAttempt.Execute();

            // Then
            Assert.IsTrue(defuseAttempt.IsDefuser(_player1));
            Assert.IsFalse(defuseAttempt.IsDefuser(_player2));
            Assert.IsFalse(defuseAttempt.IsDefuser(_player3));
        }
        
        [Test]
        public void Execute_ShouldReturnDefuseAttemptReturningIsDefuserTrueForLastPlayerAndFalseForOthers_WhenRandomReturnMaxValue_WithThreePlayers()
        {
            // Given
            var players = new List<Player> {_player1, _player2, _player3};
            
            _playerRepository.GetAll()
                .Returns(players.AsReadOnly());
            
            _random.Range(Arg.Any<int>(), Arg.Any<int>())
                .Returns(players.Count - 1);

            // When
            var defuseAttempt = _createDefuseAttempt.Execute();

            // Then
            Assert.IsFalse(defuseAttempt.IsDefuser(_player1));
            Assert.IsFalse(defuseAttempt.IsDefuser(_player2));
            Assert.IsTrue(defuseAttempt.IsDefuser(_player3));
        }
        
        [Test]
        public void Execute_ShouldReturnDefuseAttemptReturningIsDefuserTrueForTwoFirstPlayersAndFalseForOthers_WhenRandomReturnAlwaysZero_WithFivePlayers()
        {
            // Given
            var players = new List<Player> {_player1, _player2, _player3, _player4, _player5};

            _playerRepository.GetAll()
                .Returns(players.AsReadOnly());

            _random.Range(Arg.Any<int>(), Arg.Any<int>())
                .Returns(0);

            // When
            var defuseAttempt = _createDefuseAttempt.Execute();
            
            // Then
            Assert.IsTrue(defuseAttempt.IsDefuser(_player1));
            Assert.IsTrue(defuseAttempt.IsDefuser(_player2));
            Assert.IsFalse(defuseAttempt.IsDefuser(_player3));
            Assert.IsFalse(defuseAttempt.IsDefuser(_player4));
            Assert.IsFalse(defuseAttempt.IsDefuser(_player5));
        }
        
        [Test]
        public void Execute_ShouldReturnDefuseAttemptReturningIsDefuserTrueForTwoLastPlayersAndFalseForOthers_WhenRandomReturnMaxValue_WithSixPlayers()
        {
            // Given
            var players = new List<Player> {_player1, _player2, _player3, _player4, _player5, _player6};

            _playerRepository.GetAll()
                .Returns(players.AsReadOnly());
            
            _random.Range(0, 6).Returns(5);
            _random.Range(0, 5).Returns(4);
            
            // When
            var defuseAttempt = _createDefuseAttempt.Execute();
            
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
