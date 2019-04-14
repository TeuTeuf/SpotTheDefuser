using Main.Domain.DefuseAttempts;
using UnityEngine;

namespace Main.Infrastructure
{
    public class DefusingTime : IDefusingTime
    {
        public float GetDeltaTime()
        {
            return Time.deltaTime;
        }
    }
}