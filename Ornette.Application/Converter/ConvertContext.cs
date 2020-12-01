namespace Ornette.Application.Converter
{
    public interface IConvertContext
    {
        TargetDestination Destination { get; }

        ISourceDefinition Source { get; }
    }
}
