using System.Collections.Generic;
using Main.Domain.UI;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.Domain.UI
{
    public class ViewManagerTest
    {
        [Test]
        public void ActiveLayers_ShouldActiveAllLayersCorrespondingToView()
        {
            // Given
            var aViewLayer = Substitute.For<IViewLayer>();
            aViewLayer.GetView().Returns(View.HOME);
            
            var anotherViewLayer = Substitute.For<IViewLayer>();
            anotherViewLayer.GetView().Returns(View.HOME);
            
            var aViewLayerForOtherView = Substitute.For<IViewLayer>();
            aViewLayerForOtherView.GetView().Returns(View.LOBBY);
            
            var viewManager = new ViewManager(new List<IViewLayer> {aViewLayer, anotherViewLayer});

            // When
            viewManager.ActiveLayers(View.HOME);

            // Then
            aViewLayer.Received().Active();
            anotherViewLayer.Received().Active();
            aViewLayerForOtherView.DidNotReceive().Active();
        }
    }
}