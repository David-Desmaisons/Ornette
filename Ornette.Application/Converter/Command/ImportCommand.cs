namespace Ornette.Application.Converter.Command
{
    public abstract class ImportCommand
    {
        public abstract SourceType SourceType { get; }
    }
}
