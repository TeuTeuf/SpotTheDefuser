using Main.Infrastructure;

namespace Main.Domain.Players
{
    public class Player
    {
        public readonly string Name;
        private RandomSTD yolo;

        public Player() {}
        
        public Player(string name)
        {
            Name = name;
        }
    }
}
