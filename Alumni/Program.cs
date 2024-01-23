using Alumni;
using Alumni.Infrastructure;
using Alumni.Services.Utilities;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.GetSection("Settings").Bind(AppSettings.Settings);
builder.Services.RegisterServices();
builder.Services.ApplyDatabaseMigrations();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
