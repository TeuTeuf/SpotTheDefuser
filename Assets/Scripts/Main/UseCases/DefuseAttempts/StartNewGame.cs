using Main.Domain.DefuseAttempts;

namespace Main.UseCases.DefuseAttempts
{
    public class StartNewGame
    {
        private INewGameStartedListener _newGameStartedListener;

        public StartNewGame(INewGameStartedListener newGameStartedListener)
        {
            _newGameStartedListener = newGameStartedListener;
        }

        public virtual void Start()
        {
            _newGameStartedListener.OnNewGameStarted();
        }
    }
}