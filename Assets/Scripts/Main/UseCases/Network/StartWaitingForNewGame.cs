using Main.Domain.Network;
using Main.Infrastructure.Network;

namespace Main.UseCases.Network
{
    public class StartWaitingForNewGame
    {
        private readonly ISpotTheDefuserNetworkDiscovery _spotTheDefuserNetworkDiscovery;

        public StartWaitingForNewGame(ISpotTheDefuserNetworkDiscovery spotTheDefuserNetworkDiscovery)
        {
            _spotTheDefuserNetworkDiscovery = spotTheDefuserNetworkDiscovery;
        }

        public virtual void Start(string playerName)
        {
            _spotTheDefuserNetworkDiscovery.StartListeningBroadcastOnLAN();
        }
    }
}