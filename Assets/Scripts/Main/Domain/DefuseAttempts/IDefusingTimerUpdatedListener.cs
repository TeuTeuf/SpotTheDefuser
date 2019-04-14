namespace Main.Domain.DefuseAttempts
{
    public interface IDefusingTimerUpdatedListener
    {
        void OnDefusingTimerUpdated(float remainingTime);
    }
}