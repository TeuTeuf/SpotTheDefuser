using Main.Domain.Players;

namespace Main.UseCases.Players
{
    public class AddNewPlayer
    {
        private readonly IPlayerRepository _playerRepository;

        public AddNewPlayer(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public virtual void Execute(Player player)
        {
            _playerRepository.Add(player);
        }
    }
}
