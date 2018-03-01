using System;
using System.Collections.ObjectModel;
using SpotTheDefuser_Unity.Assets.Scripts.Main.Domain;

namespace SpotTheDefuser_Unity.Assets.Scripts.Main.UseCases
{
    public class GetAllPlayers
    {
        private IPlayersRepository playerRepository;

        public GetAllPlayers(IPlayersRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public ReadOnlyCollection<Player> Get()
        {
            return playerRepository.GetAll();
        }
    }
}
