using Main.Domain.Players;

namespace Main.UseCases.Players
{
    public class RemovePlayer
    {
        private readonly IPlayersRepository _playerRepository;

        public RemovePlayer(IPlayersRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public virtual void Execute(Player player)
        {
            _playerRepository.Remove(player);
        }
    }
}
