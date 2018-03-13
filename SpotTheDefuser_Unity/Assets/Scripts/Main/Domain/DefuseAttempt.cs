using System;
using System.Collections.Generic;

namespace SpotTheDefuser_Unity.Assets.Scripts.Main.Domain
{
    public class DefuseAttempt
    {
        private IList<Player> listPlayer;
        private int defuserId;

        public DefuseAttempt(IRandom random, IList<Player> listPlayer)
        {
            this.listPlayer = listPlayer;
            this.defuserId = random.Range(0, listPlayer.Count);
        }

        public bool IsDefuser(Player player)
        {
            return listPlayer.IndexOf(player) == defuserId;
        }
    }
}
