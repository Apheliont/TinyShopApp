using AutoMapper;
using Blazored.LocalStorage;
using MassTransit;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;
using TinyShop.Web.Helpers;
using TinyShop.Web;
using TinyShop.Web.Services;

var builder = WebApplication.CreateBuilder(args);
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();


builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = "Cookies";
    opt.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = "https://localhost:5007";
    options.ClientId = "interactive"; // 75 seconds
    options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
    options.ResponseType = "code";
    options.SaveTokens = true;

    options.GetClaimsFromUserInfoEndpoint = true;

    options.UseTokenLifetime = false;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.TokenValidationParameters = new
           TokenValidationParameters
    {
        NameClaimType = "name"
    };

    options.Events = new OpenIdConnectEvents
    {
        OnAccessDenied = context =>
        {
            context.HandleResponse();
            context.Response.Redirect("/");
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq();
});

builder.Services.AddOptions<MassTransitHostOptions>()
                .Configure(options =>
                {
                    options.WaitUntilStarted = true;
                    options.StartTimeout = TimeSpan.FromSeconds(10);
                    options.StopTimeout = TimeSpan.FromSeconds(30);
                });
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBreadcrumbsService, BreadcrumbsService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<TinyShop.Web.Services.ILocalStorageService, TinyShop.Web.Services.LocalStorageService>();
builder.Services.AddScoped<IUriService, UriService>();
builder.Services.AddSingleton<IMapModelService, MapModelService>();
builder.Services.AddScoped<AppState>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClient();


var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization(Localization.GetLocalizationOptions(builder.Configuration));
app.UseAuthentication();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
