using Ornette.Application.Converter.Command;

namespace Ornette.Application.Converter
{
    public interface IConvertUpdate
    {
        ImportCommand ContextCommand { get; }
    }
}