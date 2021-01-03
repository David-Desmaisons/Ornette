namespace Ornette.Application.Io
{
    public interface IFileParser<T> where T: class
    {
        T Parse(string path);
    }
}
