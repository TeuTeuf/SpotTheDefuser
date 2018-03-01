using System.Collections.ObjectModel;

namespace SpotTheDefuser_Unity.Assets.Scripts.Main.Domain
{
    public interface IPlayerRepository
    {
        void Add(Player player);
        void Remove(Player player);
        ReadOnlyCollection<Player> GetAll();
    }
}
