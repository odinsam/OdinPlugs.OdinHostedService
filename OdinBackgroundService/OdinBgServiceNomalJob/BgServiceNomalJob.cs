using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using OdinPlugs.OdinHostedService.BgServiceModels;
using OdinPlugs.OdinUtils.OdinExtensions.BasicExtensions.OdinObject;
using Serilog;

namespace OdinPlugs.OdinHostedService.OdinBackgroundService.OdinBackgroundJob
{
    public class BgServiceNomalJob : BackgroundService
    {
        private readonly Action action;
        private int executionCount = 0;
        public BgServiceNomalJob(OdinNomalJob_Model options)
        {
            this.action = options.ActionJob;
        }
        private void NomalJobWorker()
        {
            try
            {
                int count = Interlocked.Increment(ref executionCount);
                this.action();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToJson(enumStringFormat.Json));
                throw ex;
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
#if DEBUG
            Log.Information("后台服务 【 BgService - Nomal - Job 】 【 Run 】");
#endif
            return ExecuteAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            NomalJobWorker();
            StopAsync(stoppingToken);
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
#if DEBUG
            Log.Information($"Service:【 BgService - Nomal - Job - Stop 】\tTime:【 {DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss")} 】");
#endif
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose() { }
    }
}