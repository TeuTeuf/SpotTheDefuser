using System.Collections.Generic;
using Main.Domain.UI;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.Domain.UI
{
    public class ViewManagerTest
    {
        private ViewManager _viewManager;
        private IViewLayer _aHomeViewLayer;
        private IViewLayer _anotherHomeViewLayer;
        private IViewLayer _aLobbyViewLayer;

        [SetUp]
        public void Init()
        {
            _aHomeViewLayer = Substitute.For<IViewLayer>();
            _aHomeViewLayer.GetView().Returns(View.Home);
            
            _anotherHomeViewLayer = Substitute.For<IViewLayer>();
            _anotherHomeViewLayer.GetView().Returns(View.Home);
            
            _aLobbyViewLayer = Substitute.For<IViewLayer>();
            _aLobbyViewLayer.GetView().Returns(View.Lobby);

            var allViewLayers = new List<IViewLayer> {_aHomeViewLayer, _anotherHomeViewLayer, _aLobbyViewLayer};
            _viewManager = new ViewManager(allViewLayers);
        }

        [Test]
        public void New_ShouldDisableAllViewLayersOnCreation()
        {
            // Then
            _aHomeViewLayer.Received().Disable();
            _anotherHomeViewLayer.Received().Disable();
            _aLobbyViewLayer.Received().Disable();
        }
        
        [Test]
        public void EnableLayers_ShouldEnableAllLayersCorrespondingToView()
        {
            // When
            _viewManager.EnableLayers(View.Home);

            // Then
            _aHomeViewLayer.Received().Enable();
            _anotherHomeViewLayer.Received().Enable();
            _aLobbyViewLayer.DidNotReceive().Enable();
        }

        [Test]
        public void DisableActiveLayers_ShouldDisableLayersPreviouslyEnabled()
        {
            // Given
            _viewManager.EnableLayers(View.Home);
            
            _ClearReceivedCallsOnAllViewsDisable();

            // When
            _viewManager.DisableActiveLayers();

            // Then
            _aHomeViewLayer.Received().Disable();
            _anotherHomeViewLayer.Received().Disable();
            _aLobbyViewLayer.DidNotReceive().Disable();
        }

        [Test]
        public void DisableActiveLayers_ShouldNotDisableLayersAlreadyDisabled()
        {
            // Given
            _viewManager.EnableLayers(View.Home);
            _viewManager.DisableActiveLayers();

            _ClearReceivedCallsOnAllViewsDisable();
            
            // When
            _viewManager.DisableActiveLayers();

            // Then
            _aHomeViewLayer.DidNotReceive().Disable();
            _anotherHomeViewLayer.DidNotReceive().Disable();
            _aLobbyViewLayer.DidNotReceive().Disable();
        }

        [Test]
        public void ReplaceCurrent_ShouldDisableAllActiveLayersAndEnableLayersCorrespondingToGivenView()
        {
            // Given
            _viewManager.EnableLayers(View.Home);
            _ClearReceivedCallsOnAllViewsDisable();
            
            // When
            _viewManager.ReplaceCurrentLayers(View.Lobby);

            // Then
            _aHomeViewLayer.Received().Disable();
            _anotherHomeViewLayer.Received().Disable();
            _aLobbyViewLayer.DidNotReceive().Disable();
            _aLobbyViewLayer.Received().Enable();
        }

        private void _ClearReceivedCallsOnAllViewsDisable()
        {
            _aHomeViewLayer.ClearReceivedCalls();
            _anotherHomeViewLayer.ClearReceivedCalls();
            _aLobbyViewLayer.ClearReceivedCalls();
        }
    }
}