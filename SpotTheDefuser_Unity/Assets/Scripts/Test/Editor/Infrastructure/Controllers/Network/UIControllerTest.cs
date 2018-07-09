using Main.Infrastructure.Controllers.Network;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Test.Editor.Infrastructure.Controllers.Network
{
    [TestFixture]
    public class UIControllerTest
    {
        [Test]
        public void ClickOnNewDefuseAttempt_ShouldExecuteSetNewDefuseAttemptOnServer_OnNetworkControllers()
        {
            // Given
            var networkControllers = Substitute.For<NetworkControllers>();

            var uiController = new GameObject().AddComponent<UIController>();
            uiController.NetworkControllers = networkControllers;

            // When
            uiController.ClickOnNewDefuseAttempt();

            // Then
            networkControllers.SetNewDefuseAttemptOnServer();
        }
    }
}