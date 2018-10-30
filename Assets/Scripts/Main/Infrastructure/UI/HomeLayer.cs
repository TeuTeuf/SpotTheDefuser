using UnityEngine;

namespace Main.Infrastructure.UI
{
    public class HomeLayer : BasicViewLayer
    {
        public void OnEndEditOnPlayerName(string playerName)
        {
            Debug.Log("OnEndEditOnPlayerName: " + playerName);
        }
        
        public void OnClickOnHost()
        {
            Debug.Log("OnClickOnHost");
        }
        
        public void OnClickOnJoin()
        {
            Debug.Log("OnClickOnJoin");
        }
    }
}