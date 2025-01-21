namespace QueueSystem.Domain.Models
{
    public class BackgroundModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public DateTime CalledAt { get; set; } = DateTime.Now;
    }
}