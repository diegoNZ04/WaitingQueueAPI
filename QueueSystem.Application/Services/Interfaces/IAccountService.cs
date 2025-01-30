using QueueSystem.Application.Dtos;

namespace QueueSystem.Infra.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<ServiceResponse> RegisterUserAsync(RegisterUserRequest request);
        public Task<LoginResponse> LoginUserAsync(string email, string password);
        public Task<LoginResponse> RefreshTokenAsync(string refreshToken);
    }
}