using System.Linq;
using IdentityServer4.EFCore.Server.Data;
using IdentityServer4.EFCore.Server.Extensions;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer4.EFCore.Server
{
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

            var ddd = Configuration["Authorization:IdentityServer4"];

            services.AddDbContext<TestDbContext>(options =>
            {
                options.UseMySQL("Data Source=10.27.225.165;port=3306;Initial Catalog=id4_test2;user id=wcsuser;password=wcsuser;charset=utf8;Convert Zero Datetime=True;Allow Zero Datetime=True;SslMode=none;TreatTinyAsBoolean=true;");
            });

            //参见：http://www.cnblogs.com/stulzq/p/8120518.html
            services.AddIdentityServer()
                .AddConfigurationStore<TestDbContext>()
                .AddOperationalStore<TestDbContext>()
                .AddInMemoryApiResources(Config.GetApiResources())  //配置资源
                .AddInMemoryClients(Config.GetClients())        //配置客户端
                .AddProfileService<ProfileService>()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //使用identityserver中间件
            app.UseIdentityServer();
            app.UseMvc();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                //serviceScope.ServiceProvider.GetRequiredService<TestDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<TestDbContext>();
                context.Database.Migrate();
                if (!(context.Clients.Count() > 0))
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }

                    context.SaveChanges();
                }

                //if (!context.IdentityResources.Any())
                //{
                //    foreach (var resource in Config.GetIdentityResources())
                //    {
                //        context.IdentityResources.Add(resource.ToEntity());
                //    }
                //    context.SaveChanges();
                //}

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }

    }
}
