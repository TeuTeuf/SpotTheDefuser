using System.Collections.ObjectModel;
using Main.Domain.Players;

namespace Main.UseCases.Players
{
    public class GetAllPlayers
    {
        private readonly IPlayerRepository _playerRepository;

        public GetAllPlayers(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public ReadOnlyCollection<Player> Get()
        {
            return _playerRepository.GetAll();
        }
    }
}
