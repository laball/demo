using NLog.Web;
using Summary.Net7.WebAPI.Filters;

namespace Summary.Net7.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("init main function");
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

                builder.Services.AddControllers();

                //builder.Services.AddScoped(sp => )

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

                app.UseAuthorization();
                app.MapControllers();
                app.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}