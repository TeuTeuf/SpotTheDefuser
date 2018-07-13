using Main.Domain.Players;

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
        }
    }
}
