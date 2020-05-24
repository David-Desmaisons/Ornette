using System.Collections.Generic;
using Ornette.Application.Model;

namespace Ornette.Application.Io
{
    public interface IIoReader
    {
        IEnumerable<Track> GetDirectoryTracks(string path);

        Track GetTrack(string path);
    }
}
