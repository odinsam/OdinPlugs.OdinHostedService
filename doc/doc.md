# OdinPlugs.OdinHostedService

### 使用方法

1.1 后台任务 - 普通任务，立即执行，只执行一次
```csharp
services.AddOdinBgServiceNomalJob(opt =>
{
    opt.ActionJob = () =>
    {
#if DEBUG
        Log.Information($"Service:【 BgService - Nomal - Job - Running 】\tTime:【 {DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss")} 】");
#endif
    };
});
```

1.2 后台任务 - 延迟调用，只执行一次
```csharp
services.AddOdinBgServiceScheduleJob(opt =>
{
    opt.DueTime = 5000;
    opt.ActionJob = () =>
    {
#if DEBUG
        Log.Information($"Service:【 BgService - ScheduleJob - Running 】\tTime:【 {DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss")} 】");
#endif
    };
});
```

1.3 后台任务 - 循环任务执行：重复执行的任务，使用常见的时间循环模式
```csharp
services.AddOdinBgServiceScheduleJob(opt =>
{
    opt.DueTime = 5000;
    opt.ActionJob = () =>
    {
#if DEBUG
        Log.Information($"Service:【 BgService - ScheduleJob - Running 】\tTime:【 {DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss")} 】");
#endif
    };
});
```

1.4 后台任务 - 循环任务执行：重复执行的任务(任务执行完后继续自动执行)
```csharp
services.AddOdinBgServiceLoopJob(opt =>
{
    opt.ActionJob = () =>
    {
#if DEBUG
        Log.Information($"Service:【 BgService - LoopJob - Running 】\tTime:【 {DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss")} 】");
#endif
        Thread.Sleep(1000);
    };
});
```

1.5 后台任务 - 自定义任务
```csharp
services.AddOdinBgServiceJob(opt =>
{
    Timer timer = null;
    void worker(object state)
    {
#if DEBUG
        Log.Information($"Service:【 BgService - Running 】\tTime:【 {DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss")} 】");
#endif
    }
    opt.StartAsyncAction = () =>
    {
        timer = new Timer(worker, null, 0, 2000);
    };
    opt.ExecuteAsyncAction = () =>
    {

    };
    opt.StopAsyncAction = () =>
    {
        timer?.Change(Timeout.Infinite, 0);
    };
    opt.DisposeAction = () =>
    {
        timer?.Dispose();
    };
});
```

1.6 后台任务 - 多任务执行
```csharp
services
    .AddOdinBgServiceJob(opt =>
    {
        Timer timer = null;
        void worker(object state)
        {
#if DEBUG
            Log.Information($"Service:【 BgService - Running 】\tTime:【 {DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss")} 】");
#endif
        }
        opt.StartAsyncAction = () =>
        {
            timer = new Timer(worker, null, 0, 2000);
        };
        opt.ExecuteAsyncAction = () =>
        {

        };
        opt.StopAsyncAction = () =>
        {
            timer?.Change(Timeout.Infinite, 0);
        };
        opt.DisposeAction = () =>
        {
            timer?.Dispose();
        };
    })
    .AddOdinBgServiceLoopJob(opt =>
    {
        opt.ActionJob = () =>
        {
            // new ReceiveRabbitMQHelper().ReceiveMQ(_Options);
#if DEBUG
            Log.Information($"Service:【 BgService - LoopJob - Running 】\tTime:【 {DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss")} 】");
#endif
            Thread.Sleep(1000);
        };
    })
    .AddOdinBgServiceRecurringJob(opt =>
    {
        opt.Period = TimeSpan.FromSeconds(1);
        opt.ActionJob = () =>
        {
            // new ReceiveRabbitMQHelper().ReceiveMQ(_Options);
#if DEBUG
            Log.Information($"Service:【 BgService - RecurringJob - Running 】\tTime:【 {DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss")} 】");
#endif
        };
    })
    .AddOdinBgServiceNomalJob(opt =>
    {
        opt.ActionJob = () =>
        {
#if DEBUG
            Log.Information($"Service:【 BgService - Nomal- Job - Running 】\tTime:【 {DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss")} 】");
#endif
        };
    })
    .AddOdinBgServiceScheduleJob(opt =>
    {
        opt.DueTime = 5000;
        opt.ActionJob = () =>
        {
#if DEBUG
            Log.Information($"Service:【 BgService - ScheduleJob - Running 】\tTime:【 {DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss")} 】");
#endif
        };
    });
```