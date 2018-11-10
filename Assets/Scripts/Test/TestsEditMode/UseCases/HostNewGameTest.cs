using Main.Domain;
using Main.UseCases;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.UseCases
{
    public class HostNewGameTest
    {
        private HostNewGame _hostNewGame;
        private INetworkDiscovery _networkDiscovery;
        private INetworkManager _networkManager;
        
        [SetUp]
        public void Init()
        {
            _networkDiscovery = Substitute.For<INetworkDiscovery>();
            _networkManager = Substitute.For<INetworkManager>();
            _hostNewGame = new HostNewGame(_networkDiscovery, _networkManager);
        }
        
        [Test]
        public void Host_ShouldStartNetworkDiscoveryAsServer()
        {
            // When
            _hostNewGame.Host();


            // Then
            _networkDiscovery.Received().StartAsServer();
        }

        [Test]
        public void Host_ShouldStartHostingOnNetworkManager()
        {
            // When
            _hostNewGame.Host();

            // Then
            _networkManager.Received().StartHost();
        }
    }
}