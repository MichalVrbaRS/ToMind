using Blazored.LocalStorage;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using ToMind.Components;
using ToMind.Data;
using ToMind.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddXmlFile("web.config", optional: true, reloadOnChange: true);
AddWebConfigAppSettings(builder.Configuration, builder.Environment.ContentRootPath);
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options =>
    {
        options.DetailedErrors = builder.Environment.IsDevelopment();
    });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ListLocalStorage>();
builder.Services.AddSingleton<ListSecurityService>();
builder.Services.AddScoped<TopBarState>();
var connectionString = builder.Configuration.GetConnectionString("ToMind")
    ?? throw new InvalidOperationException("Connection string 'ToMind' not found.");
builder.Services.AddDbContext<ToMindDbContext>(options =>
    options.UseSqlite(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

static void AddWebConfigAppSettings(IConfigurationBuilder configuration, string contentRoot)
{
    var webConfigPath = Path.Combine(contentRoot, "web.config");
    if (!File.Exists(webConfigPath))
    {
        return;
    }

    var document = XDocument.Load(webConfigPath);
    var appSettings = document.Root?.Element("appSettings");
    if (appSettings is null)
    {
        return;
    }

    var settings = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
    foreach (var entry in appSettings.Elements("add"))
    {
        var key = entry.Attribute("key")?.Value;
        if (string.IsNullOrWhiteSpace(key))
        {
            continue;
        }

        settings[key] = entry.Attribute("value")?.Value;
    }

    if (settings.Count > 0)
    {
        configuration.AddInMemoryCollection(settings);
    }
}
