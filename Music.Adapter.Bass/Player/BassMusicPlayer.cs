using System;
using Ornette.Application.Player;
using Un4seen.Bass;

namespace Music.Adapter.Bass.Player
{
    public class BassMusicPlayer : IMusicPlayer
    {
        private readonly string _Email;
        private readonly string _Password;

        private static bool Is64Bits => (IntPtr.Size == 8);
        private static string Path => Is64Bits ? "x64" : "x86";

        private static BassMusicPlayer _BassMusicPlayer = null;

        private BassMusicPlayer(string email, string password)
        {
            _Email = email;
            _Password = password;
        }

        private BassMusicPlayer Init()
        {
            BassNet.Registration(_Email, _Password);
            Un4seen.Bass.Bass.LoadMe(Path);

            if (!Un4seen.Bass.Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
                throw new Exception("Not possible create ");

            return this;
        }

        public int Volume
        {
            get => (int)Math.Round(Un4seen.Bass.Bass.BASS_GetConfig(BASSConfig.BASS_CONFIG_GVOL_STREAM) / 100d) ;
            set => Un4seen.Bass.Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_GVOL_STREAM, value * 100);
        }

        public static IMusicPlayer Init(string email, string password)
        {
            if (_BassMusicPlayer != null)
                return _BassMusicPlayer;

            return _BassMusicPlayer = new BassMusicPlayer(email, password).Init();
        }

        public void Dispose()
        {
            Release();
            _BassMusicPlayer = null;
        }

        private void Release()
        {
            Un4seen.Bass.Bass.BASS_Free();
        }

        private void RestartBassIfNeeded()
        {
            if (Un4seen.Bass.Bass.BASS_IsStarted())
                return;

            Release();
            Init();
        }

        public ITrackPlayer CreateTrackPlayer(string path)
        {
            RestartBassIfNeeded();
            var stream = Un4seen.Bass.Bass.BASS_StreamCreateFile(path, 0, 0, BASSFlag.BASS_DEFAULT);
            return (stream == 0) ? BrokenTrackPlayer.Instance : new BassTrackPlayer(stream);
        }
    }
}
