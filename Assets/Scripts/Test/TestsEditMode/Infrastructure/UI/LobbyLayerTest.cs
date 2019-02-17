using Main.Domain.UI;
using Main.Infrastructure.UI;
using NUnit.Framework;
using UnityEngine;

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
        }
        
        [Test]
        public void GetView_ShouldReturnLobbyView()
        {
            // Then
            Assert.That(_lobbyLayer.GetView(), Is.EqualTo(View.Lobby));
        }
    }
}