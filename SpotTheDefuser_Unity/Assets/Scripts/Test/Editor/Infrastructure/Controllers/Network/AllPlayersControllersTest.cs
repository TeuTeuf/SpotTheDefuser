using Main.Infrastructure.Controllers.Network;
using NSubstitute;
using NUnit.Framework;

namespace Test.Editor.Infrastructure.Controllers.Network
{
    [TestFixture]
    public class AllPlayersControllersTest
    {
        [Test]
        public void SetNewDefuseAttemptOnServer_ShouldExecuteCmdSetNewDefuseAttempt_OnLocalPlayerController()
        {
            // Given
            var localPlayerController = Substitute.For<IPlayerController>();
            var allPlayerControllers = new AllPlayerControllers {LocalPlayerController = localPlayerController};

            // When
            allPlayerControllers.SetNewDefuseAttemptOnServer();

            // Then
            localPlayerController.Received().CmdSetNewDefuseAttempt();
        }
    }
}