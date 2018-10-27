using System.Collections.Generic;

namespace Main.Domain.UI
{
    public class ViewManager : IViewManager
    {
        private readonly Dictionary<View, List<IViewLayer>> _viewLayersByView;

        public ViewManager(List<IViewLayer> allViewLayers)
        {
            _viewLayersByView = new Dictionary<View, List<IViewLayer>>();

            foreach (var layer in allViewLayers)
            {
                var view = layer.GetView();
                if (! _viewLayersByView.ContainsKey(view))
                {
                    _viewLayersByView.Add(view, new List<IViewLayer>());
                }
                _viewLayersByView[view].Add(layer);
            }
        }

        public void ActiveLayers(View view)
        {
            _viewLayersByView[view].ForEach(layer => layer.Active());
        }

        public void DisableActiveLayers()
        {
            throw new System.NotImplementedException();
        }
    }
}