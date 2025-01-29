
namespace QueueSystem.Domain.Dtos
{
    public class CallNextClientResponse
    {
        public ClientCalledResponse Called { get; set; } = null!;
        public int Position { get; set; }
    }
}