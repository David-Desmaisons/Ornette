using Neutronium.MVVMComponents;
using Neutronium.MVVMComponents.Relay;
using Ornette.Application.Model;
using Ornette.Application.MusicPlayer;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace Ornette.Application.ViewModel
{
    public class PlayerViewModel : ReactiveObject
    {
        private readonly IPlayer _Player;
        private readonly ObservableAsPropertyHelper<Track> _CurrentTrackMapper;
        private readonly ObservableAsPropertyHelper<int?> _PositionMapper;
        private readonly ObservableAsPropertyHelper<PlayState> _StateMapper;

        public ObservableCollection<Track> Tracks => _Player.Tracks;

        public int Volume
        {
            get => _Player.Volume;
            set => _Player.Volume = value;
        }

        public bool RandomPlay
        {
            get => _Player.RandomPlay;
            set => _Player.RandomPlay = value;
        }

        public bool AutoReplay
        {
            get => _Player.AutoReplay;
            set => _Player.AutoReplay = value;
        }

        public Track CurrentTrack
        {
            get => _CurrentTrackMapper.Value;
            set => _Player.SetCurrentTrack(value);
        }

        public int? PositionInSeconds
        {
            get => _PositionMapper.Value;
            set
            {
                if (value.HasValue)
                    _Player.SetPositionInSeconds(value.Value);
            } 
        }

        public PlayState State => _StateMapper.Value;

        public ICommandWithoutParameter Play { get; }
        public ICommandWithoutParameter Pause { get; }
        public ICommandWithoutParameter Stop { get; }
        public ICommandWithoutParameter Next { get; }
        public ICommandWithoutParameter Back { get; }

        public PlayerViewModel(IPlayer player)
        {
            _Player = player;

            _PositionMapper = _Player.Events.Select(evt => evt.PositionInSeconds).DistinctUntilChanged().ToProperty(this, nameof(PositionInSeconds));
            _StateMapper = _Player.Events.Select(evt => evt.State).ToProperty(this, nameof(State));
            _CurrentTrackMapper = _Player.CurrentTrack.ToProperty(this, nameof(CurrentTrack));

            Play = new RelayToogleCommand(player.Play);
            Pause = new RelayToogleCommand(player.Pause);
            Stop = new RelayToogleCommand(player.Stop);
            Next = new RelayToogleCommand(player.Next);
            Back = new RelayToogleCommand(player.Back);
        }
    }
}
