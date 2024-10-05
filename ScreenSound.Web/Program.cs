using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ScreenSound.Web;
using ScreenSound.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddTransient<ArtistasAPI>();
builder.Services.AddTransient<MusicasAPI>();
//builder.Services.AddScoped<ArtistaAPI>();

builder.Services.AddHttpClient("API", client => {
	client.BaseAddress = new Uri(builder.Configuration["APIServer:Url"]!);
	client.DefaultRequestHeaders.Add("Accept", "application/json");
});
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
