namespace Main.Domain.DefuseAttempts
{
    public interface INewDefuseAttemptSetListener
    {
        void OnNewDefuseAttemptSet(DefuseAttempt defuseAttempt);
    }
}