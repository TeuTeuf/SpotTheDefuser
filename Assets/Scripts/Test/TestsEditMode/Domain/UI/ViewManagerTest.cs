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
            _aViewLayer.View.Returns(View.HOME);
            
            _anotherViewLayer = Substitute.For<IViewLayer>();
            _anotherViewLayer.View.Returns(View.HOME);
            
            _aViewLayerForOtherView = Substitute.For<IViewLayer>();
            _aViewLayerForOtherView.View.Returns(View.LOBBY);

            var allViewLayers = new List<IViewLayer> {_aViewLayer, _anotherViewLayer};
            
            _viewManager = new ViewManager(allViewLayers);
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
            _aViewLayer.Received().Disable();
            _anotherViewLayer.Received().Disable();
            _aViewLayerForOtherView.DidNotReceive().Disable();
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
            _aViewLayer.Received(1).Disable();
            _anotherViewLayer.Received(1).Disable();
            _aViewLayerForOtherView.DidNotReceive().Disable();
        }
    }
}