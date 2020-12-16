using System;
using System.Threading;
using Ornette.Application.Io;

namespace Ornette.Application.Converter.Strategy
{
    public interface IConvertFolderStrategy
    {
        void IntrospectFolder(FolderContext context, IConverterDispatcher converter, IProgress<IConvertUpdate> progress, CancellationToken token);
    }
}
