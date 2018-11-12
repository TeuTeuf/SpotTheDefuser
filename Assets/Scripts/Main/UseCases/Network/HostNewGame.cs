using Main.Domain.Network;

namespace Main.UseCases.Network
{
    public class HostNewGame
    {
        private readonly ILobbyManager _lobbyManager;

        public HostNewGame(ILobbyManager lobbyManager)
        {
            _lobbyManager = lobbyManager;
        }

        public virtual void Host()
        {
            _lobbyManager.Host();
        }
    }
}