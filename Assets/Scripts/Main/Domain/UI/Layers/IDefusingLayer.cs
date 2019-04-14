namespace Main.Domain.UI.Layers
{
    public interface IDefusingLayer
    {
        void UpdateDisplayedBomb(string bombId, bool isPlayerDefuser);
        void UpdateTimer(float remainingTime);
    }
}