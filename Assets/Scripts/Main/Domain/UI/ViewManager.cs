using System.Collections.Generic;

namespace Main.Domain.UI
{
    public class ViewManager : IViewManager
    {
        private readonly List<IViewLayer> _viewLayers;

        public ViewManager(List<IViewLayer> viewLayers)
        {
            _viewLayers = viewLayers;
        }

        public void ActiveLayers(View view)
        {
            _viewLayers.ForEach(layer => 
            {
                if (view == layer.GetView())
                {
                    layer.Active();
                }
            });
        }

        public void DisableActiveLayers()
        {
            throw new System.NotImplementedException();
        }
    }
}