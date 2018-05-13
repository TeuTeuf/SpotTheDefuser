using Main.Domain.Players;
using Main.UseCases.Players;
using UnityEngine;
using Zenject;

namespace Main.Infrastructure.Players
{
    public class PlayerController : MonoBehaviour {

        [Inject]
        public AddNewPlayer AddNewPlayer;

        [Inject]
        public RemovePlayer RemovePlayer;

        [Inject]
        public GetAllPlayers GetAllPlayers;

        private Player _player;

        public void Start () 
        {
            _player = new Player("Player");
            AddNewPlayer.Execute(_player);

            Debug.Log("New Player Added!");
        }
    }
}
