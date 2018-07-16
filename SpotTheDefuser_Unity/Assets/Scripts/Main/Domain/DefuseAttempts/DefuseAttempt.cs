using System.Collections.Generic;
using System.Collections.ObjectModel;
using Main.Domain.Players;

namespace Main.Domain.DefuseAttempts
{
    public class DefuseAttempt
    {
        private readonly IList<Player> _defuserPlayers;

        public DefuseAttempt(IRandom random, ReadOnlyCollection<Player> allPlayers)
        {
            var numberOfDefuserPlayers = GetNumberOfDefuserPlayers(allPlayers.Count);
            _defuserPlayers = GetDefuserPlayers(random, numberOfDefuserPlayers, allPlayers);
        }

        public virtual bool IsDefuser(Player player)
        {
            return _defuserPlayers.Contains(player);
        }

        private static List<Player> GetDefuserPlayers(IRandom random, int numberOfDefuserPlayers, ReadOnlyCollection<Player> allPlayers)
        {
            var players = new List<Player>(allPlayers);
            var defuserPlayers = new List<Player>();
            for (var i = 0; i < numberOfDefuserPlayers; i++)
            {
                var defuserIndex = random.Range(0, players.Count);
                defuserPlayers.Add(players[defuserIndex]);
                players.RemoveAt(defuserIndex);
            }

            return defuserPlayers;
        }

        private static int GetNumberOfDefuserPlayers(int nbAllPlayers)
        {
            var isNumberOfPlayersEven = nbAllPlayers % 2 == 0;
            var nbDefuserPlayers = nbAllPlayers / 2;

            if (isNumberOfPlayersEven)
            {
                nbDefuserPlayers--;
            }

            return nbDefuserPlayers;
        }
    }
}
