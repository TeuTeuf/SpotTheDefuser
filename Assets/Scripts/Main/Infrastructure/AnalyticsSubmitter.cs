using System.Collections.Generic;
using Main.Domain;
using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using Main.Domain.UI;
using UnityEngine.Analytics;

namespace Main.Infrastructure
{
    public class AnalyticsSubmitter : IAnalyticsSubmitter
    {
        private readonly DefusingState _defusingState;
        private readonly AllPlayers _allPlayers;

        public AnalyticsSubmitter(DefusingState defusingState, AllPlayers allPlayers)
        {
            _defusingState = defusingState;
            _allPlayers = allPlayers;
        }

        public void TrackBombExploded()
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

        public void TrackBombDefused()
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
        
        public void TrackScreenVisit(View view)
        {
            AnalyticsEvent.ScreenVisit(view.ToString());
        }
    }
}