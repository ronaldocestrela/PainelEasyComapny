using WebCliente.Components;
using WebCliente.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add HttpClient
builder.Services.AddHttpClient();

// Add authentication service
builder.Services.AddScoped<IAuthService, AuthService>();

// Add health checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

// Map health check endpoint
app.MapHealthChecks("/health");

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
