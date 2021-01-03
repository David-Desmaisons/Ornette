namespace Ornette.Application.Io
{
    public interface IFileReader
    {
        string[] ReadAllLines(string path);
    }
}
