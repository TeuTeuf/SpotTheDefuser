using Main.Domain;
using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using Main.Infrastructure.Controllers.Network;
using Main.UseCases.DefuseAttempts;
using Main.UseCases.Players;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Test.Editor.Infrastructure.Controllers.Network
{
    public class PlayerControllerTest
    {
        private PlayerController _playerController;

        private AllPlayerControllers _allPlayerControllers;
        private IRandom _random;
        private AllPlayers _allPlayers;
        private DefusingState _defusingState;
        private SetNewDefuseAttempt _setNewDefuseAttempt;
        private AddNewPlayer _addNewPlayer;

        [SetUp]
        public void Init()
        {
            _random = Substitute.For<IRandom>();

            _allPlayerControllers = new AllPlayerControllers();
            _allPlayers = Substitute.For<AllPlayers>();
            _defusingState = Substitute.For<DefusingState>();
            
            _setNewDefuseAttempt = Substitute.For<SetNewDefuseAttempt>(_random, _allPlayers, _defusingState);
            _addNewPlayer = Substitute.For<AddNewPlayer>(_allPlayers);
            
            _playerController = new GameObject().AddComponent<PlayerController>();
            _playerController.AllPlayerControllers = _allPlayerControllers;
            _playerController.SetDefuseAttempt = _setNewDefuseAttempt;
            _playerController.AddNewPlayer = _addNewPlayer;
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
        public void CmdAddNewPlayer_ShouldSetPlayerProperty()
        {
            // Given
            const string playerName = "Player Name";

            // When
            _playerController.CmdAddNewPlayer(playerName);

            // Then
            Assert.AreEqual(playerName, _playerController.Player.Name);
        }
    }
}