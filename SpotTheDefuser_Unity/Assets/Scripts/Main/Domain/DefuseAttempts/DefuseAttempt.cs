using System.Collections.Generic;
using Main.Domain.Players;

namespace Main.Domain.DefuseAttempts
{
    public class DefuseAttempt
    {
        private readonly IList<Player> _defuserPlayers;

        public DefuseAttempt(IList<Player> defuserPlayers)
        {
            _defuserPlayers = defuserPlayers;
        }

        public bool IsDefuser(Player player)
        {
            return _defuserPlayers.Contains(player);
        }
    }
}
