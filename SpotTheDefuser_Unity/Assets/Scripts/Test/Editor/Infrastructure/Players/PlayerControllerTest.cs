using System.Collections.Generic;
using Main.Domain.Players;
using Main.Infrastructure.Players;
using Main.UseCases.Players;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using UnityEngine;

namespace Test.Editor.Infrastructure.Players
{
    public class PlayerControllerTest {
        
        [Test]
        public void Start_shouldExecuteAddNewPlayerUseCaseWithNewPlayerObject()
        {
            // Given
            var playersRepository = Substitute.For<IPlayersRepository>();
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
        public void OnDestroy_shouldExecuteRemovePlayerUseCaseWithPlayerPreviouslyAddedOnStart()
        {
            // Given
            var playersRepository = Substitute.For<IPlayersRepository>();
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
