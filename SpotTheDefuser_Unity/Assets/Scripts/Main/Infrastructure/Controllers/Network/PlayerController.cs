using Main.Domain.Players;
using Main.UseCases.DefuseAttempts;
using Main.UseCases.Players;
using UnityEngine.Networking;
using Zenject;

namespace Main.Infrastructure.Controllers.Network
{
    public class PlayerController : NetworkBehaviour, IPlayerController {

        [Inject] public AddNewPlayer AddNewPlayer;
        [Inject] public SetNewDefuseAttempt SetDefuseAttempt;
        [Inject] public TryToDefuse TryToDefuse;

        [Inject] public AllPlayerControllers AllPlayerControllers;

        private Player _player;

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            AllPlayerControllers.LocalPlayerController = this;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            AllPlayerControllers.AddPlayerControllerOnServer(this);
        }

        [Command]
        public void CmdSetNewDefuseAttempt()
        {
            SetDefuseAttempt.Set();
        }

        [Command]
        public void CmdAddNewPlayer(string playerName)
        {
            _player = new Player(playerName);
            AddNewPlayer.Execute(_player);
        }

        [Command]
        public void CmdTryToDefuse()
        {
            TryToDefuse.Try(_player);
        }
    }
}
