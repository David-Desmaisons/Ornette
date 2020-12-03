using Ornette.Application.Converter.Command;

namespace Ornette.Application.Converter
{
    public interface IMusicConverterBuilder<in T> where T: ImportCommand
    {
        IMusicConverter Build(T command);
    }

    public interface IMusicConverterBuilder : IMusicConverterBuilder<FolderConverterCommand>
    {
    }
}
