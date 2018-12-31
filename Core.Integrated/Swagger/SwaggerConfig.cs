using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Core.Integrated
{
    public static class SwaggerConfig
    {
        public static IServiceCollection UseSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.ResolveConflictingActions(apiDeses => apiDeses.FirstOrDefault());

                options.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                options.SwaggerDoc("v2", new Info { Title = "My API", Version = "v2" });

                options.DocInclusionPredicate((version, apiDes) =>
                {
                    //根据Controller的ApiVersionAttribute匹配版本
                    var versionAttrs = ((ControllerActionDescriptor)apiDes.ActionDescriptor).ControllerTypeInfo.AsType()
                        .GetCustomAttributes(false)
                        .Where(c => c is ApiVersionAttribute).Select(c => c as ApiVersionAttribute);

                    var versionMatches = from va in versionAttrs
                                         from v in va.Versions
                                         where v.MajorVersion.HasValue && version.Contains(v.MajorVersion.Value.ToString())
                                         select true;

                    if (versionMatches.Any())
                    {
                        //替换版本参数
                        var ApiVersionKey = "version";
                        var versionPra = apiDes.ParameterDescriptions.FirstOrDefault(p => p.Name == ApiVersionKey);
                        if (versionPra != null)
                        {
                            apiDes.ParameterDescriptions.Remove(versionPra);
                            apiDes.RelativePath = apiDes.RelativePath.Replace($"v{{{ApiVersionKey}}}", version);
                        }
                        return true;
                    }

                    return false;
                });

                //API分组
                options.TagActionsBy(apiDes =>
                {
                    //按ApiGroupAttribute分组
                    //Action上的按ApiGroupAttribute分组优先
                    var apiGroupAttrs = apiDes.ActionAttributes()
                        .FirstOrDefault(p => p is ApiGroupAttribute) as ApiGroupAttribute;
                    if (apiGroupAttrs == null)
                    {
                        //若Action上没有ApiGroupAttribute，则考虑Controller上的
                        apiGroupAttrs = apiDes.ControllerAttributes()
                            .FirstOrDefault(p => p is ApiGroupAttribute) as ApiGroupAttribute;
                        if (apiGroupAttrs != null)
                        {
                            return apiGroupAttrs.GroupName;
                        }
                    }
                    else
                    {
                        return apiGroupAttrs.GroupName;
                    }

                    return apiDes.GroupName;
                });
            });

            return services;
        }

        public static void ConfigSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
            });
        }
    }
}