using Main.Domain.DefuseAttempts;

namespace Main.Domain
{
    public interface IDeviceInfo
    {
        BombLanguage GetDeviceBombLanguage();
    }
}