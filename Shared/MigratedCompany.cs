using NServiceBus;

namespace Shared
{
    public class CompanyMigrated : IEvent
    {
        public string ReturnedId { get; set; }
    }
}
