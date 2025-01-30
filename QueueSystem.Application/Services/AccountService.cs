
using QueueSystem.Application.Dtos;
using QueueSystem.Application.Implements.Interfaces;
using QueueSystem.Domain.Entities;
using QueueSystem.Domain.Entities.Interfaces;
using QueueSystem.Infra.Services.Interfaces;

namespace QueueSystem.Infra.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AccountService(IAccountRepository accountRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _accountRepository = accountRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
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

        public async Task<string> LoginUserAsync(LoginRequest request)
        {
            var user = await _accountRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new Exception("Senha incorreta");
            }

            return _jwtTokenGenerator.GenerateToken(user);
        }
    }
}