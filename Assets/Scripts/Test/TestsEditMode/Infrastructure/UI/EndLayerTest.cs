using Main.Domain.UI;
using Main.Infrastructure.UI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Test.TestsEditMode.Infrastructure.UI
{
    [TestFixture]
    public class EndLayerTest
    {
        private EndLayer _endLayer;

        [SetUp]
        public void Init()
        {
            _endLayer = new GameObject().AddComponent<EndLayer>();
            _endLayer.nbBombsDefusedText = new GameObject().AddComponent<Text>();
        }

        [Test]
        public void UpdateNbBombsDefused_ShouldSetTextToNbBombsDefused()
        {
            // Given
            const int nbBombsDefused = 42;

            // When
            _endLayer.UpdateNbBombsDefused(nbBombsDefused);

            // Then
            Assert.That(_endLayer.nbBombsDefusedText.text, Is.EqualTo("42"));
        }
        
        [Test]
        public void GetView_ShouldReturnEndView()
        {
            // When
            var view = _endLayer.GetView();

            // Then
            Assert.That(view, Is.EqualTo(View.End));
        }
    }
}