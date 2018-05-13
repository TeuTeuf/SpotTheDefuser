using Main.Domain.Players;

namespace Main.UseCases.Players
{
    public class AddNewPlayer
    {
        private readonly IPlayersRepository _playerRepository;

        public AddNewPlayer(IPlayersRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public virtual void Execute(Player player)
        {
            _playerRepository.Add(player);
        }
    }
}
