using Main.Domain.Players;

namespace Main.UseCases.Players
{
    public class RemovePlayer
    {
        private readonly AllPlayers _allPlayers;

        public RemovePlayer(AllPlayers allPlayers)
        {
            _allPlayers = allPlayers;
        }

        public virtual void Execute(Player player)
        {
            _allPlayers.Remove(player);
        }
    }
}
