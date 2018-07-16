using Main.Domain.Players;

namespace Main.Domain.DefuseAttempts
{
    public class DefusingState
    {
        public DefuseAttempt CurrentDefuseAttempt { get; set; }

        public virtual bool IsCurrentAttemptDefuser(Player player)
        {
            return CurrentDefuseAttempt.IsDefuser(player);
        }
    }
}