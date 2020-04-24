namespace Ornette.Application.Infra
{
    public interface IRandomProvider
    {
        int Next(int maxValue);
    }
}
