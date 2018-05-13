using System.Collections.ObjectModel;

namespace Main.Domain.Players
{
    public interface IPlayersRepository
    {
        void Add(Player player);
        void Remove(Player player);
        ReadOnlyCollection<Player> GetAll();
    }
}
