using Main.Domain.Players;

namespace Main.UseCases.Players
{
    public class RemovePlayer
    {
        private readonly PlayerRepository _playerRepository;

        public RemovePlayer(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public virtual void Execute(Player player)
        {
            _playerRepository.Remove(player);
        }
    }
}
