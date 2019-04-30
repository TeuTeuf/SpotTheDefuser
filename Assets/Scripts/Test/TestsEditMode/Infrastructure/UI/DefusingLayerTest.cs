using Main.Domain;
using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using Main.Domain.UI;
using Main.Infrastructure.Controllers.Network;
using Main.Infrastructure.UI;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Test.TestsEditMode.Infrastructure.UI
{
    [TestFixture]
    public class DefusingLayerTest
    {
        private AllBombs _allBombs;
        private AllPlayerControllers _allPlayerControllers;
        private IDefusingTime _defusingTime;

        private DefusingLayer _defusingLayer;

        [SetUp]
        public void Init()
        {
            _allBombs = Substitute.For<AllBombs>(Substitute.For<IRandom>(), new IBomb[1]);
            _allPlayerControllers = Substitute.For<AllPlayerControllers>(Substitute.For<AllPlayers>());
            _defusingTime = Substitute.For<IDefusingTime>();

            _defusingLayer = new GameObject().AddComponent<DefusingLayer>();
            _defusingLayer.bombImage = new GameObject().AddComponent<Image>();
            _defusingLayer.Init(_allBombs, _allPlayerControllers, _defusingTime);
        }

        [Test]
        public void UpdateDisplayedBomb_ShouldDisplayBombCorrespondingToId()
        {
            // Given
            const string bombId = "bombId";
            const bool isDefuser = true;
            var aBomb = Substitute.For<IBomb>();
            var aSprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0f, 0f, 1f, 1f), Vector2.up);
            
            aBomb.GetSprite(isDefuser).Returns(aSprite);

            _allBombs.GetByBombId(bombId).Returns(aBomb);

            // When
            _defusingLayer.UpdateDisplayedBomb(bombId, isDefuser);

            // Then
            Assert.That(_defusingLayer.bombImage.sprite, Is.EqualTo(aSprite));
        }

        [Test]
        public void OnClickOnDefuse_ShouldTryToDefuseOnServer()
        {
            // When
            _defusingLayer.OnClickOnDefuse();

            // Then
            _allPlayerControllers
                .Received()
                .TryToDefuseOnServer();
        }

        [Test]
        public void GetView_ShouldReturnDefusing()
        {
            // When
            var view = _defusingLayer.GetView();

            // Then
            Assert.That(view, Is.EqualTo(View.Defusing));
        }

        [Test]
        public void UpdateTimer_ShouldSetValueOfDisplayedTimer()
        {
            // Given
            _defusingLayer.timerText = new GameObject().AddComponent<Text>();
            
            // When
            _defusingLayer.UpdateTimer(42f);

            // Then
            Assert.That(_defusingLayer.timerText.text, Is.EqualTo("00:42:00"));
        }

        [Test]
        public void Update_ShouldUpdateTimerCorrespondingToRemainingDefusingTime()
        {
            // Given
            _defusingLayer.timerText = new GameObject().AddComponent<Text>();
            _defusingLayer.UpdateTimer(82.14f);
            _defusingTime.GetDeltaTime().Returns(10f);

            // When
            _defusingLayer.Update();

            // Then
            Assert.That(_defusingLayer.timerText.text, Is.EqualTo("01:12:14"));
        }
    }
}