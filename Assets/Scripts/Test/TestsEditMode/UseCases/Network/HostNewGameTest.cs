using Main.Domain.Network;
using Main.Domain.UI;
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
        
        [SetUp]
        public void Init()
        {
            _spotTheDefuserNetworkManager = Substitute.For<ISpotTheDefuserNetworkManager>();
            _spotTheDefuserNetworkDiscovery = Substitute.For<ISpotTheDefuserNetworkDiscovery>();
            _hostNewGame = new HostNewGame(_spotTheDefuserNetworkManager, _spotTheDefuserNetworkDiscovery);
        }

        [Test]
        public void Host_ShouldStartHostingOnNetworkManager()
        {
            // When
            _hostNewGame.Host();

            // Then
            _spotTheDefuserNetworkManager.Received().Host();
        }

        [Test]
        public void Host_ShouldStartBroadcastingOnNetworkDiscovery()
        {
            // When
            _hostNewGame.Host();

            // Then
            _spotTheDefuserNetworkDiscovery.Received().StartBroadcastingOnLAN();
        }
    }
}