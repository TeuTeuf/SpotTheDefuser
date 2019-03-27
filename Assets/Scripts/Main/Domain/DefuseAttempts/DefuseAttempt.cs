using System.Collections.Generic;
using System.Collections.ObjectModel;
using Main.Domain.Players;

namespace Main.Domain.DefuseAttempts
{
    public class DefuseAttempt
    {
        public string BombId { get; }
        
        private readonly DefuserCounter _defuserCounter;
        private readonly IList<Player> _defuserPlayers;

        public DefuseAttempt(IRandom random, DefuserCounter defuserCounter, AllBombs allBombs,
            ReadOnlyCollection<Player> allPlayers)
        {
            _defuserCounter = defuserCounter;
            _defuserPlayers = GetDefuserPlayers(random, allPlayers);
            BombId = allBombs.PickRandomBombId();
        }

        public virtual bool IsDefuser(Player player)
        {
            return _defuserPlayers.Contains(player);
        }

        private List<Player> GetDefuserPlayers(IRandom random, ReadOnlyCollection<Player> allPlayers)
        {
            var numberOfDefuserPlayers = _defuserCounter.GetNumberOfDefuserPlayers(allPlayers.Count);
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
    }
}
