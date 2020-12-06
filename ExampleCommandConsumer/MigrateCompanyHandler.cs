using NServiceBus;
using NServiceBus.Logging;
using Shared;
using System.Threading.Tasks;

namespace ExampleCommandConsumer
{
    public class MigrateCompanyHandler : IHandleMessages<MigrateCompany>
    {
        static ILog log = LogManager.GetLogger<MigrateCompanyHandler>();

        public Task Handle(MigrateCompany message, IMessageHandlerContext context)
        {
            log.Info($"Noice. Got the migrate company command with message Id: {message.Id} and Name: {message.Name}");

            return context.Publish(new CompanyMigrated { ReturnedId = "RandomId" });
        }
    }
}
