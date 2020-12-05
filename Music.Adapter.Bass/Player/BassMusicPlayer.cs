using System;
using Music.Adapter.Bass.Core;
using Ornette.Application.Player;
using Un4seen.Bass;

namespace Music.Adapter.Bass.Player
{
    public class BassMusicPlayer : IMusicPlayer
    {
        private readonly ISessionManager _SessionManager;

        public BassMusicPlayer(ISessionManager sessionManager)
        {
            _SessionManager = sessionManager;
        }

        public int Volume
        {
            get => (int)Math.Round(Un4seen.Bass.Bass.BASS_GetConfig(BASSConfig.BASS_CONFIG_GVOL_STREAM) / 100d) ;
            set => Un4seen.Bass.Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_GVOL_STREAM, value * 100);
        }

        public void Dispose()
        {
            _SessionManager.Dispose();
        }

        public ITrackPlayer CreateTrackPlayer(string path)
        {
            _SessionManager.Restart();
            var stream = Un4seen.Bass.Bass.BASS_StreamCreateFile(path, 0, 0, BASSFlag.BASS_DEFAULT);
            return (stream == 0) ? BrokenTrackPlayer.Instance : new BassTrackPlayer(stream);
        }
    }
}
