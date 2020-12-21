using Ornette.Application.Infra;

namespace Ornette.IO.Implementation
{
    public class FileReader: IFileReader
    {
        public string[] ReadAllLines(string path)
        {
            return System.IO.File.ReadAllLines(path);
        }
    }
}
