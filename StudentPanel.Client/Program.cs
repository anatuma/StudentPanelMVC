using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StudentPanel.Client;
using StudentPanel.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.Services.AddScoped<StateContainer>();
builder.Services.AddHttpClient<StudentsApiClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5028/"); 
});

await builder.Build().RunAsync();