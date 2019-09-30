using UnityEngine;

namespace Main.Domain.DefuseAttempts
{
    public interface IBomb
    {
        string Id { get; }
        BombLanguage Language { get; }
        int MinBombsAlreadyDefused { get; }
        
        Sprite GetSprite(bool isDefuser);
    }
}