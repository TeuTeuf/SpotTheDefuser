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
        public void OnClickOnNewDefuseAttempt_ShouldExecuteSetNewDefuseAttemptOnServer_OnAllPlayerControllers()
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

        [Test]
        public void OnEndEditOnPlayerName_ShouldSetPlayerNameVariable()
        {
            // Given
            const string playerName = "playerName";
            var uiController = new GameObject().AddComponent<UIController>();

            // When
            uiController.OnEndEditOnPlayerName(playerName);
            
            // Then
            Assert.AreEqual(playerName, uiController.PlayerName);
        }
    }
}