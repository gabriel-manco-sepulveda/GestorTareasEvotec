using Evotec.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Evotec.Client.Extensiones;

using Evotec.Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5204/") });

builder.Services.AddScoped<IEstadoService, EstadoService>();
builder.Services.AddScoped<ITareaService, TareaService>();

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AutenticacionExtension>();
builder.Services.AddAuthorizationCore();

builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();



