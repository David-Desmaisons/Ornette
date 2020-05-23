using System.Linq;

namespace Ornette.Application.Io
{
    public static class FileExtension
    {
        public static string Wv => ".wv";
        public static string Flac => ".flac";
        public static string Mp3 => ".mp3";
        public static string Mp2 => ".mp2";
        public static string Aiff => ".aiff";
        public static string Ape => ".ape";
        public static string Aac => ".aac";
        public static string M4A => ".m4a";
        public static string Mp4 => ".mp4";
        public static string Ogg => ".ogg";
        public static string Wav => ".wav";
        public static string Wma => ".wma";

        public static readonly string[] AllExtensions = {
            Mp3,
            Mp2,
            Wma,
            M4A,
            Aac,
            Mp4,
            Aiff,
            Ape,
            Flac,
            Ogg,
            Wav,
            Wv
        };
    }
}
