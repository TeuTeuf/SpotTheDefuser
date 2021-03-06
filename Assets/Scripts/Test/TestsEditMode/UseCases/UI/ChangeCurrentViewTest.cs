using Main.Domain.UI;
using Main.UseCases.UI;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.UseCases.UI
{
    public class ChangeCurrentViewTest
    {
        private ChangeCurrentView _changeCurrentView;
        private IViewManager _viewManager;
        
        [SetUp]
        public void Init()
        {
            _viewManager = Substitute.For<IViewManager>();
            _changeCurrentView = new ChangeCurrentView(_viewManager);
        }

        [Test]
        public void Change_ShouldReplaceCurrentLayersToGivenView()
        {
            // Given
            const View view = View.Home;

            // When
            _changeCurrentView.Change(view);

            // Then
            _viewManager
                .Received()
                .ReplaceCurrentLayers(view);
        }
    }
}