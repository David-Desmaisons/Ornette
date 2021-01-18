namespace Ornette.Application.Io
{
    public interface IFileParser<out T> where T: class
    {
        T Parse(string path);
    }
}
