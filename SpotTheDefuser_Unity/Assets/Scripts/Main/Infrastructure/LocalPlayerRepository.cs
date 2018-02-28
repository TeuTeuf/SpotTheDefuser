using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SpotTheDefuser_Unity.Assets.Scripts.Main.Domain;

namespace SpotTheDefuser_Unity.Assets.Scripts.Main.Infrastructure
{
    public class LocalPlayerRepository : IPlayerRepository
    {
        List<Player> players = new List<Player>();

        public void Add(Player player)
        {
            players.Add(player);
        }

        public ReadOnlyCollection<Player> GetAll()
        {
            return players.AsReadOnly();
        }
    }
}
