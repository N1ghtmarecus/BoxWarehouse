using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace WebAPI.Installers
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(x =>
            {
                x.EnableAnnotations();
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                x.ExampleFilters();

                // Add JWT Authentication
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme,
                    }
                };
                x.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, Array.Empty<string>() }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.AddSwaggerExamplesFromAssemblyOf<Program>();
        }
    }
}
