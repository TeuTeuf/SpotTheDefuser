using Main.Domain;
using UnityEngine;

namespace Main.Infrastructure
{
    public class RandomSTD : IRandom
    {
        public int Range(int includedMin, int excludedMax)
        {
            return Random.Range(includedMin, excludedMax);
        }
    }
}