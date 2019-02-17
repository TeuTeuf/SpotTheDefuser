using Main.Domain;
using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using Main.Domain.UI;
using Main.Infrastructure.Controllers.Network;
using Main.UseCases.DefuseAttempts;
using Main.UseCases.Players;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Test.TestsEditMode.Infrastructure.Controllers.Network
{
    public class PlayerControllerTest
    {
        private SetNewDefuseAttempt _setNewDefuseAttempt;
        private AddNewPlayer _addNewPlayer;
        private TryToDefuse _tryToDefuse;
        
        private PlayerController _playerController;

        private AllPlayerControllers _allPlayerControllers;
        private IUIController _uiController;
        private GameObject _playerControllerGameObject;

        [SetUp]
        public void Init()
        {
            var random = Substitute.For<IRandom>();
            var allPlayers = Substitute.For<AllPlayers>();
            var defusingState = Substitute.For<DefusingState>();
            var defusingListener = Substitute.For<IDefusingListener>();

            _uiController = Substitute.For<IUIController>();

            _setNewDefuseAttempt = Substitute.For<SetNewDefuseAttempt>(random, allPlayers, defusingState);
            _addNewPlayer = Substitute.For<AddNewPlayer>(allPlayers, null);
            _tryToDefuse = Substitute.For<TryToDefuse>(defusingState, defusingListener);
            
            _allPlayerControllers = new AllPlayerControllers(allPlayers);

            _playerControllerGameObject = new GameObject();
            _playerController = _playerControllerGameObject.AddComponent<PlayerController>();
            _playerController.Init(_addNewPlayer, _setNewDefuseAttempt, _tryToDefuse, _allPlayerControllers, _uiController);
        }

        [Test]
        public void OnStartLocalPlayer_ShouldSetLocalPlayerControllerOnAllPlayerControllers()
        {
            // Given
            var allPlayerControllers = Substitute.For<AllPlayerControllers>(new AllPlayers());
            
            var playerController = new GameObject().AddComponent<PlayerController>();
            playerController.Init(null, null, null, allPlayerControllers, null);
            
            // When
            playerController.OnStartLocalPlayer();

            // Then
            Assert.AreSame(playerController, allPlayerControllers.LocalPlayerController);
        }

        [Test]
        public void OnStartLocalPlayer_ShouldAddLocalPlayerToServer()
        {
            // Given
            var allPlayerControllers = Substitute.For<AllPlayerControllers>(new AllPlayers());
            
            var playerController = new GameObject().AddComponent<PlayerController>();
            playerController.Init(null, null, null, allPlayerControllers, null);
            
            // When
            playerController.OnStartLocalPlayer();

            // Then
            allPlayerControllers
                .Received()
                .AddLocalPlayerOnServer();
        }

        [Test]
        public void OnStartServer_ShouldAddPlayerControllerToPlayerControllersInAllPlayerControllers()
        {
            // Given
            var otherPlayerController = new GameObject().AddComponent<PlayerController>();
            otherPlayerController.Init(null, null, null, _allPlayerControllers, null);

            // When
            _playerController.OnStartServer();
            otherPlayerController.OnStartServer();

            // Then
            Assert.Contains(_playerController, _allPlayerControllers.GetPlayerControllersOnServer());
            Assert.Contains(otherPlayerController, _allPlayerControllers.GetPlayerControllersOnServer());
        }

        [Test]
        public void CmdSetNewDefuseAttempt_ShouldExecuteSetNewDefuseAttemptUseCase()
        {
            // When
            _playerController.CmdSetNewDefuseAttempt();

            // Then
            _setNewDefuseAttempt
                .Received()
                .Set();
        }

        [Test]
        public void CmdAddNewPlayer_ShouldExecuteAddNewPlayerUseCase()
        {
            // Given
            var player = new Player("Player Name");

            // When
            _playerController.CmdAddNewPlayer(player);

            // Then
            _addNewPlayer
                .Received()
                .Execute(Arg.Is<Player>(addedPlayer => addedPlayer == player));
        }
        
        [Test]
        public void CmdTryToDefuse_ShouldExecuteTryToDefuseUseCase()
        {
            // Given
            var player = new Player("Player Name");

            _playerController.CmdAddNewPlayer(player);
            
            // When
            _playerController.CmdTryToDefuse();
            
            // Then
            _tryToDefuse
                .Received()
                .Try(Arg.Is<Player>(defuserPlayer => defuserPlayer == player));
        }

        [Test]
        public void OnPlayerAdded_ShouldUpdateLobbyView()
        {
            // Given
            var players = new[]
            {
                new Player("Player Name 1"),
                new Player("Player Name 2"),
            };

            // When
            _playerController.OnPlayerAdded(players);

            // Then
            _uiController
                .Received()
                .UpdateLobby(players);
        }

        [Test]
        public void RpcOnPlayerAdded_ShouldNotUpdateLobbyViewIfHasNotNetworkAuthority()
        {
            // Given
            var players = new[]
            {
                new Player("Player Name 1"),
                new Player("Player Name 2"),
            };

            // When
            _playerController.RpcOnPlayerAdded(players);

            // Then
            _uiController
                .DidNotReceive()
                .UpdateLobby(players);
        }
    }
}