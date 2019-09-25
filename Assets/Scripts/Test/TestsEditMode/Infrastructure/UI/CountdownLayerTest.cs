using Main.Domain.DefuseAttempts;
using Main.Domain.UI;
using Main.Infrastructure.UI;
using Main.UseCases.UI;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Test.TestsEditMode.Infrastructure.UI
{
    [TestFixture]
    public class CountdownLayerTest
    {
        private CountdownLayer _countdownLayer;

        private IDefusingTime _defusingTime;
        private ChangeCurrentView _changeCurrentView;
        
        [SetUp]
        public void Init()
        {
            _defusingTime = Substitute.For<IDefusingTime>();

            var viewManager = Substitute.For<IViewManager>();
            _changeCurrentView = Substitute.For<ChangeCurrentView>(viewManager);
            
            _countdownLayer = new GameObject().AddComponent<CountdownLayer>();
            _countdownLayer.countdownText = new GameObject().AddComponent<Text>();

            _countdownLayer.Init(_defusingTime, _changeCurrentView);
        }

        [Test]
        public void Update_ShouldSetCountdownToInitialValueAtBeginning()
        {
            // Given
            _countdownLayer.Start();
            
            // When
            _defusingTime.GetDeltaTime().Returns(0f);
            _countdownLayer.Update();

            // Then
            Assert.That(_countdownLayer.countdownText.text, Is.EqualTo("3"));
        }

        [Test]
        public void Update_ShouldRoundCountdownToUpper()
        {
            // Given
            _countdownLayer.Start();
            
            // When
            _defusingTime.GetDeltaTime().Returns(2.9f);
            _countdownLayer.Update();

            // Then
            Assert.That(_countdownLayer.countdownText.text, Is.EqualTo("1"));
        }

        [Test]
        public void Update_ShouldDecrementCountdownAfterSubsequentUpdate()
        {
            // Given
            _countdownLayer.Start();
            
            // When
            _defusingTime.GetDeltaTime().Returns(0.5f);
            _countdownLayer.Update();
            
            _defusingTime.GetDeltaTime().Returns(0.5f);
            _countdownLayer.Update();

            // Then
            Assert.That(_countdownLayer.countdownText.text, Is.EqualTo("2"));
        }

        [Test]
        public void Update_ShouldChangeToDefusingViewWhenCountdownReachZero()
        {
            // Given
            _countdownLayer.Start();
            
            // When
            _defusingTime.GetDeltaTime().Returns(3.5f);
            _countdownLayer.Update();

            // Then
            _changeCurrentView.Received().Change(View.Defusing);
        }
        
        [Test]
        public void Update_ShouldNOTChangeToDefusingViewWhenCountdownHASNTReachZero()
        {
            // Given
            _countdownLayer.Start();
            
            // When
            _defusingTime.GetDeltaTime().Returns(0.5f);
            _countdownLayer.Update();

            // Then
            _changeCurrentView.DidNotReceive().Change(View.Defusing);
        }
        
        [Test]
        public void GetView_ShouldReturnCountdown()
        {
            // Then
            Assert.That(_countdownLayer.GetView(), Is.EqualTo(View.Countdown));
        }
    }
}