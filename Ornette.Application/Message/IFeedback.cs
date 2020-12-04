namespace Ornette.Application.Message
{
    public interface IFeedback
    {
        MessageType Type { get; }
        string Information { get; }
    }
}
