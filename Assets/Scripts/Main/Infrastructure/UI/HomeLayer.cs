using Main.Domain.UI;
using Main.UseCases.Network;
using Zenject;

namespace Main.Infrastructure.UI
{
    public class HomeLayer : BaseLayer
    {
        private HostNewGame _hostNewGame;
        private StartWaitingForNewGame _startWaitingForNewGame;
        
        private string _playerName;

        [Inject]
        public void Init(HostNewGame hostNewGame, StartWaitingForNewGame startWaitingForNewGame)
        {
            _hostNewGame = hostNewGame;
            _startWaitingForNewGame = startWaitingForNewGame;
        }

        public void OnEndEditOnPlayerName(string playerName)
        {
            _playerName = playerName;
        }

        public void OnClickOnHost()
        {
            _hostNewGame.Host(_playerName);
        }
        
        public void OnClickOnJoin()
        {
            _startWaitingForNewGame.Start(_playerName);
        }

        public override View GetView()
        {
            return View.Home;
        }
    }
}