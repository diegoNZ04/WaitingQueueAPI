using QueueSystem.Domain.Enums;

namespace QueueSystem.Domain.Dtos
{
    public class ClientDto
    {
        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public ClientPriority Priority { get; set; }
    }
}