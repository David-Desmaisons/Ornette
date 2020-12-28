namespace Ornette.Application.Integration.Cue.Parser
{
    public interface ICueElementBuilder
    {
        ICueElementBuilder Parse(CueInstruction command);

        CueSheet Build();
    }

    public interface ICueElementAggregator<in T> : ICueElementBuilder
    {
        void AddChild(T element);
    }
}
