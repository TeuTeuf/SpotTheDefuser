using Main.Domain.Network;
using Main.Domain.Players;
using Main.Domain.UI;
using Main.Infrastructure.UI;
using Main.UseCases.Network;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Test.TestsEditMode.Infrastructure.UI
{
    [TestFixture]
    public class HomeLayerTest
    {
        private HomeLayer _homeLayer;
        
        private HostNewGame _hostNewGame;
        private StartWaitingForNewGame _startWaitingForNewGame;
        private IViewManager _viewManager;


        [SetUp]
        public void Init()
        {
            _viewManager = Substitute.For<IViewManager>();

            var allPlayers = new AllPlayers();
            
            var networkManager = Substitute.For<ISpotTheDefuserNetworkManager>();
            var networkDiscovery = Substitute.For<ISpotTheDefuserNetworkDiscovery>();
            
            _hostNewGame = Substitute.For<HostNewGame>(networkManager, networkDiscovery, _viewManager, allPlayers);
            _startWaitingForNewGame = Substitute.For<StartWaitingForNewGame>(networkDiscovery, _viewManager, allPlayers);
            
            _homeLayer = new GameObject().AddComponent<HomeLayer>();
            
            _homeLayer.Init(_hostNewGame, _startWaitingForNewGame);
        }
        
        [Test]
        public void OnEndEditOnPlayerName_OnClickOnHost_ShouldStartHostingNewGameForGivenPlayerName()
        {
            // Given
            const string playerName = "Player Name";

            // When
            _homeLayer.OnEndEditOnPlayerName(playerName);
            _homeLayer.OnClickOnHost();


            // Then
            _hostNewGame.Received().Host(playerName);
        }
        
        [Test]
        public void OnEndEditOnPlayerName_OnClickOnJoin_ShouldStartWaitingForNewGame()
        {
            // Given
            const string playerName = "Player Name";
            
            // When
            _homeLayer.OnEndEditOnPlayerName(playerName);
            _homeLayer.OnClickOnJoin();
            
            // Then
            _startWaitingForNewGame.Received().Start(playerName);
        }
    }
}