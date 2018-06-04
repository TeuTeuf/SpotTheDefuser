using System.Collections.Generic;
using Main.Domain.Players;

namespace Main.Domain.DefuseAttempts
{
    public class DefuseAttempt
    {
        private readonly IList<Player> _defuserPlayers;

//        public DefuseAttempt(IRandom random, IEnumerable<Player> players)
//        {
//            _defuserPlayers = GetDefuserPlayers(random, players);
//        }

        public DefuseAttempt(IList<Player> defuserPlayers)
        {
            _defuserPlayers = defuserPlayers;
        }

        public bool IsDefuser(Player player)
        {
            return _defuserPlayers.Contains(player);
        }

//        private static List<Player> GetDefuserPlayers(IRandom random, IEnumerable<Player> players)
//        {
//            var allPlayers = new List<Player>(players);
//            var numberOfDefuserPlayers = GetNumberOfDefuserPlayers(allPlayers);
//            
//            var defuserPlayers = new List<Player>();
//            for (var i = 0; i < numberOfDefuserPlayers; i++)
//            {
//                var defuserIndex = random.Range(0, allPlayers.Count);
//                defuserPlayers.Add(allPlayers[defuserIndex]);
//                allPlayers.RemoveAt(defuserIndex);
//            }
//
//            return defuserPlayers;
//        }
//
//        private static int GetNumberOfDefuserPlayers(ICollection<Player> players)
//        {
//            var isNumberOfPlayersEven = players.Count % 2 == 0;
//            var nbDefuserPlayers = players.Count / 2;
//
//            if (isNumberOfPlayersEven)
//            {
//                nbDefuserPlayers--;
//            }
//
//            return nbDefuserPlayers;
//        }
    }
}
