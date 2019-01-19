using Main.Domain.Players;
using Main.UseCases.DefuseAttempts;
using Main.UseCases.Players;
using UnityEngine.Networking;
using Zenject;

namespace Main.Infrastructure.Controllers.Network
{
    public class PlayerController : NetworkBehaviour /*, IPlayerController*/
    {
        private AddNewPlayer _addNewPlayer;
        private SetNewDefuseAttempt _setNewDefuseAttempt;
        private TryToDefuse _tryToDefuse;

        private AllPlayerControllers _allPlayerControllers;
        private UIController _uiController;

        private Player _player;

        [Inject]
        public void Init(AddNewPlayer addNewPlayer, SetNewDefuseAttempt setNewDefuseAttempt, TryToDefuse tryToDefuse,
            AllPlayerControllers allPlayerControllers, UIController uiController)
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
                _uiController.SetDebugMessage($"{player.Name} tried to defuse. Success: {defuseSucceeded}");
            }
        }
    }
}