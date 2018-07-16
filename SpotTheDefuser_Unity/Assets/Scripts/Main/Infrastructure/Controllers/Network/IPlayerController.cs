namespace Main.Infrastructure.Controllers.Network
{
    public interface IPlayerController
    {
        void CmdSetNewDefuseAttempt();
        void CmdAddNewPlayer(string playerName);
        void CmdTryToDefuse();
    }
}