using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;


namespace WebApp.Helpers;


/// <summary>
/// Web Application Helper For Swagger Configuration.
/// </summary>
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    
    /// <summary>
    /// Defines All API Versions Supported.
    /// </summary>
    private readonly IApiVersionDescriptionProvider _provider;

    
    /// <summary>
    /// Basic Swagger Configurator Constructor.
    ///  - Defines Version Provider.
    /// </summary>
    /// <param name="provider">Defines All API Versions Supported.</param>
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
        _provider = provider;
    
    
    /// <summary>
    /// Method Configures Swagger With Needed Options.
    /// </summary>
    /// <param name="options">Defines Configuration Options.</param>
    public void Configure(SwaggerGenOptions options)
    {
        // Add All Possible API Versions Found
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                new OpenApiInfo
                {
                    Title = $"API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString()
                });
        }
        
        // Include XML Comments (Enable Creation In CS PROJ file)
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
        
        // Use FullName for SchemaID - Avoids Conflicts Between Classes Using The Same Name
        options.CustomSchemaIds(i => i.FullName);
        
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description =
                "JWT Authorization header using the Bearer scheme.\r\n<br>" +
                "Enter 'Bearer'[space] and then your token in the text box below.\r\n<br>" +
                "Example: <b>Bearer eyJhbGciOiJIUzUxMiIsIn...</b>\r\n<br>" +
                "You will get the bearer from the <i>account/login</i> or <i>account/register</i> endpoint.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
    }
}