using Main.Domain.UI;

namespace Main.UseCases.UI
{
    public class ChangeCurrentView
    {
        private readonly IViewManager _viewManager;

        public ChangeCurrentView(IViewManager viewManager)
        {
            _viewManager = viewManager;
        }

        public virtual void Change(View newCurrentView)
        {
            _viewManager.DisableActiveLayers();
            _viewManager.EnableLayers(newCurrentView);
        }
    }
}