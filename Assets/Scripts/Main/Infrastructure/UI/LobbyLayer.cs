using Main.Domain.Players;
using Main.Domain.UI;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Main.Infrastructure.UI
{
    public class LobbyLayer : BaseLayer, ILobbyLayer
    {
        [FormerlySerializedAs("NbDefusersText")] public Text nbDefusersText;
        [FormerlySerializedAs("NbBombsText")] public Text nbBombsText;

        private const string DEFAULT_NB_DEFUSERS_DISPLAYED = "1";
        private const string DEFAULT_NB_BOMBS_DISPLAYED = "0";

        public void Start()
        {
            nbDefusersText.text = DEFAULT_NB_DEFUSERS_DISPLAYED;
            nbBombsText.text = DEFAULT_NB_BOMBS_DISPLAYED;
        }

        public void UpdatePlayerList(Player[] allPlayers)
        {
            nbDefusersText.text = allPlayers.Length.ToString();
        }

        public override View GetView()
        {
            return View.Lobby;
        }
    }
}