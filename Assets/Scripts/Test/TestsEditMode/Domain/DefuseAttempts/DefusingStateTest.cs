using System.Collections.Generic;
using Main.Domain;
using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.Domain.DefuseAttempts
{
    [TestFixture]
    public class DefusingStateTest
    {
        private DefuseAttempt _currentDefuseAttempt;
        private DefusingState _defusingState;
        
        [SetUp]
        public void Init()
        {
            _currentDefuseAttempt = Substitute.For<DefuseAttempt>(
                Substitute.For<IRandom>(),
                new DefuserCounter(),
                Substitute.For<AllBombs>(Substitute.For<IRandom>(), new IBomb[1]),
                new List<Player>().AsReadOnly()
                );
            _defusingState = new DefusingState {CurrentDefuseAttempt = _currentDefuseAttempt};
        }

        [Test]
        public void IsCurrentAttemptDefuser_ShouldReturnFalse_WhenCurrentDefuseAttemptReturnFalse()
        {
            // Given
            var player = new Player("Player Name");
            _currentDefuseAttempt.IsDefuser(player).Returns(false);

            // When
            var isCurrentAttemptDefuser = _defusingState.IsCurrentAttemptDefuser(player);

            // Then
            Assert.IsFalse(isCurrentAttemptDefuser);
        }

        [Test]
        public void IsCurrentAttemptDefuser_ShouldReturnTrue_WhenCurrentDefuseAttemptReturnTrue()
        {
            // Given
            var player = new Player("Player Name");
            _currentDefuseAttempt.IsDefuser(player).Returns(true);

            // When
            var isCurrentAttemptDefuser = _defusingState.IsCurrentAttemptDefuser(player);

            // Then
            Assert.IsTrue(isCurrentAttemptDefuser);
        }

        [Test]
        public void IncrementBombsDefused_ShouldIncrementNumberOfBombsDefused()
        {
            // When
            _defusingState.IncrementBombsDefused();

            // Then
            Assert.That(_defusingState.NbBombsDefused, Is.EqualTo(1));
            
            // When
            _defusingState.IncrementBombsDefused();

            // Then
            Assert.That(_defusingState.NbBombsDefused, Is.EqualTo(2));
        }
    }
}