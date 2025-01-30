namespace QueueSystem.Domain.Entities
{
    public class Background
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public DateTime CalledAt { get; set; } = DateTime.UtcNow;

        public Client Client { get; set; } = null!;
    }
}