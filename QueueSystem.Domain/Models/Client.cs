namespace QueueSystem.Domain.Models
{
    public class ClientModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Priority { get; set; } = string.Empty;

        public string Status { get; set; } = "Em espera";

        public int QueueId { get; set; }

        public QueueModel Queue { get; set; } = null!;
    }
}