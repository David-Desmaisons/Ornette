using System;
using System.Threading;
using Ornette.Application.Io;
using Ornette.Application.Message;

namespace Ornette.Application.Converter.Strategy
{
    public interface IConvertFolderStrategy
    {
        void IntrospectFolder(FolderContext context, IConverterDispatcher converter, IProgress<Feedback> progress, CancellationToken token);
    }
}
