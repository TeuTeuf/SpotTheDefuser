using System.Collections.Generic;

namespace Main.Domain.UI
{
    public class ViewManager : IViewManager
    {
        private readonly IDictionary<View, List<IViewLayer>> _viewLayersByView;
        
        private List<IViewLayer> _activeViewLayers;
        
        public ViewManager(IEnumerable<IViewLayer> allViewLayers)
        {
            _viewLayersByView = new Dictionary<View, List<IViewLayer>>();

            foreach (var layer in allViewLayers)
            {
                layer.Disable();
                _RegisterViewLayerByView(layer);
            }
        }

        public void EnableLayers(View view)
        {
            _viewLayersByView[view].ForEach(layer => layer.Enable());
            _activeViewLayers = _viewLayersByView[view];
        }

        public void DisableActiveLayers()
        {
            _activeViewLayers?.ForEach(layer => layer.Disable());
            _activeViewLayers = null;
        }

        private void _RegisterViewLayerByView(IViewLayer layer)
        {
            var view = layer.GetView();
            if (!_viewLayersByView.ContainsKey(view))
            {
                _viewLayersByView.Add(view, new List<IViewLayer>());
            }

            _viewLayersByView[view].Add(layer);
        }
    }
}