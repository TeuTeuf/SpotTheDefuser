using System.Collections.Generic;
using System.Collections.ObjectModel;
using Main.Domain.Players;

namespace Main.Infrastructure.Players
{
    public class LocalPlayersRepository : IPlayersRepository
    {
	    private readonly List<Player> _players = new List<Player>();

        public void Add(Player player)
        {
            _players.Add(player);
        }

		public void Remove(Player player)
		{
			_players.Remove(player);
		}

		public ReadOnlyCollection<Player> GetAll()
		{
			return _players.AsReadOnly();
		}
    }
}
