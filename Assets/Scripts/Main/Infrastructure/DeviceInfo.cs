using Main.Domain;
using Main.Domain.DefuseAttempts;
using UnityEngine;

namespace Main.Infrastructure
{
    public class DeviceInfo : IDeviceInfo
    {
        public BombLanguage GetDeviceBombLanguage()
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.French:
                    return BombLanguage.FRENCH;
                
                case SystemLanguage.English:
                    return BombLanguage.ENGLISH;
                
                default:
                    return BombLanguage.ENGLISH;
            }
        }
    }
}