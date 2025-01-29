using QueueSystem.Domain.Dtos;

namespace QueueSystem.Infra.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<ServiceResponse> RegisterUserAsync(RegisterUserRequest request);
    }
}