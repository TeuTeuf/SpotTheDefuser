using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Main.Domain.Players
{
    public class AllPlayers
    {
	    private readonly List<Player> _players = new List<Player>();
	    public Player LocalPlayer { get; private set; }

	    public virtual void Add(Player player)
        {
            _players.Add(player);
        }

		public virtual void Remove(Player player)
		{
			_players.Remove(player);
		}

		public virtual ReadOnlyCollection<Player> GetAll()
		{
			return _players.AsReadOnly();
		}
    }
}
