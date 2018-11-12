using System.Collections;
using Main.Domain.Network;
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
        private HostNewGame _hostNewGame;
        private HomeLayer _homeLayer;
        private AllPlayerControllers _allPlayerControllers;

        [SetUp]
        public void Init()
        {
            var networkManager = Substitute.For<ILobbyManager>();
            _hostNewGame = Substitute.For<HostNewGame>(networkManager);
            
            _allPlayerControllers = Substitute.For<AllPlayerControllers>();
            _homeLayer = new GameObject().AddComponent<HomeLayer>();
            
            _homeLayer.Init(_hostNewGame, _allPlayerControllers);
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
    }
}