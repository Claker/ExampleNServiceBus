using NServiceBus;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ExampleEventConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "CompanyMigrated handler 1";

            var endpointConfiguration = new EndpointConfiguration("EventConsumer1");
            var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            transport.ConnectionString(ConfigurationManager.AppSettings["ConnectionString"]);

            var endpoint = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.ReadLine();

            await endpoint.Stop().ConfigureAwait(false);
        }
    }
}
