using NServiceBus;
using NServiceBus.Logging;
using Shared;
using System.Threading.Tasks;

namespace ExampleEventConsumer
{
    public class CompanyMigratedHandler : IHandleMessages<CompanyMigrated>
    {
        static ILog log = LogManager.GetLogger<CompanyMigratedHandler>();

        public Task Handle(CompanyMigrated message, IMessageHandlerContext context)
        {
            log.Info($"Got the company migrated event ~ In second publisher ~ with message Id: {message.ReturnedId}");

            return Task.CompletedTask;
        }
    }
}
