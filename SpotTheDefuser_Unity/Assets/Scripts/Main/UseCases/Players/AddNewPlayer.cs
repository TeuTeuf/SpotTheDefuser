using System.Diagnostics;
using Main.Domain.Players;
using Debug = UnityEngine.Debug;

namespace Main.UseCases.Players
{
    public class AddNewPlayer
    {
        private readonly AllPlayers _allPlayers;

        public AddNewPlayer(AllPlayers allPlayers)
        {
            _allPlayers = allPlayers;
        }

        public virtual void Execute(Player player)
        {
            _allPlayers.Add(player);
            Debug.Log("Player added: " + player.Name);
        }
    }
}
