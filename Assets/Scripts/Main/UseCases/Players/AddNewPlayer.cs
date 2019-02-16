using Main.Domain.Players;
using UnityEngine;

namespace Main.UseCases.Players
{
    public class AddNewPlayer
    {
        private readonly AllPlayers _allPlayers;
        private readonly IPlayerAddedListener _playerAddedListener;

        public AddNewPlayer(AllPlayers allPlayers, IPlayerAddedListener playerAddedListener)
        {
            _playerAddedListener = playerAddedListener;
            _allPlayers = allPlayers;
        }

        public virtual void Execute(Player player)
        {
            _allPlayers.Add(player);
            _playerAddedListener.OnPlayerAdded(player);
            Debug.Log("Player added: " + player.Name);
        }
    }
}
