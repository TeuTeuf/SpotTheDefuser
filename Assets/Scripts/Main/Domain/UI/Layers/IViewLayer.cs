namespace Main.Domain.UI.Layers
{
    public interface IViewLayer
    {
        void Enable();
        void Disable();
        View GetView();
    }
}