using System;
using System.Linq;
using System.Threading;
using Ornette.Application.Converter.Strategy.Cluster;
using Ornette.Application.Integration.Cue;
using Ornette.Application.Io;
using Ornette.Application.Io.Extension;
using Ornette.Application.Message;

namespace Ornette.Application.Converter.Strategy.Implementation
{
    public class BasicConvertFolderStrategy : IConvertFolderStrategy
    {
        private readonly IFileParser<CueSheet> _CueFileParser;
        private readonly IClusterFactory _ClusterFactory;

        public BasicConvertFolderStrategy(IFileParser<CueSheet> cueFileParser, IClusterFactory clusterFactory)
        {
            _CueFileParser = cueFileParser;
            _ClusterFactory = clusterFactory;
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
            var clusters = _ClusterFactory.GetClustersFromFolderContext(context)
                                .Where(ctx => ctx.IsLossless)
                                .ToList();

            if (clusters.Count == 0)
            {
                progress.Report(Feedback.Warning("Nothing to import"));
                return;
            }
        }
    }
}
