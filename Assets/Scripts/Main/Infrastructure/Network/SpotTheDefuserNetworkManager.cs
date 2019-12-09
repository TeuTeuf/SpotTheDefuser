using System;
using Main.Domain.Network;
using Mirror;
using Mirror.LiteNetLib4Mirror;
using Zenject;

namespace Main.Infrastructure.Network
{
    public class SpotTheDefuserNetworkManager : LiteNetLib4MirrorNetworkManager, ISpotTheDefuserNetworkManager
    {
        private ISpotTheDefuserNetworkDiscovery _spotTheDefuserNetworkDiscovery;
        
        private NetworkRole _networkRole = NetworkRole.None;
        
        private enum NetworkRole { None, Host, Client }

        [Inject]
        public void Init(ISpotTheDefuserNetworkDiscovery spotTheDefuserNetworkDiscovery)
        {
            _spotTheDefuserNetworkDiscovery = spotTheDefuserNetworkDiscovery;
        }

        public void Host()
        {
            _networkRole = NetworkRole.Host;
            StartHost();
        }

        public void Join(string hostAddress)
        {
            _networkRole = NetworkRole.Client;
            networkAddress = hostAddress;
            StartClient();
        }

        public void Stop()
        {
            switch (_networkRole)
            {
                case NetworkRole.Host:
                    StopHost();
                    break;
                case NetworkRole.Client:
                    StopClient();
                    break;
            }
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