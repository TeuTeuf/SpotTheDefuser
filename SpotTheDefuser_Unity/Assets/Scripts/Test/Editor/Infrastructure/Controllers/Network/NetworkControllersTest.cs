using Main.Infrastructure.Controllers.Network;
using NSubstitute;
using NUnit.Framework;

namespace Test.Editor.Infrastructure.Controllers.Network
{
    [TestFixture]
    public class NetworkControllersTest
    {
        [Test]
        public void SetNewDefuseAttemptOnServer_ShouldExecuteCmdSetNewDefuseAttempt_OnLocalPlayerController()
        {
            // Given
            var localPlayerController = Substitute.For<PlayerController>();
            var networkControllers = new NetworkControllers {LocalPlayerController = localPlayerController};

            // When
            networkControllers.SetNewDefuseAttemptOnServer();

            // Then
            localPlayerController.Received().CmdSetNewDefuseAttempt();
        }
    }
}