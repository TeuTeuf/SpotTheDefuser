namespace Main.Domain.UI
{
    public interface IViewManager
    {
        void EnableLayers(View view);
        void DisableActiveLayers();
        void ReplaceCurrentLayers(View view);
    }
}