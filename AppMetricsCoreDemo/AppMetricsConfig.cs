using System;
using App.Metrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppMetricsCoreDemo
{
    public static class AppMetricsConfig
    {
        public static IServiceCollection UseAppMatrics(
            this IServiceCollection services,
            IConfiguration configuration,
            string sectionKey = AppMetricsConsts.DefaultConfigurationSection)
        {
            var options = new AppMetricsOptions();
            sectionKey = sectionKey ?? AppMetricsConsts.DefaultConfigurationSection;
            ConfigurationBinder.Bind(configuration, sectionKey, options);

            return UseAppMatrics(services, options);
        }

        public static IServiceCollection UseAppMatrics(
            this IServiceCollection services,
            AppMetricsOptions appMetricsOptions)
        {
            if (appMetricsOptions.Enable)
            {
                var metricsBuiler = AppMetrics.CreateDefaultBuilder()
                    .Configuration
                    .Configure(options =>
                        {
                            options.AddAppTag(appMetricsOptions.App);
                            options.AddEnvTag(appMetricsOptions.Env);
                        })
                        .Report.ToInfluxDb(
              options =>
              {
                  options.InfluxDb.BaseUri = new Uri(appMetricsOptions.ConnectionString);
                  options.InfluxDb.Database = appMetricsOptions.DataBase;
                  options.InfluxDb.UserName = appMetricsOptions.UserName;
                  options.InfluxDb.Password = appMetricsOptions.Password;
                  options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
                  options.HttpPolicy.FailuresBeforeBackoff = 5;
                  options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                  options.FlushInterval = TimeSpan.FromSeconds(5);
              })
              .Build();

                services.AddMetrics(metricsBuiler);

                //TODO:2.x 需要调用次方法
                //services.AddMetricsReportScheduler();

                //3.0 后需要引用App.Metrics.Extensions.Hosting 并调用次AddMetricsReportingHostedService方法
                //3.0 App.Metrics.AspNetCore.Tracking 已废弃；
                services.AddMetricsReportingHostedService();

                services.AddMetricsTrackingMiddleware();
                services.AddMetricsEndpoints();

                //*************************************************************************
                //Unable to load one or more of the requested types. Retrieve the LoaderExceptions property for more information.
                //原因是App.Metrics.Health.Abstractions在执行过程中会导致异常；
                //var dependencyContext = DependencyContext.Load(typeof(CustomHealthCheck).Assembly);
                //var healthBuilder = AppMetricsHealth.CreateDefaultBuilder()
                //    .HealthChecks.RegisterFromAssembly(services, dependencyContext)
                //    .BuildAndAddTo(services);

                //services.AddHealth(healthBuilder);
                //services.AddHealthEndpoints(Configuration);
                //*************************************************************************
            }

            return services;
        }

        public static IApplicationBuilder ConfigureAppMatrics(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseMetricsAllMiddleware();
            // Or to cherry-pick the tracking of interest
            //app.UseMetricsActiveRequestMiddleware();
            //app.UseMetricsErrorTrackingMiddleware();
            //app.UseMetricsPostAndPutSizeTrackingMiddleware();
            //app.UseMetricsRequestTrackingMiddleware();
            //app.UseMetricsOAuth2TrackingMiddleware();
            //app.UseMetricsApdexTrackingMiddleware();

            app.UseMetricsAllEndpoints();
            //app.UseHealthAllEndpoints();
            //// Or to cherry-pick endpoint of interest
            //app.UseMetricsEndpoint();
            //app.UseMetricsTextEndpoint();
            //app.UseEnvInfoEndpoint();

            return app;
        }

    }
}
