using Main.Domain.Network;
using UnityEngine.Networking;

namespace Main.Infrastructure.Network
{
    public class LobbyManager : ILobbyManager
    {
        private readonly NetworkManager _networkManager;
        
        public LobbyManager(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public void Host()
        {
            _networkManager.StartHost();
        }
    }
}