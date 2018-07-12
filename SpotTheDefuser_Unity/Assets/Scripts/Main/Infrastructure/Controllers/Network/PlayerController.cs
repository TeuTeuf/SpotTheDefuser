using Main.Domain.Players;
using Main.UseCases.DefuseAttempts;
using Main.UseCases.Players;
using UnityEngine.Networking;
using Zenject;

namespace Main.Infrastructure.Controllers.Network
{
    public class PlayerController : NetworkBehaviour, IPlayerController {

        [Inject] public AddNewPlayer AddNewPlayer;

        [Inject] public RemovePlayer RemovePlayer;

        [Inject] public SetNewDefuseAttempt SetDefuseAttempt;

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

        public void Start () 
        {
            _player = new Player("Player");
            AddNewPlayer.Execute(_player);
        }

        public void OnDestroy()
        {
            RemovePlayer.Execute(_player);
        }

        [Command]
        public void CmdSetNewDefuseAttempt()
        {
            SetDefuseAttempt.Set();
        }
    }
}
