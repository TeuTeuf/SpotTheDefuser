using Main.Domain.DefuseAttempts;
using Main.Domain.Players;

namespace Main.UseCases.DefuseAttempts
{
    public class TryToDefuse
    {
        private readonly DefusingState _defusingState;
        private readonly IDefuseSucceededListener _defuseSucceededListener;
        private readonly IDefuseFailedListener _defuseFailedListener;

        public TryToDefuse(DefusingState defusingState, IDefuseSucceededListener defuseSucceededListener,
            IDefuseFailedListener defuseFailedListener)
        {
            _defuseFailedListener = defuseFailedListener;
            _defusingState = defusingState;
            _defuseSucceededListener = defuseSucceededListener;
        }

        public virtual void Try(Player player)
        {
            if (_defusingState.IsCurrentAttemptDefuser(player))
            {
                _defusingState.IncrementBombsDefused();
                _defuseSucceededListener.OnDefuseSucceeded();
            }
            else
            {
                _defuseFailedListener.OnDefuseFailed(_defusingState.NbBombsDefused);
            }
        }
    }
}