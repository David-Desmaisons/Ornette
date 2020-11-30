using System;

namespace Ornette.Application.Player
{
    public interface IMusicPlayer  : IDisposable
    {
        int Volume { get; set; }

        ITrackPlayer CreateTrackPlayer(string path);
    }
}
