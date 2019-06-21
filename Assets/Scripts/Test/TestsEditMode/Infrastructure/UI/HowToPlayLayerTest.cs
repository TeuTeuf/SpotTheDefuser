using Main.Domain.UI;
using Main.Infrastructure.UI;
using Main.UseCases.UI;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Test.TestsEditMode.Infrastructure.UI
{
    [TestFixture]
    public class HowToPlayLayerTest
    {
        private HowToPlayLayer _howToPlayLayer;
        private ChangeCurrentView _changeCurrentView;

        [SetUp]
        public void Init()
        {
            _changeCurrentView = Substitute.For<ChangeCurrentView>(Substitute.For<IViewManager>());
            _howToPlayLayer = new GameObject().AddComponent<HowToPlayLayer>();
            _howToPlayLayer.Init(_changeCurrentView);
        }

        [Test]
        public void OnClickOnBack_ShouldChangeCurrentViewToHome()
        {
            // When
            _howToPlayLayer.OnClickOnBack();
            
            // Then
            _changeCurrentView.Received().Change(View.Home);
        }

        [Test]
        public void GetView_ShouldReturnHowToPlayView()
        {
            // Then
            Assert.That(_howToPlayLayer.GetView(), Is.EqualTo(View.HowToPlay));
        }
    }
}