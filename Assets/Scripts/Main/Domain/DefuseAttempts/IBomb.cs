using UnityEngine;

namespace Main.Domain.DefuseAttempts
{
    public interface IBomb
    {
        string Id { get; }
        BombLanguage Language { get; }
        
        
        Sprite GetSprite(bool isDefuser);
    }
}