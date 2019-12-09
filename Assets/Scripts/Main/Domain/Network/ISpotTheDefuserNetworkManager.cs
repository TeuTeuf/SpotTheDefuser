namespace Main.Domain.Network
{
    public interface ISpotTheDefuserNetworkManager
    {
        void Host();
        void Join(string hostAddress);
        void Stop();
    }
}