using System;

namespace Ornette.Application.Model.Descriptions
{
    public class TrackDescriptionBuilder
    {
        public TrackDescription Build()
        {
            return new TrackDescription(Name, Album, TrackNumber, Duration);
        }

        public TrackPositionDescription TrackNumber { get;  private set; }
        public string Name { get;  private set; }
        public TimeSpan? Duration { get; private set;}
        private Func<AlbumDescription> _AlbumDescriptionBuilder = () => null;
        public AlbumDescription Album => _AlbumDescriptionBuilder();

        public TrackDescriptionBuilder SetTrackNumber(uint? trackNumber)
        {
            TrackNumber = trackNumber.HasValue ? TrackPositionDescription.FromTrackPosition(trackNumber.Value) : null;
            return this;
        }

        public TrackDescriptionBuilder SetName(string name)
        {
            Name = name;
            return this;
        }

        public TrackDescriptionBuilder SetDuration(TimeSpan? duration)
        {
            Duration = duration;
            return this;
        }

        public TrackDescriptionBuilder SetAlbum(AlbumDescription album)
        {
            _AlbumDescriptionBuilder = () => album;
            return this;
        }

        public TrackDescriptionBuilder SetAlbum(AlbumDescriptionBuilder albumBuilder)
        {
            _AlbumDescriptionBuilder = albumBuilder.Build;
            return this;
        }
    }
}
