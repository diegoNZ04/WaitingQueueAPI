namespace QueueSystem.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Priority { get; set; } = string.Empty;

        public string Status { get; set; } = "Em espera";

        public int QueueId { get; set; }

        public Queue Queue { get; set; } = null!;
    }
}