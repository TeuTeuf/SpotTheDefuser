using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using UnityEngine;

namespace Main.Infrastructure.Controllers.Network
{
    public class DefusingListener : IDefusingListener
    {
        private readonly AllPlayerControllers _allPlayerControllers;

        public DefusingListener(AllPlayerControllers allPlayerControllers)
        {
            _allPlayerControllers = allPlayerControllers;
        }

        public void OnDefuseTried(bool defuseSucceeded, Player player)
        {
            _allPlayerControllers.OnDefuseTried(defuseSucceeded, player);
        }
    }
}