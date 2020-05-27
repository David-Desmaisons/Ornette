using System;

namespace Ornette.Application.Model.Descriptions
{
    public class TrackPositionDescription : IComparable<TrackPositionDescription>
    {
        public string Position =>
            DiscNumber.HasValue ? $"{DiscNumber}-{TrackPosition}" :
            Side != null ? $"{Side}{TrackPosition}" : $"{TrackPosition}";

        private string Side { get; }
        private uint? DiscNumber { get; }
        private uint TrackPosition { get; }

        private TrackPositionDescription(uint trackPosition, string side = null, uint? discNumber = null)
        {
            TrackPosition = trackPosition;
            Side = side;
            DiscNumber = discNumber;
        }

        public static TrackPositionDescription FromTrackPosition(uint trackPosition)
        {
            return new TrackPositionDescription(trackPosition);
        }

        public static TrackPositionDescription FromTrackPositionAndDiscNumber(uint trackPosition, uint discNumber)
        {
            return new TrackPositionDescription(trackPosition, discNumber: discNumber);
        }

        public static TrackPositionDescription FromTrackPositionAndDiscSide(uint trackPosition, string side)
        {
            return new TrackPositionDescription(trackPosition, side: side);
        }

        public int CompareTo(TrackPositionDescription other)
        {
            return (other == null) ? 1 : string.Compare(Position, other.Position, StringComparison.Ordinal);
        }
    }
}
