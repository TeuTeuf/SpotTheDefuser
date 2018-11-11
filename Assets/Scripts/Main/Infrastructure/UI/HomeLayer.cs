using System.Collections;
using Main.Infrastructure.Controllers.Network;
using Main.UseCases.Network;
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
            StartCoroutine(nameof(WaitForLocalPlayerConnected));
        }
        
        public void OnClickOnJoin()
        {
            Debug.Log("OnClickOnJoin");
        }

        private IEnumerator WaitForLocalPlayerConnected()
        {
            while (_allPlayerControllers.LocalPlayerController == null)
            {
                yield return null;
            }
            
            _allPlayerControllers.AddNewPlayerOnServer(_playerName);
        }
    }
}