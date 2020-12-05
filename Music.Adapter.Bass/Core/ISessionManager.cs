using System;

namespace Music.Adapter.Bass.Core
{
    public interface ISessionManager : IDisposable
    {
        void Restart();
    }
}
