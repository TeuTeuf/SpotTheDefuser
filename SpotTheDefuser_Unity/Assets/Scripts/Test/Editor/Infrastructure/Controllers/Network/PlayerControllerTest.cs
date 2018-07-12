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
    public class PlayerControllerTest {
        
        [Test]
        public void OnStartLocalPlayer_ShouldSetLocalPlayerControlerOnAllPlayerControllers()
        {
            // Given
            var allPlayerControllers = new AllPlayerControllers();
            
            var playerController = new GameObject().AddComponent<PlayerController>();
            playerController.AllPlayerControllers = allPlayerControllers;
            
            // When
            playerController.OnStartLocalPlayer();
            
            // Then
            Assert.AreSame(playerController, allPlayerControllers.LocalPlayerController);
        }

        [Test]
        public void OnStartServer_ShouldAddPlayerControllerToPlayerControllersInAllPlayerControllers()
        {
            // Given
            var allPlayersControllers = new AllPlayerControllers();
            
            var playerController1 = new GameObject().AddComponent<PlayerController>();
            playerController1.AllPlayerControllers = allPlayersControllers;
            
            var playerController2 = new GameObject().AddComponent<PlayerController>();
            playerController2.AllPlayerControllers = allPlayersControllers;
            
            // When
            playerController1.OnStartServer();
            playerController2.OnStartServer();
            
            // Then
            Assert.Contains(playerController1, allPlayersControllers.GetPlayerControllersOnServer());
            Assert.Contains(playerController2, allPlayersControllers.GetPlayerControllersOnServer());
        }

        [Test]
        public void CmdSetNewDefuseAttempt_ShouldExecuteSetNewDefuseAttemptUseCase()
        {
            // Given
            var random = Substitute.For<IRandom>();
            var playerRepository = Substitute.For<PlayerRepository>();
            var defusingState = Substitute.For<DefusingState>();
            var setNewDefuseAttempt = Substitute.For<SetNewDefuseAttempt>(random, playerRepository, defusingState);
            
            var playerController = new GameObject().AddComponent<PlayerController>();
            playerController.SetDefuseAttempt = setNewDefuseAttempt;
            
            // When
            playerController.CmdSetNewDefuseAttempt();
            
            // Then
            setNewDefuseAttempt.Received().Set();
        }
        
        [Test]
        public void Start_ShouldExecuteAddNewPlayerUseCaseWithNewPlayerObject()
        {
            // Given
            var playersRepository = Substitute.For<PlayerRepository>();
            var mockAddNewPlayer = Substitute.For<AddNewPlayer>(playersRepository);

            var playerController = new GameObject().AddComponent<PlayerController>();
            playerController.AddNewPlayer = mockAddNewPlayer;

            // When
            playerController.Start();

            // Then
            mockAddNewPlayer
                .Received()
                .Execute(Arg.Any<Player>());
        }

        [Test]
        public void OnDestroy_ShouldExecuteRemovePlayerUseCaseWithPlayerPreviouslyAddedOnStart()
        {
            // Given
            var playersRepository = Substitute.For<PlayerRepository>();
            var mockAddNewPlayer = Substitute.For<AddNewPlayer>(playersRepository);
            var mockRemovePlayer = Substitute.For<RemovePlayer>(playersRepository);
            
            var playerController = new GameObject().AddComponent<PlayerController>();
            playerController.AddNewPlayer = mockAddNewPlayer;
            playerController.RemovePlayer = mockRemovePlayer;

            Player playerAdded = null;
            mockAddNewPlayer
                .When(addNewPlayer => addNewPlayer.Execute(Arg.Any<Player>()))
                .Do(info => playerAdded = (Player) info[0]);
            playerController.Start();
            
            // When
            playerController.OnDestroy();
            
            // Then
            mockRemovePlayer
                .Received()
                .Execute(playerAdded);
        }
    }
}
