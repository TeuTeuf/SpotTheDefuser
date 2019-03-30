using Main.Domain;
using Main.Domain.DefuseAttempts;
using Main.Domain.UI;
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
        private DefusingLayer _defusingLayer;
        private AllBombs _allBombs;

        [SetUp]
        public void Init()
        {
            _allBombs = Substitute.For<AllBombs>(Substitute.For<IRandom>(), new IBomb[1]);
            _defusingLayer = new GameObject().AddComponent<DefusingLayer>();
            _defusingLayer.bombImage = new GameObject().AddComponent<Image>();
            _defusingLayer.Init(_allBombs);
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
        public void GetView_ShouldReturnDefusing()
        {
            // When
            var view = _defusingLayer.GetView();

            // Then
            Assert.That(view, Is.EqualTo(View.Defusing));
        }
    }
}