using Microsoft.AspNetCore.Localization;
using System.Globalization;
using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Domain.OrderAggregate;
using ToolsBazaar.Domain.ProductAggregate;
using ToolsBazaar.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
var app = builder.Build();
var supportedCultures = new[]
{
 new CultureInfo("en-US"),
 new CultureInfo("fr"),
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    // Formatting numbers, dates, etc.
    SupportedCultures = supportedCultures,
    // UI strings that we have localized.
    SupportedUICultures = supportedCultures
});


// Add services to the container.






// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) { 

}

app.UseRouting();

app.MapControllerRoute("default",
                       "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();