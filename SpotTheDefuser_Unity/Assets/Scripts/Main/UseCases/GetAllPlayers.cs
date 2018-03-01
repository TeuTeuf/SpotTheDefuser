using System;
using System.Collections.ObjectModel;
using SpotTheDefuser_Unity.Assets.Scripts.Main.Domain;

namespace SpotTheDefuser_Unity.Assets.Scripts.Main.UseCases
{
    public class GetAllPlayers
    {
        private IPlayerRepository playerRepository;

        public GetAllPlayers(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public ReadOnlyCollection<Player> Get()
        {
            return playerRepository.GetAll();
        }
    }
}
