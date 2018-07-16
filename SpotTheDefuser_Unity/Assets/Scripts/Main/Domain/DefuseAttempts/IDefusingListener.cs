using Main.Domain.Players;

namespace Main.Domain.DefuseAttempts
{
    public interface IDefusingListener
    {
        void OnDefuseTried(bool defuseSucceeded, Player player);
    }
}