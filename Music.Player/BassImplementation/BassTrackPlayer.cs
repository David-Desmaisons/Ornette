using System;
using System.Reactive.Subjects;
using System.Timers;
using Un4seen.Bass;

namespace Music.Player.BassImplementation
{
    public class BassTrackPlayer : ITrackPlayer
    {
        private int _Stream;
        private readonly Subject<TrackEvent> _EventEmitter = new Subject<TrackEvent>();
        private readonly Timer _Timer;

        public TimeSpan? Duration { get; }
        public PlayState State { get; private set; }

        private double MaxPositionInSeconds => (_Stream==0)? 0 : Convert(Bass.BASS_ChannelGetLength(_Stream));
        private double CurrentPositionInSeconds => (_Stream == 0) ? 0 : Convert(Bass.BASS_ChannelGetPosition(_Stream));

        public BassTrackPlayer(int stream, int timerInterval = 100)
        {
            _Stream = stream;
            State = (_Stream == 0) ? PlayState.Broken : PlayState.Ready;
            _Timer = new Timer(timerInterval)
            {
                AutoReset = true
            };
            _Timer.Elapsed += _Timer_Elapsed;
            Duration = TimeSpan.FromSeconds(MaxPositionInSeconds);
        }

        private void _Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnTimer();
        }

        private double Convert(long position) => Bass.BASS_ChannelBytes2Seconds(_Stream, position);

        protected void OnTimer()
        {
            if (CurrentPositionInSeconds >= MaxPositionInSeconds)
            {
                State = PlayState.Ready;
            }

            EmitEvent();
        }

        public void Pause()
        {
            if (_Stream != 0)
                return;

            Bass.BASS_ChannelPause(_Stream);
            TimerStop(PlayState.Paused);
        }

        public void Play()
        {
            if (_Stream != 0)
                return;

            State = PlayState.Playing;
            Bass.BASS_ChannelPlay(_Stream, false);
            TimerStart();
        }

        public void Stop()
        {
            if (_Stream != 0)
                return;

            Bass.BASS_ChannelStop(_Stream);
            TimerStop(PlayState.Ready);
        }

        private void TimerStart()
        {
            _Timer.Start();
            EmitEvent();
        }

        private void TimerStop(PlayState state)
        {
            State = state;
            _Timer.Stop();
            EmitEvent();
        }

        public void Dispose()
        {
            if (_Stream == 0)
                return;

            State = PlayState.Ready;
            Bass.BASS_ChannelStop(_Stream);
            Bass.BASS_StreamFree(_Stream);
            _Timer.Elapsed -= _Timer_Elapsed;
            _Timer.Stop();
            _Timer.Dispose();
            _Stream = 0;
        }

        public void SetPosition(TimeSpan value)
        {
            if (_Stream == 0)
                return;

            var position = Bass.BASS_ChannelSeconds2Bytes(_Stream, value.TotalSeconds);
            Bass.BASS_ChannelSetPosition(_Stream, position);
            EmitEvent();
        }

        public IDisposable Subscribe(IObserver<TrackEvent> observer)
        {
            return _EventEmitter.Subscribe(observer);
        }

        private void EmitEvent()
        {
            var @event = new TrackEvent(TimeSpan.FromSeconds(CurrentPositionInSeconds), State);
            _EventEmitter.OnNext(@event);
        }
    }
}
