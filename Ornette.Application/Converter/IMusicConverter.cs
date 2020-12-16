using System;
using System.Threading;
using Ornette.Application.Converter.Command;

namespace Ornette.Application.Converter
{
    public interface IMusicConverter<in T> where T : ConvertCommand
    {
        IObservable<ConvertedFile> Convert(T command, CancellationToken token = default(CancellationToken), IProgress<IConvertUpdate> progress = null);
    }
}
