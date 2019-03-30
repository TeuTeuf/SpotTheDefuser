using System.Collections.ObjectModel;
using Main.Domain.Network;
using Main.Domain.Players;
using Main.Domain.UI;
using Main.Infrastructure.Network;
using Main.UseCases.DefuseAttempts;
using Main.UseCases.Players;
using Main.UseCases.UI;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Main.Infrastructure.Controllers.Network
{
    public class PlayerController : NetworkBehaviour
    {
        private AddNewPlayer _addNewPlayer;
        private StartNewGame _startNewGame;
        private SetNewDefuseAttempt _setNewDefuseAttempt;
        private TryToDefuse _tryToDefuse;
        private ChangeCurrentView _changeCurrentView;

        private AllPlayerControllers _allPlayerControllers;
        private IUIController _uiController;

        private NetworkBehaviourChecker _networkBehaviourChecker;
        private ISpotTheDefuserNetworkDiscovery _spotTheDefuserNetworkDiscovery;

        public Player Player { get; private set; }

        [Inject]
        public void Init(AddNewPlayer addNewPlayer, StartNewGame startNewGame, SetNewDefuseAttempt setNewDefuseAttempt,
            TryToDefuse tryToDefuse,
            ChangeCurrentView changeCurrentView,
            AllPlayerControllers allPlayerControllers, IUIController uiController,
            NetworkBehaviourChecker networkBehaviourChecker,
            ISpotTheDefuserNetworkDiscovery spotTheDefuserNetworkDiscovery)
        {
            _setNewDefuseAttempt = setNewDefuseAttempt;
            _spotTheDefuserNetworkDiscovery = spotTheDefuserNetworkDiscovery;
            _networkBehaviourChecker = networkBehaviourChecker;
            _changeCurrentView = changeCurrentView;
            _startNewGame = startNewGame;
            _addNewPlayer = addNewPlayer;
            _tryToDefuse = tryToDefuse;
            _allPlayerControllers = allPlayerControllers;
            _uiController = uiController;
        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            _allPlayerControllers.LocalPlayerController = this;
            _allPlayerControllers.AddLocalPlayerOnServer();
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            _allPlayerControllers.AddPlayerControllerOnServer(this);
        }

        [Command]
        public void CmdAddNewPlayer(Player player)
        {
            Player = player;
            _addNewPlayer.Execute(player);
        }

        [Command]
        public void CmdTryToDefuse()
        {
            _tryToDefuse.Try(Player);
        }

        [Command]
        public void CmdStartNewGame()
        {
            _startNewGame.Start();
        }

        [ClientRpc]
        public void RpcOnDefuseTried(bool defuseSucceeded, Player player)
        {
            if (hasAuthority)
            {
                Debug.Log($"{player.Name} tried to defuse. Success: {defuseSucceeded}");
            }
        }

        [ClientRpc]
        public void RpcOnPlayerAdded(Player[] allPlayers)
        {
            if (_networkBehaviourChecker.IsLocalPlayer(this))
            {
                _uiController.UpdateLobby(allPlayers);
            }
        }

        [ClientRpc]
        public void RpcOnNewGameStarted()
        {
            if (_networkBehaviourChecker.IsLocalPlayer(this))
            {
                _changeCurrentView.Change(View.Defusing);
            }

            if (_networkBehaviourChecker.IsHostingLocalPlayer(this))
            {
                _spotTheDefuserNetworkDiscovery.StopBroadcastingOnLAN();
            }
        }

        [Command]
        public void CmdOnNewGameStarted()
        {
            _setNewDefuseAttempt.Set();
        }

        [ClientRpc]
        public void RpcOnNewDefuseAttemptSet(string defuseAttemptBombId, bool isPlayerDefuser)
        {
            if (_networkBehaviourChecker.IsLocalPlayer(this))
            {
                _uiController.UpdateDefusing(defuseAttemptBombId, isPlayerDefuser);
                Debug.Log($"New Defuse Attempt set with bomb {defuseAttemptBombId}!");
            }
        }
    }
}