using Main.Domain.UI;
using Main.Infrastructure.UI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Test.TestsEditMode.Infrastructure.UI
{
    [TestFixture]
    public class LobbyLayerTest
    {
        private LobbyLayer _lobbyLayer;

        [SetUp]
        public void Init()
        {
            _lobbyLayer = new GameObject().AddComponent<LobbyLayer>();
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
    }
}