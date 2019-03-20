using UnityEngine;

namespace Main.Domain.DefuseAttempts
{
    public interface IBomb
    {
        Sprite GetSprite(bool isDefuser);
    }
}