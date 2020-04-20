using Ornette.Application.MusicPlayer;
using System;
using Un4seen.Bass;

namespace Music.Adapter.Bass
{
    public class BassMusicPlayer : IMusicPlayer
    {
        private static bool Is64Bits => (IntPtr.Size == 8);
        private static string Path => Is64Bits ? "x64" : "x86";

        private static BassMusicPlayer _BassMusicPlayer = null;

        public double Volume
        {
            get => Un4seen.Bass.Bass.BASS_GetConfig(BASSConfig.BASS_CONFIG_GVOL_STREAM) / 100d;
            set => Un4seen.Bass.Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_GVOL_STREAM, (int)(Math.Truncate(value * 100)));
        }

        private BassMusicPlayer() { }

        public static IMusicPlayer Init(string email, string password)
        {
            if (_BassMusicPlayer != null)
                return _BassMusicPlayer;

            Un4seen.Bass.BassNet.Registration(email, password);
            Un4seen.Bass.Bass.LoadMe(Path);

            if (!Un4seen.Bass.Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
                throw new Exception("Not possible create ");

            return _BassMusicPlayer = new BassMusicPlayer(); ;
        }

        public void Dispose()
        {
            Un4seen.Bass.Bass.BASS_Free();
            _BassMusicPlayer = null;
        }

        public ITrackPlayer CreateTrackPlayer(string path)
        {
            var stream = Un4seen.Bass.Bass.BASS_StreamCreateFile(path, 0, 0, BASSFlag.BASS_DEFAULT);
            return (stream == 0) ? BrokenTrackPlayer .Instance : new BassTrackPlayer(stream);
        }
    }
}
