using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Beisen.Survey.Web
{
    public class DisableVoloAbpSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var keys = new List<string>();
            var prefix = "Volo.";
            foreach (var key in context?.SchemaRepository?.Schemas?.Keys)
            {
                if (key.StartsWith(prefix))
                {
                    keys.Add(key);
                }
            }

            foreach (var key in keys)
            {
                context.SchemaRepository.Schemas.Remove(key);
            }
        }
    }
}