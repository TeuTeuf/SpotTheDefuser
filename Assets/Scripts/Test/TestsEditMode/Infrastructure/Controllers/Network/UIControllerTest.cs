using Main.Domain.Players;
using Main.Domain.UI;
using Main.Domain.UI.Layers;
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
        private ChangeCurrentView _changeCurrentView;
        private ILobbyLayer _lobbyLayer;
        private IDefusingLayer _defusingLayer;
        private IEndLayer _endLayer;

        [SetUp]
        public void Init()
        {
            var viewManager = Substitute.For<IViewManager>();
            _changeCurrentView = Substitute.For<ChangeCurrentView>(viewManager);

            _lobbyLayer = Substitute.For<ILobbyLayer>();
            _defusingLayer = Substitute.For<IDefusingLayer>();
            _endLayer = Substitute.For<IEndLayer>();

            _uiController = new GameObject().AddComponent<UIController>();
            _uiController.Init(_changeCurrentView, _lobbyLayer, _defusingLayer, _endLayer);
        }

        [Test]
        public void Start_ShouldChangeCurrentViewToStartingView()
        {
            // Given
            _uiController.startingView = View.Lobby;

            // When
            _uiController.Start();

            // Then
            _changeCurrentView
                .Received()
                .Change(View.Lobby);
        }

        [Test]
        public void UpdateLobby_ShouldUpdatePlayerListOfLobbyLayer()
        {
            // Given
            var players = new[]
            {
                new Player("Player1"),
                new Player("Player2"),
            };

            // When
            _uiController.UpdateLobby(players);

            // Then
            _lobbyLayer
                .Received()
                .UpdatePlayerList(players);
        }

        [Test]
        public void UpdateDefusing_ShouldUpdateBombOfDefusingLayer()
        {
            // Given
            const string bombId = "bombId";
            const bool isPlayerDefuser = true;

            // When
            _uiController.UpdateDefusing(bombId, isPlayerDefuser);

            // Then
            _defusingLayer
                .Received()
                .UpdateDisplayedBomb(bombId, isPlayerDefuser);
        }

        [Test]
        public void UpdateEnd_ShouldUpdateNbBombsDefusedOfEndLayer()
        {
            // Given
            const int nbBombsDefused = 42;

            // When
            _uiController.UpdateEnd(nbBombsDefused);

            // Then
            _endLayer
                .Received()
                .UpdateNbBombsDefused(nbBombsDefused);
        }
    }
}