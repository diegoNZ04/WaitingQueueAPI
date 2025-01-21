using QueueSystem.Domain.Enums;

namespace QueueSystem.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public ClientPriority Priority { get; set; }

        public ClientStatus Status { get; set; }
    }
}