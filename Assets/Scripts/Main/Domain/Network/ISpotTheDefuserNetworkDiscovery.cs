namespace Main.Domain.Network
{
    public interface ISpotTheDefuserNetworkDiscovery
    {
        void StartBroadcastingOnLAN();
        void StopBroadcastingOnLAN();

        void StartListeningBroadcastOnLAN();
        void OnReceivedBroadcast(string fromAddress, string data);
    }
}