using System.Collections.Generic;
using Main.Domain;
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
        private readonly IAnalyticsSubmitter _analyticsSubmitter;

        public TryToDefuse(DefusingState defusingState, IDefuseSucceededListener defuseSucceededListener,
            IDefuseFailedListener defuseFailedListener, IAnalyticsSubmitter analyticsSubmitter)
        {
            _defuseFailedListener = defuseFailedListener;
            _analyticsSubmitter = analyticsSubmitter;
            _defusingState = defusingState;
            _defuseSucceededListener = defuseSucceededListener;
        }

        public virtual void Try(Player player)
        {
            if (_defusingState.IsCurrentAttemptDefuser(player))
            {
                _analyticsSubmitter.TrackBombDefused();
                _defusingState.IncrementBombsDefused();
                _defuseSucceededListener.OnDefuseSucceeded();
            }
            else
            {
                _analyticsSubmitter.TrackBombExploded();
                _defuseFailedListener.OnDefuseFailed(_defusingState.NbBombsDefused);
            }
        }
    }
}