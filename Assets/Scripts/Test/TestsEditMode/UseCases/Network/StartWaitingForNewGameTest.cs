using Main.Domain.Network;
using Main.Domain.Players;
using Main.Domain.UI;
using Main.UseCases.Network;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.UseCases.Network
{
    [TestFixture]
    public class StartWaitingForNewGameTest
    {
        private ISpotTheDefuserNetworkDiscovery _spotTheDefuserNetworkDiscovery;
        private StartWaitingForNewGame _startWaitingForNewGame;
        private IViewManager _viewManager;
        private AllPlayers _allPlayers;

        [SetUp]
        public void Init()
        {
            _spotTheDefuserNetworkDiscovery = Substitute.For<ISpotTheDefuserNetworkDiscovery>();
            _viewManager = Substitute.For<IViewManager>();
            _allPlayers = new AllPlayers();
            
            _startWaitingForNewGame = new StartWaitingForNewGame(
                _spotTheDefuserNetworkDiscovery, 
                _viewManager, 
                _allPlayers
            );
        }
        
        [Test]
        public void Start_ShouldStartListeningBroadcastOnLAN()
        {
            // When
            _startWaitingForNewGame.Start("");

            // Then
            _spotTheDefuserNetworkDiscovery.Received().StartListeningBroadcastOnLAN();
        }

        [Test]
        public void Start_ShouldSwitchToLobbyView()
        {
            // When
            _startWaitingForNewGame.Start("");

            // Then
            _viewManager.Received().ReplaceCurrentLayers(View.Lobby);
        }

        [Test]
        public void Start_ShouldSetLocalPlayerNameOnAllPlayerControllers()
        {
            // Given
            const string playerName = "Player Name";

            // When
            _startWaitingForNewGame.Start(playerName);

            // Then
            Assert.That(_allPlayers.LocalPlayer.Name, Is.EqualTo(playerName));
        }
    }
}