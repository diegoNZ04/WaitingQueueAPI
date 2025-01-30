
using QueueSystem.Application.Dtos;
using QueueSystem.Domain.Entities;
using QueueSystem.Domain.Entities.Interfaces;
using QueueSystem.Infra.Services.Interfaces;

namespace QueueSystem.Infra.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<ServiceResponse> RegisterUserAsync(RegisterUserRequest request)
        {
            var existingUser = await _accountRepository.GetByEmailAsync(request.Email);

            if (existingUser != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Email já está em uso."
                };
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = hashedPassword,
                Role = request.Role
            };

            await _accountRepository.AddAsync(newUser);
            return new ServiceResponse
            {
                Success = true,
                Message = "Usuário registrado com sucesso."
            };
        }
    }
}