using System;

namespace Ornette.Application.MusicPlayer
{
    public interface IMusicPlayer  : IDisposable
    {
        int Volume { get; set; }

        ITrackPlayer CreateTrackPlayer(string path);
    }
}
