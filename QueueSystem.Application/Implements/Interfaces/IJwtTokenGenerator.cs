using QueueSystem.Domain.Entities;

namespace QueueSystem.Application.Implements.Interfaces
{
    public interface IJwtTokenGenerator
    {
        public string GenerateAccessToken(User user);
        public string GenerateRefreshToken();
    }
}