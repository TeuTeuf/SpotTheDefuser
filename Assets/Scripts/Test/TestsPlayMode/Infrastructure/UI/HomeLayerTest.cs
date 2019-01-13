using System.Collections;
using Main.Domain.Network;
using Main.Domain.UI;
using Main.Infrastructure.Controllers.Network;
using Main.Infrastructure.UI;
using Main.UseCases.Network;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Test.TestsPlayMode.Infrastructure.UI
{
    public class HomeLayerTest
    {
        private HomeLayer _homeLayer;
        
        private HostNewGame _hostNewGame;
        private StartWaitingForNewGame _startWaitingForNewGame;
        private AllPlayerControllers _allPlayerControllers;
        private IViewManager _viewManager;


        [SetUp]
        public void Init()
        {
            var networkManager = Substitute.For<ISpotTheDefuserNetworkManager>();
            var networkDiscovery = Substitute.For<ISpotTheDefuserNetworkDiscovery>();
            _hostNewGame = Substitute.For<HostNewGame>(networkManager, networkDiscovery);
            _startWaitingForNewGame = Substitute.For<StartWaitingForNewGame>(networkDiscovery);
            
            _allPlayerControllers = Substitute.For<AllPlayerControllers>();
            _homeLayer = new GameObject().AddComponent<HomeLayer>();

            _viewManager = Substitute.For<IViewManager>();
            
            _homeLayer.Init(_hostNewGame, _startWaitingForNewGame, _allPlayerControllers, _viewManager);
        }

        [UnityTest]
        public IEnumerator OnClickOnHost_OnEndEditOnPlayerName_ShouldAddPlayerThroughAllPlayerControllers()
        {
            // Given
            const string playerName = "Player Name";
            
            var playerController = new GameObject().AddComponent<PlayerController>();
            _allPlayerControllers.LocalPlayerController = playerController;

            // When
            _homeLayer.OnEndEditOnPlayerName(playerName);
            _homeLayer.OnClickOnHost();

            yield return null;

            // Then
            _allPlayerControllers.Received().AddNewPlayerOnServer(playerName);
        }

        [UnityTest]
        public IEnumerator OnClickOnHost_ShouldCallUseCaseHostNewGameBeforeAddingPlayer()
        {
            // Given
            var playerController = new GameObject().AddComponent<PlayerController>();
            _allPlayerControllers.LocalPlayerController = playerController;
            
            // When
            _homeLayer.OnClickOnHost();
            
            yield return null;

            // Then
            Received.InOrder(() =>
            {
                _hostNewGame.Host();
                _allPlayerControllers.AddNewPlayerOnServer(Arg.Any<string>());
            });
        }
        
        [UnityTest]
        public IEnumerator OnClickOnHost_ShouldChangeViewAfterAddingPlayerOnServer()
        {
            // Given
            var playerController = new GameObject().AddComponent<PlayerController>();
            _allPlayerControllers.LocalPlayerController = playerController;
            
            // When
            _homeLayer.OnClickOnHost();
            
            yield return null;

            // Then
            Received.InOrder(() =>
            {
                _allPlayerControllers.AddNewPlayerOnServer(Arg.Any<string>());
                _viewManager.ReplaceCurrentLayers(View.Lobby);
            });
        }
        
        [Test]
        public void OnClickOnJoin_ShouldStartWaitingForNewGame()
        {
            // Given
            const string playerName = "Player Name";
            
            // When
            _homeLayer.OnEndEditOnPlayerName(playerName);
            _homeLayer.OnClickOnJoin();
            
            // Then
            _startWaitingForNewGame.Received().Start(playerName);
        }

        [Test]
        public void OnClickOnJoin_ShouldSwitchToLobbyView()
        {
            // When
            _homeLayer.OnClickOnJoin();

            // Then
            _viewManager.Received().ReplaceCurrentLayers(View.Lobby);
        }
    }
}