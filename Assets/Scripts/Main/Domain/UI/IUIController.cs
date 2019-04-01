using Main.Domain.Players;

namespace Main.Domain.UI
{
    public interface IUIController
    {
        void UpdateLobby(Player[] allPlayers);
        void UpdateDefusing(string defuseAttemptBombId, bool isPlayerDefuser);
        void UpdateEnd(int nbBombsDefused);
    }
}