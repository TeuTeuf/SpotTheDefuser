using Main.Domain.Network;
using Main.UseCases.Network;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.UseCases.Network
{
    public class HostNewGameTest
    {
        private HostNewGame _hostNewGame;
        private INetworkManager _networkManager;
        
        [SetUp]
        public void Init()
        {
            _networkManager = Substitute.For<INetworkManager>();
            _hostNewGame = new HostNewGame(_networkManager);
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