namespace QueueSystem.Domain.Models
{
    public class QueueModel
    {
        public int Id { get; set; }

        public string Category { get; set; } = string.Empty;

        public ICollection<ClientModel> Clients { get; } = new List<ClientModel>();
    }
}