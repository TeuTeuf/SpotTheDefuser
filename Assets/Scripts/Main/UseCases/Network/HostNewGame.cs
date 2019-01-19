using Main.Domain.Network;
using Main.Domain.Players;
using Main.Domain.UI;

namespace Main.UseCases.Network
{
    public class HostNewGame
    {
        private readonly ISpotTheDefuserNetworkManager _spotTheDefuserNetworkManager;
        private readonly ISpotTheDefuserNetworkDiscovery _spotTheDefuserNetworkDiscovery;
        private readonly IViewManager _viewManager;
        private readonly AllPlayers _allPlayers;

        public HostNewGame(
            ISpotTheDefuserNetworkManager spotTheDefuserNetworkManager,
            ISpotTheDefuserNetworkDiscovery spotTheDefuserNetworkDiscovery,
            IViewManager viewManager,
            AllPlayers allPlayers
        ) {
            _spotTheDefuserNetworkManager = spotTheDefuserNetworkManager;
            _spotTheDefuserNetworkDiscovery = spotTheDefuserNetworkDiscovery;
            _viewManager = viewManager;
            _allPlayers = allPlayers;
        }

        public virtual void Host(string playerName)
        {
            _spotTheDefuserNetworkManager.Host();
            _spotTheDefuserNetworkDiscovery.StartBroadcastingOnLAN();
            _viewManager.ReplaceCurrentLayers(View.Lobby);
            _allPlayers.LocalPlayer = new Player(playerName);
        }
    }
}