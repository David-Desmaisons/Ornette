using System;
using System.Linq;
using System.Threading;
using Ornette.Application.Io;
using Ornette.Application.Io.Extension;
using Ornette.Application.Message;

namespace Ornette.Application.Converter.Strategy
{
    public class ConvertFolderStrategy: IConvertFolderStrategy
    {
        public void IntrospectFolder(FolderContext context, IConverterDispatcher converter, IProgress<Feedback> progress,
            CancellationToken token)
        {
            DoIntrospectFolder(context, converter, progress, token);
            converter.OnEnd();
        }

        private void DoIntrospectFolder(FolderContext context, IConverterDispatcher converter, IProgress<Feedback> progress,
            CancellationToken token)
        {
            var lossless = context.GetAll(FileType.LosslessMusic).ToList();
            if (lossless.Count == 0)
            {
                progress.Report(Feedback.Warning("Nothing to import"));
                return;
            }
        }
    }
}
