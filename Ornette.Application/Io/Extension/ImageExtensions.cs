namespace Ornette.Application.Io.Extension
{
    public static class ImageExtensions
    {
        public static string Jpg => ".jpg";
        public static string Jpeg => ".jpeg";
        public static string Bmp => ".bmp";
        public static string Png => ".png";
        public static string Tiff => ".tiff";
        public static string Tif => ".tif";
        public static string Gif => ".gif";

        public static readonly string[] All = {
            Jpg,
            Jpeg,
            Bmp,
            Png,
            Tiff,
            Tif,
            Gif
        };
    }
}
