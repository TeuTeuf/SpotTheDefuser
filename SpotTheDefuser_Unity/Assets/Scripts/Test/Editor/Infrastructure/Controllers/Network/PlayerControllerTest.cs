using Main.Domain.Players;
using Main.Infrastructure.Controllers;
using Main.Infrastructure.Controllers.Network;
using Main.UseCases.Players;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Test.Editor.Infrastructure.Controllers.Network
{
    public class PlayerControllerTest {
        
        [Test]
        public void OnStartLocalPlayer_ShouldSetLocalPlayerControlerOnNetworkControllers()
        {
            // Given
            var networkControllers = new NetworkControllers();
            
            var playerController = new GameObject().AddComponent<PlayerController>();
            playerController.NetworkControllers = networkControllers;
            
            // When
            playerController.OnStartLocalPlayer();
            
            // Then
            Assert.AreSame(playerController, networkControllers.LocalPlayerController);
        }

        [Test]
        public void OnStartServer_ShouldAddPlayerControllerToPlayerControllersInNetworkControllers()
        {
            // Given
            var networkControllers = new NetworkControllers();
            
            var playerController1 = new GameObject().AddComponent<PlayerController>();
            playerController1.NetworkControllers = networkControllers;
            
            var playerController2 = new GameObject().AddComponent<PlayerController>();
            playerController2.NetworkControllers = networkControllers;
            
            // When
            playerController1.OnStartServer();
            playerController2.OnStartServer();
            
            // Then
            Assert.Contains(playerController1, networkControllers.GetPlayerControllersOnServer());
            Assert.Contains(playerController2, networkControllers.GetPlayerControllersOnServer());
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
