using QueueSystem.Domain.Entities;

namespace QueueSystem.Application.Implements.Interfaces
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(User user);
    }
}