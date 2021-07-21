using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using OdinPlugs.OdinHostedService.BgServiceModels;
using OdinPlugs.OdinUtils.OdinExtensions.BasicExtensions.OdinObject;
using Serilog;

namespace OdinPlugs.OdinHostedService.OdinBackgroundService.OdinScheduleJob
{
    public class BgServiceScheduleJob : BackgroundService
    {
        private readonly Action action;
        private int executionCount = 0;
        private Timer _timer;
        int dueTime;
        public BgServiceScheduleJob(OdinScheduleJob_Model options)
        {
            this.dueTime = options.DueTime;
            this.action = options.ActionJob;
        }
        private void ScheduleJobWorker(object state)
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
            Log.Information("后台服务 【 BgService - ScheduleJob 】 【 run 】");
#endif
            _timer = new Timer(ScheduleJobWorker, null, dueTime, Timeout.Infinite);
            return ExecuteAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
#if DEBUG
            Log.Information($"Service:【 BgService - ScheduleJob - Stop 】\tTime:【 {DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss")} 】");
#endif
            _timer?.Change(Timeout.Infinite, 0);
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _timer?.Dispose();
        }
    }
}