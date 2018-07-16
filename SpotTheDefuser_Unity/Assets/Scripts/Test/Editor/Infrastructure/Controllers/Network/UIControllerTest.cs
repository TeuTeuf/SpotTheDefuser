using Main.Infrastructure.Controllers.Network;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Test.Editor.Infrastructure.Controllers.Network
{
    [TestFixture]
    public class UIControllerTest
    {
        private UIController _uiController;
        private AllPlayerControllers _allPlayerControllers;
        
        [SetUp]
        public void Init()
        {
            _allPlayerControllers = Substitute.For<AllPlayerControllers>();
            _uiController = new GameObject().AddComponent<UIController>();
            _uiController.AllPlayerControllers = _allPlayerControllers;
        }
        
        [Test]
        public void OnClickOnNewDefuseAttempt_ShouldExecuteSetNewDefuseAttemptOnServer_OnAllPlayerControllers()
        {
            // When
            _uiController.OnClickOnNewDefuseAttempt();

            // Then
            _allPlayerControllers.Received().SetNewDefuseAttemptOnServer();
        }

        [Test]
        public void OnEndEditOnPlayerName_ShouldSetPlayerNameProperty()
        {
            // Given
            const string playerName = "playerName";

            // When
            _uiController.OnEndEditOnPlayerName(playerName);
            
            // Then
            Assert.AreEqual(playerName, _uiController.PlayerName);
        }

        [Test]
        public void OnClickOnAddPlayer_ShouldAddNewPlayerOnServer_OnAllPlayerControllers()
        {
            // Given
            const string playerName = "playerName";
            _uiController.OnEndEditOnPlayerName(playerName);

            // When
            _uiController.OnClickOnAddPlayer();
            
            // Then
            _allPlayerControllers.Received().AddNewPlayerOnServer(playerName);
        }

        [Test]
        public void OnClickOnTryToDefuse_ShouldTryToDefuseOnServer_OnAllPlayerControllers()
        {
            // When
            _uiController.OnClickOnTryToDefuse();
            
            // Then
            _allPlayerControllers.Received().TryToDefuseOnServer();
        }
    }
}