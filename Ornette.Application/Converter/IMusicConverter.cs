using System;
using System.Threading;

namespace Ornette.Application.Converter
{
    public interface IMusicConverter
    {
        IObservable<ConvertedFile> Start(CancellationToken token = default(CancellationToken), IProgress<IConvertUpdate> progress = null);
    }
}
