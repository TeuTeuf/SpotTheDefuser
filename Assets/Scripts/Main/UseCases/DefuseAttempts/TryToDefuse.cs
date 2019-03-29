using Main.Domain.DefuseAttempts;
using Main.Domain.Players;

namespace Main.UseCases.DefuseAttempts
{
    public class TryToDefuse
    {
        private readonly DefusingState _defusingState;
        private readonly IDefuseTriedListener _defuseTriedListener;

        public TryToDefuse(DefusingState defusingState, IDefuseTriedListener defuseTriedListener)
        {
            _defusingState = defusingState;
            _defuseTriedListener = defuseTriedListener;
        }

        public virtual void Try(Player player)
        {
            var isDefuser = _defusingState.IsCurrentAttemptDefuser(player);
            _defuseTriedListener.OnDefuseTried(isDefuser, player);
        }
    }
}