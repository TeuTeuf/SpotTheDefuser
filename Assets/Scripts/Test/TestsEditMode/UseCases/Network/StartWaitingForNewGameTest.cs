using Main.Domain.Network;
using Main.UseCases.Network;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.UseCases.Network
{
    [TestFixture]
    public class StartWaitingForNewGameTest
    {
        [Test]
        public void Start_ShouldStartListeningBroadcastOnLAN()
        {
            // Given
            var spotTheDefuserNetworkDiscovery = Substitute.For<ISpotTheDefuserNetworkDiscovery>();
            var startWaitingForNewGame = new StartWaitingForNewGame(spotTheDefuserNetworkDiscovery);

            // When
            startWaitingForNewGame.Start("Player Name");

            // Then
            spotTheDefuserNetworkDiscovery.Received().StartListeningBroadcastOnLAN();
        }
    }
}