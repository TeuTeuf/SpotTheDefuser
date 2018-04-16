using NUnit.Framework;
using SpotTheDefuser_Unity.Assets.Scripts.Main.UseCases;
using NSubstitute;
using SpotTheDefuser_Unity.Assets.Scripts.Main.Domain;

public class PlayerControllerTest {

    [Test]
    public void Start_shouldExecuteAddNewPlayerUseCaseWithNewPlayerObject()
    {
        // Given
        IPlayersRepository playersRepository = Substitute.For<IPlayersRepository>();
        AddNewPlayer mockAddNewPlayer = Substitute.For<AddNewPlayer>(playersRepository);

        PlayerController playerController = new PlayerController();
        playerController.addNewPlayer = mockAddNewPlayer;

        // When
        playerController.Start();

        // Then
        mockAddNewPlayer
            .Received()
            .Execute(Arg.Is<Player>(player => player.pseudo == "Player"));
    }

}
