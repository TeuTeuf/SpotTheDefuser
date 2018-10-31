using Main.Infrastructure.UI;
using Main.UseCases;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Test.TestsEditMode.Infrastructure.UI
{
    public class HomeLayerTest
    {
        private HostNewGame _hostNewGame;
        private HomeLayer _homeLayer;

        [SetUp]
        public void Init()
        {
            _hostNewGame = Substitute.For<HostNewGame>();
            _homeLayer = new GameObject().AddComponent<HomeLayer>();
            _homeLayer.Init(_hostNewGame);
        }

        [Test]
        public void OnClickOnHost_OnEndEditOnPlayerName_ShouldCallUseCaseWithPlayerNameInput()
        {
            // Given
            var playerName = "Player Name";

            // When
            _homeLayer.OnEndEditOnPlayerName(playerName);
            _homeLayer.OnClickOnHost();

            // Then
            _hostNewGame
                .Received()
                .Host(playerName);
        }   
    }
}