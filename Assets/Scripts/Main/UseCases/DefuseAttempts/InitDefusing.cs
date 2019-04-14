using Main.Domain.DefuseAttempts;

namespace Main.UseCases.DefuseAttempts
{
    public class InitDefusing
    {
        private DefusingState _defusingState;

        public InitDefusing(DefusingState defusingState)
        {
            _defusingState = defusingState;
        }

        public virtual void Init()
        {
            _defusingState.StartNewTimer();
        }
    }
}