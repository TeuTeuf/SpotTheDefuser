using Main.Domain.Players;
using Main.UseCases.Players;
using UnityEngine.Networking;
using Zenject;

namespace Main.Infrastructure.Controllers.Network
{
    public class PlayerController : NetworkBehaviour {

        [Inject] public AddNewPlayer AddNewPlayer;

        [Inject] public RemovePlayer RemovePlayer;

        [Inject] public NetworkControllers NetworkControllers;
        
        private Player _player;

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            NetworkControllers.LocalPlayerController = this;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            NetworkControllers.AddPlayerControllerOnServer(this);
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
    }
}
