using System.Collections.Generic;
using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using UnityEngine.Analytics;

namespace Main.UseCases.DefuseAttempts
{
    public class TryToDefuse
    {
        private readonly DefusingState _defusingState;
        private readonly IDefuseSucceededListener _defuseSucceededListener;
        private readonly IDefuseFailedListener _defuseFailedListener;
        private readonly AllPlayers _allPlayers;

        public TryToDefuse(DefusingState defusingState, IDefuseSucceededListener defuseSucceededListener,
            IDefuseFailedListener defuseFailedListener, AllPlayers allPlayers)
        {
            _defuseFailedListener = defuseFailedListener;
            _defusingState = defusingState;
            _defuseSucceededListener = defuseSucceededListener;
            _allPlayers = allPlayers;
        }

        public virtual void Try(Player player)
        {
            if (_defusingState.IsCurrentAttemptDefuser(player))
            {
                TrackBombDefused();
                _defusingState.IncrementBombsDefused();
                _defuseSucceededListener.OnDefuseSucceeded();
            }
            else
            {
                TrackBombExploded();
                _defuseFailedListener.OnDefuseFailed(_defusingState.NbBombsDefused);
            }
        }

        private void TrackBombExploded()
        {
            AnalyticsEvent.LevelFail(
                _defusingState.CurrentDefuseAttempt.BombId,
                new Dictionary<string, object>
                {
                    {"currentNbBomb", _defusingState.NbBombsDefused},
                    {"remainingTime", _defusingState.RemainingTime},
                    {"nbPlayers", _allPlayers.GetAll().Count}
                }
            );
        }

        private void TrackBombDefused()
        {
            AnalyticsEvent.LevelComplete(
                _defusingState.CurrentDefuseAttempt.BombId,
                new Dictionary<string, object>
                {
                    {"currentNbBomb", _defusingState.NbBombsDefused},
                    {"remainingTime", _defusingState.RemainingTime},
                    {"nbPlayers", _allPlayers.GetAll().Count}
                }
            );
        }
    }
}