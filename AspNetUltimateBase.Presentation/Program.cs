using NLog;
using NLog.Web;
using AspNetUltimateBase.Application.Extensions;
using AspNetUltimateBase.Infrastructure.Extensions;
using AspNetUltimateBase.Infrastructure.Seeders;
using AspNetUltimateBase.Presentation;
using AspNetUltimateBase.Presentation.Extensions;

Logger logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("Starting up...");

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseNLog();

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation(builder.Configuration);

WebApplication app = builder.Build();

IServiceScope scope = app.Services.CreateScope();
IPopulator populator = scope.ServiceProvider.GetRequiredService<IPopulator>();
await populator.Populate();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(ProgramConsts.AllowAllCorsPolicyName);
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "REST API v1");
    c.RoutePrefix = "";
});
app.MapDefaultControllerRoute();

app.Run();
