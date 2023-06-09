using Microsoft.AspNetCore.ResponseCompression;
using UI.Entities;
using UI.Hubs;
using UI.Mappers;
using UI.Services;
using UI.Services.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// add services to the container.
builder.Services.AddSingleton<IGameEngine, GameEngine>();
builder.Services.AddSingleton<IConverter<Game, UI.Models.Game>, GameEntityToModelMapper>();
builder.Services.AddSingleton<IConverter<Player, UI.Models.Player>, PlayerEntityToModelMapper>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddResponseCompression(opts =>
{
  opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
    new[] { "application/octet-stream" });
});

WebApplication app = builder.Build();

app.UseResponseCompression();

// configure the https request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<GameHub>("/gamehub/{GameCode}");
app.MapFallbackToPage("/_Host");

app.Run();