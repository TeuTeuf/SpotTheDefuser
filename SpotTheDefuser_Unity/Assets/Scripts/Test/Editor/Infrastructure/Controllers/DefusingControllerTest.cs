using Main.Domain.Players;
using Main.Infrastructure.Controllers;
using Main.UseCases.Players;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Test.Editor.Infrastructure.Controllers
{
    public class DefusingControllerTest {
        
        [Test]
        public void Start_shouldExecuteAddNewPlayerUseCaseWithNewPlayerObject()
        {
            // Given
            var playersRepository = Substitute.For<PlayerRepository>();
            var mockAddNewPlayer = Substitute.For<AddNewPlayer>(playersRepository);

            var playerController = new GameObject().AddComponent<DefusingController>();
            playerController.AddNewPlayer = mockAddNewPlayer;

            // When
            playerController.Start();

            // Then
            mockAddNewPlayer
                .Received()
                .Execute(Arg.Any<Player>());
        }

        [Test]
        public void OnDestroy_shouldExecuteRemovePlayerUseCaseWithPlayerPreviouslyAddedOnStart()
        {
            // Given
            var playersRepository = Substitute.For<PlayerRepository>();
            var mockAddNewPlayer = Substitute.For<AddNewPlayer>(playersRepository);
            var mockRemovePlayer = Substitute.For<RemovePlayer>(playersRepository);
            
            var playerController = new GameObject().AddComponent<DefusingController>();
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
