using System;
using Core.Dto;
using Core.Integrated;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.AppStart;
using WebApplication1.MT;
using WebApplication1.Quartz;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //see https://www.cnblogs.com/xiangchangdong/p/8602737.html

            //不必调用
            //services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            services.EnableCors()
                .EnableVersion()
                .UseSwagger()
                .UseRedis()
                //.UseNHibernate()
                .UseEF(Configuration.GetConnectionString("DefaultConnection"))
                .UseMassTransitEx(typeof(DemoMessageConsumer).Assembly)
                .UseQuartz(c => new JobFactory(c), typeof(DemoJob).Assembly)
                .UseAbpAutoMapper(typeof(UserOutputDTO).Assembly)
                .UseRefit();

            services.AddMvc().AddControllersAsServices(); // 需要调用
            services.AddMvcCore().AddApiExplorer();

            return services.UseAutofac();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="applicationLifetime"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.ConfigQuartz(serviceProvider, applicationLifetime);
            app.ConfigMassTransit(serviceProvider, applicationLifetime);
            app.UseMvcWithDefaultRoute();
            app.ConfigSwaggerUI();
        }
    }
}