using Main.Domain.UI;
using Main.Infrastructure.Controllers.Network;
using Main.UseCases.UI;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Test.TestsEditMode.Infrastructure.Controllers.Network
{
    [TestFixture]
    public class UIControllerTest
    {
        private UIController _uiController;
        private AllPlayerControllers _allPlayerControllers;
        private ChangeCurrentView _changeCurrentView;

        [SetUp]
        public void Init()
        {
            var viewManager = Substitute.For<IViewManager>();
            _changeCurrentView = Substitute.For<ChangeCurrentView>(viewManager);
            _allPlayerControllers = Substitute.For<AllPlayerControllers>();
            
            _uiController = new GameObject().AddComponent<UIController>();
            _uiController.Init(_allPlayerControllers, _changeCurrentView);
        }

        [Test]
        public void Start_ShouldChangeCurrentViewToStartingView()
        {
            // Given
            _uiController.StartingView = View.Lobby;

            // When
            _uiController.Start();

            // Then
            _changeCurrentView.Received().Change(View.Lobby);
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
        public void OnClickOnTryToDefuse_ShouldTryToDefuseOnServer_OnAllPlayerControllers()
        {
            // When
            _uiController.OnClickOnTryToDefuse();
            
            // Then
            _allPlayerControllers.Received().TryToDefuseOnServer();
        }
    }
}