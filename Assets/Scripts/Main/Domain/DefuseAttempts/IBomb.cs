using UnityEngine;

namespace Main.Domain.DefuseAttempts
{
    public interface IBomb
    {
        string Id { get; }
        
        Sprite GetSprite(bool isDefuser);
    }
}