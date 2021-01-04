using System;
using System.Linq;
using System.Threading;
using Ornette.Application.Integration.Cue;
using Ornette.Application.Io;
using Ornette.Application.Io.Extension;
using Ornette.Application.Message;

namespace Ornette.Application.Converter.Strategy
{
    public class ConvertFolderStrategy: IConvertFolderStrategy
    {
        private readonly IFileParser<CueSheet> _CueFileParser;

        public ConvertFolderStrategy(IFileParser<CueSheet> cueFileParser)
        {
            _CueFileParser = cueFileParser;
        }

        public void IntrospectFolder(FolderContext context, IConverterDispatcher converter, IProgress<Feedback> progress,
            CancellationToken token)
        {
            DoIntrospectFolder(context, converter, progress, token);
            converter.OnEnd();
        }

        private void DoIntrospectFolder(FolderContext context, IConverterDispatcher converter, IProgress<Feedback> progress,
            CancellationToken token)
        {
            var clusters = context.AllContexts.Where(ctx => ctx.Get(FileType.LosslessMusic)?.Length > 0).ToList();
            if (clusters.Count == 0)
            {
                progress.Report(Feedback.Warning("Nothing to import"));
                return;
            }
        }
    }
}
