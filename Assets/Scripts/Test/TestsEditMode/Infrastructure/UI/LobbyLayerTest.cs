using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using Main.Domain.UI;
using Main.Infrastructure.Controllers.Network;
using Main.Infrastructure.UI;
using NSubstitute;
using NSubstitute.Exceptions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Test.TestsEditMode.Infrastructure.UI
{
    [TestFixture]
    public class LobbyLayerTest
    {
        private LobbyLayer _lobbyLayer;
        private DefuserCounter _defuserCounter;
        private AllPlayerControllers _allPlayerControllers;

        [SetUp]
        public void Init()
        {
            _defuserCounter = Substitute.For<DefuserCounter>();
            _allPlayerControllers = Substitute.For<AllPlayerControllers>(Substitute.For<AllPlayers>());
            _lobbyLayer = new GameObject().AddComponent<LobbyLayer>();
            
            _lobbyLayer.Init(_defuserCounter, _allPlayerControllers);
            _lobbyLayer.nbDefusersText = new GameObject().AddComponent<Text>();
            _lobbyLayer.nbBombsText = new GameObject().AddComponent<Text>();
            _lobbyLayer.playButton = new GameObject().AddComponent<Button>();
        }

        [Test]
        public void Start_ShouldSetNbDefusersTo0AndBombsTo1()
        {
            // When
            _lobbyLayer.Start();

            // Then
            Assert.That(_lobbyLayer.nbDefusersText.text, Is.EqualTo("0"));
            Assert.That(_lobbyLayer.nbBombsText.text, Is.EqualTo("1"));
        }

        [Test]
        public void Start_ShouldSetPlayButtonNotInteractable()
        {
            // When
            _lobbyLayer.Start();
            
            // Then
            Assert.That(_lobbyLayer.playButton.interactable, Is.False);
        }
        
        [Test]
        public void GetView_ShouldReturnLobbyView()
        {
            // Then
            Assert.That(_lobbyLayer.GetView(), Is.EqualTo(View.Lobby));
        }

        [Test]
        public void UpdatePlayerList_ShouldSetCorrectNumberOfPlayerBombsAndDefusers()
        {
            // Given
            var players = new[] { new Player("player A"), new Player("player B"), new Player("player C")};
            
            _defuserCounter
                .GetNumberOfDefuserPlayers(players.Length)
                .Returns(1);
            
            _defuserCounter
                .GetNumberOfBombPlayers(players.Length)
                .Returns(2);
            
            // When
            _lobbyLayer.UpdatePlayerList(players);
            
            // Then
            Assert.That(_lobbyLayer.nbDefusersText.text, Is.EqualTo("1"));
            Assert.That(_lobbyLayer.nbBombsText.text, Is.EqualTo("2"));
        }

        [Test]
        public void UpdatePlayerList_ShouldEnablePlayButtonWhenOneDefuser()
        {
            // Given
            var players = new[] { new Player("player A"), new Player("player B"), new Player("player C")};

            _defuserCounter
                .GetNumberOfDefuserPlayers(players.Length)
                .Returns(1);

            _lobbyLayer.Start();
            
            // When
            _lobbyLayer.UpdatePlayerList(players);
            
            // Then
            Assert.That(_lobbyLayer.playButton.interactable, Is.True);
        }
        
        [Test]
        public void UpdatePlayerList_ShouldEnablePlayButtonWhenMoreThanOneDefuser()
        {
            // Given
            var players = new[] { new Player("player A"), new Player("player B"), new Player("player C")};

            _defuserCounter
                .GetNumberOfDefuserPlayers(players.Length)
                .Returns(3);

            _lobbyLayer.Start();
            
            // When
            _lobbyLayer.UpdatePlayerList(players);
            
            // Then
            Assert.That(_lobbyLayer.playButton.interactable, Is.True);
        }

        [Test]
        public void UpdatePlayerList_ShouldNotEnablePlayButtonNoDefuser()
        {
            // Given
            var players = new[] { new Player("player A"), new Player("player B"), new Player("player C")};

            _defuserCounter
                .GetNumberOfDefuserPlayers(players.Length)
                .Returns(0);

            _lobbyLayer.Start();
            
            // When
            _lobbyLayer.UpdatePlayerList(players);
            
            // Then
            Assert.That(_lobbyLayer.playButton.interactable, Is.False);
        }

        [Test]
        public void OnClickOnPlay_ShouldStartNewGameOnAllPlayerControllers()
        {
            // When
            _lobbyLayer.OnClickOnPlay();
            
            // Then
            _allPlayerControllers
                .Received()
                .StartNewGameOnServer();
        }
    }
}