using Main.Domain.Network;
using UnityEngine.Networking;

namespace Main.Infrastructure.Network
{
    public class SpotTheDefuserNetworkManager : ISpotTheDefuserNetworkManager
    {
        private readonly NetworkManager _networkManager;
        
        public SpotTheDefuserNetworkManager(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public void Host()
        {
            _networkManager.StartHost();
        }
    }
}