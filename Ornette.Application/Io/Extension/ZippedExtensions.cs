namespace Ornette.Application.Io.Extension
{
    public static class ZippedExtensions
    {
        public static string Rar => ".rar";
        public static string Zip => ".zip";
        public static string SevenZ => ".7z";
        public static string Tar => ".tar";
        public static string Lzh => ".lzh";

        public static readonly string[] All = {
            Rar,
            Zip,
            SevenZ,
            Tar,
            Lzh
        };
    }
}
