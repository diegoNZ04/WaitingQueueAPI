namespace QueueSystem.Domain.Dtos
{
    public class ClientResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Position { get; set; }
        public string Priority { get; set; } = string.Empty;
    }
}