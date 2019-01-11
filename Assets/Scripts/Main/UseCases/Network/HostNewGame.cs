using Main.Domain.Network;
using Main.Domain.UI;

namespace Main.UseCases.Network
{
    public class HostNewGame
    {
        private readonly ISpotTheDefuserNetworkManager _spotTheDefuserNetworkManager;
        private readonly ISpotTheDefuserNetworkDiscovery _spotTheDefuserNetworkDiscovery;

        public HostNewGame(ISpotTheDefuserNetworkManager spotTheDefuserNetworkManager,
            ISpotTheDefuserNetworkDiscovery spotTheDefuserNetworkDiscovery)
        {
            _spotTheDefuserNetworkManager = spotTheDefuserNetworkManager;
            _spotTheDefuserNetworkDiscovery = spotTheDefuserNetworkDiscovery;
        }

        public virtual void Host()
        {
            _spotTheDefuserNetworkManager.Host();
            _spotTheDefuserNetworkDiscovery.StartBroadcastingOnLAN();
        }
    }
}