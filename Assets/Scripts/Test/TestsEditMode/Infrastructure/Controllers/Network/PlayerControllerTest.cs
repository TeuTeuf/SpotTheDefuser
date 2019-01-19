using Main.Domain;
using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
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

        [SetUp]
        public void Init()
        {
            var random = Substitute.For<IRandom>();
            var allPlayers = Substitute.For<AllPlayers>();
            var defusingState = Substitute.For<DefusingState>();
            var defusingListener = Substitute.For<IDefusingListener>();

            _setNewDefuseAttempt = Substitute.For<SetNewDefuseAttempt>(random, allPlayers, defusingState);
            _addNewPlayer = Substitute.For<AddNewPlayer>(allPlayers);
            _tryToDefuse = Substitute.For<TryToDefuse>(defusingState, defusingListener);
            
            _allPlayerControllers = new AllPlayerControllers(allPlayers);
            
            _playerController = new GameObject().AddComponent<PlayerController>();
            _playerController.Init(_addNewPlayer, _setNewDefuseAttempt, _tryToDefuse, _allPlayerControllers, null);
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
            allPlayerControllers.Received().AddLocalPlayerOnServer();
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
            _setNewDefuseAttempt.Received().Set();
        }

        [Test]
        public void CmdAddNewPlayer_ShouldExecuteAddNewPlayerUseCase()
        {
            // Given
            var player = new Player("Player Name");

            // When
            _playerController.CmdAddNewPlayer(player);

            // Then
            _addNewPlayer.Received().Execute(Arg.Is<Player>(addedPlayer => addedPlayer == player));
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
            _tryToDefuse.Received().Try(Arg.Is<Player>(defuserPlayer => defuserPlayer == player));
        }
    }
}