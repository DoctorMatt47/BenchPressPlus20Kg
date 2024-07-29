using BenchPressPlus20Kg.Domain;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebUi;
using WebUi.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddBlazoredLocalStorage();

using var reader = new StreamReader("WorkoutSheet.csv");
builder.Services.AddSingleton<IWorkoutRepository>(new CsvWorkoutRepository(reader));
builder.Services.AddSingleton<IPlanRepository, LocalStoragePlanRepository>();
builder.Services.AddSingleton<IFailureTestService, HardcodeFailureTestService>();
builder.Services.AddSingleton<IPlanService, PlanService>();

await builder.Build().RunAsync();
