using Ornette.Application.Converter.Command;

namespace Ornette.Application.Converter
{
    public interface IMusicConverterBuilder
    {
        IMusicConverter BuildFromDirectory(FolderImporterCommand command);
    }
}
