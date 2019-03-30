using Main.Domain.Players;

namespace Main.Domain.UI.Layers
{
    public interface ILobbyLayer
    {
        void UpdatePlayerList(Player[] allPlayers);
    }
}