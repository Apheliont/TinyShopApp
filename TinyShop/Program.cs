using Blazored.LocalStorage;
using DataAccessLib.Data;
using DataAccessLib.DataAccess;
using DataAccessLib.Models;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TinyShop.Areas.Identity;
using TinyShop.Data;
using TinyShop.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("SqlDefaultConnection")));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers();
builder.Services.AddMassTransit(x => {
    x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(c => {
        c.Host(builder.Configuration["RabbitMq:connectionString"]);
        c.ConfigureEndpoints(context);
    }));
    x.AddRequestClient<ProductSearchRequestModel>();
}).AddMassTransitHostedService();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddSingleton<ISqlDataAccess>(x => new SqlDataAccess(builder.Configuration.GetConnectionString("SqlDefaultConnection")));
builder.Services.AddScoped<IProductDataService, ProductDataService>();
builder.Services.AddScoped<IProductSqlDataService, ProductSqlDataService>();
builder.Services.AddScoped<IBreadcrumbSqlDataService, BreadcrumbSqlDataService>();
builder.Services.AddScoped<ICategorySqlDataService, CategorySqlDataService>();
builder.Services.AddScoped<IPurchaseSqlDataService, PurchaseSqlDataService>();
builder.Services.AddScoped<IPurchaseDataService, PurchaseDataService>();
builder.Services.AddScoped<IUserUtilities, UserUtilities>();
builder.Services.AddScoped<ILocalStorage, LocalStorage>();
builder.Services.AddScoped<IUriUtils, UriUtils>();
builder.Services.AddScoped<AppState>();
builder.Services.AddBlazoredLocalStorage();




var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization(Localization.GetLocalizationOptions(builder.Configuration));

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
