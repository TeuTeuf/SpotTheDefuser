using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using Main.Domain.UI;
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

        [SetUp]
        public void Init()
        {
            _defuserCounter = Substitute.For<DefuserCounter>();
            _lobbyLayer = new GameObject().AddComponent<LobbyLayer>();
            
            _lobbyLayer.Init(_defuserCounter);
            _lobbyLayer.nbDefusersText = new GameObject().AddComponent<Text>();
            _lobbyLayer.nbBombsText = new GameObject().AddComponent<Text>();
        }

        [Test]
        public void Start_ShouldSetNbDefusersTo1AndBombsTo0()
        {
            // When
            _lobbyLayer.Start();

            // Then
            Assert.That(_lobbyLayer.nbDefusersText.text, Is.EqualTo("1"));
            Assert.That(_lobbyLayer.nbBombsText.text, Is.EqualTo("0"));
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
                .Returns(2);
            
            _defuserCounter
                .GetNumberOfBombPlayers(players.Length)
                .Returns(1);
            
            // When
            _lobbyLayer.UpdatePlayerList(players);
            
            // Then
            Assert.That(_lobbyLayer.nbDefusersText.text, Is.EqualTo("2"));
            Assert.That(_lobbyLayer.nbBombsText.text, Is.EqualTo("1"));
        }
    }
}