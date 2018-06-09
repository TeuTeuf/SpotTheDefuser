using Main.Domain.Players;

namespace Main.UseCases.Players
{
    public class SetLocalPlayer
    {
        private readonly PlayerRepository _playerRepository;

        public SetLocalPlayer(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public void Set(Player player)
        {
            _playerRepository.SetLocalPlayer(player);
        }
    }
}