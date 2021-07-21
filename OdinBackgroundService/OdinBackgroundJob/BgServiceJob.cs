using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using OdinPlugs.OdinHostedService.BgServiceModels;
using Serilog;

namespace OdinPlugs.OdinHostedService.OdinBackgroundService.OdinBackgroundJob
{
    public class BgServiceJob : BackgroundService
    {
        private readonly Action action;
        private readonly Action startAsyncAction;
        private readonly Action executeAsyncAction;
        private readonly Action stopAsyncAction;
        private readonly Action disposeAction;
        private int executionCount = 0;
        public BgServiceJob(OdinBgJob_Model options)
        {
            this.action = options.ActionJob;
            this.startAsyncAction = options.StartAsyncAction;
            this.executeAsyncAction = options.ExecuteAsyncAction;
            this.stopAsyncAction = options.StopAsyncAction;
            this.disposeAction = options.DisposeAction;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
#if DEBUG
            Log.Information("后台服务 【 BgService - Loop - Job 】 【 Run 】");
#endif
            this.startAsyncAction();
            return ExecuteAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.executeAsyncAction();
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
#if DEBUG
            Log.Information($"Service:【 BgService - Loop - Job - Stop 】\tTime:【 {DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss")} 】");
#endif
            this.stopAsyncAction();
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            this.disposeAction();
        }
    }
}