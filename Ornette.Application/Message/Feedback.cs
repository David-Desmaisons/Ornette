namespace Ornette.Application.Message
{
    public class Feedback: IFeedback
    {
        private Feedback(MessageType type, string information)
        {
            Type = type;
            Information = information;
        }

        public static Feedback Warning(string message) => new Feedback(MessageType.Warning, message);
        public static Feedback Info(string message) => new Feedback(MessageType.Info, message);
        public static Feedback Error(string message) => new Feedback(MessageType.Error, message);

        public MessageType Type { get; }
        public string Information { get; }
    }
}
