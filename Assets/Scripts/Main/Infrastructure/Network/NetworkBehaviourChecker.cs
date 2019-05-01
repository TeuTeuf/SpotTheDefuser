using Mirror;

namespace Main.Infrastructure.Network
{
    public class NetworkBehaviourChecker
    {
        public virtual bool IsLocalPlayer(NetworkBehaviour networkBehaviour)
        {
            return networkBehaviour.hasAuthority;
        }
        
        public virtual bool IsHostingLocalPlayer(NetworkBehaviour networkBehaviour)
        {
            return networkBehaviour.hasAuthority && networkBehaviour.isServer;
        }
    }
}