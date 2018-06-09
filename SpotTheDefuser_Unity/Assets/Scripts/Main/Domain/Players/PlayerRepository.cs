using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Main.Domain.Players
{
    public class PlayerRepository : IPlayerRepository
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
