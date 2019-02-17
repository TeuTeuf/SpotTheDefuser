using Main.Domain.Players;

namespace Main.Domain.UI
{
    public interface IUIController
    {
        void UpdateLobby(Player[] allPlayers);
    }
}