using System;
using System.Reactive.Subjects;
using System.Timers;
using Ornette.Application.MusicPlayer;

namespace Music.Adapter.Bass
{
    public class BassTrackPlayer : ITrackPlayer
    {
        private readonly int _Stream;
        private readonly Subject<PlayEvent> _EventEmitter = new Subject<PlayEvent>();
        private readonly Timer _Timer;

        public TimeSpan? Duration { get; }
        public PlayState State { get; private set; }

        private double MaxPositionInSeconds => Convert(Un4seen.Bass.Bass.BASS_ChannelGetLength(_Stream));
        private double CurrentPositionInSeconds => Convert(Un4seen.Bass.Bass.BASS_ChannelGetPosition(_Stream));

        public BassTrackPlayer(int stream, int timerInterval = 100)
        {
            _Stream = stream;
            State = PlayState.Ready;
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

        private double Convert(long position) => Un4seen.Bass.Bass.BASS_ChannelBytes2Seconds(_Stream, position);

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
            Un4seen.Bass.Bass.BASS_ChannelPause(_Stream);
            TimerStop(PlayState.Paused);
        }

        public void Play()
        {
            State = PlayState.Playing;
            Un4seen.Bass.Bass.BASS_ChannelPlay(_Stream, false);
            TimerStart();
        }

        public void Stop()
        {
            Un4seen.Bass.Bass.BASS_ChannelStop(_Stream);
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
            State = PlayState.Ready;
            Un4seen.Bass.Bass.BASS_ChannelStop(_Stream);
            Un4seen.Bass.Bass.BASS_StreamFree(_Stream);
            _Timer.Elapsed -= _Timer_Elapsed;
            _Timer.Stop();
            _Timer.Dispose();
        }

        public void SetPosition(TimeSpan value)
        {
            var position = Un4seen.Bass.Bass.BASS_ChannelSeconds2Bytes(_Stream, value.TotalSeconds);
            Un4seen.Bass.Bass.BASS_ChannelSetPosition(_Stream, position);
            EmitEvent();
        }

        public IDisposable Subscribe(IObserver<PlayEvent> observer)
        {
            return _EventEmitter.Subscribe(observer);
        }

        private void EmitEvent()
        {
            var @event = new PlayEvent(TimeSpan.FromSeconds(CurrentPositionInSeconds), State);
            _EventEmitter.OnNext(@event);
        }
    }
}
