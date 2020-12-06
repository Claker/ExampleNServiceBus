using NServiceBus;
using NServiceBus.Logging;
using Shared;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ExampleProducer
{
    class Program
    {
        static ILog log = LogManager.GetLogger<Program>();

        static async Task Main(string[] args)
        {
            Console.Title = "ClientUI";
            var endpoint = new EndpointConfiguration("ClientUI");
            var transport = endpoint.UseTransport<AzureServiceBusTransport>();
            transport.ConnectionString(ConfigurationManager.AppSettings["ConnectionString"]);
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(MigrateCompany), "MigrateCompanyHandler");
            var endpointInstance = await Endpoint.Start(endpoint).ConfigureAwait(false);

            await RunLoop(endpointInstance).ConfigureAwait(false);

            await endpointInstance.Stop().ConfigureAwait(false);
        }

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                log.Info("Press 'P' to migrate company, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        // Instantiate the command
                        var command = new MigrateCompany
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = $"Name{new Random().Next(1, 9999999)}",
                        };

                        log.Info($"Sending MigrateCompany command, Company Id = {command.Id}");
                        await endpointInstance.Send(command).ConfigureAwait(false);

                        break;

                    case ConsoleKey.Q:
                        return;

                    default:
                        log.Info("Unknown input. Please try again.");
                        break;
                }
            }
        }
    }
}
