using Main.Domain.DefuseAttempts;
using Main.Domain.Network;
using Main.Domain.Players;
using Main.Domain.UI;
using Main.Infrastructure.Controllers.Network;
using Main.Infrastructure.Network;
using Main.UseCases.DefuseAttempts;
using Main.UseCases.Players;
using Main.UseCases.UI;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Test.TestsEditMode.Infrastructure.Controllers.Network
{
    public class PlayerControllerTest
    {
        private AddNewPlayer _addNewPlayer;
        private TryToDefuse _tryToDefuse;
        private StartNewGame _startNewGame;
        private ChangeCurrentView _changeCurrentView;

        private NetworkBehaviourChecker _networkBehaviourChecker;
        
        private PlayerController _playerController;

        private AllPlayerControllers _allPlayerControllers;
        private IUIController _uiController;
        private ISpotTheDefuserNetworkDiscovery _spotTheDefuserNetworkDiscovery;

        [SetUp]
        public void Init()
        {
            var allPlayers = Substitute.For<AllPlayers>();
            var defusingState = Substitute.For<DefusingState>();
            var defusingListener = Substitute.For<IDefusingListener>();

            _uiController = Substitute.For<IUIController>();

            _addNewPlayer = Substitute.For<AddNewPlayer>(allPlayers, null);
            _tryToDefuse = Substitute.For<TryToDefuse>(defusingState, defusingListener);
            _startNewGame = Substitute.For<StartNewGame>(Substitute.For<INewGameStartedListener>());
            _changeCurrentView = Substitute.For<ChangeCurrentView>(Substitute.For<IViewManager>());

            _networkBehaviourChecker = Substitute.For<NetworkBehaviourChecker>();
            
            _allPlayerControllers = new AllPlayerControllers(allPlayers);

            _spotTheDefuserNetworkDiscovery = Substitute.For<ISpotTheDefuserNetworkDiscovery>();

            _playerController = new GameObject().AddComponent<PlayerController>();
            _playerController.Init(_addNewPlayer, _startNewGame, _tryToDefuse, _changeCurrentView, _allPlayerControllers, _uiController, _networkBehaviourChecker, _spotTheDefuserNetworkDiscovery);
        }

        [Test]
        public void OnStartLocalPlayer_ShouldSetLocalPlayerControllerOnAllPlayerControllers()
        {
            // Given
            var allPlayerControllers = Substitute.For<AllPlayerControllers>(new AllPlayers());
            
            var playerController = new GameObject().AddComponent<PlayerController>();
            playerController.Init(null, null, null, _changeCurrentView, allPlayerControllers, null, _networkBehaviourChecker, _spotTheDefuserNetworkDiscovery);
            
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
            playerController.Init(null, null,null, _changeCurrentView, allPlayerControllers, null, _networkBehaviourChecker, _spotTheDefuserNetworkDiscovery);
            
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
            otherPlayerController.Init(null, null,null, _changeCurrentView, _allPlayerControllers, null, _networkBehaviourChecker, _spotTheDefuserNetworkDiscovery);

            // When
            _playerController.OnStartServer();
            otherPlayerController.OnStartServer();

            // Then
            Assert.Contains(_playerController, _allPlayerControllers.GetPlayerControllersOnServer());
            Assert.Contains(otherPlayerController, _allPlayerControllers.GetPlayerControllersOnServer());
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
        public void CmdStartNewGame_ShouldExecuteStartNewGameUseCase()
        {
            // When
            _playerController.CmdStartNewGame();
            
            // Then
            _startNewGame
                .Received()
                .Start();
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
        public void RpcOnPlayerAdded_ShouldUpdateLobbyView_WhenPlayerHasAuthority()
        {
            // Given
            var players = new[]
            {
                new Player("Player Name 1"),
                new Player("Player Name 2"),
            };
            
            _networkBehaviourChecker
                .HasAuthority(_playerController)
                .Returns(true);

            // When
            _playerController.RpcOnPlayerAdded(players);

            // Then
            _uiController
                .Received()
                .UpdateLobby(players);
        }

        [Test]
        public void RpcOnPlayerAdded_ShouldNotUpdateLobbyView_WhenPlayerHasNotNetworkAuthority()
        {
            // Given
            var players = new[]
            {
                new Player("Player Name 1"),
                new Player("Player Name 2"),
            };
            
            _networkBehaviourChecker
                .HasAuthority(_playerController)
                .Returns(false);

            // When
            _playerController.RpcOnPlayerAdded(players);

            // Then
            _uiController
                .DidNotReceive()
                .UpdateLobby(players);
        }

        [Test]
        public void RpcOnNewGameStarted_ShouldChangeCurrentViewOnPlayerWithAuthority()
        {
            // Given
            _networkBehaviourChecker
                .HasAuthority(_playerController)
                .Returns(true);
            
            // When
            _playerController.RpcOnNewGameStarted();

            // Then
            _changeCurrentView
                .Received()
                .Change(View.Defusing);
        }

        [Test]
        public void RpcOnNewGameStarted_ShouldNotChangeCurrentView_WhenPlayerDoesntHaveAuthority()
        {
            // Given
            _networkBehaviourChecker
                .HasAuthority(_playerController)
                .Returns(false);
            
            // When
            _playerController.RpcOnNewGameStarted();

            // Then
            _changeCurrentView
                .DidNotReceive()
                .Change(View.Defusing);
        }
        
        [Test]
        public void RpcOnNewGameStarted_ShouldStopBroadcastingOnNetwork_WhenPlayerIsServer()
        {
            // Given
            _networkBehaviourChecker
                .IsServer(_playerController)
                .Returns(true);
            
            // When
            _playerController.RpcOnNewGameStarted();

            // Then
            _spotTheDefuserNetworkDiscovery
                .Received()
                .StopBroadcastingOnLAN();
        }
        
        [Test]
        public void RpcOnNewGameStarted_ShouldNOTStopBroadcastingOnNetwork_WhenPlayerIsNOTServer()
        {
            // Given
            _networkBehaviourChecker
                .IsServer(_playerController)
                .Returns(false);
            
            // When
            _playerController.RpcOnNewGameStarted();

            // Then
            _spotTheDefuserNetworkDiscovery
                .DidNotReceive()
                .StopBroadcastingOnLAN();
        }
    }
}