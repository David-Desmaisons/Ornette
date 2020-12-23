namespace Ornette.Application.Integration.Cue.Parser
{
    public interface IParserContext
    {
        IParserContext Parse(string command, string[] parameters);
    }
}
