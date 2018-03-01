using System;
using SpotTheDefuser_Unity.Assets.Scripts.Main.Domain;

namespace SpotTheDefuser_Unity.Assets.Scripts.Main.UseCases
{
    public class RemovePlayer
    {
        private IPlayerRepository playerRepository;

        public RemovePlayer(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public void execute(Player player)
        {
            playerRepository.Remove(player);
        }
    }
}
