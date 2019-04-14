using Main.Domain.DefuseAttempts;

namespace Main.UseCases.Network
{
    public class StartNewGame
    {
        private readonly INewGameStartedListener _newGameStartedListener;

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