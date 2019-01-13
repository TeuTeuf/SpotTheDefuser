using Main.Domain.Network;
using Main.Domain.UI;
using Main.Infrastructure.Controllers.Network;
using Main.UseCases.Network;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.UseCases.Network
{
    public class HostNewGameTest
    {
        private HostNewGame _hostNewGame;
        private ISpotTheDefuserNetworkManager _spotTheDefuserNetworkManager;
        private ISpotTheDefuserNetworkDiscovery _spotTheDefuserNetworkDiscovery;
        private IViewManager _viewManager;
        private AllPlayerControllers _allPlayerControllers;

        [SetUp]
        public void Init()
        {
            _spotTheDefuserNetworkManager = Substitute.For<ISpotTheDefuserNetworkManager>();
            _spotTheDefuserNetworkDiscovery = Substitute.For<ISpotTheDefuserNetworkDiscovery>();
            _viewManager = Substitute.For<IViewManager>();
            
            _allPlayerControllers = Substitute.For<AllPlayerControllers>();
            
            _hostNewGame = new HostNewGame(_spotTheDefuserNetworkManager, _spotTheDefuserNetworkDiscovery, _viewManager, _allPlayerControllers);
        }

        [Test]
        public void Host_ShouldStartHostingOnNetworkManager()
        {
            // When
            _hostNewGame.Host("");

            // Then
            _spotTheDefuserNetworkManager.Received().Host();
        }

        [Test]
        public void Host_ShouldStartBroadcastingOnNetworkDiscovery()
        {
            // When
            _hostNewGame.Host("");

            // Then
            _spotTheDefuserNetworkDiscovery.Received().StartBroadcastingOnLAN();
        }

        [Test]
        public void Host_ShouldSwitchViewToLobby()
        {
            // When
            _hostNewGame.Host("");

            // Then
            _viewManager.Received().ReplaceCurrentLayers(View.Lobby);
        }

        [Test]
        public void Host_ShouldSetLocalPlayerName()
        {
            //Given
            const string playerName = "Player Name";
            
            // When
            _hostNewGame.Host(playerName);

            // Then
            Assert.That(_allPlayerControllers.LocalPlayerName, Is.EqualTo(playerName));
        }
    }
}