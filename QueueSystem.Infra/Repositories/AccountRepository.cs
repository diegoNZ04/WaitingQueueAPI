using Microsoft.EntityFrameworkCore;
using QueueSystem.Domain.Models;
using QueueSystem.Infra.Data;
using QueueSystem.Infra.Repositories.Interfaces;

namespace QueueSystem.Infra.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationContext _context;
        public AccountRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserModel user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserModel> GetByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}