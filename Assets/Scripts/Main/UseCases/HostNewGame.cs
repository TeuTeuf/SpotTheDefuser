using UnityEngine;

namespace Main.UseCases
{
    public class HostNewGame
    {
        public virtual void Host(string playerName)
        {
            Debug.Log("Host new game by " + playerName);
        }
    }
}