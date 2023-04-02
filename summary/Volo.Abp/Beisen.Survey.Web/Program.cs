using Serilog;
using Serilog.Events;

namespace Beisen.Survey.Web
{
    public static class Program
    {
        public async static Task<int> Main(string[] args)
        {
            BuildLog();
            return await Run(args);
        }

        static async Task<int> Run(string[] args)
        {
            try
            {
                Log.Information("Starting web host.");
                var builder = WebApplication.CreateBuilder(args);
                builder.Host.AddAppSettingsSecretsJson()
                    .UseAutofac()
                    .UseSerilog();
                await builder.AddApplicationAsync<SurveyWebModule>();
                var app = builder.Build();
                await app.InitializeApplicationAsync();
                await app.RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        static void BuildLog()
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File($"Logs/logs-{DateTime.Now.ToString("yyyy-MM-dd hh-mm")}.log"))
            .WriteTo.Async(c => c.Console())
            .WriteTo.Async(c => c.Debug())
            .CreateLogger();
        }

        public static void Main_Old(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}