namespace Main.Domain.DefuseAttempts
{
    public class DefuserCounter
    {
        public int GetNumberOfDefuserPlayers(int nbAllPlayers)
        {
            var isNumberOfPlayersEven = nbAllPlayers % 2 == 0;
            var nbDefuserPlayers = nbAllPlayers / 2;

            if (isNumberOfPlayersEven)
            {
                nbDefuserPlayers--;
            }

            return nbDefuserPlayers;
        }
    }
}