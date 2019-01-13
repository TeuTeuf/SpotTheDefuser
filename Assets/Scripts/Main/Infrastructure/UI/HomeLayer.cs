using System.Collections;
using Main.Domain.UI;
using Main.Infrastructure.Controllers.Network;
using Main.UseCases.Network;
using UnityEngine;
using Zenject;

namespace Main.Infrastructure.UI
{
    public class HomeLayer : BasicViewLayer
    {
        private const View NEXT_VIEW = View.Lobby;
        
        private HostNewGame _hostNewGame;
        private StartWaitingForNewGame _startWaitingForNewGame;
        
        private AllPlayerControllers _allPlayerControllers;
        private IViewManager _viewManager;
        
        private string _playerName;

        [Inject]
        public void Init(HostNewGame hostNewGame, StartWaitingForNewGame startWaitingForNewGame, AllPlayerControllers allPlayerControllers,
            IViewManager viewManager)
        {
            _hostNewGame = hostNewGame;
            _startWaitingForNewGame = startWaitingForNewGame;
            _allPlayerControllers = allPlayerControllers;
            _viewManager = viewManager;
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
            _viewManager.ReplaceCurrentLayers(NEXT_VIEW);
            // ^ Should be removed and moved in StartWaitingForNewGame usecase
        }
    }
}