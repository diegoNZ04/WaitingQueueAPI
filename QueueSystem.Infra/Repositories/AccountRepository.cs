using Microsoft.EntityFrameworkCore;
using QueueSystem.Domain.Entities;
using QueueSystem.Domain.Entities.Interfaces;
using QueueSystem.Infra.Data;

namespace QueueSystem.Infra.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationContext _context;
        public AccountRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}