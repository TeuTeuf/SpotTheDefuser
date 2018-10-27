namespace Main.Domain.UI
{
    public interface IViewLayer
    {
        View View { get; }
        void Enable();
        void Disable();
    }
}