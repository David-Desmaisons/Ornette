using System;

namespace Ornette.Application.MusicPlayer
{
    public interface IMusicPlayer  : IDisposable
    {
        double Volume { get; set; }

        ITrackPlayer CreateTrackPlayer(string path);
    }
}
