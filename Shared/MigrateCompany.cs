using NServiceBus;

namespace Shared
{
    public class MigrateCompany : ICommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
