using System.Collections.ObjectModel;
using Neutronium.MVVMComponents;
using Neutronium.MVVMComponents.Relay;
using Ornette.Application.Model;

namespace Ornette.Application.ViewModel
{
    public class PlayerViewModel : Neutronium.BuildingBlocks.ViewModel
    {
        private readonly Model.Player _Player;

        public ObservableCollection<TrackMetaData> Tracks => _Player.Tracks;
        public double Volume => _Player.Volume;


        private TrackMetaData _CurrentTrack;
        public TrackMetaData CurrentTrack
        {
            get => _CurrentTrack;
            private set
            {
                if (!Set(ref _CurrentTrack, value))
                    return;

                UpdatePlayer(value);
            }
        }

        public ICommandWithoutParameter Play { get; }
        public ICommandWithoutParameter Pause { get; }
        public ICommandWithoutParameter Stop { get; }

        public PlayerViewModel(Model.Player player)
        {
            _Player = player;

            Play  = new RelayToogleCommand(player.Play);
            Pause = new RelayToogleCommand(player.Pause);
            Stop  = new RelayToogleCommand(player.Stop);
        }
    }
}
