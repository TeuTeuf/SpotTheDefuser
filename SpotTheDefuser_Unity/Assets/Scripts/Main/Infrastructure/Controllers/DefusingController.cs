using Main.Domain.Players;
using Main.UseCases.Players;
using UnityEngine.Networking;
using Zenject;

namespace Main.Infrastructure.Controllers
{
    public class DefusingController : NetworkBehaviour {

        [Inject] public AddNewPlayer AddNewPlayer;

        [Inject] public RemovePlayer RemovePlayer;

        private Player _player;

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
