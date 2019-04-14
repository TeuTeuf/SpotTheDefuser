using Main.Domain.Players;
using UnityEngine;
using Zenject;

namespace Main.Domain.DefuseAttempts
{
    public class DefusingState : ITickable
    {
        public DefuseAttempt CurrentDefuseAttempt { get; private set; }
        public virtual int NbBombsDefused { get; private set; }
        public virtual int RemainingTime { get; private set; }

        public DefusingState()
        {
            NbBombsDefused = 0;
        }

        public void SetNewDefuseAttempt(DefuseAttempt defuseAttempt)
        {
            CurrentDefuseAttempt = defuseAttempt;
            RemainingTime += defuseAttempt.TimeToDefuse;
        }

        public void Tick()
        {
            Debug.Log($"Tick: {Time.time}");
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