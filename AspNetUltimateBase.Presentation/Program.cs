using NLog;
using NLog.Web;
using AspNetUltimateBase.Application.Extensions;
using AspNetUltimateBase.Infrastructure.Extensions;
using AspNetUltimateBase.Infrastructure.Seeders;

Logger logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("Starting up...");

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add NLog to ASP.NET Core
builder.Host.UseNLog();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation(builder.Configuration);

WebApplication app = builder.Build();

// Populate database with seed data.
IServiceScope scope = app.Services.CreateScope();
IPopulator populator = scope.ServiceProvider.GetRequiredService<IPopulator>();
await populator.Populate();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "REST API v1");
    c.RoutePrefix = "";
});

app.Run();

public partial class Program { }

