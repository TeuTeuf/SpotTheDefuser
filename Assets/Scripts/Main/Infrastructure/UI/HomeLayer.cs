using Main.Infrastructure.Controllers.Network;
using Main.UseCases;
using UnityEngine;
using Zenject;

namespace Main.Infrastructure.UI
{
    public class HomeLayer : BasicViewLayer
    {
        private HostNewGame _hostNewGame;
        private AllPlayerControllers _allPlayerControllers;
        
        private string _playerName;

        [Inject]
        public void Init(HostNewGame hostNewGame, AllPlayerControllers allPlayerControllers)
        {
            _hostNewGame = hostNewGame;
            _allPlayerControllers = allPlayerControllers;
        }

        public void OnEndEditOnPlayerName(string playerName)
        {
            _playerName = playerName;
        }

        public void OnClickOnHost()
        {
            _hostNewGame.Host();
            _allPlayerControllers.AddNewPlayerOnServer(_playerName);
        }

        public void OnClickOnJoin()
        {
            Debug.Log("OnClickOnJoin");
        }
    }
}