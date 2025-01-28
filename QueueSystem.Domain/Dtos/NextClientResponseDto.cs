namespace QueueSystem.Domain.Dtos
{
    public class NextClientResponseDto
    {
        public CalledClientDto Called { get; set; } = null!;
        public int Position { get; set; }
    }
}