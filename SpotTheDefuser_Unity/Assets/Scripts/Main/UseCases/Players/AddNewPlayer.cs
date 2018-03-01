using SpotTheDefuser_Unity.Assets.Scripts.Main.Domain;

namespace SpotTheDefuser_Unity.Assets.Scripts.Main.UseCases
{
    public class AddNewPlayer
    {
        IPlayersRepository playerRepository;

        public AddNewPlayer(IPlayersRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public void Execute(Player player)
        {
            playerRepository.Add(player);
        }
    }
}
