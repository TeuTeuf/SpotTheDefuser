using Main.Domain.UI;
using Main.Infrastructure.UI;
using NUnit.Framework;
using UnityEngine;

namespace Test.TestsEditMode.Infrastructure.UI
{
    public class BasicViewLayerTest
    {
        private BasicLayer _basicLayer;

        [SetUp]
        public void Init()
        {
            _basicLayer = new GameObject().AddComponent<BasicLayer>();
        }
        
        [Test]
        public void GetView_ShouldReturnViewSetHasPublicProperty()
        {
            // When
            _basicLayer.view = View.Lobby;

            // Then
            Assert.AreEqual(View.Lobby, _basicLayer.GetView());
        }

        [Test]
        public void Enable_ShouldEnableGameObject()
        {
            // Given
            _basicLayer.gameObject.SetActive(false);
            
            // When
            _basicLayer.Enable();

            // Then
            Assert.IsTrue(_basicLayer.gameObject.activeSelf);
        }

        [Test]
        public void Disable_ShouldDisableGameObject()
        {
            // Given
            _basicLayer.gameObject.SetActive(true);

            // When
            _basicLayer.Disable();

            // Then
            Assert.IsFalse(_basicLayer.gameObject.activeSelf);
        }
    }
}