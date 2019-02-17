using Main.Domain.Players;
using Main.Domain.UI;

namespace Main.Infrastructure.UI
{
    public class LobbyLayer : BaseLayer, ILobbyLayer
    {
        public override View GetView()
        {
            return View.Lobby;
        }

        public void UpdatePlayerList(Player[] allPlayers)
        {
            throw new System.NotImplementedException();
        }
    }
}