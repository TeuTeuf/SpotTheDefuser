using System.Collections.Generic;
using Main.Domain;
using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using Main.UseCases.DefuseAttempts;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.UseCases.DefuseAttempts
{
    [TestFixture]
    public class SetNewDefuseAttemptTest
    {
        private IRandom _random;
        private IDeviceInfo _deviceInfo;
        private DefuserCounter _defuserCounter;
        private AllPlayers _allPlayers;
        private DefusingState _defusingState;
        private AllBombs _allBombs;
        private INewDefuseAttemptSetListener _newDefuseAttemptSetListener;
        private SetNewDefuseAttempt _setNewDefuseAttempt;

        [SetUp]
        public void Init()
        {
            _random = Substitute.For<IRandom>();
            _random.Range(Arg.Any<int>(), Arg.Any<int>()).Returns(0);

            _deviceInfo = Substitute.For<IDeviceInfo>();
            
            _defuserCounter = new DefuserCounter();
            
            _allPlayers = Substitute.For<AllPlayers>();
            _allPlayers.GetAll().Returns(new List<Player>().AsReadOnly());
            
            _defusingState = new DefusingState(
                Substitute.For<IDefusingTime>(),
                Substitute.For<IDefusingTimerUpdatedListener>(),
                Substitute.For<IDefuseFailedListener>());

            _allBombs = Substitute.For<AllBombs>(_random, new IBomb[0], _deviceInfo);

            _newDefuseAttemptSetListener = Substitute.For<INewDefuseAttemptSetListener>();
            
            _setNewDefuseAttempt = new SetNewDefuseAttempt(
                _random, _allPlayers, _allBombs, _defusingState, _defuserCounter, _newDefuseAttemptSetListener);
        }
        
        [Test]
        public void Set_ShouldSetNewDefuseAttemptInDefusingState()
        {
            // When
            _setNewDefuseAttempt.Set();

            // Then
            Assert.IsInstanceOf<DefuseAttempt>(_defusingState.CurrentDefuseAttempt);
        }

        [Test]
        public void Set_ShouldNotifyNewDefuseAttemptListener()
        {
            // When
            _setNewDefuseAttempt.Set();


            // Then
            _newDefuseAttemptSetListener
                .Received()
                .OnNewDefuseAttemptSet(Arg.Any<DefuseAttempt>());
        }
    }
}