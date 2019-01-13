using Main.Domain.Network;
using Main.Domain.UI;
using Main.Infrastructure.Controllers.Network;

namespace Main.UseCases.Network
{
    public class HostNewGame
    {
        private readonly ISpotTheDefuserNetworkManager _spotTheDefuserNetworkManager;
        private readonly ISpotTheDefuserNetworkDiscovery _spotTheDefuserNetworkDiscovery;
        private readonly IViewManager _viewManager;
        private readonly AllPlayerControllers _allPlayerControllers;

        public HostNewGame(ISpotTheDefuserNetworkManager spotTheDefuserNetworkManager,
            ISpotTheDefuserNetworkDiscovery spotTheDefuserNetworkDiscovery, IViewManager viewManager,
            AllPlayerControllers allPlayerControllers)
        {
            _allPlayerControllers = allPlayerControllers;
            _viewManager = viewManager;
            _spotTheDefuserNetworkManager = spotTheDefuserNetworkManager;
            _spotTheDefuserNetworkDiscovery = spotTheDefuserNetworkDiscovery;
        }

        public virtual void Host(string playerName)
        {
            _allPlayerControllers.LocalPlayerName = playerName;
            _spotTheDefuserNetworkManager.Host();
            _spotTheDefuserNetworkDiscovery.StartBroadcastingOnLAN();
            _viewManager.ReplaceCurrentLayers(View.Lobby);
        }
    }
}