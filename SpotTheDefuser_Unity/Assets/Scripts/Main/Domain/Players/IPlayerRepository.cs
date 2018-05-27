using System.Collections.ObjectModel;

namespace Main.Domain.Players
{
    public interface IPlayerRepository
    {
        void Add(Player player);
        void Remove(Player player);
        ReadOnlyCollection<Player> GetAll();
    }
}
