using System;

namespace Ornette.Application
{
    public interface IMusicPlayer  : IDisposable
    {
        double Volume { get; set; }

        ITrackPlayer CreateTrackPlayer(string path);
    }
}
