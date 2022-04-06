using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using GreenPipes;

namespace TinyShop.ProductSearch.Consumers
{
    internal class SearchProductRequestConsumerDefinition : ConsumerDefinition<SearchProductRequestConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<SearchProductRequestConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Interval(5, 1000));
        }
    }
}
