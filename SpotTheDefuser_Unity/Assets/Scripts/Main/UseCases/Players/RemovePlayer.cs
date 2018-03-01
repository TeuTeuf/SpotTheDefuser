using System;
using SpotTheDefuser_Unity.Assets.Scripts.Main.Domain;

namespace SpotTheDefuser_Unity.Assets.Scripts.Main.UseCases
{
    public class RemovePlayer
    {
        private IPlayersRepository playerRepository;

        public RemovePlayer(IPlayersRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public void execute(Player player)
        {
            playerRepository.Remove(player);
        }
    }
}
