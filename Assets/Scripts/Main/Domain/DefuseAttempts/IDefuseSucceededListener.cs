using Main.Domain.Players;

namespace Main.Domain.DefuseAttempts
{
    public interface IDefuseSucceededListener
    {
        void OnDefuseSucceeded();
    }
}