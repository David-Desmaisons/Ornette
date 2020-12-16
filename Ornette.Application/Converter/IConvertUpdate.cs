using Ornette.Application.Converter.Command;
using Ornette.Application.Message;

namespace Ornette.Application.Converter
{
    public interface IConvertUpdate : IFeedback
    {
        ConvertCommand ContextCommand { get; }
    }
}