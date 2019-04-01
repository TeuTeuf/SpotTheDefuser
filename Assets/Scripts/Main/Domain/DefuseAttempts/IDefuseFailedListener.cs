using Main.Domain.Players;

namespace Main.Domain.DefuseAttempts
{
    public interface IDefuseFailedListener
    {
        void OnDefuseFailed();
    }
}