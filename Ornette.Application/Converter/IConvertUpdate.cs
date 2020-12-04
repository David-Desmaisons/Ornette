using Ornette.Application.Converter.Command;
using Ornette.Application.Message;

namespace Ornette.Application.Converter
{
    public interface IConvertUpdate : IFeedback
    {
        ImportCommand ContextCommand { get; }
    }
}