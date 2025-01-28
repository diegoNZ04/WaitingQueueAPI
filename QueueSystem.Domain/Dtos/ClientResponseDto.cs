using QueueSystem.Domain.Enums;

namespace QueueSystem.Domain.Dtos
{
    public class ClientResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Position { get; set; }
        public ClientPriority Priority { get; set; }
    }
}