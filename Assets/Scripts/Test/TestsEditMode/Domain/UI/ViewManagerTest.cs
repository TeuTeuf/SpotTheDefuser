using System.Collections.Generic;
using Main.Domain.UI;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.Domain.UI
{
    public class ViewManagerTest
    {
        private ViewManager _viewManager;
        private IViewLayer _aViewLayer;
        private IViewLayer _anotherViewLayer;
        private IViewLayer _aViewLayerForOtherView;

        [SetUp]
        public void Init()
        {
            _aViewLayer = Substitute.For<IViewLayer>();
            _aViewLayer.GetView().Returns(View.HOME);
            
            _anotherViewLayer = Substitute.For<IViewLayer>();
            _anotherViewLayer.GetView().Returns(View.HOME);
            
            _aViewLayerForOtherView = Substitute.For<IViewLayer>();
            _aViewLayerForOtherView.GetView().Returns(View.LOBBY);

            var allViewLayers = new List<IViewLayer> {_aViewLayer, _anotherViewLayer, _aViewLayerForOtherView};
            _viewManager = new ViewManager(allViewLayers);
        }

        [Test]
        public void New_ShouldDisableAllViewLayersOnCreation()
        {
            // Then
            _aViewLayer.Received().Disable();
            _anotherViewLayer.Received().Disable();
            _aViewLayerForOtherView.Received().Disable();
        }
        
        [Test]
        public void EnableLayers_ShouldEnableAllLayersCorrespondingToView()
        {
            // When
            _viewManager.EnableLayers(View.HOME);

            // Then
            _aViewLayer.Received().Enable();
            _anotherViewLayer.Received().Enable();
            _aViewLayerForOtherView.DidNotReceive().Enable();
        }

        [Test]
        public void DisableActiveLayers_ShouldDisableLayersPreviouslyEnabled()
        {
            // Given
            _viewManager.EnableLayers(View.HOME);

            // When
            _viewManager.DisableActiveLayers();

            // Then
            _aViewLayer.Received(2).Disable();
            _anotherViewLayer.Received(2).Disable();
            _aViewLayerForOtherView.Received(1).Disable();
        }

        [Test]
        public void DisableActiveLayers_ShouldNotDisableLayersAlreadyDisabled()
        {
            // Given
            _viewManager.EnableLayers(View.HOME);
            _viewManager.DisableActiveLayers();

            // When
            _viewManager.DisableActiveLayers();

            // Then
            _aViewLayer.Received(2).Disable();
            _anotherViewLayer.Received(2).Disable();
            _aViewLayerForOtherView.Received(1).Disable();
        }
    }
}