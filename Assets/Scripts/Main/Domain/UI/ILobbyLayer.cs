using Main.Domain.Players;

namespace Main.Domain.UI
{
    public interface ILobbyLayer
    {
        void UpdatePlayerList(Player[] allPlayers);
    }
}