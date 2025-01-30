namespace QueueSystem.Application.Dtos
{
    public class RegisterClientRequest
    {
        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Priority { get; set; } = string.Empty;

        public int QueueId { get; set; }
    }
}