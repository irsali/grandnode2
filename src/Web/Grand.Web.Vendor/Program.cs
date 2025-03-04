using Grand.Infrastructure;
using Grand.Web.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

//add configuration
builder.Configuration.AddAppSettingsJsonFile(args);

builder.AddServiceDefaults();

builder.Host.UseDefaultServiceProvider((_, options) =>
{
    options.ValidateScopes = false;
    options.ValidateOnBuild = false;
});

//add services
StartupBase.ConfigureServices(builder.Services, builder.Configuration);

builder.ConfigureApplicationSettings();

//build app
var app = builder.Build();

//request pipeline
StartupBase.ConfigureRequestPipeline(app, builder.Environment);

//run app
await app.RunAsync();