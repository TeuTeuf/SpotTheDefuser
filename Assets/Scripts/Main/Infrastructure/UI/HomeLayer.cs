using Main.UseCases;
using UnityEngine;
using Zenject;

namespace Main.Infrastructure.UI
{
    public class HomeLayer : BasicViewLayer
    {
        private HostNewGame _hostNewGame;
        
        private string _playerName;

        [Inject]
        public void Init(HostNewGame hostNewGame)
        {
            _hostNewGame = hostNewGame;
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
            Debug.Log("OnClickOnJoin");
        }
    }
}