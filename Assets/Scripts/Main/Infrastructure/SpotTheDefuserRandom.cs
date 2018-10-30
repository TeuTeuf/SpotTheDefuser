using Main.Domain;
using UnityEngine;

namespace Main.Infrastructure
{
    public class SpotTheDefuserRandom : IRandom
    {
        public int Range(int includedMin, int excludedMax)
        {
            return Random.Range(includedMin, excludedMax);
        }
    }
}