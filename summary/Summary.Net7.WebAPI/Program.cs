using NLog;
using NLog.Web;
using Summary.Net7.WebAPI.Filters;
using Summary.Net7.WebAPI.Middelwares;

namespace Summary.Net7.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

            try
            {
                logger.Info("app run");

                AppRun(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        static void AppRun(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // 使用 NLog
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            // 支持构造注入的 ActionFilter
            var services = builder.Services;
            services.AddScoped(sp => new MyActionFilterAttribute(sp.GetRequiredService<ILogger<MyActionFilterAttribute>>()));
            services.AddScoped(sp => new MyExceptionFilterAttribute(sp.GetRequiredService<ILogger<MyExceptionFilterAttribute>>()));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("111");
            //    await next.Invoke();
            //});

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("222");
            //});

            app.Map("/Error", HandleErrorMap);
            app.MapWhen(context => context.Request.Query.ContainsKey("mapWhen"), HandleMapWhen);

            app.UseMiddleware<TestMiddelware>(app, Array.Empty<object>());

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        private static void HandleMapWhen(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("HandleMapWhen-Use ");
                await next.Invoke();
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("HandleMapWhen-Run ");
            });
        }

        private static void HandleErrorMap(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("HandleErrorMap-Use ");
                await next.Invoke();
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("HandleErrorMap-Run ");
            });
        }
    }
}