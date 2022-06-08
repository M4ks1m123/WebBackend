using WebBlazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7100") });


builder.Services.AddScoped<IProductProvider, ProductsProvider>();
builder.Services.AddScoped<IUserProvider, UsersProvider>();
builder.Services.AddScoped<IOrderProvider, OrdersProvider>();

await builder.Build().RunAsync();
