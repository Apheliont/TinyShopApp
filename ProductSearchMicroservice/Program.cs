using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductSearchMicroservice.Consumers;
using ProductSearchMicroservice.Data;
using ProductSearchMicroservice.DataAccess;

IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
var services = new ServiceCollection();

services.AddSingleton<IElasticDataAccess>(x => new ElasticDataAccess(configuration["ElasticSearch:connectionString"]));
services.AddScoped<IProductService, ProductService>();
services.AddMassTransit(x =>
{
    x.AddConsumer<SearchProductRequestConsumer>(typeof(SearchProductRequestConsumerDefinition));
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((context, cfg) => {
        cfg.Host(configuration["RabbitMq:connectionString"]);
        cfg.ConfigureEndpoints(context);
    });
});


var provider = services.BuildServiceProvider();
var busControl = provider.GetRequiredService<IBusControl>();

await busControl.StartAsync(new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);

try
{
    Console.WriteLine("Search service is running. Press enter to exit");

    await Task.Run(() => Console.ReadLine());
}
finally
{
    await busControl.StopAsync();
}



