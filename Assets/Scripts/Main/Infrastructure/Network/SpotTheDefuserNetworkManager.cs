using Main.Domain.Network;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using Zenject;

namespace Main.Infrastructure.Network
{
    public class SpotTheDefuserNetworkManager : NetworkManager, ISpotTheDefuserNetworkManager
    {
        private ISpotTheDefuserNetworkDiscovery _spotTheDefuserNetworkDiscovery;

        [Inject]
        public void Init(ISpotTheDefuserNetworkDiscovery spotTheDefuserNetworkDiscovery)
        {
            _spotTheDefuserNetworkDiscovery = spotTheDefuserNetworkDiscovery;
        }

        public void Host()
        {
            StartHost();
        }

        public void Join(string hostAddress)
        {
            networkAddress = hostAddress;
            StartClient();
        }

        public override void OnClientConnect(NetworkConnection networkConnection)
        {
            base.OnClientConnect(networkConnection);
            if (networkConnection.address != "localServer")
            {
                _spotTheDefuserNetworkDiscovery.StopBroadcastingOnLAN();
            }
        }
    }
}