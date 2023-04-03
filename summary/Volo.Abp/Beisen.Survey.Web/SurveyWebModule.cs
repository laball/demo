using Beisen.Survey.Application;
using Beisen.Survey.EntityFrameworkCore;
using Beisen.Survey.HttpApi;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace Beisen.Survey.Web
{
    [DependsOn(typeof(SurveyHttpApiModule))]
    [DependsOn(typeof(SurveyApplicationModule))]
    [DependsOn(typeof(SurveyEntityFrameworkCoreModule))]
    [DependsOn(typeof(AbpAutofacModule))]
    [DependsOn(typeof(AbpAutoMapperModule))]
    [DependsOn(typeof(AbpSwashbuckleModule))]
    public class SurveyWebModule : AbpModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Survey API");
            });

            // app.UseUnitOfWork();
            // app.UseAuditing();
            // app.UseAbpSerilogEnrichers();

            app.UseConfiguredEndpoints();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            // 绕过授权服务，通常用于集成测试
            context.Services.AddAlwaysAllowAuthorization();

            ConfigureAutoMapper();
            ConfigureAutoApiControllers();
            ConfigureSwaggerServices(context.Services);
            ConfigureAntiForgery();
        }

        //private void ConfigureClaimsService()
        //{
        //    Configure<AbpClaimsServiceOptions>(options =>
        //    {
        //        options.RequestedClaims.Add("SocialSecurityNumber");
        //    });
        //}

        private void ConfigureAntiForgery()
        {
            Configure<AbpAntiForgeryOptions>(options =>
            {
                options.AutoValidate = false;// 直接通过开关关闭
                //options.AutoValidateFilter = type => false;// 设置过滤器关闭
                //options.AutoValidateIgnoredHttpMethods.Add("POST");// 加入到忽略Http方法列表中
            });
        }

        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<SurveyWebModule>();
                options.AddMaps<SurveyApplicationModule>();
            });
        }

        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(SurveyApplicationModule).Assembly);
            });
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddAbpSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Survey API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                    options.HideAbpEndpoints();
                    options.SchemaGeneratorOptions.SchemaFilters.Add(new DisableVoloAbpSchemaFilter());
                }
            );
        }
    }
}