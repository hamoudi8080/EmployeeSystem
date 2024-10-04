using BlazorEmployeeApplication;
using BlazorEmployeeApplication.Auth;
using BlazorEmployeeApplication.Models;
using BlazorEmployeeApplication.Services;
using EmployeeManagmentModel;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shared.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAutoMapper(typeof(EmployeeProfile));
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
// Configure HttpClient for API calls
builder.Services.AddHttpClient<IEmployeeService, EmployeeService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7210/");
});


builder.Services.AddHttpClient<IDepartmentService, DepartmentService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7210/");
});

builder.Services.AddScoped<IAuthService, JwtAuthService>(sp =>
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7210/")
    };
    return new JwtAuthService(httpClient);
});

// Optionally, if you need a general-purpose HttpClient with the application's base address
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
AuthorizationPolicies.AddPolicies(builder.Services);
await builder.Build().RunAsync();
