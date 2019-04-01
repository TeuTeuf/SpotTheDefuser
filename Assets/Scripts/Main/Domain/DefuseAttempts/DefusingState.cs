using Main.Domain.Players;

namespace Main.Domain.DefuseAttempts
{
    public class DefusingState
    {
        public DefuseAttempt CurrentDefuseAttempt { get; set; }
        public virtual int NbBombsDefused { get; private set; }

        public DefusingState()
        {
            NbBombsDefused = 0;
        }

        public virtual bool IsCurrentAttemptDefuser(Player player)
        {
            return CurrentDefuseAttempt.IsDefuser(player);
        }

        public virtual void IncrementBombsDefused()
        {
            NbBombsDefused++;
        }
    }
}