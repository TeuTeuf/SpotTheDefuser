using System;
using UnityEngine.Networking;

namespace Main.Infrastructure.Network
{
    public class NetworkBehaviourChecker
    {
        public virtual bool HasAuthority(NetworkBehaviour networkBehaviour)
        {
            return networkBehaviour.hasAuthority;
        }
        
        public virtual bool IsServer(NetworkBehaviour networkBehaviour)
        {
            return networkBehaviour.isServer;
        }
    }
}