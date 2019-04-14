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
        private DefusingState _defusingState;
        private AllPlayerControllers _allPlayerControllers;
        
        private DefusingLayer _defusingLayer;

        [SetUp]
        public void Init()
        {
            _allBombs = Substitute.For<AllBombs>(Substitute.For<IRandom>(), new IBomb[1]);
            _allPlayerControllers = Substitute.For<AllPlayerControllers>(Substitute.For<AllPlayers>());
            _defusingState = Substitute.For<DefusingState>(Substitute.For<IDefusingTime>());
            _defusingLayer = new GameObject().AddComponent<DefusingLayer>();
            _defusingLayer.bombImage = new GameObject().AddComponent<Image>();
            _defusingLayer.Init(_allBombs, _allPlayerControllers, _defusingState);
        }

        [Test]
        public void UpdateDisplayedBomb_ShouldDisplayBombCorrespondingToId()
        {
            // Given
            const string bombId = "bombId";
            const bool isDefuser = true;
            var aBomb = Substitute.For<IBomb>();
            var aSprite = Sprite.Create(Texture2D.whiteTexture, Rect.zero, Vector2.up);
            
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
        public void Update_ShouldUpdateTimerCorrespondingToRemainingDefusingTime()
        {
            // Given
            _defusingLayer.timerText = new GameObject().AddComponent<Text>();
            _defusingState.RemainingTime.Returns(72.143f);

            // When
            _defusingLayer.Update();

            // Then
            Assert.That(_defusingLayer.timerText.text, Is.EqualTo("01:12:14"));
        }
    }
}