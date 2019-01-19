using Main.Domain.Network;

namespace Main.UseCases.Network
{
    public class ConnectToNewGame
    {
        private readonly ISpotTheDefuserNetworkManager _spotTheDefuserNetworkManager;
        private readonly ISpotTheDefuserNetworkDiscovery _spotTheDefuserNetworkDiscovery;

        public ConnectToNewGame(ISpotTheDefuserNetworkManager spotTheDefuserNetworkManager,
            ISpotTheDefuserNetworkDiscovery spotTheDefuserNetworkDiscovery)
        {
            _spotTheDefuserNetworkDiscovery = spotTheDefuserNetworkDiscovery;
            _spotTheDefuserNetworkManager = spotTheDefuserNetworkManager;
        }

        public virtual void Connect(string hostAddress)
        {
            _spotTheDefuserNetworkManager.Join(hostAddress);
            _spotTheDefuserNetworkDiscovery.StopBroadcastingOnLAN();
        }
    }
}