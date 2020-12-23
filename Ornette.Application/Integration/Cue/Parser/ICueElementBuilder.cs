namespace Ornette.Application.Integration.Cue.Parser
{
    public interface ICueElementBuilder
    {
        ICueElementBuilder Parse(string command, string[] parameters);

        ICueElementBuilder End();
    }
}
