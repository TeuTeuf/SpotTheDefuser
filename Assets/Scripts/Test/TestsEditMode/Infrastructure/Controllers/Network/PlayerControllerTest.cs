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
            
            _allPlayerControllers = new AllPlayerControllers();
            
            _playerController = new GameObject().AddComponent<PlayerController>();
            _playerController.AllPlayerControllers = _allPlayerControllers;
            _playerController.SetDefuseAttempt = _setNewDefuseAttempt;
            _playerController.AddNewPlayer = _addNewPlayer;
            _playerController.TryToDefuse = _tryToDefuse;
        }

        [Test]
        public void OnStartLocalPlayer_ShouldSetLocalPlayerControlerOnAllPlayerControllers()
        {
            // When
            _playerController.OnStartLocalPlayer();

            // Then
            Assert.AreSame(_playerController, _allPlayerControllers.LocalPlayerController);
        }

        [Test]
        public void OnStartServer_ShouldAddPlayerControllerToPlayerControllersInAllPlayerControllers()
        {
            // Given
            var otherPlayerController = new GameObject().AddComponent<PlayerController>();
            otherPlayerController.AllPlayerControllers = _allPlayerControllers;

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
            const string playerName = "Player Name";

            // When
            _playerController.CmdAddNewPlayer(playerName);

            // Then
            _addNewPlayer.Received().Execute(Arg.Is<Player>(player => player.Name == playerName));
        }
        
        [Test]
        public void CmdTryToDefuse_ShouldExecuteTryToDefuseUseCase()
        {
            // Given
            const string playerName = "Player Name";

            _playerController.CmdAddNewPlayer(playerName);
            
            // When
            _playerController.CmdTryToDefuse();
            
            // Then
            _tryToDefuse.Received().Try(Arg.Is<Player>(player => player.Name == playerName));
        }
    }
}