using System;
namespace SpotTheDefuser_Unity.Assets.Scripts.Main.Domain
{
    public interface IRandom
    {
        int Range(int includedMin, int excludedMax);
    }
}
