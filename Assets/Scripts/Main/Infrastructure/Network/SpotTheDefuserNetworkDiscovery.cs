using Main.Domain;
using Main.Domain.Network;
using UnityEngine;
using UnityEngine.Networking;

namespace Main.Infrastructure.Network
{
    public class SpotTheDefuserNetworkDiscovery : NetworkDiscovery, ISpotTheDefuserNetworkDiscovery
    {
        private void Start()
        {
            Initialize();
        }

        public void StartBroadcastingOnLAN()
        {
            StartAsServer();
        }

        public void StopBroadcastingOnLAN()
        {
            StopBroadcast();
        }

        public void StartListeningBroadcastOnLAN()
        {
            StartAsClient();
        }

        public override void OnReceivedBroadcast(string fromAddress, string data)
        {
            StopBroadcast();
            base.OnReceivedBroadcast(fromAddress, data);
            Debug.Log("OnReceivedBroadcast");
        }
    }
}