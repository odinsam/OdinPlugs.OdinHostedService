using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using OdinPlugs.OdinHostedService.BgServiceModels;
using OdinPlugs.OdinUtils.OdinExtensions.BasicExtensions.OdinObject;
using Serilog;

namespace OdinPlugs.OdinHostedService.OdinBackgroundService.OdinRecurringJob
{
    public class BgServiceRecurringJob : BackgroundService
    {
        private readonly Action action;
        private int executionCount = 0;
        private Timer _timer;
        TimeSpan period;
        public BgServiceRecurringJob(OdinRecurringJob_Model options)
        {
            this.period = options.Period;
            this.action = options.ActionJob;
        }
        private void RecurringJobWorker(object state)
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
            Log.Information("后台服务 【 BgService - RecurringJob 】 【 Run 】");
#endif
            _timer = new Timer(RecurringJobWorker, null, TimeSpan.Zero, period);
            // worker = DoWorkAsync();
            return ExecuteAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
#if DEBUG
            Log.Information($"Service:【 BgService - RecurringJob - Stop 】\tTime:【 {DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss")} 】");
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