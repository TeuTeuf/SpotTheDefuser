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
        private IDefusingTimerUpdatedListener _defusingTimerUpdatedListener;
        private IDefuseFailedListener _defuseFailedListener;

        private DefusingState _defusingState;

        [SetUp]
        public void Init()
        {
            _defusingTime = Substitute.For<IDefusingTime>();
            _defusingTimerUpdatedListener = Substitute.For<IDefusingTimerUpdatedListener>();
            _defuseFailedListener = Substitute.For<IDefuseFailedListener>();
            
            _currentDefuseAttempt = Substitute.For<DefuseAttempt>(
                Substitute.For<IRandom>(),
                new DefuserCounter(),
                Substitute.For<AllBombs>(
                    Substitute.For<IRandom>(), 
                    new IBomb[0],
                    Substitute.For<IDeviceInfo>()),
                new List<Player>().AsReadOnly(),
                0
            );
            
            _defusingState = new DefusingState(_defusingTime, _defusingTimerUpdatedListener, _defuseFailedListener);
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
        public void SetNewDefuseAttempt_ShouldTriggerTimerListener()
        {
            // Given
            const int timeToDefuse = 25;
            _currentDefuseAttempt.TimeToDefuse.Returns(timeToDefuse);

            // When
            _defusingState.SetNewDefuseAttempt(_currentDefuseAttempt);

            // Then
            _defusingTimerUpdatedListener
                .Received()
                .OnDefusingTimerUpdated(timeToDefuse);
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
            
            _defusingTimerUpdatedListener
                .Received()
                .OnDefusingTimerUpdated(firstTimeToDefuse + secondTimeToDefuse);
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
            
            _defusingState.StartNewTimer();

            var timeBeforeTick = _defusingState.RemainingTime;

            // When
            _defusingState.Tick();

            // Then
            Assert.That(_defusingState.RemainingTime, Is.EqualTo(timeBeforeTick - deltaTime));
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

            var timeBeforeTick = _defusingState.RemainingTime;

            // When
            _defusingState.Tick();

            // Then
            Assert.That(_defusingState.RemainingTime, Is.EqualTo(timeBeforeTick));
        }

        [Test]
        public void Tick_ShouldNOTTriggerFailListenerNeitherDisableTimer_WhenTimerAboveZero()
        {
            // Given
            _defusingState.StartNewTimer();
            _defusingTime.GetDeltaTime().Returns(DefusingState.STARTING_DEFUSING_TIME / 2);

            // When
            _defusingState.Tick();

            // Then
            _defuseFailedListener
                .DidNotReceive()
                .OnDefuseFailed(Arg.Any<int>());
            
            Assert.That(_defusingState.TimerEnabled, Is.True);
        }

        [Test]
        public void Tick_ShouldTriggerFailingListener_WhenTimerReachingZero()
        {
            // Given
            _defusingState.StartNewTimer();
            _defusingState.IncrementBombsDefused();
            _defusingTime.GetDeltaTime().Returns(DefusingState.STARTING_DEFUSING_TIME + 1);

            // When
            _defusingState.Tick();

            // Then
            _defuseFailedListener
                .Received()
                .OnDefuseFailed(1);
        }
        
        [Test]
        public void Tick_ShouldDisableTimer_WhenTimerReachingZero()
        {
            // Given
            _defusingState.StartNewTimer();
            _defusingTime.GetDeltaTime().Returns(DefusingState.STARTING_DEFUSING_TIME + 1);

            // When
            _defusingState.Tick();

            // Then
            Assert.That(_defusingState.TimerEnabled, Is.False);
        }
        
        [Test]
        public void Tick_ShouldNotCallFailListenerMoreThanOnce_WhenTimerReachingZero()
        {
            // Given
            _defusingState.StartNewTimer();
            _defusingState.IncrementBombsDefused();
            _defusingTime.GetDeltaTime().Returns(DefusingState.STARTING_DEFUSING_TIME + 1);

            // When
            _defusingState.Tick();
            _defusingState.Tick();

            // Then
            _defuseFailedListener
                .Received(1)
                .OnDefuseFailed(1);
        }

        [Test]
        public void StartNewTimer_ShouldEnableTimer()
        {
            // When
            _defusingState.StartNewTimer();

            // Then
            Assert.That(_defusingState.TimerEnabled, Is.True);
        }
        
        [Test]
        public void StartNewTimer_ShouldSetTimerWithDefaultValue()
        {
            // When
            _defusingState.StartNewTimer();

            // Then
            Assert.That(_defusingState.RemainingTime, Is.EqualTo(DefusingState.STARTING_DEFUSING_TIME));
        }
        
        [Test]
        public void StartNewTimer_ShouldCallTimerListener()
        {
            // When
            _defusingState.StartNewTimer();

            // Then
            _defusingTimerUpdatedListener
                .Received()
                .OnDefusingTimerUpdated(DefusingState.STARTING_DEFUSING_TIME);
        }
    }
}