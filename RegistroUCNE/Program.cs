using Microsoft.EntityFrameworkCore;
using RegistroUCNE.Components;
using RegistroUCNE.DAL;
using RegistroUCNE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var ConStr = builder.Configuration.GetConnectionString("ConStr");
builder.Services.AddDbContextFactory<Contexto>(o => o.UseNpgsql(ConStr));

builder.Services.AddScoped<RegistradorService>();
builder.Services.AddScoped<DocumentoService>();
builder.Services.AddScoped<EstudianteService>();
builder.Services.AddScoped<TipoDocumentoService>();
builder.Services.AddScoped<ConfiguracionSistemaService>();
builder.Services.AddScoped<SessionService>();
builder.Services.AddSingleton<GitHubSyncService>();

builder.Services.AddBlazorBootstrap();

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

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
