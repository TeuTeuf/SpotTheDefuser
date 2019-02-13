using Main.Domain.Players;
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
        private ChangeCurrentView _changeCurrentView;

        [SetUp]
        public void Init()
        {
            var viewManager = Substitute.For<IViewManager>();
            _changeCurrentView = Substitute.For<ChangeCurrentView>(viewManager);
            
            _uiController = new GameObject().AddComponent<UIController>();
            _uiController.Init(_changeCurrentView);
        }

        [Test]
        public void Start_ShouldChangeCurrentViewToStartingView()
        {
            // Given
            _uiController.startingView = View.Lobby;

            // When
            _uiController.Start();

            // Then
            _changeCurrentView.Received().Change(View.Lobby);
        }
    }
}