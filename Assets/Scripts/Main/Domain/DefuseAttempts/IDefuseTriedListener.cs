using Main.Domain.Players;

namespace Main.Domain.DefuseAttempts
{
    public interface IDefuseTriedListener
    {
        void OnDefuseTried(bool defuseSucceeded, Player player);
    }
}