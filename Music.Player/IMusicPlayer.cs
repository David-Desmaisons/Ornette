using System;

namespace Music.Player
{
    public interface IMusicPlayer  : IDisposable
    {
        double Volume { get; set; }

        ITrackPlayer CreateTrackPlayer(string path);
    }
}
