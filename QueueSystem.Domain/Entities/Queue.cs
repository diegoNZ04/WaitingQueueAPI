namespace QueueSystem.Domain.Entities
{
    public class Queue
    {
        public int Id { get; set; }

        public string Category { get; set; } = string.Empty;

        public List<Client> Clients { get; } = [];
    }
}