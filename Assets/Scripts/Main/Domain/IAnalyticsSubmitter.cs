using Main.Domain.UI;

namespace Main.Domain
{
    public interface IAnalyticsSubmitter
    {
        void TrackScreenVisit(View view);
        void TrackBombExploded();
        void TrackBombDefused();
    }
}