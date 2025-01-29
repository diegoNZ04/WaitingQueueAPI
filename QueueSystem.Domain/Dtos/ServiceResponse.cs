namespace QueueSystem.Domain.Dtos
{
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}