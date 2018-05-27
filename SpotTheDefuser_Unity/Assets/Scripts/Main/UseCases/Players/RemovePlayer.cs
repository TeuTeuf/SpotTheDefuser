using Main.Domain.Players;

namespace Main.UseCases.Players
{
    public class RemovePlayer
    {
        private readonly IPlayerRepository _playerRepository;

        public RemovePlayer(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public virtual void Execute(Player player)
        {
            _playerRepository.Remove(player);
        }
    }
}
