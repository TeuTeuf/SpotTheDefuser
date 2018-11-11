using Main.Domain;
using Main.Domain.Network;
using UnityEngine;
using UnityEngine.Networking;

namespace Main.Infrastructure.Network
{
    public class SpotTheDefuserNetworkManager : INetworkManager
    {
        public void StartHost()
        {
            NetworkManager.singleton.StartHost();
            Debug.LogWarning("Improve me!");
        }
    }
}