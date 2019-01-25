using Main.Domain;
using Main.Domain.Network;
using Main.UseCases.Network;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Main.Infrastructure.Network
{
    public class SpotTheDefuserNetworkDiscovery : NetworkDiscovery, ISpotTheDefuserNetworkDiscovery
    {
        private ConnectToNewGame _connectToNewGame;

        [Inject]
        public void Init(ConnectToNewGame connectToNewGame)
        {
            _connectToNewGame = connectToNewGame;
        }

        public void Start()
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
            _connectToNewGame.Connect(fromAddress);
        }
    }
}