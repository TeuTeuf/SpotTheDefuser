namespace Main.Domain.UI
{
    public interface IViewLayer
    {
        void Enable();
        void Disable();
        View GetView();
    }
}