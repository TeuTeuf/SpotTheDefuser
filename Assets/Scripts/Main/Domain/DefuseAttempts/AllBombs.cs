namespace Main.Domain.DefuseAttempts
{
    public class AllBombs
    {
        private readonly IBomb[] _bombs;
        private readonly IRandom _random;

        public AllBombs(IRandom random, IBomb[] bombs)
        {
            _random = random;
            _bombs = bombs;
        }
        
        public virtual string PickRandomBombId()
        {
            var bombIndex = _random.Range(0, _bombs.Length);
            return _bombs[bombIndex].Id;
        }
    }
}