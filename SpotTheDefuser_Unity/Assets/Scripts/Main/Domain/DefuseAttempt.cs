using System.Collections.Generic;
using Main.Domain.Players;

namespace Main.Domain
{
    public class DefuseAttempt
    {
        private readonly IList<Player> _listPlayer;
        private readonly int _defuserId;

        public DefuseAttempt(IRandom random, IList<Player> listPlayer)
        {
            _listPlayer = listPlayer;
            _defuserId = random.Range(0, listPlayer.Count);
        }

        public bool IsDefuser(Player player)
        {
            return _listPlayer.IndexOf(player) == _defuserId;
        }
    }
}
