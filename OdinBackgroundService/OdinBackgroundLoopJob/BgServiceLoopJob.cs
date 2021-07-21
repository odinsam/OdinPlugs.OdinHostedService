using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using OdinPlugs.OdinHostedService.BgServiceModels;
using OdinPlugs.OdinUtils.OdinExtensions.BasicExtensions.OdinObject;
using Serilog;

namespace OdinPlugs.OdinHostedService.OdinBackgroundService.OdinBackgroundLoopJob
{
    public class BgServiceLoopJob : BackgroundService
    {
        private readonly Action action;
        private int executionCount = 0;
        Task worker;
        public BgServiceLoopJob(OdinJob_Model options)
        {
            this.action = options.ActionJob;
        }
        private async Task LoopJobWorkerAsync()
        {
            await Task.Run(() =>
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
            }).ContinueWith(t => LoopJobWorkerAsync());

        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
#if DEBUG
            Log.Information("后台服务 【 BgService - Loop - Job 】 【 Run 】");
#endif
            return ExecuteAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await LoopJobWorkerAsync();
            // return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
#if DEBUG
            Log.Information($"Service:【 BgService - Loop - Job - Stop 】\tTime:【 {DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss")} 】");
#endif
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose() { }
    }
}