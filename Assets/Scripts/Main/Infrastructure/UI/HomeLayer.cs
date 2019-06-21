using Main.Domain.UI;
using Main.UseCases.Network;
using Main.UseCases.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main.Infrastructure.UI
{
    public class HomeLayer : BaseLayer
    {
        public const string PLAYER_NAME_KEY = "PLAYER_NAME";

        public InputField playerNameInputField;
        
        private HostNewGame _hostNewGame;
        private StartWaitingForNewGame _startWaitingForNewGame;
        private ChangeCurrentView _changeCurrentView;

        private string _playerName;

        [Inject]
        public void Init(ChangeCurrentView changeCurrentView, HostNewGame hostNewGame,
            StartWaitingForNewGame startWaitingForNewGame)
        {
            _hostNewGame = hostNewGame;
            _startWaitingForNewGame = startWaitingForNewGame;
            _changeCurrentView = changeCurrentView;
        }

        public void Start()
        {
            _playerName = PlayerPrefs.GetString(PLAYER_NAME_KEY);
            playerNameInputField.text = _playerName;
        }

        public void OnEndEditOnPlayerName(string playerName)
        {
            _playerName = playerName;
            PlayerPrefs.SetString(PLAYER_NAME_KEY, _playerName);
        }

        public void OnClickOnHost()
        {
            _hostNewGame.Host(_playerName);
        }

        public void OnClickOnJoin()
        {
            _startWaitingForNewGame.Start(_playerName);
        }

        public void OnClickOnHowToPlay()
        {
            _changeCurrentView.Change(View.HowToPlay);
        }

        public override View GetView()
        {
            return View.Home;
        }
    }
}