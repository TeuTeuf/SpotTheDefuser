using Main.Domain.Network;
using Main.Domain.Players;
using Main.Domain.UI;
using Main.Infrastructure.UI;
using Main.UseCases.Network;
using Main.UseCases.UI;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Test.TestsEditMode.Infrastructure.UI
{
    [TestFixture]
    public class HomeLayerTest
    {
        private HomeLayer _homeLayer;
        
        private HostNewGame _hostNewGame;
        private StartWaitingForNewGame _startWaitingForNewGame;
        private ChangeCurrentView _changeCurrentView;
        private IViewManager _viewManager;


        [SetUp]
        public void Init()
        {
            PlayerPrefs.DeleteAll();
            
            _viewManager = Substitute.For<IViewManager>();

            var allPlayers = new AllPlayers();
            
            var networkManager = Substitute.For<ISpotTheDefuserNetworkManager>();
            var networkDiscovery = Substitute.For<ISpotTheDefuserNetworkDiscovery>();
            
            _hostNewGame = Substitute.For<HostNewGame>(networkManager, networkDiscovery, _viewManager, allPlayers);
            _startWaitingForNewGame = Substitute.For<StartWaitingForNewGame>(networkDiscovery, _viewManager, allPlayers);
            _changeCurrentView = Substitute.For<ChangeCurrentView>(_viewManager);
            
            _homeLayer = new GameObject().AddComponent<HomeLayer>();
            _homeLayer.Init(_changeCurrentView, _hostNewGame, _startWaitingForNewGame);

            _homeLayer.playerNameInputField = new GameObject().AddComponent<InputField>();
        }

        [Test]
        public void OnStart_ShouldDisplaySavedPlayerName()
        {
            // Given
            
            const string playerName = "Player Name";
            PlayerPrefs.SetString(HomeLayer.PLAYER_NAME_KEY, playerName);

            // When
            _homeLayer.Start();

            // Then
            Assert.That(_homeLayer.playerNameInputField.text, Is.EqualTo(playerName));
        }

        [Test]
        public void OnEndEditOnPlayerName_OnClickOnHost_ShouldStartHostingNewGameForGivenPlayerName()
        {
            // Given
            const string playerName = "Player Name";

            // When
            _homeLayer.OnEndEditOnPlayerName(playerName);
            _homeLayer.OnClickOnHost();


            // Then
            _hostNewGame.Received().Host(playerName);
        }

        [Test]
        public void OnStart_OnClickOnHost_ShouldStartHostingNewGameForSavedPlayerName()
        {
            // Given
            const string playerName = "Player Name";
            PlayerPrefs.SetString(HomeLayer.PLAYER_NAME_KEY, playerName);

            // When
            _homeLayer.Start();
            _homeLayer.OnClickOnHost();


            // Then
            _hostNewGame.Received().Host(playerName);
        }

        [Test]
        public void OnEndEditOnPlayerName_OnClickOnJoin_ShouldStartWaitingForNewGame()
        {
            // Given
            const string playerName = "Player Name";
            
            // When
            _homeLayer.OnEndEditOnPlayerName(playerName);
            _homeLayer.OnClickOnJoin();
            
            // Then
            _startWaitingForNewGame.Received().Start(playerName);
        }

        [Test]
        public void OnEndEditOnPlayerName_ShouldUpdatePlayerPrefs()
        {
            // Given
            const string playerName = "Player Name";
            
            // When
            _homeLayer.OnEndEditOnPlayerName(playerName);


            // Then
            Assert.That(PlayerPrefs.GetString(HomeLayer.PLAYER_NAME_KEY), Is.EqualTo(playerName));
        }

        [Test]
        public void OnClickOnHowToPlay_ShouldDisplayHowToPlayScreen()
        {
            // When
            _homeLayer.OnClickOnHowToPlay();
            
            // Then
            _changeCurrentView.Received().Change(View.HowToPlay);
        }

        [Test]
        public void GetView_ShouldReturnHomeView()
        {
            // Then
            Assert.That(_homeLayer.GetView(), Is.EqualTo(View.Home));
        }
    }
}