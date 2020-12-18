using Ornette.Application.Converter.Command;
using Ornette.Application.Message;

namespace Ornette.Application.Converter
{
    internal class ConvertUpdate : IConvertUpdate
    {
        public MessageType Type { get; }
        public string Information { get; }
        public ConvertCommand ContextCommand { get; }

        public ConvertUpdate(MessageType type, string information, ConvertCommand contextCommand)
        {
            Type = type;
            Information = information;
            ContextCommand = contextCommand;
        }

        public ConvertUpdate(Feedback feedback, ConvertCommand contextCommand)
        {
            Type = feedback.Type;
            Information = feedback.Information;
            ContextCommand = contextCommand;
        }
    }
}
