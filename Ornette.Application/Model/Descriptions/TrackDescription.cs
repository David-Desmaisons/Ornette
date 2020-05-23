using System;

namespace Ornette.Application.Model.Descriptions
{
    public class TrackDescription
    {
        public TrackDescription(string name, AlbumDescription album, uint? trackNumber = null, TimeSpan? duration = null)
        {
            TrackNumber = trackNumber;
            Name = name;
            Album = album;
            Duration = duration;
        }

        public uint? TrackNumber { get; }
        public string Name { get; }
        public TimeSpan? Duration { get; }
        public AlbumDescription Album { get; }
    }
}
