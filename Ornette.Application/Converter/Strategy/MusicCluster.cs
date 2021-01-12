namespace Ornette.Application.Converter.Strategy
{
    public class MusicCluster
    {
        public string[] MusicFiles { get; }
        public string[] ImageFiles { get; }
        public string MainFolder { get; }
        public bool IsLossless { get; }

        public MusicCluster(string mainFolder, string[] musicFiles, string[] imageFiles, bool isLossless)
        {
            MainFolder = mainFolder;
            MusicFiles = musicFiles;
            ImageFiles = imageFiles;
            IsLossless = isLossless;
        }
    }
}
