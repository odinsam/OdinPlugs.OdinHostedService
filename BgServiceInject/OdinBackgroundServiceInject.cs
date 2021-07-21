using System;
using Microsoft.Extensions.DependencyInjection;
using OdinPlugs.OdinHostedService.BgServiceModels;
using OdinPlugs.OdinHostedService.OdinBackgroundService.OdinBackgroundJob;
using OdinPlugs.OdinHostedService.OdinBackgroundService.OdinBackgroundLoopJob;
using OdinPlugs.OdinHostedService.OdinBackgroundService.OdinRecurringJob;
using OdinPlugs.OdinHostedService.OdinBackgroundService.OdinScheduleJob;

namespace OdinPlugs.OdinHostedService.BgServiceInject
{
    public static class OdinBackgroundServiceInject
    {
        /// <summary>
        /// 后台任务 - 自定义任务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOdinBgServiceJob(this IServiceCollection services, Action<OdinBgJob_Model> options)
        {
            var opt = new OdinBgJob_Model();
            options(opt);
            services.AddHostedService<BgServiceJob>(
                provider => new BgServiceJob(opt));
            return services;
        }

        /// <summary>
        /// 后台任务 - 普通任务，立即执行，只执行一次
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOdinBgServiceNomalJob(this IServiceCollection services, Action<OdinNomalJob_Model> options)
        {
            var opt = new OdinNomalJob_Model();
            options(opt);
            services.AddHostedService<BgServiceNomalJob>(
                provider => new BgServiceNomalJob(opt));
            return services;
        }

        /// <summary>
        /// 后台任务 - 延迟调用，只执行一次
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOdinBgServiceScheduleJob(this IServiceCollection services, Action<OdinScheduleJob_Model> options)
        {
            var opt = new OdinScheduleJob_Model();
            options(opt);
            services.AddHostedService<BgServiceScheduleJob>(
                provider => new BgServiceScheduleJob(opt));
            return services;
        }


        /// <summary>
        /// 后台任务 - 循环任务执行：重复执行的任务，使用常见的时间循环模式
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOdinBgServiceRecurringJob(this IServiceCollection services, Action<OdinRecurringJob_Model> options)
        {
            var opt = new OdinRecurringJob_Model();
            options(opt);
            services.AddHostedService<BgServiceRecurringJob>(
                provider => new BgServiceRecurringJob(opt));
            return services;
        }

        /// <summary>
        /// 后台任务 - 循环任务执行：重复执行的任务(任务执行完后继续自动执行)
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOdinBgServiceLoopJob(this IServiceCollection services, Action<OdinJob_Model> options)
        {
            var opt = new OdinJob_Model();
            options(opt);
            services.AddHostedService<BgServiceLoopJob>(
                provider => new BgServiceLoopJob(opt));
            return services;
        }
    }
}