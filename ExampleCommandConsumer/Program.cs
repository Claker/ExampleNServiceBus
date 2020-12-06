using NServiceBus;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ExampleCommandConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "MigrateCompany handler";

            var endpoint = new EndpointConfiguration("MigrateCompanyHandler");
            var transport = endpoint.UseTransport<AzureServiceBusTransport>();
            transport.ConnectionString(ConfigurationManager.AppSettings["ConnectionString"]);
            transport.TopicName("companymigrated");

            var endpointInstance = await Endpoint.Start(endpoint).ConfigureAwait(false);

            Console.ReadLine();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
