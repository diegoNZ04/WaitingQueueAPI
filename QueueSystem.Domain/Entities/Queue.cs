namespace QueueSystem.Domain.Entities
{
    public class Queue
    {
        public int Id { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Queue<Client> Clients { get; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}