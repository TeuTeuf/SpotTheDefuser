using Main.Infrastructure.Network;
using Main.UseCases.Network;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Test.TestsEditMode.Infrastructure.Network
{
    [TestFixture]
    public class SpotTheDefuserNetworkDiscoveryTest
    {
        private ConnectToNewGame _connectToNewGame;
        private SpotTheDefuserNetworkDiscovery _spotTheDefuserNetworkDiscovery;

        [SetUp]
        public void Init()
        {
            _connectToNewGame = Substitute.For<ConnectToNewGame>();

            _spotTheDefuserNetworkDiscovery = new GameObject().AddComponent<SpotTheDefuserNetworkDiscovery>();
            _spotTheDefuserNetworkDiscovery.Init(_connectToNewGame);
        }
        
        [Test]
        public void OnReceivedBroadcast_ShouldConnectToHost()
        {
            // Given
            const string fromAddress = "fromAddress";
            
            // When
            _spotTheDefuserNetworkDiscovery.OnReceivedBroadcast(fromAddress, "data");

            // Then
            _connectToNewGame.Received().Connect(fromAddress);
        }
    }
}