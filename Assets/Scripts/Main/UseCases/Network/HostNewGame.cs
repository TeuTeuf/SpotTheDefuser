using Main.Domain.Network;

namespace Main.UseCases.Network
{
    public class HostNewGame
    {
        private readonly INetworkManager _networkManager;

        public HostNewGame(INetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public virtual void Host()
        {
            _networkManager.StartHost();
        }
    }
}