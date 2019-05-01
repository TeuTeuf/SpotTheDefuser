namespace Main.Domain.Network
{
    public interface ISpotTheDefuserNetworkDiscovery
    {
        void StartBroadcastingOnLAN();
        void StopBroadcastingOnLAN();

        void StartListeningBroadcastOnLAN();
    }
}