using Main.Domain.Network;

namespace Main.UseCases.Network
{
    public class StopNetwork
    {
        private readonly ISpotTheDefuserNetworkManager _spotTheDefuserNetworkManager;

        public StopNetwork(ISpotTheDefuserNetworkManager spotTheDefuserNetworkManager)
        {
            _spotTheDefuserNetworkManager = spotTheDefuserNetworkManager;
        }

        public void Stop()
        {
            _spotTheDefuserNetworkManager.Stop();
        }
    }
}