using Main.Domain.DefuseAttempts;
using Main.Domain.Players;

namespace Main.UseCases.DefuseAttempts
{
    public class TryToDefuse
    {
        private readonly DefusingState _defusingState;
        private readonly IDefusingListener _defusingListener;

        public TryToDefuse(DefusingState defusingState, IDefusingListener defusingListener)
        {
            _defusingState = defusingState;
            _defusingListener = defusingListener;
        }

        public virtual void Try(Player player)
        {
            var isDefuser = _defusingState.IsCurrentAttemptDefuser(player);
            _defusingListener.OnDefuseTried(isDefuser, player);
        }
    }
}