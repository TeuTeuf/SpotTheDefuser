using UnityEngine.Networking;
using Zenject;

namespace Main.Infrastructure.Controllers.Network
{
    public class ServerController : NetworkBehaviour
    {
        [Inject] public NetworkControllers NetworkControllers { get; set; }
        
        public void Awake()
        {
            NetworkControllers.ServerController = this;
        }

    }
}