using System;

namespace Ornette.Application.Model.Descriptions
{
    public class TrackDescription
    {
        public TrackDescription(string name, AlbumDescription album, TimeSpan? duration = null, int? trackNumber = null)
        {
            TrackNumber = trackNumber;
            Name = name;
            Album = album;
            Duration = duration;
        }

        public int? TrackNumber { get; }
        public string Name { get; }
        public TimeSpan? Duration { get; }
        public AlbumDescription Album { get; }
    }
}
