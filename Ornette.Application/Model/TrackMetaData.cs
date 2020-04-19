using System;

namespace Ornette.Application.Model
{
    public class TrackMetaData
    {
        public TrackMetaData(string name, TimeSpan? duration = null, int? trackNumber = null)
        {
            TrackNumber = trackNumber;
            Name = name;
            Duration = duration;
        }

        public int? TrackNumber { get; }
        public string Name { get; }
        public TimeSpan? Duration { get; }
    }
}
