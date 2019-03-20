namespace Main.Domain.DefuseAttempts
{
    public class DefuserCounter
    {
        public virtual int GetNumberOfDefuserPlayers(int nbAllPlayers)
        {
            var isNumberOfPlayersEven = nbAllPlayers % 2 == 0;
            var nbDefuserPlayers = nbAllPlayers / 2;

            if (isNumberOfPlayersEven)
            {
                nbDefuserPlayers--;
            }

            return nbDefuserPlayers;
        }

        public virtual int GetNumberOfExplosivePlayers(int nbAllPlayers)
        {
            return nbAllPlayers - GetNumberOfDefuserPlayers(nbAllPlayers);
        }
    }
}