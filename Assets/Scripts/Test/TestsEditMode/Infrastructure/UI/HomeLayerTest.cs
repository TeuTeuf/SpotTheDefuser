using Main.Domain;
using Main.Infrastructure.Controllers.Network;
using Main.Infrastructure.UI;
using Main.UseCases;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Test.TestsEditMode.Infrastructure.UI
{
    public class HomeLayerTest
    {
        private HostNewGame _hostNewGame;
        private HomeLayer _homeLayer;
        private AllPlayerControllers _allPlayerControllers;

        [SetUp]
        public void Init()
        {
            var networkDiscovery = Substitute.For<INetworkDiscovery>();
            var networkManager = Substitute.For<INetworkManager>();
            _hostNewGame = Substitute.For<HostNewGame>(networkDiscovery, networkManager);
            
            _allPlayerControllers = Substitute.For<AllPlayerControllers>();
            _homeLayer = new GameObject().AddComponent<HomeLayer>();
            
            _homeLayer.Init(_hostNewGame, _allPlayerControllers);
        }

        [Test]
        public void OnClickOnHost_OnEndEditOnPlayerName_ShouldAddPlayerThroughAllPlayerControllers()
        {
            // Given
            const string playerName = "Player Name";

            // When
            _homeLayer.OnEndEditOnPlayerName(playerName);
            _homeLayer.OnClickOnHost();

            // Then
            _allPlayerControllers.Received().AddNewPlayerOnServer(playerName);
        }

        [Test]
        public void OnClickOnHost_ShouldCallUseCaseHostNewGameBeforeAddingPlayer()
        {
            // When
            _homeLayer.OnClickOnHost();

            // Then
            Received.InOrder(() =>
            {
                _hostNewGame.Host();
                _allPlayerControllers.AddNewPlayerOnServer(Arg.Any<string>());
            });
        }
    }
}