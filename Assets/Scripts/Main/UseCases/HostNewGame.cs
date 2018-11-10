using Main.Domain;

namespace Main.UseCases
{
    public class HostNewGame
    {
        private readonly INetworkDiscovery _networkDiscovery;
        private readonly INetworkManager _networkManager;

        public HostNewGame(INetworkDiscovery networkDiscovery, INetworkManager networkManager)
        {
            _networkDiscovery = networkDiscovery;
            _networkManager = networkManager;
        }

        public virtual void Host()
        {
            _networkDiscovery.StartAsServer();
            _networkManager.StartHost();
        }
    }
}