namespace Main.Domain
{
    public interface IRandom
    {
        int Range(int includedMin, int excludedMax);
    }
}
