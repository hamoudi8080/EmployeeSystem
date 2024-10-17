using Blazored.SessionStorage;
using BlazorEmployeeApplication;
using BlazorEmployeeApplication.Auth;
using BlazorEmployeeApplication.Models;
using BlazorEmployeeApplication.Services;
using EmployeeManagmentModel;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Shared.Auth;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddAuthorizationCore();
builder.Services.AddAutoMapper(typeof(EmployeeProfile));

/*
builder.Services.AddHttpClient<IEmployeeService, EmployeeService>(client =>
{
    client.BaseAddress = new Uri("https://webapibackend-ascmfwe9gye2f2c4.germanywestcentral-01.azurewebsites.net/");
});

builder.Services.AddHttpClient<IDepartmentService, DepartmentService>(client =>
{
    client.BaseAddress = new Uri("https://webapibackend-ascmfwe9gye2f2c4.germanywestcentral-01.azurewebsites.net/");
});

builder.Services.AddScoped<IAuthService, JwtAuthService>(sp =>
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri("https://webapibackend-ascmfwe9gye2f2c4.germanywestcentral-01.azurewebsites.net/")
    };

    var sessionStorage = sp.GetRequiredService<ISessionStorageService>();

    return new JwtAuthService(httpClient, sessionStorage);
});
*/

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://webapibackend-ascmfwe9gye2f2c4.germanywestcentral-01.azurewebsites.net/") });
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IAuthService, JwtAuthService>(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    var sessionStorage = sp.GetRequiredService<ISessionStorageService>();
    return new JwtAuthService(httpClient, sessionStorage);
});
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
builder.Services.AddBlazoredSessionStorage();


// Configure HttpClient for API calls
/*
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
 
    var sessionStorage = sp.GetRequiredService<ISessionStorageService>();

    // Return a new instance of JwtAuthService with all dependencies
    return new JwtAuthService(httpClient, sessionStorage);
});
*/

/*
// Resolve CustomAuthProvider and call InitAsync
var serviceProvider = builder.Services.BuildServiceProvider();
var authProvider = serviceProvider.GetRequiredService<CustomAuthProvider>();
await authProvider.InitAsync(); // Ensure the state is initialized
*/
// Optionally, if you need a general-purpose HttpClient with the application's base address
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
AuthorizationPolicies.AddPolicies(builder.Services);
await builder.Build().RunAsync();
