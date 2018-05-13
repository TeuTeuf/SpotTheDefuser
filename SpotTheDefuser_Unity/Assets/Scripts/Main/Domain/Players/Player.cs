namespace Main.Domain.Players
{
    public class Player
    {
        private readonly string _pseudo;

        public string Pseudo
        {
            get { return _pseudo; }
        }

        public Player(string pseudo)
        {
            _pseudo = pseudo;
        }
    }
}
