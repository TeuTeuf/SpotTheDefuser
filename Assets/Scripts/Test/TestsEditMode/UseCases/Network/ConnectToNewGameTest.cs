using Main.Domain.Network;
using Main.UseCases.Network;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.UseCases.Network
{
    [TestFixture]
    public class ConnectToNewGameTest
    {
        private ISpotTheDefuserNetworkManager _spotTheDefuserNetworkManager;
        private ConnectToNewGame _connectToNewGame;
        private ISpotTheDefuserNetworkDiscovery _spotTheDefuserNetworkDiscovery;

        [SetUp]
        public void Init()
        {
            _spotTheDefuserNetworkManager = Substitute.For<ISpotTheDefuserNetworkManager>();
            _spotTheDefuserNetworkDiscovery = Substitute.For<ISpotTheDefuserNetworkDiscovery>();
            _connectToNewGame = new ConnectToNewGame(_spotTheDefuserNetworkManager, _spotTheDefuserNetworkDiscovery);
        }
        
        [Test]
        public void Connect_ShouldConnectToHost()
        {
            // Given
            const string hostAddress = "127.0.0.1";
 
            // When
            _connectToNewGame.Connect(hostAddress);

            // Then
            _spotTheDefuserNetworkManager.Received().Join(hostAddress);
        }

        [Test]
        public void Connect_ShouldStopBroadcasting()
        {
            // When
            _connectToNewGame.Connect("");

            // Then
            _spotTheDefuserNetworkDiscovery.Received().StopBroadcastingOnLAN();
        }
    }
}