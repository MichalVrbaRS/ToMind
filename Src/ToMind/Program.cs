using Blazored.LocalStorage;
using Microsoft.EntityFrameworkCore;
using ToMind.Components;
using ToMind.Data;
using ToMind.Services;

var builder = WebApplication.CreateBuilder(args);

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
