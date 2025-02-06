namespace QueueSystem.Application.Dtos
{
    public class CreateQueueRequest
    {
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}