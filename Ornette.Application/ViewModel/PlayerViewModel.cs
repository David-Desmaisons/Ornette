using System.Collections.ObjectModel;
using Neutronium.MVVMComponents;
using Neutronium.MVVMComponents.Relay;
using Ornette.Application.Model;
using ReactiveUI;

namespace Ornette.Application.ViewModel
{
    public class PlayerViewModel : ReactiveObject
    {
        private readonly IPlayer _Player;
        private readonly ObservableAsPropertyHelper<TrackMetaData> _CurrentTrackMapper;

        public ObservableCollection<TrackMetaData> Tracks => _Player.Tracks;

        public double Volume
        {
            get => _Player.Volume;
            set => _Player.Volume = value;
        }

        public TrackMetaData CurrentTrack
        {
            get => _CurrentTrackMapper.Value;
            set => _Player.SetCurrentTrack(value);
        }

        public ICommandWithoutParameter Play { get; }
        public ICommandWithoutParameter Pause { get; }
        public ICommandWithoutParameter Stop { get; }

        public PlayerViewModel(IPlayer player)
        {
            _Player = player;
            _CurrentTrackMapper = _Player.CurrentTrack.ToProperty(this, nameof(CurrentTrack));

            Play  = new RelayToogleCommand(player.Play);
            Pause = new RelayToogleCommand(player.Pause);
            Stop  = new RelayToogleCommand(player.Stop);
        }
    }
}
