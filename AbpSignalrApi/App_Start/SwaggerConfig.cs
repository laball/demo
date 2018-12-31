using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using Microsoft.Web.Http.Routing;
using Swashbuckle.Application;
using Swashbuckle.Swagger;

namespace AbpSignalrApi
{
    public static class SwaggerConfig
    {
        public static void UseSwagger(this HttpConfiguration httpConfiguration)
        {
            httpConfiguration.EnableSwagger(c =>
            {
                c.MultipleApiVersions((apiDesc, version) =>
                {
                    //移除version参数，并替换URL中的版本参数，便于使用Swaggger页面测试接口
                    var versionPra = apiDesc.ParameterDescriptions.FirstOrDefault(p => p.Name == ApiVersionKey);
                    if (versionPra != null)
                    {
                        apiDesc.ParameterDescriptions.Remove(versionPra);
                    }

                    ApiVersionRouteConstraint versionConstraint = null;
                    if (apiDesc.Route.Constraints.ContainsKey(ApiVersionKey))
                    {
                        versionConstraint = apiDesc.Route.Constraints[ApiVersionKey] as ApiVersionRouteConstraint;
                        if (versionConstraint != null)
                        {
                            apiDesc.RelativePath = apiDesc.RelativePath.Replace($"{{{ApiVersionKey}}}", version);
                        }
                    }

                    return versionConstraint != null;
                },
                    vc =>
                    {
                        vc.Version("1", "API V1");
                    });

                c.GroupActionsBy(apiDesc => apiDesc.GetControllerAndActionAttributes<ApiGroupAttribute>().Any() ?
                apiDesc.GetControllerAndActionAttributes<ApiGroupAttribute>().First().GroupName + "_" +
                apiDesc.GetControllerAndActionAttributes<ApiGroupAttribute>().First().Useage : "无模块归类");

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                var commentsStock = "Bin//AbpSignalrApi.xml";
                var commentsStockFile = Path.Combine(baseDirectory, commentsStock);
                c.IncludeXmlComments(commentsStockFile);
            })
                .EnableSwaggerUi(c =>
                {
                    c.InjectJavaScript(Assembly.GetExecutingAssembly(), "Snw.Compass.Web.Scripts.swagger_lang.js");
                    //swagger集成权限验证
                    var thisAssembly = typeof(SwaggerConfig).Assembly;
                    c.InjectJavaScript(thisAssembly, authFile);
                    c.DisableValidator();
                });

            //RegisterServices(httpConfiguration);
        }
        private const string authFile = "Snw.Compass.Web.Scripts.swagger_randomauth.js";
        private const string ApiVersionKey = "apiVersion";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void RegisterServices(HttpConfiguration config)
        {
            var constraintsResolver = new DefaultInlineConstraintResolver();
            constraintsResolver.ConstraintMap.Add("apiVersionConstraint", typeof(ApiVersionRouteConstraint));
            config.MapHttpAttributeRoutes(constraintsResolver);
            //config.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(config));
        }
    }
}