using Main.Domain.UI;

namespace Main.Infrastructure.UI
{
    public class LobbyLayer : BaseLayer
    {
        public override View GetView()
        {
            return View.Lobby;
        }
    }
}