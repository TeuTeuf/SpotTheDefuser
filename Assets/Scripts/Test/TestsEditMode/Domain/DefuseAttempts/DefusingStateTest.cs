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
        private IDefusingTime _defusingTime;
        
        private DefusingState _defusingState;

        [SetUp]
        public void Init()
        {
            _defusingTime = Substitute.For<IDefusingTime>();
            
            _currentDefuseAttempt = Substitute.For<DefuseAttempt>(
                Substitute.For<IRandom>(),
                new DefuserCounter(),
                Substitute.For<AllBombs>(Substitute.For<IRandom>(), new IBomb[1]),
                new List<Player>().AsReadOnly()
                );
            _defusingState = new DefusingState(_defusingTime);
        }

        [Test]
        public void SetNewDefuseAttempt_ShouldSetCurrentDefuseAttempt()
        {
            // When
            _defusingState.SetNewDefuseAttempt(_currentDefuseAttempt);

            // Then
            Assert.That(_defusingState.CurrentDefuseAttempt, Is.EqualTo(_currentDefuseAttempt));
        }

        [Test]
        public void SetNewDefuseAttempt_ShouldIncrementTimerWhenSettingANewDefuseAttempt()
        {
            // Given
            const int timeToDefuse = 25;
            _currentDefuseAttempt.TimeToDefuse.Returns(timeToDefuse);

            // When
            _defusingState.SetNewDefuseAttempt(_currentDefuseAttempt);

            // Then
            Assert.That(_defusingState.RemainingTime, Is.EqualTo(timeToDefuse));
        }
        
        [Test]
        public void SetNewDefuseAttempt_ShouldIncrementTimerWhenSettingASecondNewDefuseAttempt()
        {
            // Given
            const int firstTimeToDefuse = 25;
            const int secondTimeToDefuse = 10;
            
            _currentDefuseAttempt.TimeToDefuse.Returns(firstTimeToDefuse);
            _defusingState.SetNewDefuseAttempt(_currentDefuseAttempt);

            _currentDefuseAttempt.TimeToDefuse.Returns(secondTimeToDefuse);

            // When
            _defusingState.SetNewDefuseAttempt(_currentDefuseAttempt);

            // Then
            Assert.That(_defusingState.RemainingTime, Is.EqualTo(firstTimeToDefuse + secondTimeToDefuse));
        }

        [Test]
        public void IsCurrentAttemptDefuser_ShouldReturnFalse_WhenCurrentDefuseAttemptReturnFalse()
        {
            // Given
            var player = new Player("Player Name");
            _currentDefuseAttempt.IsDefuser(player).Returns(false);
            _defusingState.SetNewDefuseAttempt(_currentDefuseAttempt);

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
            _defusingState.SetNewDefuseAttempt(_currentDefuseAttempt);

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

        [Test]
        public void Tick_ShouldDecrementRemaingTimeOfDeltaTime()
        {
            // Given
            var deltaTime = 5.5f;
            _defusingTime.GetDeltaTime().Returns(deltaTime);

            var timeToDefuse = 10;
            _currentDefuseAttempt.TimeToDefuse.Returns(timeToDefuse);
            _defusingState.SetNewDefuseAttempt(_currentDefuseAttempt);
            
            _defusingState.TimerEnabled = true;

            // When
            _defusingState.Tick();

            // Then
            Assert.That(_defusingState.RemainingTime, Is.EqualTo(timeToDefuse - deltaTime));
        }
        
        [Test]
        public void Tick_ShouldNOTDecrementRemaingTime_WhenTimerIsNotEnable()
        {
            // Given
            var deltaTime = 5.5f;
            _defusingTime.GetDeltaTime().Returns(deltaTime);

            var timeToDefuse = 10;
            _currentDefuseAttempt.TimeToDefuse.Returns(timeToDefuse);
            _defusingState.SetNewDefuseAttempt(_currentDefuseAttempt);

            _defusingState.TimerEnabled = false;

            // When
            _defusingState.Tick();

            // Then
            Assert.That(_defusingState.RemainingTime, Is.EqualTo(timeToDefuse));
        }
    }
}