using CoreWebApp.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWebApp
{

    /// <summary>
    /// see http://www.jessetalk.cn/2017/11/11/aspnet-core-object-webhost/
    /// 
    /// Startup可以继承接口 <see cref="Microsoft.AspNetCore.Hosting.IStartup"/>
    /// VS默认添加的Startup类，不继承，采用的是Convention的方式；
    /// 
    /// 继承<see cref="Microsoft.AspNetCore.Hosting.IStartup"/>会带来部分不便，
    /// 如，需要在<see cref="Microsoft.AspNetCore.Hosting.IStartup.Configure(IApplicationBuilder)"/>方法中使用更多参数时；
    /// 
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<TestMiddleware>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware<TestMiddleware>();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
