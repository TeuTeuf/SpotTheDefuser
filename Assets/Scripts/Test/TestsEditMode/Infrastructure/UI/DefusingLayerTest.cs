using Main.Domain.UI;
using Main.Infrastructure.UI;
using NUnit.Framework;
using UnityEngine;

namespace Test.TestsEditMode.Infrastructure.UI
{
    [TestFixture]
    public class DefusingLayerTest
    {
        private DefusingLayer _defusingLayer;

        [SetUp]
        public void Init()
        {
            _defusingLayer = new GameObject().AddComponent<DefusingLayer>();
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