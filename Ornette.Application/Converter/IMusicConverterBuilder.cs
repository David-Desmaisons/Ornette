namespace Ornette.Application.Converter
{
    public interface IMusicConverterBuilder
    {
        IMusicConverter BuildFromDirectory(string directory, TargetDestination destination);
    }
}
