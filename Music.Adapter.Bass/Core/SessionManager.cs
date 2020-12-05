using System;
using Un4seen.Bass;

namespace Music.Adapter.Bass.Core
{
    public class SessionManager : ISessionManager
    {
        private readonly string _Email;
        private readonly string _Password;

        private static bool Is64Bits => (IntPtr.Size == 8);
        private static string Path => Is64Bits ? "x64" : "x86";

        public SessionManager(string email, string password)
        {
            _Email = email;
            _Password = password;
            Init();
        }

        private void Init()
        {
            BassNet.Registration(_Email, _Password);
            Un4seen.Bass.Bass.LoadMe(Path);

            if (!Un4seen.Bass.Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
                throw new Exception("Not possible create Bass session");
        }

        public void Dispose()
        {
            Un4seen.Bass.Bass.BASS_Free();
        }

        public void Restart()
        {
            if (Un4seen.Bass.Bass.BASS_IsStarted())
                return;

            Dispose();
            Init();
        }
    }
}
