using System.Collections.ObjectModel;
using Main.Domain.Players;

namespace Main.UseCases.Players
{
    public class GetAllPlayers
    {
        private readonly AllPlayers _allPlayers;

        public GetAllPlayers(AllPlayers allPlayers)
        {
            _allPlayers = allPlayers;
        }

        public ReadOnlyCollection<Player> Get()
        {
            return _allPlayers.GetAll();
        }
    }
}
