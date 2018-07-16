using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using UnityEngine;

namespace Main.Infrastructure
{
    public class DefusingListener : IDefusingListener
    {
        public void OnDefuseTried(bool defuseSucceeded, Player player)
        {
            Debug.Log(string.Format("{0} tried to defuse. Success: {1}", defuseSucceeded, player.Name));
        }
    }
}