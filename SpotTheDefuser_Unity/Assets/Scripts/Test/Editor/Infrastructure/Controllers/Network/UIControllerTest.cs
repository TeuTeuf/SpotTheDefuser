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
        public void ClickOnNewDefuseAttempt_ShouldExecuteSetNewDefuseAttemptOnServer_OnAllPlayerControllers()
        {
            // Given
            var allPlayerControllers = Substitute.For<AllPlayerControllers>();

            var uiController = new GameObject().AddComponent<UIController>();
            uiController.AllPlayerControllers = allPlayerControllers;

            // When
            uiController.OnClickOnNewDefuseAttempt();

            // Then
            allPlayerControllers.SetNewDefuseAttemptOnServer();
        }
    }
}