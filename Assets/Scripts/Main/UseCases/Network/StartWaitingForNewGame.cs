using Main.Domain.Network;
using Main.Domain.Players;
using Main.Domain.UI;

namespace Main.UseCases.Network
{
    public class StartWaitingForNewGame
    {
        private readonly ISpotTheDefuserNetworkDiscovery _spotTheDefuserNetworkDiscovery;
        private readonly IViewManager _viewManager;
        private readonly AllPlayers _allPlayers;

        public StartWaitingForNewGame(ISpotTheDefuserNetworkDiscovery spotTheDefuserNetworkDiscovery,
            IViewManager viewManager, AllPlayers allPlayers)
        {
            _spotTheDefuserNetworkDiscovery = spotTheDefuserNetworkDiscovery;
            _viewManager = viewManager;
            _allPlayers = allPlayers;
        }

        public virtual void Start(string playerName)
        {
            _spotTheDefuserNetworkDiscovery.StartListeningBroadcastOnLAN();
            _viewManager.ReplaceCurrentLayers(View.Lobby);
            _allPlayers.LocalPlayer = new Player(playerName);
        }
    }
}