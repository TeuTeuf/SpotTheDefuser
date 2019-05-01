using System.Collections;
using System.Net;
using Main.Domain.Network;
using Main.UseCases.Network;
using Mirror;
using Mirror.LiteNetLib4Mirror;
using UnityEngine;
using Zenject;

namespace Main.Infrastructure.Network
{
    public class SpotTheDefuserNetworkDiscovery : LiteNetLib4MirrorDiscovery, ISpotTheDefuserNetworkDiscovery
    {
        private ConnectToNewGame _connectToNewGame;

        private bool _listeningForClient = false;
        private bool _listeningForHost = false;

        [Inject]
        public void Init(ConnectToNewGame connectToNewGame)
        {
            _connectToNewGame = connectToNewGame;
        }

        public void StartBroadcastingOnLAN()
        {
            _listeningForClient = true;
        }

        public void StopBroadcastingOnLAN()
        {
            _listeningForClient = false;
        }

        protected override bool ProcessDiscoveryRequest(IPEndPoint ipEndPoint, string text, out string response)
        {
            var responseStatus = _listeningForClient ? "accepted" : "rejected";
            response = $"{text}::{responseStatus}";
            
            Debug.Log($"Received request from {ipEndPoint}: {response}");

            return _listeningForClient;
        }

        public void StartListeningBroadcastOnLAN()
        {
            _listeningForHost = true;
            InitializeFinder();
            onDiscoveryResponse.AddListener(OnDiscoveryAccepted);
            StartCoroutine(SendDiscoveryRequests());
        }

        private IEnumerator SendDiscoveryRequests()
        {
            while (_listeningForHost)
            {
                SendDiscoveryRequest("SpotTheDefuser::LookingForHost");
                Debug.Log("Sending Discovery Request...");
                yield return new WaitForSeconds(1);
            }
        }

        private void OnDiscoveryAccepted(IPEndPoint ipEndPoint, string text)
        {
            if (!_listeningForHost)
                return;
            
            Debug.Log($"Discovery request accepted: {ipEndPoint}");
            _listeningForHost = false;
            _connectToNewGame.Connect(ipEndPoint.Address.ToString());
            StopDiscovery();
        }
    }
}