using Main.Domain.UI;
using Main.Infrastructure.UI;
using NUnit.Framework;
using UnityEngine;

namespace Test.TestsEditMode.Infrastructure.UI
{
    public class BasicViewLayerTest
    {
        private BasicViewLayer _basicViewLayer;

        [SetUp]
        public void Init()
        {
            _basicViewLayer = new GameObject().AddComponent<BasicViewLayer>();
        }
        
        [Test]
        public void GetView_ShouldReturnViewSetHasPublicProperty()
        {
            // When
            _basicViewLayer.View = View.LOBBY;

            // Then
            Assert.AreEqual(View.LOBBY, _basicViewLayer.View);
        }

        [Test]
        public void Enable_ShouldEnableGameObject()
        {
            // Given
            _basicViewLayer.gameObject.SetActive(false);
            
            // When
            _basicViewLayer.Enable();

            // Then
            Assert.IsTrue(_basicViewLayer.gameObject.activeSelf);
        }

        [Test]
        public void Disable_ShouldDisableGameObject()
        {
            // Given
            _basicViewLayer.gameObject.SetActive(true);

            // When
            _basicViewLayer.Disable();

            // Then
            Assert.IsFalse(_basicViewLayer.gameObject.activeSelf);
        }
    }
}