namespace Ornette.Application.Infra
{
    public interface IFileReader
    {
        string[] ReadAllLines(string path);
    }
}
