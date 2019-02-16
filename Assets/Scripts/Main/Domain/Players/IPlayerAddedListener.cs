namespace Main.Domain.Players
{
    public interface IPlayerAddedListener
    {
        void OnPlayerAdded(Player player);
    }
}