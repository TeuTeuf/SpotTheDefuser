using Main.Domain.Network;
using Main.Domain.UI;
using Main.Infrastructure.Controllers.Network;

namespace Main.UseCases.Network
{
    public class StartWaitingForNewGame
    {
        private readonly ISpotTheDefuserNetworkDiscovery _spotTheDefuserNetworkDiscovery;
        private readonly IViewManager _viewManager;
        private readonly AllPlayerControllers _allPlayerControllers;

        public StartWaitingForNewGame(ISpotTheDefuserNetworkDiscovery spotTheDefuserNetworkDiscovery, IViewManager viewManager, AllPlayerControllers allPlayerControllers)
        {
            _spotTheDefuserNetworkDiscovery = spotTheDefuserNetworkDiscovery;
            _viewManager = viewManager;
            _allPlayerControllers = allPlayerControllers;
        }

        public virtual void Start(string playerName)
        {
            _allPlayerControllers.LocalPlayerName = playerName;
            _spotTheDefuserNetworkDiscovery.StartListeningBroadcastOnLAN();
            _viewManager.ReplaceCurrentLayers(View.Lobby);
        }
    }
}