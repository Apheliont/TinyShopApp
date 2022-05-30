using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using TinyShop.Catalog;
using TinyShop.Catalog.Repositories;

IConfiguration config = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .Build();

IHostBuilder builder = Host.CreateDefaultBuilder(args)

    .ConfigureServices(services =>
{
    services.AddDbContext<AppDbContext>(opt =>
    {
        opt.UseNpgsql(config.GetConnectionString("Default"),
                     options => options.EnableRetryOnFailure())
            .UseSnakeCaseNamingConvention();
    });

    IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
    services.AddSingleton(mapper);
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<ICategoryRepository, CategoryRepository>();
    services.AddScoped<IBreadcrumbsRepository, BreadcrumbsRepository>();

    services.AddMassTransit(x =>
    {
        x.AddConsumers(Assembly.GetEntryAssembly());
        x.SetKebabCaseEndpointNameFormatter();
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.ConfigureJsonSerializerOptions();
            cfg.Host(config["RabbitMq:connectionString"]);
            cfg.ConfigureEndpoints(context);
        });
    });
});


var app = builder.Build();
app.Run();

