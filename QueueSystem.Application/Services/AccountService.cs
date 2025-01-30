
using Microsoft.EntityFrameworkCore;
using QueueSystem.Application.Dtos;
using QueueSystem.Application.Implements.Interfaces;
using QueueSystem.Domain.Entities;
using QueueSystem.Domain.Entities.Interfaces;
using QueueSystem.Infra.Data;
using QueueSystem.Infra.Services.Interfaces;

namespace QueueSystem.Infra.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ApplicationContext _context;
        public AccountService(
            IAccountRepository accountRepository,
            IJwtTokenGenerator jwtTokenGenerator,
            ApplicationContext context)
        {
            _accountRepository = accountRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _context = context;
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

        public async Task<LoginResponse> LoginUserAsync(string email, string password)
        {
            var user = await _accountRepository.GetByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                throw new Exception("Senha incorreta.");
            }

            var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<LoginResponse> RefreshTokenAsync(string refreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new Exception("Refresh token inválido ou expirado.");
            }

            var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
            var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _context.SaveChangesAsync();

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}