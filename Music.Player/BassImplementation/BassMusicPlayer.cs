using System;
using Un4seen.Bass;

namespace Music.Player.BassImplementation
{
    public class BassMusicPlayer : IMusicPlayer
    {
        private static bool Is64Bits => (IntPtr.Size == 8);
        private static string Path => Is64Bits ? "x64" : "x86";

        private static BassMusicPlayer _BassMusicPlayer = null;

        public double Volume
        {
            get => (Bass.BASS_GetConfig(BASSConfig.BASS_CONFIG_GVOL_STREAM) / 10000d);
            set => Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_GVOL_STREAM, (int)(Math.Truncate(value * 10000)));
        }

        private BassMusicPlayer() {}

        public static IMusicPlayer Init(string email, string password)
        {
            if (_BassMusicPlayer!= null)
                return _BassMusicPlayer;

            Un4seen.Bass.BassNet.Registration(email, password);
            Bass.LoadMe(Path);

            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
                throw new Exception("Not possible create ");

            return _BassMusicPlayer = new BassMusicPlayer(); ;
        }

        public void Dispose()
        {
            Bass.BASS_Free();
            _BassMusicPlayer = null;
        }

        public ITrackPlayer CreateTrackPlayer(string path)
        {
            var stream = Bass.BASS_StreamCreateFile(path, 0, 0, BASSFlag.BASS_DEFAULT);
            return new BassTrackPlayer(stream);
        }
    }
}
