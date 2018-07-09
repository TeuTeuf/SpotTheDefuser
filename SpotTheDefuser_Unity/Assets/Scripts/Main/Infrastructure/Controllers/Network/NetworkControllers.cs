using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Main.Infrastructure.Controllers.Network
{
    public class NetworkControllers
    {
        public ServerController ServerController { get; set; }
        public PlayerController LocalPlayerController { get; set; }
        
        private readonly List<PlayerController> _playerControllersOnServer;
        
        public NetworkControllers()
        {
            _playerControllersOnServer = new List<PlayerController>();
        }
        
        public void AddPlayerControllerOnServer(PlayerController playerController)
        {
            _playerControllersOnServer.Add(playerController);
        }

        public ReadOnlyCollection<PlayerController> GetPlayerControllersOnServer()
        {
            return _playerControllersOnServer.AsReadOnly();
        }
    }
}