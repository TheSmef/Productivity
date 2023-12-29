using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Productivity.Client;
using Productivity.Client.Services.DataServices;
using Productivity.Client.Services.DataServices.Interfaces;
using Productivity.Client.Services.ReportServices;
using Productivity.Client.Services.ReportServices.Interfaces;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Services.AddHttpClient("Main" ,sp =>  { sp.BaseAddress = new Uri("https://localhost:8081/"); });

builder.Services.AddScoped<ICultureService, CultureService>();
builder.Services.AddScoped<IRegionService, RegionService>();
builder.Services.AddScoped<IProductivityService, ProductivityService>();

builder.Services.AddScoped<IProductivityReportService, ProductivityReportService>();
builder.Services.AddScoped<ICultureReportService, CultureReportService>();

await builder.Build().RunAsync();
