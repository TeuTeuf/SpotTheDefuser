using Main.Domain.Players;
using UnityEngine;
using Zenject;

namespace Main.Domain.DefuseAttempts
{
    public class DefusingState : ITickable
    {
        public DefuseAttempt CurrentDefuseAttempt { get; private set; }
        public virtual int NbBombsDefused { get; private set; }
        public virtual float RemainingTime { get; private set; }
        public bool TimerEnabled { get; set; }

        private IDefusingTime _defusingTime;

        public DefusingState(IDefusingTime defusingTime)
        {
            _defusingTime = defusingTime;
            NbBombsDefused = 0;
        }

        public void SetNewDefuseAttempt(DefuseAttempt defuseAttempt)
        {
            CurrentDefuseAttempt = defuseAttempt;
            RemainingTime += defuseAttempt.TimeToDefuse;
        }

        public virtual bool IsCurrentAttemptDefuser(Player player)
        {
            return CurrentDefuseAttempt.IsDefuser(player);
        }

        public virtual void IncrementBombsDefused()
        {
            NbBombsDefused++;
        }

        public void Tick()
        {
            if (TimerEnabled)
            {
                RemainingTime -= _defusingTime.GetDeltaTime();
            }
            Debug.Log($"Remaining time: {RemainingTime}");
        }
    }
}