using Main.Infrastructure.Data;
using UnityEngine;

namespace Main.Domain.DefuseAttempts
{
    public class AllBombs
    {
        private readonly IBomb[] _bombs;

        public AllBombs(IBomb[] bombs)
        {
            _bombs = bombs;
        }
        
        public virtual string PickRandomBombName()
        {
            foreach (var bomb in _bombs)
            {
                Debug.Log($"{bomb.Id} {bomb.GetSprite(true).name} {bomb.GetSprite(false).name}");
            }
            Debug.Log($"nb bombs{_bombs.Length}");
            Debug.LogWarning("Implement Me !");
            return "PickedRandomBomb";
        }
    }
}