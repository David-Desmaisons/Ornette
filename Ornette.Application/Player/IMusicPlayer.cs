using System;

namespace Ornette.Application.Player
{
    public interface IMusicPlayer  : IDisposable
    {
        double Volume { get; set; }

        ITrackPlayer CreateTrackPlayer(string path);
    }
}
