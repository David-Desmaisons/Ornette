namespace Ornette.Application.Converter.Command
{
    public class Mp3Encoding
    {
        public Mp3Encoding(int bitRate, int targetSampleRate, Mp3EncodingMode mode, Mp3EncodingQuality quality)
        {
            BitRate = bitRate;
            TargetSampleRate = targetSampleRate;
            Mode = mode;
            Quality = quality;
        }

        public int BitRate { get;  }
        public int TargetSampleRate { get; }
        public Mp3EncodingMode Mode { get; }
        public Mp3EncodingQuality Quality { get; }

        public static Mp3Encoding HighStandard { get; } = new Mp3Encoding(320, 44100, Mp3EncodingMode.Default, Mp3EncodingQuality.Quality);
        public static Mp3Encoding HighestStandard { get; } = new Mp3Encoding(320, 44100, Mp3EncodingMode.Default, Mp3EncodingQuality.Q0);
    }
}
