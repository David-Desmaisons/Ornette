using System;

namespace Ornette.Application.Integration.Cue
{
    public struct CueIndex
    {
        public CueIndex(int minutes, int seconds, int frames)
        {
            if (minutes > 99 || minutes < 0)
                throw new ArgumentOutOfRangeException(nameof(minutes));

            if (seconds > 59 || seconds < 0)
                throw new ArgumentOutOfRangeException(nameof(seconds));

            if (frames > 74 || frames < 0)
                throw new ArgumentOutOfRangeException(nameof(frames));

            Minutes = minutes;
            Seconds = seconds;
            Frames = frames;
        }

        public int Minutes { get; }
        public int Seconds { get; }
        public int Frames { get; }

        public int TotalFrames => TotalSeconds * 75 + Frames;
        public int TotalSeconds => Minutes * 60 + Seconds;
        public TimeSpan ToTimeSpan() => TimeSpan.FromSeconds(TotalSeconds + TotalFrames / 75);
    }
}
