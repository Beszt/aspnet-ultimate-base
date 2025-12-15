using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using AspNetUltimateBase.Presentation.Settings;

namespace AspNetUltimateBase.Presentation.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddCors(options =>
        {
            options.AddPolicy(ProgramConsts.AllowAllCorsPolicyName, policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        JwtSettings jwt = new();
        configuration.GetSection("Jwt").Bind(jwt);
        services.AddSingleton(jwt);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Bearer";
            options.DefaultScheme = "Bearer";
            options.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwt.Issuer,
                ValidAudience = jwt.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
            };
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = ProgramInfo.AppVersion,
                Title = $"{ProgramInfo.Name} API",
                Description = "Demo showcasing example endpoints of the aspnet-ultimate-base template for kickstarting new ASP.NET Web API projects with a ready-to-go developer experience.",
                Contact = new OpenApiContact
                {
                    Name = "Maciej Obarzanek",
                    Email = "Maciej.Obarzanek@gmail.com",
                    Url = new Uri("https://github.com/Beszt/aspnet-ultimate-base")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/license/mit/")
                },
            });
            c.EnableAnnotations();
        });
    }
}
