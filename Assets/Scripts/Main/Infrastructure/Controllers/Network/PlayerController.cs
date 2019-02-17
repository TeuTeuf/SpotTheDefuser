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
        private SetNewDefuseAttempt _setNewDefuseAttempt;
        private TryToDefuse _tryToDefuse;

        private AllPlayerControllers _allPlayerControllers;
        private IUIController _uiController;

        private Player _player;

        [Inject]
        public void Init(AddNewPlayer addNewPlayer, SetNewDefuseAttempt setNewDefuseAttempt, TryToDefuse tryToDefuse,
            AllPlayerControllers allPlayerControllers, IUIController uiController)
        {
            _addNewPlayer = addNewPlayer;
            _setNewDefuseAttempt = setNewDefuseAttempt;
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
        public void CmdSetNewDefuseAttempt()
        {
            _setNewDefuseAttempt.Set();
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

        [ClientRpc]
        public void RpcOnDefuseTried(bool defuseSucceeded, Player player)
        {
            if (hasAuthority)
            {
                Debug.Log($"{player.Name} tried to defuse. Success: {defuseSucceeded}");
            }
        }

        [ClientRpc]
        public void RpcOnPlayerAdded(Player player)
        {
            if (hasAuthority)
            {
                OnPlayerAdded(player);
            }
        }

        public void OnPlayerAdded(Player player)
        {
            _uiController.UpdateLobby();
        }
    }
}