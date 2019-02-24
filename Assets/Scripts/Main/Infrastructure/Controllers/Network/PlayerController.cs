using System.Collections.ObjectModel;
using Main.Domain.Players;
using Main.Domain.UI;
using Main.UseCases.DefuseAttempts;
using Main.UseCases.Players;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Main.Infrastructure.Controllers.Network
{
    public class PlayerController : NetworkBehaviour
    {
        private AddNewPlayer _addNewPlayer;
        private StartNewGame _startNewGame;
        private TryToDefuse _tryToDefuse;

        private AllPlayerControllers _allPlayerControllers;
        private IUIController _uiController;

        private Player _player;

        [Inject]
        public void Init(AddNewPlayer addNewPlayer, StartNewGame startNewGame, TryToDefuse tryToDefuse,
            AllPlayerControllers allPlayerControllers, IUIController uiController)
        {
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
            _player = player;
            _addNewPlayer.Execute(player);
        }

        [Command]
        public void CmdTryToDefuse()
        {
            _tryToDefuse.Try(_player);
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
            if (hasAuthority)
            {
                OnPlayerAdded(allPlayers);
            }
        }

        public void OnPlayerAdded(Player[] allPlayers)
        {
            _uiController.UpdateLobby(allPlayers);
        }
    }
}