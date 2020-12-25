namespace Ornette.Application.Integration.Cue.Parser
{
    public interface ICueElementBuilder
    {
        ICueElementBuilder Parse(CueInstruction command);

        ICueElementBuilder End();
    }

    public interface ICueElementAggregator<in T> : ICueElementBuilder
    {
        void AddChild(T element);
    }
}
